using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using NTAF.Core;
using System.Windows.Forms;

namespace NTAF.Core {
    #region IAboutMe
    public interface IAboutMe {
        [XmlIgnore()]
        string aboutMe {
            get;
        }
    }
    //IAboutMe extender
    public static class ExIAboutMe {
        public static string getIAboutMe( object sender ) {
            try {
                string retVal="";
                IAboutMe AMe = null;

                if ( sender is IAboutMe )
                    AMe = ( IAboutMe )sender;

                if ( AMe != null )
                    retVal = AMe.aboutMe;
                else
                    retVal = "Details not avalable...";

                return retVal;
            }
            catch {
                return "Details not avalable...";
            }
        }
    }
    #endregion

    #region INTId
    public interface INTId {
        string ID { get; set; }
        string IDPreFix { get; }
    }
    #endregion

    #region ICopyClip
    public interface ICopyClip {
        String CopyClip { get; set; }
    }
    #endregion

    #region INTName
    public interface INTName {
        event NTEventHandler<NameChangeArgs> EventNameChanged;
        string Name { get; set; }
    }
    #endregion

    #region IValidate
    public interface IValidate {
        void Valid();
    }
    #endregion

    #region IUpdateProgress
    public interface IUpdateProgress {
        /// <summary>
        /// Suscribe to this to notify when updates start
        /// </summary>
        event NTEventHandler<UpdaterEventArgs>  Updating;

        /// <summary>
        /// Suscribe to this to get an update when something changes durring the updating process
        /// </summary>
        event NTEventHandler<UpdateProgressEventArgs>  Update;

        /// <summary>
        /// This will notify that the update progress is finished
        /// </summary>
        event NTEventHandler  Updated;
    }
    #endregion

    #region ICost
    public interface ICost {
        int Cost { get; }
    }
    #endregion

    #region IExists
    public interface IExists {
        bool Exists( object obj );
    }
    #endregion

    #region IOwner
    public interface IOwner {
        object myOwner { get; set; }
    }
    #endregion

    #region ILockable
    public interface ILockable {
        event NTEventHandler LockStatusChange;

        bool FileLocked { get; }

        string FilePassword { set; }//get;

        void LockFile();

        void UnLockFile( string cleartypePassword );

        bool CheckPassword( string cleartypePassword );
    }
    #endregion

    #region ITrackChange
    public interface ITrackChange {

        event NTEventHandler EventDataStateChanged;

        bool DataChanged { get; set; }

    }
    #endregion

    #region IMemberCopy
    /// <summary>
    /// provides a common interface to do a deep cloning operation on applicable objects.
    /// </summary>
    public interface IMemberCopy {
        /// <summary>
        /// A method to do a true deep copy on an object so that when a field changes on one object it
        /// doesn't change on both or all objects. Note this method should NOT copy avalable event subscribers.
        /// </summary>
        /// <param name="members">The original object to copy members from</param>
        void CopyMembers( object members );

    }
    #endregion 
}
