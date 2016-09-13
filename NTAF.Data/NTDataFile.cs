using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using Ionic.Zip;
using NTAF.Core;
using NTAF.PlugInFramework;
using System.Windows.Controls;
using System.Drawing;
using System.Linq;
using NTAF.PlugInFramework.OrphanControls;
using System.Diagnostics;
using System.ComponentModel;

namespace NTAF.Core {
    [Serializable()]//, XmlInclude(typeof(KeyVal<String,Version>))]
    public class NTDataFile : ITrackChange, ILockable, IUpdateProgress, ILink {

        #region Fields

        private char[] _IDPreFix;

        //private List<String>
        //    I_RequirePlugins = new List<string>();
        private string _Path;

        OperationLogger actions = new OperationLogger();

        private bool dataChanged = false;

        private string datafilename = "";

        private bool fileLock = false;

        /// <summary>
        /// stored encrypted password
        /// </summary>
        private string filePassword = "";

        private List<OCCBase>
            i_OCCPlugins = new List<OCCBase>(PluginEngine.GetOCCPlugIns());

        private OrphanCollector
            orphanCollector = new PlugInFramework.OrphanControls.OrphanCollector();

        //ContextMenu
        //    i_FileMenu,
        //    i_RootMenu,
        //    i_NodeMenu,
        //    i_OrphanRootMenu,
        //    i_OrphansMenu;
        //private List<ObjectClassBase>
        //    i_Orphans = new List<ObjectClassBase>();
        OrphanTree orphanTree = new OrphanTree();

        #endregion Fields

        #region Constructors

        public NTDataFile() {
            IDPreFix = "NULL";
            }

        public NTDataFile(String path) {
            FullFileName = path;
            IDPreFix = "NULL";
            }

        //public NTDataFile( NTDataFile dataFile ) {
        //    throw new System.NotImplementedException();
        //}
        public NTDataFile(String path, string idPreFix) {
            _Path = path;
            IDPreFix = idPreFix;
            }

        public NTDataFile(String path, string idPreFix, string dataSetName) {
            _Path = path;
            IDPreFix = idPreFix;
            DataFileName = dataSetName;
            }

        #endregion Constructors

        #region Delegates

        public delegate void NTDataFileDisposing(NTDataFile DataFile);

        #endregion Delegates

        #region Events

        //private List<ObjectClassBase>
        //    orphanedObjects = new List<ObjectClassBase>();
        public event NTEventHandler EventDataStateChanged;

        public event NTEventHandler<ItemChangedArgs> EventOrphansChanged;

        public event NTEventHandler LockStatusChange;

        public event NTEventHandler<UpdateProgressEventArgs> Update;

        public event NTEventHandler Updated;

        public event NTEventHandler<UpdaterEventArgs> Updating;

        #endregion Events

        #region Enums

        public enum NTFileType { NTX, NTD }
        public enum NTSaveType { FileDefault, SaveAs, Copy, CopyAs };
        private enum UndoActionKeyWords {
            Drop,
            Add,
            Edit
            }

        #endregion Enums

        #region Properties

        [XmlIgnore(), Browsable(false)]
        public ObjectClassBase[] AllData {
            get {
                List<ObjectClassBase> retVal = new List<ObjectClassBase>();

                foreach (OCCBase oc in i_OCCPlugins) {
                    foreach (Object obj in oc.Objects) {
                        if (obj is ObjectClassBase) {
                            retVal.Add((ObjectClassBase)obj);
                            }
                        }
                    }
                return retVal.ToArray();
                }
            }

        [XmlElement()]
        public string Author { get; set; }

        [XmlElement()]
        public string AuthorEmail { get; set; }

        [XmlElement()]
        public string AuthorWebsite { get; set; }
        [XmlIgnore(), Browsable(false)]
        public OCCBase[] Collectors {
            get { return i_OCCPlugins.ToArray(); }
            }

        [XmlIgnore(), Browsable(false)]
        public bool DataChanged {
            get { return dataChanged; }
            set {
                dataChanged = value;

                if (EventDataStateChanged != null)
                    EventDataStateChanged();
                }
            }

        [XmlAttribute(), ReadOnly(true)]
        public string DataFileName {
            get { return datafilename; }
            set {
                datafilename = value;

                if (EventDataStateChanged != null)
                    EventDataStateChanged();
                }
            }

        [XmlElement(), ReadOnly(true)]
        public DateTime DateCreated { get; set; }

        [XmlElement(), ReadOnly(true)]
        public DateTime DateLastEdited { get; set; }

        [XmlElement()]
        public string Description { get; set; }
        /// <summary>
        /// Gets the full path to the file but doesn't include the file name
        /// </summary>
        [XmlIgnore(), Browsable(false)]
        public string FileDir {
            get { return System.IO.Path.GetDirectoryName(_Path); }
            }

        [XmlIgnore(), Browsable(false)]
        public string FileExtention {
            get { return System.IO.Path.GetExtension(_Path); }
            }

        //new
        /// <summary>
        /// Returns true of the file is locked
        /// </summary>
        [XmlIgnore(), ReadOnly(true)]
        public bool FileLocked {
            get {
                bool retval = false;
                if (Properties.Settings.Default.Loading)
                    retval = false;
                else
                    retval = fileLock;

                return retval;
                }
            }

        /// <summary>
        /// Gets the file name with no path
        /// </summary>
        [XmlIgnore(), ReadOnly(true)]
        public string FileName {
            get { return System.IO.Path.GetFileName(_Path); }
            }

        /// <summary>
        /// Gets the file name with no path or file extension
        /// </summary>
        [XmlIgnore(), ReadOnly(true)]
        public string FileNameNoExt {
            get { return System.IO.Path.GetFileNameWithoutExtension(_Path); }
            }

        //new
        /// <summary>
        /// Sets the file password if the file is not currently locked
        /// or takes a clear type string, encrypts it and stores it in the file
        /// </summary>
        /// <exception cref="FileLockedException">FileLockedException - if the file is locked</exception>
        [XmlAttribute(), Browsable(false)]
        public string FilePassword {
            get { return filePassword; }
            set {
                if (FileLocked)
                    throw new FileLockedException("File is locked, and cannot be edited.");

                if (value.Trim() == String.Empty) {
                    filePassword = value;
                    fileLock = false;
                    DataChanged = true;
                    return;
                    }

                if (!Properties.Settings.Default.Loading) {
                    filePassword = Security.Encrypt(value);
                    DataChanged = true;
                    } else
                    filePassword = value;
                }
            }

        [XmlIgnore(), Browsable(false)]
        public string FilePath {
            get { return _Path.Remove(_Path.LastIndexOf(System.IO.Path.GetFileName(_Path))); }
            }

        /// <summary>
        /// Gets or sets the full path to the file
        /// "C:\blah\blah\blah.NTD"
        /// </summary>
        [XmlIgnore(), Browsable(false)]
        public string FullFileName {
            get { return _Path; }
            set { _Path = value; }
            }

        /// <summary>
        /// Gets or Sets the 4 digit Alpha Numeric identifier for the data file\n
        /// Note:strings longer than 4 chars are cut off after the 4th so\n
        /// "ABCDE" will turn in to "ABCD"
        /// </summary>
        [XmlAttribute(), ReadOnly(true)]
        public string IDPreFix {
            get {
                String retVal = "";
                foreach (char CH in _IDPreFix)
                    retVal += CH.ToString();
                return retVal.ToString();
                }
            set { _IDPreFix = value.ToCharArray(0, 4); }
            }

        [XmlIgnore(), Browsable(false)]
        public String[] IDs {
            get {
                List<String>
                    retVal = new List<string>();

                foreach (ObjectClassBase obj in AllData)
                    retVal.Add(obj.ID);

                return retVal.ToArray();
                }
            }

