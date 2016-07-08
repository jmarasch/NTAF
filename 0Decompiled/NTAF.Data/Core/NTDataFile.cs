using Ionic.Zip;
using NTAF.Core.Properties;
using NTAF.PlugInFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace NTAF.Core
{
	[Serializable]
	public class NTDataFile : ITrackChange, ILockable, IUpdateProgress, ILink
	{
		private ContextMenuStrip i_RootMenu;

		private ContextMenuStrip i_NodeMenu;

		private List<OCCBase> i_OCCPlugins = new List<OCCBase>(PluginEngine.GetOCCPlugIns());

		private List<ObjectClassBase> i_Orphans = new List<ObjectClassBase>();

		private char[] _IDPreFix;

		private string _Path;

		private bool dataChanged = false;

		private bool fileLock = false;

		private string datafilename = "";

		private string filePassword = "";

		private OperationLogger actions = new OperationLogger();

		private List<ObjectClassBase> orphanedObjects = new List<ObjectClassBase>();

		[XmlIgnore]
		public ObjectClassBase[] AllData
		{
			get
			{
				List<ObjectClassBase> retVal = new List<ObjectClassBase>();
				foreach (OCCBase oc in this.i_OCCPlugins)
				{
					object[] objects = oc.Objects;
					for (int i = 0; i < (int)objects.Length; i++)
					{
						object obj = objects[i];
						if (obj is ObjectClassBase)
						{
							retVal.Add((ObjectClassBase)obj);
						}
					}
				}
				return retVal.ToArray();
			}
		}

		[XmlIgnore]
		public OCCBase[] Collectors
		{
			get
			{
				return this.i_OCCPlugins.ToArray();
			}
		}

		[XmlIgnore]
		public bool DataChanged
		{
			get
			{
				return this.dataChanged;
			}
			set
			{
				this.dataChanged = value;
				if (this.EventDataStateChanged != null)
				{
					this.EventDataStateChanged();
				}
			}
		}

		[XmlAttribute]
		public string DataFileName
		{
			get
			{
				return this.datafilename;
			}
			set
			{
				this.datafilename = value;
				if (this.EventDataStateChanged != null)
				{
					this.EventDataStateChanged();
				}
			}
		}

		[XmlIgnore]
		public string FileDir
		{
			get
			{
				return Path.GetDirectoryName(this._Path);
			}
		}

		[XmlIgnore]
		public string FileExtention
		{
			get
			{
				return Path.GetExtension(this._Path);
			}
		}

		[XmlIgnore]
		public bool FileLocked
		{
			get
			{
				bool retval = false;
				retval = (!Settings.Default.Loading ? this.fileLock : false);
				return retval;
			}
		}

		[XmlIgnore]
		public string FileName
		{
			get
			{
				return Path.GetFileName(this._Path);
			}
		}

		[XmlAttribute]
		public string FilePassword
		{
			get
			{
				return JustDecompileGenerated_get_FilePassword();
			}
			set
			{
				JustDecompileGenerated_set_FilePassword(value);
			}
		}

		public string JustDecompileGenerated_get_FilePassword()
		{
			return this.filePassword;
		}

		public void JustDecompileGenerated_set_FilePassword(string value)
		{
			if (this.FileLocked)
			{
				throw new FileLockedException("File is locked, and cannot be edited.");
			}
			if (value.Trim() == string.Empty)
			{
				this.filePassword = value;
				this.fileLock = false;
				this.DataChanged = true;
			}
			else if (Settings.Default.Loading)
			{
				this.filePassword = value;
			}
			else
			{
				this.filePassword = Security.Encrypt(value);
				this.DataChanged = true;
			}
		}

		[XmlIgnore]
		public string FilePath
		{
			get
			{
				string str = this._Path.Remove(this._Path.LastIndexOf(Path.GetFileName(this._Path)));
				return str;
			}
		}

		[XmlIgnore]
		public string FullFileName
		{
			get
			{
				return this._Path;
			}
			set
			{
				this._Path = value;
			}
		}

		[XmlAttribute]
		public string IDPreFix
		{
			get
			{
				string retVal = "";
				char[] chrArray = this._IDPreFix;
				for (int i = 0; i < (int)chrArray.Length; i++)
				{
					char CH = chrArray[i];
					retVal = string.Concat(retVal, CH.ToString());
				}
				return retVal.ToString();
			}
			set
			{
				this._IDPreFix = value.ToCharArray(0, 4);
			}
		}

		[XmlIgnore]
		public string[] IDs
		{
			get
			{
				List<string> retVal = new List<string>();
				ObjectClassBase[] allData = this.AllData;
				for (int i = 0; i < (int)allData.Length; i++)
				{
					retVal.Add(allData[i].ID);
				}
				return retVal.ToArray();
			}
		}

		[XmlIgnore]
		public SerializableVersion[] LoadedPlugins
		{
			get
			{
				List<SerializableVersion> retVal = new List<SerializableVersion>();
				Assembly[] assemblyArray = PluginEngine.LoadedAssemblies();
				for (int i = 0; i < (int)assemblyArray.Length; i++)
				{
					Type[] types = assemblyArray[i].GetTypes();
					for (int j = 0; j < (int)types.Length; j++)
					{
						Type typ = types[j];
						List<object> attributes = new List<object>(typ.GetCustomAttributes(typeof(ObjectClassPlugIn), true));
						if (attributes.Count >= 1)
						{
							foreach (ObjectClassPlugIn ocpi in attributes)
							{
								retVal.Add(ocpi.version);
							}
						}
						attributes = new List<object>(typ.GetCustomAttributes(typeof(OCCPlugIn), true));
						if (attributes.Count >= 1)
						{
							foreach (OCCPlugIn occpi in attributes)
							{
								retVal.Add(occpi.version);
							}
						}
						attributes = new List<object>(typ.GetCustomAttributes(typeof(EditorPlugIn), true));
						if (attributes.Count >= 1)
						{
							foreach (EditorPlugIn edpi in attributes)
							{
								retVal.Add(edpi.version);
							}
						}
						attributes = new List<object>(typ.GetCustomAttributes(typeof(TreeNodePlugIn), true));
						if (attributes.Count >= 1)
						{
							foreach (TreeNodePlugIn tnpi in attributes)
							{
								retVal.Add(tnpi.version);
							}
						}
					}
				}
				return retVal.ToArray();
			}
		}

		[XmlIgnore]
		public ObjectClassBase[] Orphans
		{
			get
			{
				return this.orphanedObjects.ToArray();
			}
		}

		public NTDataFile()
		{
			this.IDPreFix = "NULL";
		}

		public NTDataFile(string path)
		{
			this.FullFileName = path;
			this.IDPreFix = "NULL";
		}

		public NTDataFile(string path, string idPreFix)
		{
			this._Path = path;
			this.IDPreFix = idPreFix;
		}

		public NTDataFile(string path, string idPreFix, string dataSetName)
		{
			this._Path = path;
			this.IDPreFix = idPreFix;
			this.DataFileName = dataSetName;
		}

		public void Add(ObjectClassBase toAdd)
		{
			if (this.FileLocked)
			{
				throw new FileLockedException("File is locked, and cannot be edited.");
			}
			try
			{
				OCCBase tmpOCC = this.GetCollector(toAdd);
				if (tmpOCC == null)
				{
					throw new InvalidOperationException("Could not find that type among the plugins");
				}
				if (tmpOCC.Exists(toAdd))
				{
					throw new ItemException("That Object Already exists in the collection");
				}
				toAdd.MyDataChanged += new NTEventHandler(this.NTDataFile_EventMyDataChanged);
				if (toAdd != null)
				{
					((IOwner)toAdd).myOwner = this;
				}
				tmpOCC.AddObject(toAdd);
				this.DataChanged = true;
				if (!Settings.Default.PerformingAction)
				{
					this.actions.AddUndoableOpp(NTDataFile.UndoActionKeyWords.Add, toAdd);
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		public void AddOrpahns(ObjectClassBase[] NewOrphans)
		{
			this.orphanedObjects.AddRange(NewOrphans);
		}

		public void AddOrphan(ObjectClassBase OrphanedObject)
		{
			if (!this.OrphanExists(OrphanedObject))
			{
				this.MoveToOrphanList(OrphanedObject, null);
			}
		}

		private bool CheckForReferences(ObjectClassBase Item)
		{
			bool flag;
			int num;
			byte objLayer = 0;
			OCCBase[] collectors = this.Collectors;
			for (num = 0; num < (int)collectors.Length; num++)
			{
				OCCBase colector = collectors[num];
				if (colector.CollectionType == Item.CollectionType)
				{
					objLayer = colector.objectLayer;
				}
			}
			int i = objLayer + 1;
			while (true)
			{
				if (i <= 10)
				{
					collectors = this.Collectors;
					for (num = 0; num < (int)collectors.Length; num++)
					{
						OCCBase occ = collectors[num];
						if (occ.objectLayer == i)
						{
							foreach (ObjectClassBase obj in occ)
							{
								if (!obj.CheckForReferences(Item))
								{
									continue;
								}
								flag = true;
								return flag;
							}
						}
					}
					i++;
				}
				else
				{
					flag = false;
					break;
				}
			}
			return flag;
		}

		private bool checkForRequiredPlugins(SerializableVersion[] requiredPlugins, out string[] MessageList)
		{
			List<SerializableVersion> loadedPlugs = new List<SerializableVersion>(this.LoadedPlugins);
			List<string> msgList = new List<string>();
			bool retval = true;
			bool currentTest = true;
			SerializableVersion[] serializableVersionArray = requiredPlugins;
			for (int i = 0; i < (int)serializableVersionArray.Length; i++)
			{
				SerializableVersion reqPlugin = serializableVersionArray[i];
				currentTest = loadedPlugs.Contains(reqPlugin);
				if (!currentTest)
				{
					foreach (SerializableVersion ldplug in loadedPlugs)
					{
						if ((ldplug.Name != reqPlugin.Name ? false : ldplug.Type == reqPlugin.Type))
						{
							if (!(reqPlugin > ldplug))
							{
								currentTest = true;
							}
							else
							{
								currentTest = false;
								msgList.Add(string.Format("Required plugin {0} is newer than installed version,\nFound version {1}\nRequired version {2}", string.Format("{1}:{0}", reqPlugin.Name, reqPlugin.Type), ldplug.Version(), reqPlugin.Version()));
							}
						}
					}
					if (!currentTest)
					{
						msgList.Add(string.Format("The plugin {0} could not be found and is required to load this file", reqPlugin.ToString()));
					}
					if ((currentTest ? false : retval))
					{
						retval = false;
					}
				}
			}
			MessageList = msgList.ToArray();
			return retval;
		}

		public bool CheckPassword(string cleartypePassword)
		{
			return this.filePassword == Security.Encrypt(cleartypePassword);
		}

		public void ClearOrphans()
		{
			this.orphanedObjects.Clear();
		}

		public void DoRedo()
		{
			try
			{
				try
				{
					Settings.Default.PerformingAction = true;
					Operation action = this.actions.RedoAction();
					switch ((NTDataFile.UndoActionKeyWords)action.Action)
					{
						case NTDataFile.UndoActionKeyWords.Drop:
						{
							this.Drop((ObjectClassBase)action.Data);
							break;
						}
						case NTDataFile.UndoActionKeyWords.Add:
						{
							this.Add((ObjectClassBase)action.Data);
							break;
						}
						case NTDataFile.UndoActionKeyWords.Edit:
						{
							object[] tmpArray = (object[])action.Data;
							this.Edit((ObjectClassBase)tmpArray[0], (ObjectClassBase)tmpArray[1]);
							break;
						}
					}
				}
				catch
				{
					throw;
				}
			}
			finally
			{
				Settings.Default.PerformingAction = false;
			}
		}

		public void DoUndo()
		{
			try
			{
				try
				{
					Settings.Default.PerformingAction = true;
					Operation action = this.actions.UndoAction();
					switch ((NTDataFile.UndoActionKeyWords)action.Action)
					{
						case NTDataFile.UndoActionKeyWords.Drop:
						{
							this.Add((ObjectClassBase)action.Data);
							break;
						}
						case NTDataFile.UndoActionKeyWords.Add:
						{
							this.Drop((ObjectClassBase)action.Data);
							break;
						}
						case NTDataFile.UndoActionKeyWords.Edit:
						{
							object[] tmpArray = (object[])action.Data;
							this.Edit((ObjectClassBase)tmpArray[1], (ObjectClassBase)tmpArray[0]);
							break;
						}
					}
				}
				catch
				{
					throw;
				}
			}
			finally
			{
				Settings.Default.PerformingAction = false;
			}
		}

		public void Drop(ObjectClassBase toDrop)
		{
			if (this.FileLocked)
			{
				throw new FileLockedException("File is locked, and cannot be edited.");
			}
			try
			{
				OCCBase tmpOCC = this.GetCollector(toDrop);
				if (tmpOCC == null)
				{
					throw new InvalidOperationException("Could not find that type among the plugins");
				}
				if (!tmpOCC.Exists(toDrop))
				{
					throw new Exception();
				}
				if (this.CheckForReferences(toDrop))
				{
					this.MoveToOrphanList(toDrop, tmpOCC);
				}
				else
				{
					((ObjectClassBase)tmpOCC[toDrop]).MyDataChanged -= new NTEventHandler(this.NTDataFile_EventMyDataChanged);
					tmpOCC.DropObject(toDrop);
				}
				this.DataChanged = true;
				if (!Settings.Default.PerformingAction)
				{
					this.actions.AddUndoableOpp(NTDataFile.UndoActionKeyWords.Drop, toDrop);
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		public void DropOrphan(ObjectClassBase OrphanedObject)
		{
			if (this.OrphanExists(OrphanedObject))
			{
				this.orphanedObjects.Remove(OrphanedObject);
			}
		}

		public void Edit(ObjectClassBase toEdit, ObjectClassBase NewValues)
		{
			if (this.FileLocked)
			{
				throw new FileLockedException("File is locked, and cannot be edited.");
			}
			if ((toEdit == null ? true : NewValues == null))
			{
				throw new InvalidCastException("The item being edited is not of the proper type");
			}
			try
			{
				OCCBase tmpOCC_Remove = this.GetCollector(toEdit);
				OCCBase tmpOCC_Add = this.GetCollector(NewValues);
				if (tmpOCC_Remove == null)
				{
					throw new InvalidOperationException("Collector plugin not loaded for the item being edited,\nchanges not applied");
				}
				if ((tmpOCC_Remove == tmpOCC_Add ? false : tmpOCC_Add == null))
				{
					throw new InvalidOperationException("Collector plugin for the edited object is not loaded,\nchanges not applied");
				}
				if (tmpOCC_Add.Exists(NewValues))
				{
					throw new ItemException("This object already exists in the collector its being moved to,\nchanges not applied");
				}
				this.ReplaceReferences(toEdit, NewValues);
				tmpOCC_Remove.DropObject(toEdit);
				tmpOCC_Add.AddObject(NewValues);
				if (toEdit != null)
				{
					toEdit.MyDataChanged -= new NTEventHandler(this.NTDataFile_EventMyDataChanged);
				}
				if (NewValues != null)
				{
					NewValues.MyDataChanged += new NTEventHandler(this.NTDataFile_EventMyDataChanged);
				}
				this.DataChanged = true;
				object[] data = new object[] { toEdit, NewValues };
				if (!Settings.Default.PerformingAction)
				{
					this.actions.AddUndoableOpp(NTDataFile.UndoActionKeyWords.Edit, data);
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		public void ExportToCSV(string path)
		{
			DataMember dataMember;
			DataMember[] dataMemberArray;
			int k;
			char[] chrArray;
			StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.Create));
			sw.WriteLine("This file can be loaded an any good spread sheet program as table data. when loading be sure to split the data via the pipe '|' found above the '\\' key");
			try
			{
				try
				{
					bool printHeader = true;
					ObjectClassBase[] objectClasses = PluginEngine.GetObjectClasses();
					for (int i = 0; i < (int)objectClasses.Length; i++)
					{
						ObjectClassBase objectClass = objectClasses[i];
						sw.WriteLine(string.Concat("Table:", objectClass.CollectionName));
						ObjectClassBase[] allData = this.AllData;
						for (int j = 0; j < (int)allData.Length; j++)
						{
							ObjectClassBase obj = allData[j];
							if (obj.CollectionType == objectClass.CollectionType)
							{
								DataMember[] dataMembers = obj.getDataMembers();
								string line = "";
								if (printHeader)
								{
									dataMemberArray = dataMembers;
									for (k = 0; k < (int)dataMemberArray.Length; k++)
									{
										dataMember = dataMemberArray[k];
										line = string.Concat(line, string.Format("{0}|", dataMember.Field));
									}
									chrArray = new char[] { ' ', ',' };
									line = line.TrimEnd(chrArray);
									sw.WriteLine(line);
									line = "";
									printHeader = false;
								}
								try
								{
									dataMemberArray = dataMembers;
									for (k = 0; k < (int)dataMemberArray.Length; k++)
									{
										dataMember = dataMemberArray[k];
										line = string.Concat(line, string.Format("{0}|", dataMember.Data));
									}
									chrArray = new char[] { ' ', ',' };
									line = line.TrimEnd(chrArray);
									sw.WriteLine(line);
								}
								catch (Exception exception)
								{
									throw new Exception("Object out put error", exception);
								}
							}
						}
						sw.WriteLine("");
						printHeader = true;
					}
				}
				catch (Exception exception1)
				{
					throw new NTFileExecption(Path.GetFileName(path), "Could not export data", exception1);
				}
			}
			finally
			{
				sw.Close();
			}
		}

		public void ExportToTXT(string path)
		{
			StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.Create));
			try
			{
				try
				{
					ObjectClassBase[] allData = this.AllData;
					for (int i = 0; i < (int)allData.Length; i++)
					{
						object obj = allData[i];
						try
						{
							if (!(obj is ObjectClassBase))
							{
								throw new Exception();
							}
							ObjectClassBase IObj = (ObjectClassBase)obj;
							DataMember[] dataMembers = IObj.getDataMembers();
							sw.WriteLine(IObj.CollectionName);
							DataMember[] dataMemberArray = dataMembers;
							for (int j = 0; j < (int)dataMemberArray.Length; j++)
							{
								DataMember dataMember = dataMemberArray[j];
								sw.WriteLine(string.Format("{0}:{1}", dataMember.Field, dataMember.Data));
							}
							sw.WriteLine("============================================================");
						}
						catch (Exception exception)
						{
							throw new Exception("Object out put error", exception);
						}
					}
				}
				catch (Exception exception1)
				{
					throw new NTFileExecption(Path.GetFileName(path), "Could not export data", exception1);
				}
			}
			finally
			{
				sw.Close();
			}
		}

		public object FindObject(ObjectClassBase obj)
		{
			object obj1;
			object obj2;
			object retVal = null;
			if (obj != null)
			{
				OCCBase[] collectors = this.Collectors;
				for (int i = 0; i < (int)collectors.Length; i++)
				{
					OCCBase occ = collectors[i];
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
						this.MoveToOrphanList(obj, null);
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

		public string GenerateIDCode()
		{
			int hashCode = 0;
			string guidResult = string.Empty;
			do
			{
				for (guidResult = this.IDPreFix; guidResult.Length < 12; guidResult = string.Concat(guidResult, hashCode.ToString("X")))
				{
					hashCode = Guid.NewGuid().ToString().GetHashCode();
				}
				if (this.IDPreFix.Length == 4)
				{
					continue;
				}
				throw new ArgumentException("dataSetID is not valid, it must be exactly 4 characters long");
			}
			while (this.IDCodeIsUnique(guidResult.Substring(0, 12)));
			return guidResult.Substring(0, 12);
		}

		public OCCBase GetCollector(Type T)
		{
			OCCBase retVal = null;
			foreach (OCCBase iocc in this.i_OCCPlugins)
			{
				if (iocc.CollectionType == T)
				{
					retVal = iocc;
					break;
				}
			}
			return retVal;
		}

		public OCCBase GetCollector(object T)
		{
			OCCBase oCCBase;
			OCCBase retVal = null;
			foreach (OCCBase iocc in this.i_OCCPlugins)
			{
				if (!iocc.IsOfType(T.GetType()))
				{
					continue;
				}
				oCCBase = iocc;
				return oCCBase;
			}
			oCCBase = retVal;
			return oCCBase;
		}

		public T[] GetObjects<T>()
		{
			List<T> retVal = new List<T>();
			foreach (OCCBase oc in this.i_OCCPlugins)
			{
				if (typeof(T) == oc.CollectionType)
				{
					object[] objects = oc.Objects;
					for (int i = 0; i < (int)objects.Length; i++)
					{
						object obj = objects[i];
						if (obj is T)
						{
							retVal.Add((T)obj);
						}
					}
				}
			}
			return retVal.ToArray();
		}

		public void getTreeNodes(TreeNodeCollection treeObject, ContextMenuStrip RootMenu, ContextMenuStrip NodeMenu)
		{
			this.i_RootMenu = RootMenu;
			this.i_NodeMenu = NodeMenu;
			this.getTreeNodes(treeObject);
		}

		public void getTreeNodes(TreeNodeCollection treeObject)
		{
			treeObject.Clear();
			foreach (OCTreeNodeBase treeNodePlug in new List<OCTreeNodeBase>(PluginEngine.GetTreePlugIns()))
			{
				OCCBase[] collectors = this.Collectors;
				for (int i = 0; i < (int)collectors.Length; i++)
				{
					OCCBase occ = collectors[i];
					if (treeNodePlug.CanDisplay(occ.CollectionType))
					{
						treeNodePlug.AttachOCC(occ);
					}
				}
				treeNodePlug.SetMenus(this.i_RootMenu, this.i_NodeMenu);
				treeObject.Add(treeNodePlug.MainBranch());
			}
		}

		public bool IDCodeIsUnique(string IDCode)
		{
			bool retVal = false;
			List<string> loadedIDs = new List<string>();
			loadedIDs.AddRange(this.IDs);
			if (loadedIDs.Contains(IDCode))
			{
				retVal = true;
			}
			return retVal;
		}

		public void LinkData()
		{
			try
			{
				this.i_Orphans.Clear();
				if (this.Updating != null)
				{
					this.Updating(new UpdaterEventArgs((int)this.AllData.Length + 1));
				}
				int current = 0;
				for (byte i = 0; i <= 10; i = (byte)(i + 1))
				{
					OCCBase[] collectors = this.Collectors;
					for (int j = 0; j < (int)collectors.Length; j++)
					{
						OCCBase occ = collectors[j];
						if (occ.objectLayer == i)
						{
							object[] objects = occ.Objects;
							for (int k = 0; k < (int)objects.Length; k++)
							{
								ObjectClassBase obj = (ObjectClassBase)objects[k];
								current++;
								if (this.Update != null)
								{
									this.Update(new UpdateProgressEventArgs(string.Format("Linking Object {0}", obj.Name), "Linking", obj.Name, current, (int)this.AllData.Length + 1));
								}
								obj.Link(this);
							}
						}
					}
				}
				if (this.Updated != null)
				{
					this.Updated();
				}
				this.i_Orphans.Sort();
			}
			catch
			{
				throw;
			}
		}

		public void Load()
		{
			object[] objArr;
			object tmpObj;
			int i;
			Settings.Default.Loading = true;
			if (this.Updating != null)
			{
				this.Updating(new UpdaterEventArgs(5));
			}
			NTDataFile dataSet = null;
			ZipFile zipFile = null;
			string.Concat(Path.GetTempPath(), "NewTerra");
			MemoryStream ZipStream = null;
			MemoryStream ZipEntryStream = new MemoryStream();
			if (this.Update != null)
			{
				this.Update(new UpdateProgressEventArgs("Opening File...", "Opening", this.FullFileName, 2, 6));
			}
			try
			{
				zipFile = new ZipFile(this.FullFileName);
			}
			catch (ZipException zipException)
			{
				if (this.Update != null)
				{
					this.Update(new UpdateProgressEventArgs("File is protected...", "Opening", this.FullFileName, 3, 6));
				}
				ZipStream = Security.StreamCrypt(new FileStream(this.FullFileName, FileMode.Open), Security.CryptAction.decrypt);
				ZipStream.Position = (long)0;
				zipFile = ZipFile.Read(ZipStream);
			}
			if (zipFile == null)
			{
				if (this.Updated != null)
				{
					this.Updated();
				}
				throw new FileLoadException("Could not load file", this.FullFileName);
			}
			if (this.Update != null)
			{
				this.Update(new UpdateProgressEventArgs("Unpacking File this may take a while...", "Inflating", this.FullFileName, 3, 6));
			}
			try
			{
				zipFile.get_Item("Requirements.xml").Extract(ZipEntryStream);
				this.ReadObject(out objArr, ZipEntryStream, typeof(SerializableVersion[]));
				string[] msgList = new string[0];
				if (!this.checkForRequiredPlugins((new List<SerializableVersion>((SerializableVersion[])objArr)).ToArray(), out msgList))
				{
					string bigMsg = "";
					string[] strArrays = msgList;
					for (i = 0; i < (int)strArrays.Length; i++)
					{
						bigMsg = string.Concat(bigMsg, strArrays[i], "\n\n");
					}
					throw new FileLoadException(bigMsg, this.FileName);
				}
			}
			catch
			{
			}
			ZipEntryStream = new MemoryStream();
			zipFile.get_Item(string.Concat(Path.GetFileNameWithoutExtension(this.FileName), ".xml")).Extract(ZipEntryStream);
			this.ReadObject(out tmpObj, ZipEntryStream, typeof(NTDataFile));
			if (tmpObj is NTDataFile)
			{
				dataSet = (NTDataFile)tmpObj;
			}
			if (this.Update != null)
			{
				this.Update(new UpdateProgressEventArgs("File inflated finding objects...", "Hunting", this.FullFileName, 4, 6));
			}
			if (dataSet != null)
			{
				this.filePassword = dataSet.filePassword;
				this.DataFileName = dataSet.DataFileName;
				this.IDPreFix = dataSet.IDPreFix;
			}
			List<string> UnlodableObjects = new List<string>();
			int max = 6 + zipFile.get_Entries().Count - 1;
			int currentCount = 5;
			if (this.Update != null)
			{
				int num = currentCount;
				currentCount = num + 1;
				this.Update(new UpdateProgressEventArgs("Loading objects...", "Loading objects", this.FullFileName, num, max));
			}
			foreach (ZipEntry file in zipFile.get_Entries())
			{
				if (this.Update != null)
				{
					int num1 = currentCount;
					currentCount = num1 + 1;
					this.Update(new UpdateProgressEventArgs(string.Format("Loading {0}", Path.GetFileName(file.get_FileName())), "Loading", Path.GetFileName(file.get_FileName()), num1, max));
				}
				bool gotIt = false;
				object RetreivedObject = null;
				ZipEntryStream = new MemoryStream();
				zipFile.get_Item(file.get_FileName()).Extract(ZipEntryStream);
				Type[] serailPlugins = PluginEngine.GetSerailPlugins();
				i = 0;
				while (i < (int)serailPlugins.Length)
				{
					Type typ = serailPlugins[i];
					try
					{
						this.ReadObject(out RetreivedObject, ZipEntryStream, typ);
					}
					catch
					{
					}
					if (!(RetreivedObject is ObjectClassBase))
					{
						i++;
					}
					else
					{
						this.Add((ObjectClassBase)RetreivedObject);
						gotIt = true;
						break;
					}
				}
				if (!gotIt)
				{
					UnlodableObjects.Add(file.get_FileName());
				}
			}
			if (this.filePassword != string.Empty)
			{
				this.LockFile();
			}
			if (this.Updated != null)
			{
				this.Updated();
			}
			this.actions.ClearLog();
			this.DataChanged = false;
			Settings.Default.Loading = false;
		}

		public void LockFile()
		{
			if (this.filePassword == "")
			{
				throw new NullPasswordException("No Password Set");
			}
			this.fileLock = true;
			if (this.LockStatusChange != null)
			{
				this.LockStatusChange();
			}
		}

		private void MoveToOrphanList(ObjectClassBase Item, OCCBase OCC)
		{
			if (this.i_Orphans.Count <= 0)
			{
				this.i_Orphans.Add(Item);
			}
			else
			{
				int i = 0;
				while (i <= this.i_Orphans.Count - 1)
				{
					if (Item.Name.CompareTo(this.i_Orphans[i].Name) >= 0)
					{
						i++;
					}
					else
					{
						this.i_Orphans.Insert(i, Item);
						break;
					}
				}
				if (!this.i_Orphans.Contains(Item))
				{
					this.i_Orphans.Add(Item);
				}
			}
			if (OCC != null)
			{
				OCC.DropObject(Item);
			}
			if (this.EventOrphansChanged != null)
			{
				this.EventOrphansChanged(new ItemChangedArgs(this.i_Orphans.IndexOf(Item), Item, ArgAction.Add));
			}
		}

		private void NTDataFile_EventMyDataChanged()
		{
		}

		public bool OrphanExists(ObjectClassBase OrphanedObject)
		{
			return this.orphanedObjects.Contains(OrphanedObject);
		}

		public void PurgeFile()
		{
			List<ObjectClassBase> purgeList = new List<ObjectClassBase>();
			foreach (ObjectClassBase ObjRef in this.i_Orphans)
			{
				bool ObjRefHasRef = false;
				OCCBase[] collectors = this.Collectors;
				int num = 0;
				while (num < (int)collectors.Length)
				{
					OCCBase occ = collectors[num];
					if (!ObjRefHasRef)
					{
						foreach (ObjectClassBase obj in occ)
						{
							bool flag = obj.CheckForReferences(ObjRef);
							ObjRefHasRef = flag;
							if (!flag)
							{
								continue;
							}
							break;
						}
						num++;
					}
					else
					{
						break;
					}
				}
				if (!ObjRefHasRef)
				{
					purgeList.Add(ObjRef);
				}
			}
			foreach (ObjectClassBase Obj in purgeList)
			{
				this.RemoveFromOrphanList(Obj);
			}
		}

		private bool ReadObject(out object objectToRead, string path, Type T)
		{
			bool flag;
			FileStream FS = new FileStream(path, FileMode.Open);
			XmlSerializer SER = new XmlSerializer(T);
			XmlReader XMLR = new XmlTextReader(FS);
			try
			{
				try
				{
					objectToRead = SER.Deserialize(XMLR);
				}
				catch (Exception exception)
				{
					objectToRead = null;
					flag = false;
					return flag;
				}
			}
			finally
			{
				FS.Close();
			}
			flag = true;
			return flag;
		}

		private bool ReadObject(out object objectToRead, Stream stream, Type T)
		{
			bool flag;
			stream.Position = (long)0;
			XmlSerializer SER = new XmlSerializer(T);
			XmlReader XMLR = new XmlTextReader(stream);
			try
			{
				objectToRead = SER.Deserialize(XMLR);
			}
			catch (Exception exception)
			{
				objectToRead = null;
				flag = false;
				return flag;
			}
			flag = true;
			return flag;
		}

		private bool ReadObject(out object[] objectToRead, Stream stream, Type T)
		{
			bool flag;
			stream.Position = (long)0;
			XmlSerializer SER = new XmlSerializer(T);
			XmlReader XMLR = new XmlTextReader(stream);
			try
			{
				objectToRead = (object[])SER.Deserialize(XMLR);
			}
			catch (Exception exception)
			{
				objectToRead = null;
				flag = false;
				return flag;
			}
			flag = true;
			return flag;
		}

		private bool ReadObject(out object[] objectToRead, string path, Type T)
		{
			bool flag;
			FileStream FS = new FileStream(path, FileMode.Open);
			XmlSerializer SER = new XmlSerializer(T);
			XmlReader XMLR = new XmlTextReader(FS);
			try
			{
				try
				{
					objectToRead = (object[])SER.Deserialize(XMLR);
				}
				catch (Exception exception)
				{
					objectToRead = null;
					flag = false;
					return flag;
				}
			}
			finally
			{
				FS.Close();
			}
			flag = true;
			return flag;
		}

		private void RemoveFromOrphanList(ObjectClassBase Item)
		{
			int index = this.i_Orphans.IndexOf(Item);
			this.i_Orphans.Remove(Item);
			if (this.EventOrphansChanged != null)
			{
				this.EventOrphansChanged(new ItemChangedArgs(index, Item, ArgAction.Remove));
			}
		}

		private void ReplaceReferences(ObjectClassBase toEdit, ObjectClassBase NewValues)
		{
			int num;
			byte objLayer = 0;
			OCCBase[] collectors = this.Collectors;
			for (num = 0; num < (int)collectors.Length; num++)
			{
				OCCBase colector = collectors[num];
				if (colector.CollectionType == toEdit.CollectionType)
				{
					objLayer = colector.objectLayer;
				}
			}
			for (int i = objLayer + 1; i <= 10; i++)
			{
				collectors = this.Collectors;
				for (num = 0; num < (int)collectors.Length; num++)
				{
					OCCBase occ = collectors[num];
					if (occ.objectLayer == i)
					{
						foreach (ObjectClassBase obj in occ)
						{
							obj.ReplaceReferences(toEdit, NewValues);
						}
					}
				}
			}
		}

		public SerializableVersion[] RequiredPlugins()
		{
			List<SerializableVersion> retVal = new List<SerializableVersion>();
			List<Type> requiredTypes = new List<Type>();
			OCCBase[] collectors = this.Collectors;
			for (int i = 0; i < (int)collectors.Length; i++)
			{
				OCCBase occ = collectors[i];
				if (occ.Count >= 1)
				{
					ObjectClassBase[] objectClasses = PluginEngine.GetObjectClasses();
					int num = 0;
					while (num < (int)objectClasses.Length)
					{
						ObjectClassBase oc = objectClasses[num];
						if (!(oc.CollectionType == occ.CollectionType))
						{
							num++;
						}
						else
						{
							requiredTypes.Add(oc.GetType());
							break;
						}
					}
					requiredTypes.Add(occ.GetType());
				}
			}
			foreach (Type typ in requiredTypes)
			{
				List<object> attributes = new List<object>(typ.GetCustomAttributes(typeof(ObjectClassPlugIn), true));
				if (attributes.Count >= 1)
				{
					foreach (ObjectClassPlugIn ocpi in attributes)
					{
						retVal.Add(ocpi.version);
					}
				}
				attributes = new List<object>(typ.GetCustomAttributes(typeof(OCCPlugIn), true));
				if (attributes.Count >= 1)
				{
					foreach (OCCPlugIn occpi in attributes)
					{
						retVal.Add(occpi.version);
					}
				}
			}
			return retVal.ToArray();
		}

		public void Save()
		{
			try
			{
				if (!((this.FullFileName == "") | this.FullFileName == null))
				{
					this.SaveFile();
				}
				else
				{
					this.SaveAs();
				}
			}
			catch
			{
				throw;
			}
		}

		public void SaveAs()
		{
			try
			{
				SaveFileDialog SFD = new SaveFileDialog()
				{
					Filter = "NewTerra Dat Files (*.ntx)|*.ntx|All files (*.*)|*.*",
					SupportMultiDottedExtensions = true
				};
				if (SFD.ShowDialog() == DialogResult.OK)
				{
					if ((this.IDPreFix.ToUpper() == "NULL" ? true : this.IDPreFix.ToUpper() == ""))
					{
						string tmpPreFix = InputBox.Show("Please Enter a 4 digit alpha-numeric Identifier for this data set", "Data Pre-Fix");
						if (tmpPreFix != "")
						{
							ObjectClassBase[] allData = this.AllData;
							for (int i = 0; i < (int)allData.Length; i++)
							{
								ObjectClassBase ocb = allData[i];
								ocb.ID = ocb.ID.Replace(this.IDPreFix, tmpPreFix);
							}
							this.IDPreFix = tmpPreFix;
						}
					}
					this.FullFileName = SFD.FileName;
					this.SaveFile();
				}
			}
			catch
			{
				throw;
			}
		}

		private void SaveFile()
		{
			Exception ex;
			string tmpFolder = string.Concat(Path.GetTempPath(), "NewTerra");
			List<string> subFiles = new List<string>();
			if (!Directory.Exists(tmpFolder))
			{
				Directory.CreateDirectory(tmpFolder);
			}
			else if (((int)Directory.GetDirectories(tmpFolder).Length != 0 ? true : (int)Directory.GetFiles(tmpFolder).Length != 0))
			{
				Directory.Delete(tmpFolder, true);
				Directory.CreateDirectory(tmpFolder);
			}
			try
			{
				try
				{
					string fileName = this.FileName;
					char[] chrArray = new char[] { '.' };
					subFiles.Add(this.WriteObject(this, string.Concat(tmpFolder, "\\", fileName.Split(chrArray)[0])));
					subFiles.Add(this.WriteObject(this.RequiredPlugins(), string.Concat(tmpFolder, "\\Requirements")));
					ObjectClassBase[] allData = this.AllData;
					for (int i = 0; i < (int)allData.Length; i++)
					{
						object obj = allData[i];
						try
						{
							if (!(obj is ObjectClassBase))
							{
								throw new Exception();
							}
							ObjectClassBase IObj = (ObjectClassBase)obj;
							string[] collectionName = new string[] { tmpFolder, "\\", IObj.CollectionName, "\\", IObj.Name };
							string tmpFile = string.Concat(collectionName);
							subFiles.Add(string.Concat(this.WriteObject(IObj, tmpFile), "!", IObj.CollectionName));
						}
						catch (Exception exception)
						{
							ex = exception;
						}
					}
					try
					{
						if (File.Exists(this.FullFileName))
						{
							string fullFileName = this.FullFileName;
							string filePath = this.FilePath;
							string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.FileName);
							DateTime now = DateTime.Now;
							File.Move(fullFileName, string.Concat(filePath, fileNameWithoutExtension, now.ToString(".yyyyMMddHHMMss"), Path.GetExtension(this.FileName)));
							File.Delete(this.FullFileName);
						}
						ZipFile zipFile = new ZipFile(this.FullFileName);
						zipFile.set_CompressionLevel(9);
						foreach (string file in subFiles)
						{
							try
							{
								chrArray = new char[] { '!' };
								string[] splitString = file.Split(chrArray);
								if ((int)splitString.Length <= 0)
								{
									throw new Exception(string.Format("Object {0} could not be saved", file));
								}
								if ((int)splitString.Length != 1)
								{
									zipFile.AddFile(splitString[0], splitString[1]);
								}
								else
								{
									zipFile.AddFile(file, "");
								}
							}
							catch (Exception exception1)
							{
								if (!exception1.Message.Contains("already exists"))
								{
									throw;
								}
								else
								{
									MessageBox.Show(string.Concat(Path.GetFileNameWithoutExtension(file), " is sharing a duplicate name, please correct this error and re-save the file"));
								}
							}
						}
						zipFile.Save();
					}
					catch (Exception exception2)
					{
						throw;
					}
					if (this.filePassword.Trim() != string.Empty)
					{
						Security.cryptFile(this.FullFileName, Security.CryptAction.encrypt);
						File.Delete(this.FullFileName);
						File.Move(string.Concat(this.FullFileName, "~"), this.FullFileName);
					}
				}
				catch (Exception exception3)
				{
					ex = exception3;
					throw new NTFileExecption(this.FileName, string.Format("File {0} could not be saved", this.FileName), ex);
				}
			}
			finally
			{
			}
			this.DataChanged = false;
		}

		public override string ToString()
		{
			return this.ToString();
		}

		public void UnLockFile(string cleartypePassword)
		{
			if (!this.CheckPassword(cleartypePassword))
			{
				throw new InvalidPasswordException("Password incorrect");
			}
			this.fileLock = false;
			if (this.LockStatusChange != null)
			{
				this.LockStatusChange();
			}
		}

		internal ObjectClassBase updateOrphaning(ObjectClassBase orphanedObjRef)
		{
			if (!this.OrphanExists(orphanedObjRef))
			{
				if (orphanedObjRef != null)
				{
					((IOwner)orphanedObjRef).myOwner = null;
				}
				this.AddOrphan(orphanedObjRef);
			}
			return null;
		}

		private string WriteObject(object objToWrite, string path)
		{
			string str;
			path = string.Concat(path, ".xml");
			if (!Directory.Exists(Path.GetDirectoryName(path)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(path));
			}
			FileStream FS = new FileStream(path, FileMode.Create);
			try
			{
				try
				{
					XmlSerializer SER = new XmlSerializer(objToWrite.GetType());
					XmlTextWriter xmlTextWriter = new XmlTextWriterFormattedNoDeclaration(FS);
					XmlSerializerNamespaces NameSpace = new XmlSerializerNamespaces();
					NameSpace.Add("", "");
					SER.Serialize(xmlTextWriter, objToWrite, NameSpace);
					str = path;
				}
				catch
				{
					throw;
				}
			}
			finally
			{
				FS.Close();
			}
			return str;
		}

		public event NTEventHandler EventDataStateChanged;

		public event NTEventHandler<ItemChangedArgs> EventOrphansChanged;

		public event NTEventHandler LockStatusChange;

		public event NTEventHandler<UpdateProgressEventArgs> Update;

		public event NTEventHandler Updated;

		public event NTEventHandler<UpdaterEventArgs> Updating;

		public delegate void NTDataFileDisposing(NTDataFile DataFile);

		public enum NTFileType
		{
			NTX,
			NTD
		}

		public enum NTSaveType
		{
			FileDefault,
			SaveAs,
			Copy,
			CopyAs
		}

		private enum UndoActionKeyWords
		{
			Drop,
			Add,
			Edit
		}
	}
}