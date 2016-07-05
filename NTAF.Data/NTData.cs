using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using NTAF.Core.Properties;
using NTAF.Core;
using NTAF.Core;
using NTAF.PlugInFramework;
using System.Windows.Forms;
using System.Drawing;

namespace NTAF.Core {
    public class NTData: IUpdateProgress, ILink {

        #region fields
        private List<NTDataFile> 
            _DataFiles = new List<NTDataFile>();

        private List<ObjectClassBase>
            i_Orphans = new List<ObjectClassBase>( );
        
        private wrkngFle 
            myWorkingFile = new wrkngFle();

        List<TreeNode>
            OrphanLeaves = new List<TreeNode>( );
        
        #endregion

        #region events
        public event NTEventHandler EventDataChanged;
        public event NTEventHandler EventWorkingFileChanged;
        public event NTEventHandler EventFileAdded;
        //public event NTEventHandler OrphansUpdated;
        #endregion

        #region properties
        public TreeNode[] OrphanedLeaves {
            get { return OrphanLeaves.ToArray(); }
            set {
                OrphanLeaves.Clear();
                OrphanLeaves.AddRange( value );

                //if ( OrphansUpdated != null )
                //    OrphansUpdated();
            }
        }
        public NTDataFile[] LoadedData { get { return _DataFiles.ToArray(); } }

        public int Count { get { return _DataFiles.Count(); } }

        public string[] LoadedFiles {
            get {
                List<string> retVal = new List<string>();
                foreach ( NTDataFile NTDF in _DataFiles )
                    retVal.Add( NTDF.FileName );
                return retVal.ToArray();
            }
        }

        public wrkngFle WorkingFile { get { return myWorkingFile; } }

        public INTId[] LoadedObjects {
            get {
                List<INTId>
                    retVal = new List<INTId>();

                foreach ( NTDataFile NTDF in this.LoadedData )
                    retVal.AddRange( NTDF.AllData );

                return retVal.ToArray();
            }
        }

        public OCCBase[] LoadedCollectors() {
            List<OCCBase>
               occs = new List<OCCBase>( );

            foreach ( NTDataFile ntdf in LoadedData ) {
                occs.AddRange( ntdf.Collectors );
            }

            return occs.ToArray( );
        }

        #endregion

        #region methods

        public static INTId FindINTIdObjectByID( INTId[] objs, String ID ) {
            INTId
                retVal = null;

            foreach ( INTId obj in objs )
                if ( obj.ID == ID ) {
                    retVal = obj;
                    break;
                }

            return retVal;
        }

        public void SetWorkingFile( string FileName ) {
            if ( myWorkingFile.CurrentFile != null )
                if ( FileName == myWorkingFile.CurrentFile.FileName ) return;
            foreach ( NTDataFile NTDF in LoadedData )
                if ( NTDF.FileName == FileName ) {
                    if ( myWorkingFile.CurrentFile != null )
                        myWorkingFile.CurrentFile.EventDataStateChanged -= CurrentFile_EventDataStateChanged;
                    myWorkingFile.CurrentFile = NTDF;
                    myWorkingFile.CurrentFile.EventDataStateChanged += new NTEventHandler( CurrentFile_EventDataStateChanged );
                    break;
                }
            if ( EventWorkingFileChanged != null )
                EventWorkingFileChanged();
        }

        void CurrentFile_EventDataStateChanged() {
            if ( EventWorkingFileChanged != null )
                EventWorkingFileChanged();
        }

        public void DataFile_EventDataChanged() {
            if ( EventDataChanged != null )
                EventDataChanged();
        }

        //todo update how this method verifies the object to be dropped
        public void dropDeffinition( object objToDrop ) {
            if ( objToDrop is IOwner )
                if ( ( ( IOwner )objToDrop ).myOwner is NTDataFile ) {
                    if ( objToDrop is INTId ) ( ( NTDataFile )( ( IOwner )objToDrop ).myOwner ).Drop( ( ObjectClassBase )objToDrop );
                }
        }