        [XmlIgnore(), Browsable(false)]
        public SerializableVersion[] LoadedPlugins {
            get {
                List<SerializableVersion>
                    retVal = new List<SerializableVersion>();

                foreach (Assembly ass in PluginEngine.LoadedAssemblies()) {
                    foreach (Type typ in ass.GetTypes()) {
                        List<object>
                           attributes = new List<object>(typ.GetCustomAttributes(typeof(ObjectClassPlugIn), true));

                        if (attributes.Count >= 1)
                            foreach (ObjectClassPlugIn ocpi in attributes)
                                retVal.Add(ocpi.version);

                        attributes = new List<object>(typ.GetCustomAttributes(typeof(OCCPlugIn), true));

                        if (attributes.Count >= 1)
                            foreach (OCCPlugIn occpi in attributes)
                                retVal.Add(occpi.version);

                        attributes = new List<object>(typ.GetCustomAttributes(typeof(EditorPlugIn), true));

                        if (attributes.Count >= 1)
                            foreach (EditorPlugIn edpi in attributes)
                                retVal.Add(edpi.version);

                        attributes = new List<object>(typ.GetCustomAttributes(typeof(TreeNodePlugIn), true));

                        if (attributes.Count >= 1)
                            foreach (TreeNodePlugIn tnpi in attributes)
                                retVal.Add(tnpi.version);

                        }
                    }

                return retVal.ToArray();
                }
            }

        [XmlIgnore(), Browsable(false)]
        public ObjectClassBase[] Orphans {
            get { return (ObjectClassBase[])orphanCollector.Objects.ToArray(); }
            }

        #endregion Properties

        #region Methods

        public void Add(ObjectClassBase toAdd) {
            if (FileLocked)
                throw new FileLockedException("File is locked, and cannot be edited.");

            //try {
            OCCBase
                tmpOCC = GetCollector(toAdd);

            if (tmpOCC == null)
                throw new InvalidOperationException("Could not find that type among the plugins");

            if (tmpOCC.Exists(toAdd))
                throw new ItemException("That Object Already exists in the collection");

            ((ObjectClassBase)toAdd).MyDataChanged += new NTEventHandler(NTDataFile_EventMyDataChanged);

            if (toAdd is IOwner)
                ((IOwner)toAdd).myOwner = this;

            tmpOCC.AddObject(toAdd);

            DataChanged = true;

            if (!NTAF.Core.Properties.Settings.Default.PerformingAction & !Properties.Settings.Default.Loading)
                actions.AddUndoableOpp(UndoActionKeyWords.Add, toAdd);
            //} catch (Exception ex) { throw ex; }
            }

        public void AddOrpahns(ObjectClassBase[] NewOrphans) {
            foreach (ObjectClassBase orphan in NewOrphans) {
                AddOrphan(orphan);
                }
            }

        public void AddOrphan(ObjectClassBase OrphanedObject) {
            if (!orphanCollector.Exists(OrphanedObject))
                MoveToOrphanList(OrphanedObject, null);
            //orphanedObjects.Add( OrphanedObject );
            }

        public bool CheckPassword(string cleartypePassword) {
            return filePassword == Security.Encrypt(cleartypePassword);
            }

        public void ClearOrphans() {
            orphanCollector.Clear();
            }

        public void DoRedo() {
            try {
                NTAF.Core.Properties.Settings.Default.PerformingAction = true;
                Operation action = actions.RedoAction();
                switch ((UndoActionKeyWords)action.Action) {
                    //in the event of redoing an action we need to use the action as is with no crossing
                    case UndoActionKeyWords.Add:
                        //Add should delete what was added
                        Add((ObjectClassBase)action.Data);
                        break;
                    case UndoActionKeyWords.Drop:
                        //Drop needs to re add the removed item
                        Drop((ObjectClassBase)action.Data);
                        break;
                    case UndoActionKeyWords.Edit:
                        //edit needs to un-edit
                        Object[] tmpArray = (Object[])action.Data;
                        Edit((ObjectClassBase)tmpArray[0], (ObjectClassBase)tmpArray[1]);
                        break;
                    }
                } catch { throw; } finally { NTAF.Core.Properties.Settings.Default.PerformingAction = false; }
            }

        public void DoUndo() {
            try {
                NTAF.Core.Properties.Settings.Default.PerformingAction = true;
                Operation action = actions.UndoAction();
                switch ((UndoActionKeyWords)action.Action) {
                    //in the event of undoing an action we need to do the reverse of the action that happened
                    case UndoActionKeyWords.Add:
                        //Add should delete what was added
                        Drop((ObjectClassBase)action.Data);
                        break;
                    case UndoActionKeyWords.Drop:
                        //Drop needs to re add the removed item and remove from orphan list
                        Add((ObjectClassBase)action.Data);
                        DropOrphan((ObjectClassBase)action.Data);
                        break;
                    case UndoActionKeyWords.Edit:
                        //edit needs to un-edit
                        Object[] tmpArray = (Object[])action.Data;
                        Edit((ObjectClassBase)tmpArray[1], (ObjectClassBase)tmpArray[0]);
                        break;
                    }
                } catch { throw; } finally { NTAF.Core.Properties.Settings.Default.PerformingAction = false; }
            }

        /// <summary>
        /// Drops a piece of data from the collection. Basically just selects the correct drop method.
        /// </summary>
        /// <param name="toDrop">Object must have the ObjectClassBase interface</param>
        public void Drop(ObjectClassBase toDrop) {
            if (FileLocked)
                throw new FileLockedException("File is locked, and cannot be edited.");

            try {
                OCCBase
                    tmpOCC = GetCollector(toDrop);

                if (tmpOCC == null)
                    throw new InvalidOperationException("Could not find that type among the plug-ins");

                //todo this sux make better
                if (!tmpOCC.Exists(toDrop))
                    throw new Exception();

                if (!CheckForReferences(toDrop)) {

                    ((ObjectClassBase)tmpOCC[toDrop]).MyDataChanged -= NTDataFile_EventMyDataChanged;

                    tmpOCC.DropObject(toDrop);
                    } else {
                    MoveToOrphanList(toDrop, tmpOCC);
                    }
                DataChanged = true;

                if (!NTAF.Core.Properties.Settings.Default.PerformingAction & !Properties.Settings.Default.Loading)
                    actions.AddUndoableOpp(UndoActionKeyWords.Drop, toDrop);

                } catch (Exception ex) { throw ex; }
            }

        /// <summary>
        /// removes object from orphaning collector
        /// </summary>
        /// <param name="OrphanedObject"></param>
        public void DropOrphan(ObjectClassBase OrphanedObject) {
            if (orphanCollector.Exists(OrphanedObject))
                orphanCollector.DropObject(OrphanedObject);
            }

        public void Edit(ObjectClassBase toEdit, ObjectClassBase NewValues) {
            if (FileLocked)
                throw new FileLockedException("File is locked, and cannot be edited.");

            if (!(toEdit is ObjectClassBase) || !(NewValues is ObjectClassBase))
                throw new InvalidCastException("The item being edited is not of the proper type");

            //if ( ( ( ObjectClassBase )toEdit ).GetType( ) != ( ( ObjectClassBase )toEdit ).GetType( ) )
            //    throw new InvalidCastException( "The item being edited cannot accept the new values they are not of the same type" );

            try {
                OCCBase
                    tmpOCC_Remove = GetCollector(toEdit),
                    tmpOCC_Add = GetCollector(NewValues);

                if (tmpOCC_Remove == null)
                    throw new InvalidOperationException("Collector plugin not loaded for the item being edited,\nchanges not applied");

                if (tmpOCC_Remove != tmpOCC_Add && tmpOCC_Add == null)
                    throw new InvalidOperationException("Collector plugin for the edited object is not loaded,\nchanges not applied");

                if (tmpOCC_Add.Exists(NewValues))
                    throw new ItemException("This object already exists in the collector its being moved to,\nchanges not applied");

                ReplaceReferences(toEdit, NewValues);

                tmpOCC_Remove.DropObject(toEdit);

                tmpOCC_Add.AddObject(NewValues);

                //reassign missing event listeners
                if (toEdit is ObjectClassBase)
                    ((ObjectClassBase)toEdit).MyDataChanged -= NTDataFile_EventMyDataChanged;

                if (NewValues is ObjectClassBase)
                    ((ObjectClassBase)NewValues).MyDataChanged += new NTEventHandler(NTDataFile_EventMyDataChanged);

                DataChanged = true;

                Object[] data = new Object[] { toEdit, NewValues };

                if (!NTAF.Core.Properties.Settings.Default.PerformingAction)
                    actions.AddUndoableOpp(UndoActionKeyWords.Edit, data);

                } catch (Exception ex) { throw ex; }
            }

