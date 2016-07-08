using NTAF.PlugInFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace NTAF.Core
{
	public class NTData : IUpdateProgress, ILink
	{
		private List<NTDataFile> _DataFiles = new List<NTDataFile>();

		private List<ObjectClassBase> i_Orphans = new List<ObjectClassBase>();

		private NTData.wrkngFle myWorkingFile = new NTData.wrkngFle();

		private List<TreeNode> OrphanLeaves = new List<TreeNode>();

		public int Count
		{
			get
			{
				return this._DataFiles.Count<NTDataFile>();
			}
		}

		public NTDataFile[] LoadedData
		{
			get
			{
				return this._DataFiles.ToArray();
			}
		}

		public string[] LoadedFiles
		{
			get
			{
				List<string> retVal = new List<string>();
				foreach (NTDataFile NTDF in this._DataFiles)
				{
					retVal.Add(NTDF.FileName);
				}
				return retVal.ToArray();
			}
		}

		public INTId[] LoadedObjects
		{
			get
			{
				List<INTId> retVal = new List<INTId>();
				NTDataFile[] loadedData = this.LoadedData;
				for (int i = 0; i < (int)loadedData.Length; i++)
				{
					retVal.AddRange(loadedData[i].AllData);
				}
				return retVal.ToArray();
			}
		}

		public TreeNode[] OrphanedLeaves
		{
			get
			{
				return this.OrphanLeaves.ToArray();
			}
			set
			{
				this.OrphanLeaves.Clear();
				this.OrphanLeaves.AddRange(value);
			}
		}

		public NTData.wrkngFle WorkingFile
		{
			get
			{
				return this.myWorkingFile;
			}
		}

		public NTData()
		{
		}

		public NTData(string FileToLoad)
		{
			List<string> unloadables = new List<string>(this.Load(new FileInfo(FileToLoad)));
			if (unloadables.Count != 0)
			{
				string message = "";
				foreach (string str in unloadables)
				{
					message = string.Concat(message, string.Format("The file {0} could not be loaded\n", str));
				}
				throw new Exception(message);
			}
		}

		public void CloseFile(NTDataFile file)
		{
			this._DataFiles.Remove(file);
			if (this.EventDataChanged != null)
			{
				this.EventDataChanged();
			}
		}

		private void CurrentFile_EventDataStateChanged()
		{
			if (this.EventWorkingFileChanged != null)
			{
				this.EventWorkingFileChanged();
			}
		}

		public void DataFile_EventDataChanged()
		{
			if (this.EventDataChanged != null)
			{
				this.EventDataChanged();
			}
		}

		public void dropDeffinition(object objToDrop)
		{
			if (objToDrop is IOwner && ((IOwner)objToDrop).myOwner is NTDataFile)
			{
				if (objToDrop is INTId)
				{
					((NTDataFile)((IOwner)objToDrop).myOwner).Drop((ObjectClassBase)objToDrop);
				}
			}
		}

		public static INTId FindINTIdObjectByID(INTId[] objs, string ID)
		{
			INTId retVal = null;
			INTId[] nTIdArray = objs;
			int num = 0;
			while (num < (int)nTIdArray.Length)
			{
				INTId obj = nTIdArray[num];
				if (!(obj.ID == ID))
				{
					num++;
				}
				else
				{
					retVal = obj;
					break;
				}
			}
			return retVal;
		}

		public object FindObject(ObjectClassBase obj)
		{
			object obj1;
			object obj2;
			object retVal = null;
			if (obj != null)
			{
				OCCBase[] oCCBaseArray = this.LoadedCollectors();
				for (int i = 0; i < (int)oCCBaseArray.Length; i++)
				{
					OCCBase occ = oCCBaseArray[i];
					try
					{
						retVal = occ[obj];
						if (retVal != null)
						{
							break;
						}
					}
					catch (InvalidParameter invalidParameter)
					{
					}
					catch (Exception exception)
					{
						Exception ex = exception;
						string str = string.Concat("An error occurred while linking objects", Environment.NewLine, (obj != null ? "The object {0} in file {1} caused the error" : "The object {0} caused the error in an {1} file"));
						string name = obj.Name;
						if (obj != null)
						{
							obj2 = ((IOwner)obj).myOwner;
						}
						else
						{
							obj2 = "untraceable";
						}
						throw new Exception(string.Format(str, name, obj2), ex);
					}
				}
				if (retVal == null)
				{
					if (!this.i_Orphans.Contains(obj))
					{
						this.i_Orphans.Add(obj);
					}
					retVal = this.i_Orphans[this.i_Orphans.IndexOf(obj)];
				}
				obj1 = retVal;
			}
			else
			{
				obj1 = retVal;
			}
			return obj1;
		}

		public void LinkData()
		{
			try
			{
				this.i_Orphans.Clear();
				List<OCCBase> occs = new List<OCCBase>(this.LoadedCollectors());
				for (byte i = 1; i <= 10; i = (byte)(i + 1))
				{
					foreach (OCCBase occ in occs)
					{
						if (occ.objectLayer == i)
						{
							object[] objects = occ.Objects;
							for (int j = 0; j < (int)objects.Length; j++)
							{
								((ObjectClassBase)objects[j]).Link(this);
							}
						}
					}
				}
				this.i_Orphans.Sort();
				List<TreeNode> OrphanLeaves = new List<TreeNode>();
				foreach (object obj in this.i_Orphans)
				{
					NTTreeNode newNode = new NTTreeNode(obj)
					{
						NodeFont = SystemFonts.DefaultFont
					};
					if (obj is IOwner && ((IOwner)obj).myOwner == null)
					{
						newNode.ForeColor = Color.Red;
						newNode.NodeFont = new Font(newNode.NodeFont, FontStyle.Bold);
					}
					OrphanLeaves.Add(newNode);
				}
			}
			catch
			{
				throw;
			}
		}

		public Collection<string> Load(FileInfo file)
		{
			return this.Load((new List<FileInfo>()
			{
				file
			}).ToArray());
		}

		public Collection<string> Load(FileInfo[] files)
		{
			Collection<string> unloadables = new Collection<string>();
			NTDataFile loadingFile = null;
			lock (this)
			{
				FileInfo[] fileInfoArray = files;
				for (int i = 0; i < (int)fileInfoArray.Length; i++)
				{
					FileInfo file = fileInfoArray[i];
					if (this.LoadedFiles.ToList<string>().Contains(file.Name))
					{
						unloadables.Add(string.Concat(file.FullName, "\nIs already loaded and cannot be loaded more than once."));
					}
					else
					{
						try
						{
							loadingFile = new NTDataFile(file.FullName);
							loadingFile.Updating += new NTEventHandler<UpdaterEventArgs>(this.updating);
							loadingFile.Update += new NTEventHandler<UpdateProgressEventArgs>(this.update);
							loadingFile.Updated += new NTEventHandler(this.updated);
							loadingFile.Load();
							this._DataFiles.Add(loadingFile);
						}
						catch (Exception exception)
						{
							Exception ex = exception;
							unloadables.Add(string.Concat(file.FullName, "\ncould not be loaded for the following reason(s)\n", ex.Message));
						}
					}
				}
			}
			if (this.EventFileAdded != null)
			{
				this.EventFileAdded();
			}
			return unloadables;
		}

		public OCCBase[] LoadedCollectors()
		{
			List<OCCBase> occs = new List<OCCBase>();
			NTDataFile[] loadedData = this.LoadedData;
			for (int i = 0; i < (int)loadedData.Length; i++)
			{
				occs.AddRange(loadedData[i].Collectors);
			}
			return occs.ToArray();
		}

		public void NewFile(string path, string iDpreFix, string DataSetName)
		{
			NTDataFile newFile = new NTDataFile(path, iDpreFix, DataSetName)
			{
				DataChanged = true
			};
			newFile.EventDataStateChanged += new NTEventHandler(this.newFile_EventDataStateChanged);
			newFile.Updating += new NTEventHandler<UpdaterEventArgs>(this.updating);
			newFile.Update += new NTEventHandler<UpdateProgressEventArgs>(this.update);
			newFile.Updated += new NTEventHandler(this.updated);
			this._DataFiles.Add(newFile);
			if (this.EventFileAdded != null)
			{
				this.EventFileAdded();
			}
			newFile.Save();
		}

		private void newFile_EventDataStateChanged()
		{
		}

		public List<string> SaveAll()
		{
			List<string> unsaveableFiles = new List<string>();
			foreach (NTDataFile NTDF in this._DataFiles)
			{
				try
				{
					NTDF.Save();
				}
				catch (NTFileExecption nTFileExecption)
				{
					unsaveableFiles.Add(nTFileExecption.FileName);
				}
				catch (Exception exception)
				{
					throw exception;
				}
			}
			return unsaveableFiles;
		}

		public void SetWorkingFile(string FileName)
		{
			if (this.myWorkingFile.CurrentFile == null || !(FileName == this.myWorkingFile.CurrentFile.FileName))
			{
				NTDataFile[] loadedData = this.LoadedData;
				int num = 0;
				while (num < (int)loadedData.Length)
				{
					NTDataFile NTDF = loadedData[num];
					if (!(NTDF.FileName == FileName))
					{
						num++;
					}
					else
					{
						if (this.myWorkingFile.CurrentFile != null)
						{
							this.myWorkingFile.CurrentFile.EventDataStateChanged -= new NTEventHandler(this.CurrentFile_EventDataStateChanged);
						}
						this.myWorkingFile.CurrentFile = NTDF;
						this.myWorkingFile.CurrentFile.EventDataStateChanged += new NTEventHandler(this.CurrentFile_EventDataStateChanged);
						break;
					}
				}
				if (this.EventWorkingFileChanged != null)
				{
					this.EventWorkingFileChanged();
				}
			}
		}

		private void update(UpdateProgressEventArgs args)
		{
			if (this.Update != null)
			{
				this.Update(args);
			}
		}

		private void updated()
		{
			if (this.Updated != null)
			{
				this.Updated();
			}
		}

		private INTId updateOrphaning(INTId orphanedObjRef, ref NTDataFile OwnerInformation)
		{
			return null;
		}

		private void updating(UpdaterEventArgs args)
		{
			if (this.Updating != null)
			{
				this.Updating(args);
			}
		}

		public event NTEventHandler EventDataChanged;

		public event NTEventHandler EventFileAdded;

		public event NTEventHandler EventWorkingFileChanged;

		public event NTEventHandler<UpdateProgressEventArgs> Update;

		public event NTEventHandler Updated;

		public event NTEventHandler<UpdaterEventArgs> Updating;

		public class wrkngFle
		{
			private NTDataFile file;

			public NTDataFile CurrentFile
			{
				get
				{
					return this.file;
				}
				set
				{
					this.file = value;
				}
			}

			public bool DataChanged
			{
				get
				{
					return this.file.DataChanged;
				}
			}

			public string FileName
			{
				get
				{
					return this.file.FileName;
				}
			}

			public string IDPreFix
			{
				get
				{
					return this.file.IDPreFix;
				}
			}

			public string Path
			{
				get
				{
					return this.file.FullFileName;
				}
			}

			public wrkngFle()
			{
			}
		}
	}
}