        public void LinkData() {
            try {
                i_Orphans.Clear();

                List<OCCBase>
                    occs = new List<OCCBase>( LoadedCollectors( ) );

                for ( Byte i = 1; i <= PluginEngine.MAX_OBJECT_LAYER; i++ ) {
                    foreach ( OCCBase occ in occs ) {
                        if ( occ.objectLayer == i ) {//to do watch this once higher level IObject class plug-ins are created
                            foreach ( ObjectClassBase obj in occ.Objects ) {
                                obj.Link( this );
                            }
                        }
                    }
                }

                i_Orphans.Sort();

                List<TreeNode>
                    OrphanLeaves = new List<TreeNode>();

                foreach ( Object obj in i_Orphans ) {
                    NTTreeNode
                        newNode = new NTTreeNode( obj );

                    newNode.NodeFont = SystemFonts.DefaultFont; // default( Font );// new Font( "Tahoma", 8.25F );

                    //this is part of the orphaning
                    if ( obj is IOwner )
                        if ( ( ( IOwner )obj ).myOwner == null ) {//is an orphan
                            newNode.ForeColor = System.Drawing.Color.Red;
                            newNode.NodeFont = new Font( newNode.NodeFont, FontStyle.Bold );
                        }
                    OrphanLeaves.Add( newNode );
                }

                //return OrphanLeaves.ToArray();
            }
            catch { throw; }
        }

        public Object FindObject( ObjectClassBase obj ) {
            Object
                retVal = null;

            //null check
            if ( obj == null )
                return retVal;

            foreach ( OCCBase occ in LoadedCollectors( ) ) {
                try {
                    retVal = occ[obj];
                    if ( retVal != null )
                        break;
                }
                catch ( InvalidParameter ) { }
                catch ( Exception ex ) {
                    throw new Exception(
                        String.Format(
                            "An error occurred while linking objects" + Environment.NewLine +
                            ( obj is IOwner ? 
                                "The object {0} in file {1} caused the error" :
                                "The object {0} caused the error in an {1} file" ),
                            obj.Name,
                            ( obj is IOwner ? ( ( IOwner )obj ).myOwner : "untraceable" )
                            ),
                        ex );
                }
            }
            //object could not be found
            if ( retVal == null ) {
                //search the orphan list make sure its not being added twice
                if ( !i_Orphans.Contains( obj ) ) {
                    //doesn't exist add it
                    i_Orphans.Add( obj );
                }

                //retrieve the orphan from the orphan list
                retVal = i_Orphans[i_Orphans.IndexOf( obj )];
            }

            return retVal;
        }

        /// <summary>
        /// Adds the orphaned object to the orphaned list if it doesn't exist
        /// </summary>
        /// <param name="orphanedObjRef">Object that was orphaned</param>
        /// <param name="orphaned">A reset bool return flips current true => false or false => true</param>
        /// <returns>the object that was passed in but returns it from the linked orphan list</returns>
        private INTId updateOrphaning( INTId orphanedObjRef, ref NTDataFile OwnerInformation ) {
            ////find out if obj exists in orphaned list
            //if ( !orphanedObjects.Contains( orphanedObjRef ) ) {
            //    //doesn't exist make sure it has no owner info
            //    if ( orphanedObjRef is IOwner )
            //        ( ( IOwner )orphanedObjRef ).myOwner = null;
            //    //add to the list
            //    OwnerInformation.AddOrphan( orphanedObjRef );
            //}

            ////find and return object by its id
            //return FindINTIdObjectByID( orphanedObjects.ToArray(), orphanedObjRef.ID );
            return null;
        }

        #region file operations
        public Collection<string> Load( FileInfo file ) {

            List<FileInfo> loadfile = new List<FileInfo>();
            loadfile.Add( file );
            Collection<string> retVal = Load( loadfile.ToArray() );

            return retVal;
        }

        public Collection<string> Load( FileInfo[] files ) {

            Collection<string> unloadables = new Collection<string>();
            NTDataFile loadingFile = null;
            //lock for threading
            lock ( this ) {
                foreach ( FileInfo file in files ) {
                    if ( !( LoadedFiles.ToList().Contains( file.Name ) ) ) {
                        try {
                            //todo create unloadable file exception holds file name or path and reason cant be loaded
                            loadingFile = new NTDataFile( file.FullName );

                            loadingFile.Updating += new NTEventHandler<UpdaterEventArgs>( updating );
                            loadingFile.Update += new NTEventHandler<UpdateProgressEventArgs>( update );
                            loadingFile.Updated += new NTEventHandler( updated );

                            //load the data from the file
                            loadingFile.Load();

                            //add the file to the data pool
                            this._DataFiles.Add( loadingFile );

                            ////not needed? data it self didn't change
                            //if ( EventDataChanged != null )
                            //    EventDataChanged();
                        }
                        //if a file is unloadable add it to the list file names that get returned unable to load
                        catch ( Exception ex ) {
                            unloadables.Add( file.FullName + "\ncould not be loaded for the following reason(s)\n" + ex.Message );
                        }
                    }
                    //file cannot be loaded twice list it as unloadable
                    else { unloadables.Add( file.FullName + "\nIs already loaded and cannot be loaded more than once." ); }
                }
                ////link all the data across all open files and retrieve the orphaned objects
                //OrphanedLeaves = LinkData();
            }//end locking

            //notify the host program that data files have been loaded or added to the mix
            if ( EventFileAdded != null )
                EventFileAdded();

            //return the list of any files that could not be loaded
            return unloadables;
        }