        public void ExportToCSV(String path) {
            StreamWriter sw = new StreamWriter(new FileStream(path + FileNameNoExt + ".csv", FileMode.Create));
            sw.WriteLine("This file can be loaded an any good spread sheet program as table data. when loading be sure to split the data via the pipe '|' found above the '\\' key");
            try {
                bool printHeader = true;

                foreach (ObjectClassBase objectClass in PluginEngine.GetObjectClasses()) {
                    sw.WriteLine("Table:" + objectClass.CollectionName);
                    foreach (ObjectClassBase obj in this.AllData) {
                        if (obj.CollectionType == ((ObjectClassBase)objectClass).CollectionType) {

                            DataMember[] dataMembers = obj.getDataMembers();

                            String line = "";

                            if (printHeader) {

                                foreach (DataMember dataMember in dataMembers)
                                    line += String.Format("{0}|", dataMember.Field);
                                line = line.TrimEnd(new[] { ' ', ',' });
                                sw.WriteLine(line);

                                line = "";

                                printHeader = false;
                                }

                            try {

                                foreach (DataMember dataMember in dataMembers)
                                    line += String.Format("{0}|", dataMember.Data);
                                line = line.TrimEnd(new[] { ' ', ',' });
                                sw.WriteLine(line);

                                } catch (Exception ex) {
                                throw new Exception("Object out put error", ex);
                                }
                            }

                        }
                    sw.WriteLine("");
                    printHeader = true;

                    }
                } catch (Exception ex) {
                throw new NTFileExecption(Path.GetFileName(path), "Could not export data", ex);
                } finally {
                sw.Close();
                }
            }

        public void ExportToTXT(String path) {
            StreamWriter sw = new StreamWriter(new FileStream(path + FileNameNoExt + ".txt", FileMode.Create));

            try {
                foreach (Object obj in this.AllData) {
                    try {
                        if (!(obj is ObjectClassBase))
                            throw new Exception(); //todo make this better

                        ObjectClassBase
                            IObj = (ObjectClassBase)obj;

                        DataMember[] dataMembers = IObj.getDataMembers();

                        sw.WriteLine(IObj.CollectionName);

                        foreach (DataMember dataMember in dataMembers)
                            sw.WriteLine(String.Format("{0}:{1}", dataMember.Field, dataMember.Data));

                        sw.WriteLine("============================================================");

                        } catch (Exception ex) {
                        throw new Exception("Object out put error", ex);
                        }
                    }
                } catch (Exception ex) {
                throw new NTFileExecption(Path.GetFileName(path), "Could not export data", ex);
                } finally {
                sw.Close();
                }
            }

        public void ExportToXML(String path) {
            String
                tmpFolder = path + FileNameNoExt;

            if (!Directory.Exists(tmpFolder))
                Directory.CreateDirectory(tmpFolder);
            else
                 if (Directory.GetDirectories(tmpFolder).Length != 0 || Directory.GetFiles(tmpFolder).Length != 0) {
                Directory.Delete(tmpFolder, true);
                Directory.CreateDirectory(tmpFolder);
                }

            try {
                WriteObject(this, tmpFolder + "\\" + FileName.Split('.')[0]);

                WriteObject(RequiredPlugins(), tmpFolder + "\\Requirements");

                foreach (Object obj in this.AllData) {
                    try {
                        if (!(obj is ObjectClassBase))
                            throw new Exception(); //todo make this better

                        ObjectClassBase
                            IObj = (ObjectClassBase)obj;

                        String
                            tmpFile = tmpFolder + "\\" + IObj.CollectionName + "\\" + IObj.Name;

                        WriteObject(IObj, tmpFile);// + "!" + IObj.CollectionName);

                        } catch (Exception ex) { }

                    }
                } catch (Exception ex) { }
            }

        public void ExportToXMLSingle(String path) {
            String
                tmpFolder = path + FileNameNoExt;

            try {
                NTDataFileExt fileout = new NTDataFileExt {
                    InternalFile = this,
                    Author = this.Author,
                    AuthorEmail = this.AuthorEmail,
                    AuthorWebsite = this.AuthorWebsite,
                    Description = this.Description};

                WriteObject2(fileout, tmpFolder);

                } catch (Exception ex) { }
            }

        public void ExportCollectorToCSV(String path, string Collector) { }
        public void ExportCollectorToCSV(String path, OCCBase Collector) { }
        public void ExportCollectorToTXT(String path, string Collector) { }
        public void ExportCollectorToTXT(String path, OCCBase Collector) { }
        public void ExportCollectorToXML(String path, string Collector) {
            OCCBase toExport = Collectors.First(c => c.CollectionName == Collector);
            if (toExport == null) throw new ItemException("Collector Not Found");
            ExportCollectorToXML(path, toExport);
            }
        public void ExportCollectorToXML(String path, OCCBase Collector) {
            String
                tmpFolder = path + FileNameNoExt + "-" + Collector.CollectionName;

            try {
                

                WriteObject2(Collector, tmpFolder);

                } catch (Exception ex) { }
            }

        public void ExportObjectToCSV(String path, string id) { }
        public void ExportObjectToCSV(String path, ObjectClassBase obj) { }
        public void ExportObjectToTXT(String path, string id) { }
        public void ExportObjectToTXT(String path, ObjectClassBase obj) { }
        public void ExportObjectToXML(String path, string id) { }
        public void ExportObjectToXML(String path, ObjectClassBase obj) { }

        public object FindObject(string ID) {
            Object
                retVal = null;

            //null check
            if (ID == "")
                return retVal;

            foreach (OCCBase occ in Collectors) {
                try {
                    retVal = occ[ID, SearchField.ID];
                    if (retVal != null)
                        break;
                    } catch (Exception ex) {
                    Debug.Print(ex.Message);
                    }
                }
            //object could not be found
            //search the orphan list
            if (retVal == null) {
                try {
                    retVal = orphanCollector[ID, SearchField.ID];
                    } catch { }
                }
            return retVal;
            }

        public object FindObject(ObjectClassBase obj) {
            Object
                retVal = null;

            //null check
            if (obj == null)
                return retVal;
            //todo use LINQ to return just what you want instead of constantly iterating over the data

            foreach (OCCBase occ in Collectors.Where(c => obj.GetType() == c.CollectionType)) {
                try {
                    retVal = occ[obj];
                    if (retVal != null)
                        break;
                    } catch (InvalidParameter) { } catch (Exception ex) {
                    throw new Exception(
                        String.Format(
                            "An error occurred while linking objects" + Environment.NewLine +
                            (obj is IOwner ?
                                "The object {0} in file {1} caused the error" :
                                "The object {0} caused the error in an {1} file"),
                            obj.Name,
                            (obj is IOwner ? ((IOwner)obj).myOwner : "untraceable")
                            ),
                        ex);
                    }
                }
            //object could not be found
            if (retVal == null) {
                //search the orphan list make sure its not being added twice
                if (!orphanCollector.Exists(obj)) {
                    //doesn't exist add it
                    MoveToOrphanList(obj, null);
                    //i_Orphans.Add( obj );
                    }

                //retrieve the orphan from the orphan list
                retVal = orphanCollector[obj];
                }

            return retVal;
            }

        public void CopyCollectorToClipboard(string collectorName) {
            OCCBase collector = Collectors.First(c => c.CollectionName == collectorName);
            if (collector == null) throw new Exception("COuld not find selected collector in data file");
            CopyClip.CopyToClipboard(collector);
            }

        public void CopyObjecctToClipboard(string objID) {
            ObjectClassBase obj = AllData.First(o=>o.ID == objID);
            if (obj == null) throw new Exception("Could not find selected object in data file");
            CopyClip.CopyToClipboard(obj);
            }

        public void CopyFileToClipboard() {
            NTDataFileExt fileout = new NTDataFileExt {
                InternalFile = this,
                Author = this.Author,
                AuthorEmail = this.AuthorEmail,
                AuthorWebsite = this.AuthorWebsite,
                Description = this.Description
                };
            
            CopyClip.CopyToClipboard(fileout);
            }

        public string GenerateIDCode() {
            //this is what is called no matter what prams are supplied
            string guidResult = string.Empty;

            do {
                guidResult = IDPreFix;// "";
                while (guidResult.Length < 12) {
                    // Get the GUID.
                    guidResult += Guid.NewGuid().ToString().GetHashCode().ToString("X");
                    }

                if (IDPreFix.Length != 4)
                    throw new ArgumentException("dataSetID is not valid, it must be exactly 4 characters long");

                } while (IDCodeIsUnique(guidResult.Substring(0, 12)));

            // Return the first length bytes.
            return guidResult.Substring(0, 12);
            }

        [Browsable(false)]
        public OCCBase GetCollector(Type T) {
            OCCBase
                retVal = null;

            foreach (OCCBase iocc in i_OCCPlugins) {
                if (iocc.CollectionType == T) {
                    retVal = iocc;
                    break;
                    }
                }

            return retVal;
            }

        [Browsable(false)]
        public OCCBase GetCollector(Object T) {
            OCCBase
                retVal = null;

            foreach (OCCBase iocc in i_OCCPlugins)
                if (iocc.IsOfType(T.GetType()))
                    return iocc;

            return retVal;
            }

        /// <summary>
        /// Gets data in a tree form
        /// </summary>
        /// <param name="tree">Tree control node collection</param>
        public void getDataTree(NTDataTreeNode tree) {

            NTDataTreeNode rootNode = new NTDataTreeNode(this.FileName);

            getTreeNodes(rootNode);

            tree.Nodes.Add(rootNode);
            }

        public NTDataTreeNode GetDataTree() {
            NTDataTreeNode root = new NTDataTreeNode(this.FileName);
            root.NodeType = NTDataTreeNode.NodeTypeEnum.DataRoot;
            foreach (OCCBase occ in Collectors) {
                NTDataTreeNode occNode = occ.TreeData;
                //occNode.NodeType = NTDataTreeNode.NodeTypeEnum.ObjectCollector;
                //occNode.Text = occ.CollectionName;
                root.Nodes.Add(occ.TreeData);
                }
            return root;
            }

        //public Type[] GetLoadedPluginTypes() {
        //    List<Type>
        //            retVal = new List<Type>();

        //    foreach (Assembly ass in PluginEngine.LoadedAssemblies()) {
        //        foreach (Type typ in ass.GetTypes()) {
        //            if (typ.IsSubclassOf(typeof(OCCBase))) {
        //                retVal.Add(typ);
        //                }
        //            if (typ.IsSubclassOf(typeof(ObjectClassBase))) {
        //                retVal.Add(typ);
        //                }
        //            }
        //        }

        //    //retVal.AddRange(ass.GetTypes().Where(t=>t is OCCBase));
        //    return retVal.ToArray();
        //    }

        [Browsable(false)]
        public T[] GetObjects<T>() {
            List<T>
                retVal = new List<T>();

            foreach (OCCBase oc in i_OCCPlugins) {
                if (typeof(T) == oc.CollectionType)
                    foreach (Object obj in oc.Objects) {
                        if (obj is T) {
                            retVal.Add((T)obj);
                            }
                        }
                }

            return retVal.ToArray();
            }
        //todo: need to combine getnodes, programmer will have to figure out how to change the data to a visable tree
        /// <summary>
        /// Gets tree-nodes without assigning menus
        /// </summary>
        /// <param name="treeObject">Tree node that all nodes will be added to</param>
        public void getTreeNodes(NTDataTreeNode treeObject) {
            treeObject.Nodes.Clear();
            //treeObject.Clear();

            List<OCTreeNodeBase>
                i_TreeNodePlugins = new List<OCTreeNodeBase>(PluginEngine.GetTreePlugIns());

            //private List<OCCBase>
            //i_OCCPlugins = new List<OCCBase>( PluginEngine.GetOCCPlugIns() );

            //todo COMPLEETED query the collectors for each tree node to get what can be displayed so were not just fumbling around trying to figure it out one at a time over and ovr again

            foreach (OCTreeNodeBase treeNodePlug in i_TreeNodePlugins) {
                foreach (OCCBase occ in Collectors.Where(c => treeNodePlug.CanDisplay(c.CollectionType))) {
                    //if ( treeNodePlug.CanDisplay( occ.CollectionType ) )
                    treeNodePlug.AttachOCC(occ);
                    }

                //treeNodePlug.SetMenus(i_RootMenu, i_NodeMenu);

                //populate the node with collectors tree-nodes or object nodes
                treeObject.Nodes.Add(treeNodePlug.MainBranch());
                }

            //todo finally add orphan nodes here
            int
                currentCount = orphanCollector.Count;

            orphanTree.AttachOCC(orphanCollector);
            //orphanTree.SetMenus(i_OrphanRootMenu, i_OrphansMenu);

            treeObject.Nodes.Add(orphanTree.MainBranch());

            //todo need to add the orphan list to an update method
            //if the orphans branch has less than 1 item don't show it
            //todo make it an option to show empty orphan branch
            //if(orphanNodes.Count()<= 0)orphanNodes.?
            }

        public bool IDCodeIsUnique(string IDCode) {
            bool retVal = false;

            List<String>
                loadedIDs = new List<string>();

            loadedIDs.AddRange(IDs);

            if (loadedIDs.Contains(IDCode))
                retVal = true;

            return retVal;
            }

        public void LinkData() {
            try {
                orphanCollector.Clear();

                //List<OCCBase>
                //    occs = new List<OCCBase>( LoadedCollectors() );
                if (Updating != null)
                    Updating(new UpdaterEventArgs(this.AllData.Length + 1));

                int current = 0;

                for (Byte i = 0; i <= PluginEngine.MAX_OBJECT_LAYER; i++) {
                    foreach (OCCBase occ in Collectors.Where(ol => ol.objectLayer == i)) {
                        foreach (ObjectClassBase obj in occ.Objects) {
                            current++;

                            if (Update != null)
                                Update(new UpdateProgressEventArgs(String.Format("Linking Object {0}", obj.Name), "Linking", obj.Name, current, AllData.Length + 1));

                            obj.Link(this);
                            }
                        }
                    }

                Updated?.Invoke();

                //if ( Updated != null )

                //    Updated();

                //i_Orphans.Sort();
                } catch { throw; }
            }