        public void NewFile( string path, string iDpreFix, string DataSetName ) {
            NTDataFile newFile = new NTDataFile( path, iDpreFix, DataSetName );
            newFile.DataChanged = true;

            newFile.EventDataStateChanged += new NTEventHandler( newFile_EventDataStateChanged );
            newFile.Updating+=new NTEventHandler<UpdaterEventArgs>(updating);
            newFile.Update+=new NTEventHandler<UpdateProgressEventArgs>(update);
            newFile.Updated+=new NTEventHandler(updated);

            _DataFiles.Add( newFile );

            if(EventFileAdded !=null)
                EventFileAdded();

            newFile.Save();

        }

        void newFile_EventDataStateChanged() {
            //may need to setup some fashion of sectional Linking here
        }

        public List<string> SaveAll() {
            List<string> unsaveableFiles = new List<string>();

            foreach ( NTDataFile NTDF in _DataFiles )
                try {
                    NTDF.Save();
                }
                catch ( NTFileExecption SFEX ) { unsaveableFiles.Add( SFEX.FileName ); }
                catch ( Exception ex ) { throw ex; }

            return unsaveableFiles;
        }

        public void CloseFile( NTDataFile file ) {
            _DataFiles.Remove( file );

            if ( EventDataChanged != null )
                EventDataChanged();
        }

        #endregion

        #endregion

        #region constructos
        public NTData() {
            //create a new empty datafile
           // _DataFiles.Add( new NTDataFile() );
        }

        public NTData( string FileToLoad ) {
            FileInfo fileToLoad = new FileInfo( FileToLoad );

            List<string> unloadables = new List<string>( Load( fileToLoad ) );
            if ( unloadables.Count != 0 ) {
                string message = "";
                foreach ( string str in unloadables )
                    message += String.Format( "The file {0} could not be loaded\n", str );
                throw new Exception( message );
            }
            
            //if ( !Directory.Exists( preLoadDir ) )
            //    Directory.CreateDirectory( preLoadDir );

            //DirectoryInfo dirInf = new DirectoryInfo( preLoadDir );
            //List<FileInfo> fls = new List<FileInfo>();
            //fls.AddRange( dirInf.GetFiles( "*.NTX" ).ToList() );
            //fls.AddRange( dirInf.GetFiles( "*.NTD" ).ToList() );
            //List<string> unloadables = Load( fls.ToArray() ).ToList();

            //if ( unloadables.Count != 0 ) {
            //    string message = "";
            //    foreach ( string str in unloadables )
            //        message += String.Format( "The file {0} could not be loaded\n", str );
            //    throw new Exception( message );
            //}
        }
        #endregion

        #region Nested Classes
        public class wrkngFle {
            private NTDataFile file;

            public NTDataFile CurrentFile {
                get { return file; }
                set { file = value; }
            }

            public bool DataChanged {
                get { return file.DataChanged; }
                //set { file.DataChanged = value; }
            }

            public string IDPreFix {
                get { return file.IDPreFix; }
            }

            public string FileName {
                get { return file.FileName; }
            }

            public string Path {
                get { return file.FullFileName; }
            }
        }
        #endregion

        #region IUpdateProgress Members

        public event NTEventHandler<UpdaterEventArgs>  Updating;

        public event NTEventHandler<UpdateProgressEventArgs>  Update;

        public event NTEventHandler  Updated;

        private void updating( UpdaterEventArgs args ) {
            if ( Updating != null )
                Updating( args );
        }
        private void update( UpdateProgressEventArgs args ) {
            if ( Update != null )
                Update( args );
        }
        private void updated( ) {
            if ( Updated != null )
                Updated();
        }

        #endregion
    }
}