        public void Load() {
            //throw new NotImplementedException();
            Properties.Settings.Default.Loading = true;

            if (Updating != null)
                Updating(new UpdaterEventArgs(5));

            //if the file goes in for decryption set this to true in the finally we will use it to test
            //if we should delete the temporary file
            Boolean
                FileNeededDecrypt = false;

            NTDataFile //Create a temporary space to de-serialize the file information to
                dataSet = null;

            ZipFile
                zipFile = null;// new ZipFile( FullFileName );

            String
                tmpFolder = System.IO.Path.GetTempPath() + "NewTerra";

            MemoryStream
                ZipStream = null,
                ZipEntryStream = new MemoryStream();

            //check if the container wrapper is encrypted
            if (Update != null)
                Update(new UpdateProgressEventArgs("Opening File...", "Opening", FullFileName, 2, 6));

            try {
                zipFile = new ZipFile(FullFileName);
                } catch (ZipException) {
                if (Update != null)
                    Update(new UpdateProgressEventArgs("File is protected...", "Opening", FullFileName, 3, 6));

                //run the file through the de-cryptor
                ZipStream = Security.StreamCrypt(new FileStream(FullFileName, FileMode.Open), Security.CryptAction.decrypt);
                ZipStream.Position = 0;
                zipFile = ZipFile.Read(ZipStream);
                }

            //if zip file is still null throw an unloadable error
            if (zipFile == null) {
                if (Updated != null)
                    Updated();
                throw new FileLoadException("Could not load file", FullFileName);
                }

            if (Update != null)
                Update(new UpdateProgressEventArgs("Unpacking File this may take a while...", "Inflating", FullFileName, 3, 6));

            try {
                Object[]
                    objArr;

                zipFile["Requirements.xml"].Extract(ZipEntryStream);

                ReadObject(out objArr, ZipEntryStream, typeof(SerializableVersion[]));

                String[]
                    msgList = new String[0];

                List<SerializableVersion>
                    versionCheck = new List<SerializableVersion>((SerializableVersion[])objArr);

                //check for required plugins if any are found missing or an incompatible version throw an error load message
                if (!checkForRequiredPlugins(versionCheck.ToArray(), out msgList)) {
                    String
                        bigMsg = "";
                    foreach (string str in msgList) {
                        bigMsg += str + "\n\n";
                        }
                    throw new FileLoadException(bigMsg, this.FileName);
                    }
                } catch { }

            Object
                tmpObj;

            ZipEntryStream = new MemoryStream();

            zipFile[Path.GetFileNameWithoutExtension(FileName) + ".xml"].Extract(ZipEntryStream);

            ReadObject(out tmpObj, ZipEntryStream, typeof(NTDataFile));

            if (tmpObj is NTDataFile)
                dataSet = (NTDataFile)tmpObj;

            if (Update != null)
                Update(new UpdateProgressEventArgs("File inflated finding objects...", "Hunting", FullFileName, 4, 6));

            if (dataSet != null) {
                filePassword = dataSet.filePassword;
                DataFileName = dataSet.DataFileName;
                IDPreFix = dataSet.IDPreFix;
                }
            List<String>
                UnlodableObjects = new List<String>();

            int
                max = 6 + zipFile.Entries.Count - 1,
                currentCount = 5;

            if (Update != null)
                Update(new UpdateProgressEventArgs("Loading objects...", "Loading objects", FullFileName, currentCount++, max));

            foreach (ZipEntry file in zipFile.Entries) {
                if (Update != null)
                    Update(new UpdateProgressEventArgs(String.Format("Loading {0}", Path.GetFileName(file.FileName)), "Loading", Path.GetFileName(file.FileName), currentCount++, max));

                Boolean
                        gotIt = false;

                Object
                    RetreivedObject = null;

                ZipEntryStream = new MemoryStream();

                zipFile[file.FileName].Extract(ZipEntryStream);

                foreach (Type typ in PluginEngine.GetSerailPlugins()) {
                    try {
                        ReadObject(out RetreivedObject, ZipEntryStream, typ);
                        } catch { }
                    if (RetreivedObject is ObjectClassBase) {
                        Add((ObjectClassBase)RetreivedObject);
                        gotIt = true;
                        break;
                        }
                    }
                if (!gotIt)
                    UnlodableObjects.Add(file.FileName);
                }

            //should the file come equipped with a password lock the file from editing until a password is used to unlock it
            if (filePassword != String.Empty)
                LockFile();

            if (Updated != null)
                Updated();

            actions.ClearLog();

            DataChanged = false;
            Properties.Settings.Default.Loading = false;
            }

        public void Load2() {
            //throw new NotImplementedException();
            Properties.Settings.Default.Loading = true;

            if (Updating != null)
                Updating(new UpdaterEventArgs(5));

            //if the file goes in for decryption set this to true in the finally we will use it to test
            //if we should delete the temporary file
            Boolean
                FileNeededDecrypt = false;

            NTDataFile //Create a temporary space to de-serialize the file information to
                dataSet = null;

            ZipFile
                zipFile = null;// new ZipFile( FullFileName );

            String
                tmpFolder = System.IO.Path.GetTempPath() + "NewTerra";

            MemoryStream
                ZipStream = null,
                ZipEntryStream = new MemoryStream();

            //check if the container wrapper is encrypted
            if (Update != null)
                Update(new UpdateProgressEventArgs("Opening File...", "Opening", FullFileName, 2, 6));

            try {
                zipFile = new ZipFile(FullFileName);
                } catch (ZipException) {
                if (Update != null)
                    Update(new UpdateProgressEventArgs("File is protected...", "Opening", FullFileName, 3, 6));

                //run the file through the de-cryptor
                ZipStream = Security.StreamCrypt(new FileStream(FullFileName, FileMode.Open), Security.CryptAction.decrypt);
                ZipStream.Position = 0;
                zipFile = ZipFile.Read(ZipStream);
                }

            //if zip file is still null throw an unloadable error
            if (zipFile == null) {
                if (Updated != null)
                    Updated();
                throw new FileLoadException("Could not load file", FullFileName);
                }

            if (Update != null)
                Update(new UpdateProgressEventArgs("Unpacking File this may take a while...", "Inflating", FullFileName, 3, 6));

            try {
                Object[]
                    objArr;

                zipFile["Requirements.xml"].Extract(ZipEntryStream);

                ReadObject(out objArr, ZipEntryStream, typeof(SerializableVersion[]));

                String[]
                    msgList = new String[0];

                List<SerializableVersion>
                    versionCheck = new List<SerializableVersion>((SerializableVersion[])objArr);

                //check for required plugins if any are found missing or an incompatible version throw an error load message
                if (!checkForRequiredPlugins(versionCheck.ToArray(), out msgList)) {
                    String
                        bigMsg = "";
                    foreach (string str in msgList) {
                        bigMsg += str + "\n\n";
                        }
                    throw new FileLoadException(bigMsg, this.FileName);
                    }
                } catch { }

            Object
                tmpObj;

            ZipEntryStream = new MemoryStream();

            zipFile[Path.GetFileNameWithoutExtension(FileName) + ".xml"].Extract(ZipEntryStream);

            ReadObject(out tmpObj, ZipEntryStream, typeof(NTDataFile));

            if (tmpObj is NTDataFile)
                dataSet = (NTDataFile)tmpObj;

            if (Update != null)
                Update(new UpdateProgressEventArgs("File inflated finding objects...", "Hunting", FullFileName, 4, 6));

            if (dataSet != null) {
                filePassword = dataSet.filePassword;
                DataFileName = dataSet.DataFileName;
                IDPreFix = dataSet.IDPreFix;
                }
            List<String>
                UnlodableObjects = new List<String>();

            int
                max = 6 + zipFile.Entries.Count - 1,
                currentCount = 5;

            if (Update != null)
                Update(new UpdateProgressEventArgs("Loading objects...", "Loading objects", FullFileName, currentCount++, max));

            List<object> objects = new List<object>();


            //===============================start new memory loading section===========================//


            Type[] types = PluginEngine.GetSerailPlugins();

            foreach (ZipEntry file in zipFile.Entries) {
                string[] xmlType = file.FileName.Split('/');
                if (xmlType.Count() > 1) {
                    if (Update != null)
                        Update(new UpdateProgressEventArgs(String.Format("Loading {0}", Path.GetFileName(file.FileName)), "Loading", Path.GetFileName(file.FileName), currentCount++, max));
                    Type[] T = types.Where(o => o.Name == xmlType[0]).ToArray<Type>();
                    if (T.Count() > 0) { //if their is no associated occ the item hast to go to the the unloadables list
                        Object RetreivedObject = null;

                        ZipEntryStream = new MemoryStream();
                        zipFile[file.FileName].Extract(ZipEntryStream);

                        ReadObject(out RetreivedObject, ZipEntryStream, T[0]);

                        if (RetreivedObject is ObjectClassBase) {
                            Add((ObjectClassBase)RetreivedObject);
                            }

                        } else {
                        UnlodableObjects.Add(file.FileName);
                        }
                    }
                }

            //should the file come equipped with a password lock the file from editing until a password is used to unlock it
            if (filePassword != String.Empty)
                LockFile();

            if (Updated != null)
                Updated();

            actions.ClearLog();

            DataChanged = false;
            Properties.Settings.Default.Loading = false;
            }

        public void Load3() {
            //throw new NotImplementedException();

            Properties.Settings.Default.Loading = true;

            if (Updating != null)
                Updating(new UpdaterEventArgs(5));

            //if the file goes in for decryption set this to true in the finally we will use it to test
            //if we should delete the temporary file
            Boolean
                FileNeededDecrypt = false;

            NTDataFile //Create a temporary space to de-serialize the file information to
                dataSet = null;

            ZipFile
                zipFile = null;// new ZipFile( FullFileName );

            String
                tmpFolder = System.IO.Path.GetTempPath() + "NewTerra";

            MemoryStream
                ZipStream = null,
                ZipEntryStream = new MemoryStream();

            //check if the container wrapper is encrypted
            if (Update != null)
                Update(new UpdateProgressEventArgs("Opening File...", "Opening", FullFileName, 2, 6));

            try {
                zipFile = new ZipFile(FullFileName);
                } catch (ZipException) {
                if (Update != null)
                    Update(new UpdateProgressEventArgs("File is protected...", "Opening", FullFileName, 3, 6));

                //run the file through the de-cryptor
                ZipStream = Security.StreamCrypt(new FileStream(FullFileName, FileMode.Open), Security.CryptAction.decrypt);
                ZipStream.Position = 0;
                zipFile = ZipFile.Read(ZipStream);
                }

            //if zip file is still null throw an unloadable error
            if (zipFile == null) {
                if (Updated != null)
                    Updated();
                throw new FileLoadException("Could not load file", FullFileName);
                }

            if (Update != null)
                Update(new UpdateProgressEventArgs("Unpacking File this may take a while...", "Inflating", FullFileName, 3, 6));

            try {
                Object[]
                    objArr;

                zipFile["Requirements.xml"].Extract(ZipEntryStream);

                ReadObject(out objArr, ZipEntryStream, typeof(SerializableVersion[]));

                String[]
                    msgList = new String[0];

                List<SerializableVersion>
                    versionCheck = new List<SerializableVersion>((SerializableVersion[])objArr);

                //check for required plugins if any are found missing or an incompatible version throw an error load message
                if (!checkForRequiredPlugins(versionCheck.ToArray(), out msgList)) {
                    String
                        bigMsg = "";
                    foreach (string str in msgList) {
                        bigMsg += str + "\n\n";
                        }
                    throw new FileLoadException(bigMsg, this.FileName);
                    }
                } catch { }

            Object
                tmpObj;

            ZipEntryStream = new MemoryStream();

            zipFile[Path.GetFileNameWithoutExtension(FileName) + ".xml"].Extract(ZipEntryStream);

            ReadObject(out tmpObj, ZipEntryStream, typeof(NTDataFile));

            if (tmpObj is NTDataFile)
                dataSet = (NTDataFile)tmpObj;

            if (Update != null)
                Update(new UpdateProgressEventArgs("File inflated finding objects...", "Hunting", FullFileName, 4, 6));

            if (dataSet != null) {
                filePassword = dataSet.filePassword;
                DataFileName = dataSet.DataFileName;
                IDPreFix = dataSet.IDPreFix;
                Author = dataSet.Author;
                AuthorEmail = dataSet.AuthorEmail;
                AuthorWebsite = dataSet.AuthorWebsite;
                Description = dataSet.Description;
                DateCreated = dataSet.DateCreated;
                DateLastEdited = dataSet.DateLastEdited;
                }
            List<String>
                UnlodableObjects = new List<String>();

            int
                max = 6 + zipFile.Entries.Count - 1,
                currentCount = 5;

            if (Update != null)
                Update(new UpdateProgressEventArgs("Loading objects...", "Loading objects", FullFileName, currentCount++, max));

            //List<object> objects = new List<object>();

            //===============================start new memory loading section===========================//

            Type[] types = PluginEngine.GetSerailPlugins();

            List<ZipEntry> filesToLoad = new List<ZipEntry>(zipFile.Entries);

            foreach (OCCBase occp in PluginEngine.GetOCCPlugInsByLayer()) {
                if (types.Contains(occp.CollectionType)) { //make sure the type were trying load cam be deserialized by checking against the approved deserialization list.
                    string typeName = occp.CollectionType.Name;
                    ZipEntry[] currentFiles = filesToLoad.Where(ze => ze.FileName.Split('/')[0] == typeName).ToArray();
                    foreach (ZipEntry file in currentFiles) {
                        ZipEntryStream = new MemoryStream();
                        Object RetreivedObject = null;

                        if (Update != null)
                            Update(new UpdateProgressEventArgs(String.Format("Loading {0}", Path.GetFileName(file.FileName)), "Loading", Path.GetFileName(file.FileName), currentCount++, max));

                        zipFile[file.FileName].Extract(ZipEntryStream);

                        ReadObject(out RetreivedObject, ZipEntryStream, occp.CollectionType);

                        if (RetreivedObject is ObjectClassBase) {
                            Add((ObjectClassBase)RetreivedObject);

                            ((ObjectClassBase)RetreivedObject).Link(this);
                            }

                        filesToLoad.Remove(file);
                        }
                    }
                }

            if (filesToLoad.Count > 0) {
                UnlodableObjects.AddRange(from file in filesToLoad select file.FileName);
                }

            //collectiontype.name

            //PluginEngine.MAX_OBJECT_LAYER

            //should the file come equipped with a password lock the file from editing until a password is used to unlock it
            if (filePassword != String.Empty)
                LockFile();

            if (Updated != null)
                Updated();

            actions.ClearLog();

            DataChanged = false;
            Properties.Settings.Default.Loading = false;
            }

        public void Load4() {
            //todo lite load load all xml at once do no linking may have to check for orphans, need a swith in object base class for fully loaded, if not link at that time
            //would be hard as we would have to search all files for references and update accordingly...
            throw new NotImplementedException("Feature not yet activated");
            }

        /// <summary>
        /// locks the file if a password has been set
        /// </summary>
        /// <exception cref="Exception">standard plain exception thrown</exception>
        public void LockFile() {
            if (filePassword == "")
                throw new NullPasswordException("No Password Set");

            fileLock = true;

            if (LockStatusChange != null)
                LockStatusChange();



            }

        public void PurgeFile() {

            List<ObjectClassBase> purgeList = new List<ObjectClassBase>();

            foreach (ObjectClassBase ObjRef in orphanCollector) {
                Boolean ObjRefHasRef = false;
                foreach (OCCBase occ in Collectors) {
                    if (ObjRefHasRef) break;
                    //todo query with linq
                    foreach (ObjectClassBase obj in occ)
                        if ((ObjRefHasRef = obj.CheckForReferences(ObjRef)))
                            break;
                    }
                if (!ObjRefHasRef) {
                    purgeList.Add(ObjRef);
                    }
                }

            foreach (ObjectClassBase Obj in purgeList)
                RemoveFromOrphanList(Obj);

            }

        //todo try and create a warning system when loading if a collection is missing non-required plug-ins like the tree node or at least one editor
        public SerializableVersion[] RequiredPlugins() {
            //get {
            List<SerializableVersion>
                retVal = new List<SerializableVersion>();

            List<Type>
                requiredTypes = new List<Type>();

            foreach (OCCBase occ in this.Collectors) {
                if (occ.Count >= 1) {
                    foreach (ObjectClassBase oc in PluginEngine.GetObjectClasses()) {
                        if (oc.CollectionType == occ.CollectionType) {
                            requiredTypes.Add(oc.GetType());
                            break;
                            }
                        }

                    requiredTypes.Add(occ.GetType());
                    }
                }

            foreach (Type typ in requiredTypes) {
                List<object>
                       attributes = new List<object>(typ.GetCustomAttributes(typeof(ObjectClassPlugIn), true));

                if (attributes.Count >= 1)
                    foreach (ObjectClassPlugIn ocpi in attributes)
                        retVal.Add(ocpi.version);

                attributes = new List<object>(typ.GetCustomAttributes(typeof(OCCPlugIn), true));

                if (attributes.Count >= 1)
                    foreach (OCCPlugIn occpi in attributes)
                        retVal.Add(occpi.version);
                }

            return retVal.ToArray();
            }

        public void Save() {
            //throw new NotImplementedException();
            try {
                if (FullFileName == "" | FullFileName == null) {
                    //deter to save as
                    SaveAs();
                    } else {
                    SaveFile();
                    }
                } catch { throw; }

            }

        public void SaveAs() {
            try {
                //todo: wtf calling a form in a class library?!?!
                //SaveFileDialog SFD = new SaveFileDialog();
                //SFD.Filter = "NewTerra Dat Files (*.ntx)|*.ntx|All files (*.*)|*.*";
                //SFD.SupportMultiDottedExtensions = true;
                //if (SFD.ShowDialog() == DialogResult.OK) {
                //    if (IDPreFix.ToUpper() == "NULL" || IDPreFix.ToUpper() == "") {
                //        String tmpPreFix = InputBox.Show("Please Enter a 4 digit alpha-numeric Identifier for this data set", "Data Pre-Fix");
                //        if (tmpPreFix != "") {
                //            foreach (ObjectClassBase ocb in AllData)
                //                ocb.ID = ocb.ID.Replace(IDPreFix, tmpPreFix);
                //            IDPreFix = tmpPreFix;
                //            }
                //        }
                //    this.FullFileName = SFD.FileName;
                //    SaveFile();
                //  }
                } catch { throw; }
            }

        public override string ToString() {
            return base.ToString();
            }

        /// <summary>
        /// will unlock this file if passed password matched stored password
        /// </summary>
        /// <param name="cleartypePassword">un-encrypted password string used to lock the file</param>
        public void UnLockFile(string cleartypePassword) {
            //todo
            //check that passwords match if not throw error otherwise unlock the file for editing
            if (CheckPassword(cleartypePassword)) {
                fileLock = false;
                if (LockStatusChange != null)
                    LockStatusChange();
                } else
                throw new InvalidPasswordException("Password incorrect");
            }

        /// <summary>
        /// Adds the orphaned object to the orphaned list if it doesn't exist
        /// </summary>
        /// <param name="orphanedObjRef">Object that was orphaned</param>
        /// <returns>the object that was passed in but returns it from the linked orphan list</returns>
        internal ObjectClassBase updateOrphaning(ObjectClassBase orphanedObjRef) {
            //find out if obj exists in orphaned list
            if (!orphanCollector.Exists(orphanedObjRef)) {
                //doesn't exist make sure it has no owner info
                if (orphanedObjRef is IOwner)
                    ((IOwner)orphanedObjRef).myOwner = null;
                //add to the list
                AddOrphan(orphanedObjRef);
                }

            //find and return object by its id
            return null;//NTData.FindObjectClassBaseObjectByID( orphanedObjects.ToArray(), orphanedObjRef.ID );
            }

        private bool CheckForReferences(ObjectClassBase Item) {
            byte objLayer = 0;

            //figure out the objects level
            OCCBase colector = Collectors.Where(c => c.CollectionType == Item.CollectionType).First();
            objLayer = colector.objectLayer;

            foreach (OCCBase occ in Collectors.Where(c => c.objectLayer > colector.objectLayer))
                foreach (ObjectClassBase obj in occ)
                    if (obj.CheckForReferences(Item))
                        return true;

            return false;
            }

        Boolean checkForRequiredPlugins(SerializableVersion[] requiredPlugins, out String[] MessageList) {
            List<SerializableVersion>
                loadedPlugs = new List<SerializableVersion>(LoadedPlugins);

            List<String>
                msgList = new List<string>();

            Boolean
                retval = true,
                currentTest = true;

            foreach (SerializableVersion reqPlugin in requiredPlugins) {
                currentTest = loadedPlugs.Contains(reqPlugin);
                if (!currentTest) {//check if its a version issue
                    foreach (SerializableVersion ldplug in loadedPlugs) {
                        if (ldplug.Name == reqPlugin.Name && ldplug.Type == reqPlugin.Type) {//the plugin is available
                            if (reqPlugin > ldplug) {//required version is newer than the installed version
                                currentTest = false;
                                msgList.Add(String.Format("Required plugin {0} is newer than installed version,\nFound version {1}\nRequired version {2}",
                                                            String.Format("{1}:{0}", reqPlugin.Name, reqPlugin.Type),
                                                            ldplug.Version(),
                                                            reqPlugin.Version()));
                                } else
                                currentTest = true;
                            }
                        }
                    if (!currentTest)//still not found
                        msgList.Add(String.Format("The plugin {0} could not be found and is required to load this file",
                                                    reqPlugin.ToString()));

                    if (!currentTest && retval)
                        retval = false;
                    }
                }
            MessageList = msgList.ToArray();
            return retval;
            }
        private void MoveToOrphanList(ObjectClassBase Item, OCCBase OCC) {
            //find the proper insertion point and put the now orphaned object in the list
            //if (orphanCollector.Count > 0) {
            //    for (int i = 0; i <= orphanCollector.Count - 1; i++) {
            //        if (Item.Name.CompareTo(orphanCollector[i].Name) < 0) {
            //            orphanCollector. Insert(i, Item); break;
            //        }
            //    }
            //    if (!orphanCollector.Exists(Item))
            //        orphanCollector.AddObject(Item);
            //}
            //else {
            orphanCollector.AddObject(Item);
            //}

            if (OCC != null)
                OCC.DropObject(Item);

            if (EventOrphansChanged != null)
                EventOrphansChanged(new ItemChangedArgs(orphanCollector.FindIndex(Item), Item, ArgAction.Add));
            }

        void NTDataFile_EventMyDataChanged() {
            //todo throw new NotImplementedException();
            }

        private bool ReadObject(out Object objectToRead, string path, Type T) {
            FileStream  //Create a file stream object
                FS = new FileStream(path, FileMode.Open);

            XmlSerializer //Create the object that will decode the XML File
                SER = new XmlSerializer(T);

            XmlReader
                XMLR = new XmlTextReader(FS);

            try {
                objectToRead = SER.Deserialize(XMLR);
                } catch (Exception) {
                objectToRead = null;
                return false;
                } finally {
                FS.Close();

                }
            return true;
            }

        private bool ReadObject(out Object objectToRead, Stream stream, Type T) {
            //FileStream  //Create a file stream object
            //    FS = new FileStream( path, FileMode.Open );

            stream.Position = 0;

            XmlSerializer //Create the object that will decode the XML File
                SER = new XmlSerializer(T);

            XmlReader
                XMLR = new XmlTextReader(stream);

            try {
                objectToRead = SER.Deserialize(XMLR);
                } catch (Exception) {
                objectToRead = null;
                return false;
                }
            return true;
            }

        private bool ReadObject(out Object[] objectToRead, Stream stream, Type T) {
            //FileStream  //Create a file stream object
            //    FS = new FileStream( path, FileMode.Open );

            stream.Position = 0;

            XmlSerializer //Create the object that will decode the XML File
                SER = new XmlSerializer(T);

            XmlReader
                XMLR = new XmlTextReader(stream);

            try {
                objectToRead = (Object[])SER.Deserialize(XMLR);
                } catch (Exception) {
                objectToRead = null;
                return false;
                }

            return true;
            }

        private bool ReadObject(out Object[] objectToRead, string path, Type T) {
            FileStream  //Create a file stream object
                FS = new FileStream(path, FileMode.Open);

            XmlSerializer //Create the object that will decode the XML File
                SER = new XmlSerializer(T);

            XmlReader
                XMLR = new XmlTextReader(FS);

            try {
                objectToRead = (Object[])SER.Deserialize(XMLR);
                } catch (Exception) {
                objectToRead = null;
                return false;
                } finally {
                FS.Close();

                }
            return true;
            }

        private bool ReadObject2(out Object[] objectToRead, string path, Type T) {
            FileStream  //Create a file stream object
                FS = new FileStream(path, FileMode.Open);

            Type[] loadedTypes = PluginEngine.GetLoadedPluginTypes();

            XmlSerializer //Create the object that will decode the XML File
                SER = new XmlSerializer(T, loadedTypes);

            XmlReader
                XMLR = new XmlTextReader(FS);

            try {
                objectToRead = (Object[])SER.Deserialize(XMLR);
                } catch (Exception) {
                objectToRead = null;
                return false;
                } finally {
                FS.Close();

                }
            return true;
            }

        private void RemoveFromOrphanList(ObjectClassBase Item) {
            int index = orphanCollector.FindIndex(Item);

            orphanCollector.DropObject(Item);

            if (EventOrphansChanged != null)
                EventOrphansChanged(new ItemChangedArgs(index, Item, ArgAction.Remove));
            }

        private void ReplaceReferences(ObjectClassBase toEdit, ObjectClassBase NewValues) {
            byte objLayer = 0;

            //figure out the objects level
            foreach (OCCBase colector in Collectors)
                if (colector.CollectionType == toEdit.CollectionType)
                    objLayer = colector.objectLayer;

            for (int i = objLayer + 1; i <= PluginEngine.MAX_OBJECT_LAYER; ++i)
                foreach (OCCBase occ in Collectors)
                    if (occ.objectLayer == i)
                        foreach (ObjectClassBase obj in occ)
                            obj.ReplaceReferences(toEdit, NewValues);
            }
        private void SaveFile() {
            String
                tmpFolder = System.IO.Path.GetTempPath() + "NewTerra";

            List<String>
                subFiles = new List<string>();

            if (!Directory.Exists(tmpFolder))
                Directory.CreateDirectory(tmpFolder);
            else
                if (Directory.GetDirectories(tmpFolder).Length != 0 || Directory.GetFiles(tmpFolder).Length != 0) {
                Directory.Delete(tmpFolder, true);
                Directory.CreateDirectory(tmpFolder);
                }

            try {
                subFiles.Add(WriteObject(this, tmpFolder + "\\" + FileName.Split('.')[0]));

                subFiles.Add(WriteObject(RequiredPlugins(), tmpFolder + "\\Requirements"));

                foreach (Object obj in this.AllData) {
                    try {
                        if (!(obj is ObjectClassBase))
                            throw new Exception(); //todo make this better

                        ObjectClassBase
                            IObj = (ObjectClassBase)obj;

                        String
                            tmpFile = tmpFolder + "\\" + IObj.CollectionName + "\\" + IObj.Name;

                        subFiles.Add(WriteObject(IObj, tmpFile) + "!" + IObj.CollectionName);

                        } catch (Exception ex) { }

                    }

                try {
                    //todo instead of this append the old file with a time stamp at the end for versioning call back

                    if (File.Exists(FullFileName)) {

                        File.Move(FullFileName, FilePath + Path.GetFileNameWithoutExtension(FileName) + DateTime.Now.ToString(".yyyyMMddHHMMss") + Path.GetExtension(FileName));
                        File.Delete(FullFileName);

                        }
                    ZipFile zipFile = new ZipFile(FullFileName);

                    zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                    foreach (String file in subFiles) {
                        try {
                            String[]
                                splitString = file.Split('!');

                            if (splitString.Length <= 0) throw new Exception(String.Format("Object {0} could not be saved", file));

                            if (splitString.Length == 1)
                                zipFile.AddFile(file, "");
                            else
                                zipFile.AddFile(splitString[0], splitString[1]);

                            } catch (Exception ExInternal) {
                            if (ExInternal.Message.Contains("already exists")) {
                                //todo: again... with the dialog wtf?!?!
                                //MessageBox.Show(Path.GetFileNameWithoutExtension(file) +
                                //    " is sharing a duplicate name, please correct this error and re-save the file");
                                } else
                                throw;
                            }
                        }
                    zipFile.Save();
                    } catch (Exception Ex) {

                    throw;
                    }

                if (filePassword.Trim() != String.Empty) {
                    Security.cryptFile(FullFileName, Security.CryptAction.encrypt);
                    File.Delete(FullFileName);
                    File.Move(FullFileName + "~", FullFileName);
                    }
                } catch (Exception ex) {
                //File.Copy( FilePath + "~" + FileName, FullFileName, true );
                throw new NTFileExecption(this.FileName, string.Format("File {0} could not be saved", this.FileName), ex);
                } finally {
                //FS.Close();
                //File.Delete( FilePath + "~" + FileName );
                }

            this.DataChanged = false;
            }
        private String WriteObject(Object objToWrite, string path) {
            //throw new NotImplementedException();
            path += ".xml";
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            FileStream
                FS = new FileStream(path, FileMode.Create);

            try {
                XmlSerializer
                    SER = new XmlSerializer(objToWrite.GetType());
                //new part
                XmlTextWriter xmlTextWriter = new XmlTextWriterFormattedNoDeclaration(FS); // new XmlTextWriter( memoryStream, Encoding.UTF8 );

                XmlSerializerNamespaces NameSpace = new XmlSerializerNamespaces();

                NameSpace.Add("", "");

                SER.Serialize(xmlTextWriter, objToWrite, NameSpace);

                return path;
                } catch { throw; } finally {
                FS.Close();
                }
            }

        private String WriteObject2(Object objToWrite, string path) {
            //throw new NotImplementedException();
            path += ".xml";
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            FileStream
                FS = new FileStream(path, FileMode.Create);

            Type[] loadedTypes = PluginEngine.GetLoadedPluginTypes();

            try {
                XmlSerializer
                    SER = new XmlSerializer(objToWrite.GetType(), loadedTypes);
                //new part
                XmlTextWriter xmlTextWriter = new XmlTextWriterFormattedNoDeclaration(FS); // new XmlTextWriter( memoryStream, Encoding.UTF8 );

                XmlSerializerNamespaces NameSpace = new XmlSerializerNamespaces();

                NameSpace.Add("", "");

                SER.Serialize(xmlTextWriter, objToWrite, NameSpace);

                return path;
                } catch { throw; } finally {
                FS.Close();
                }
            }

        #endregion Methods

        //private OrphanNode[] PopulateNodeOrphans(int InCount, out int OutCount) {
        //    List<OrphanNode>
        //        retVal = new List<OrphanNode>();

        //    foreach (ObjectClassBase obj in i_Orphans) {
        //        //NTTreeNode
        //        //        newNode = new NTTreeNode();

        //        OrphanNode
        //            newNode = new OrphanNode();

        //        newNode.NodeFont = SystemFonts.DefaultFont;

        //        //todo add image for object here

        //        //if it knows what menus to assign do it here
        //        if (i_OrphansMenu != null)
        //            //if ( obj is INTName ) 
        //            newNode.ContextMenuStrip = i_OrphansMenu;

        //        //add the object to the node for back checking later
        //        newNode.ObjectClass = obj;

        //        //finally add the sub node to the main node
        //        retVal.Add(newNode);

        //        //need to ignore an update at this time maybe can work it back in later
        //        //if (Update != null)
        //        //    Update(new UpdateProgressEventArgs("Updating Orphaned Leaves", "Updated",
        //        //        newNode.Text, InCount++, i_Orphans.Count));
        //    }
        //    OutCount = InCount;
        //    return retVal.ToArray();
        //}

        //public void getDisplayData(ListViewGroupCollection groupCollection) { }
        /* some code in this section was found on the internet and modified to suit
         * http://simpcode.blogspot.com/2008/07/c-get-unique-key-using-guid-hash-code.html
         * original creator Weizh at Saturday, July 12, 2008 
         */
        }

    [Serializable()]
    public class NTDataFileExt {

        #region Constructors

        public NTDataFileExt() { }

        #endregion Constructors

        #region Properties

        [XmlElement(), Browsable(false)]
        public OCCBase[] Collectors {
            get { return InternalFile.Collectors; }
            set { }
            }

        [XmlIgnore()]
        public NTDataFile InternalFile { get; set; }

        [XmlElement()]
        public string Author { get; set; }

        [XmlElement()]
        public string AuthorEmail { get; set; }

        [XmlElement()]
        public string AuthorWebsite { get; set; }

        [XmlElement()]
        public string Description { get; set; }
        
        
        #endregion Properties
        }
    }

