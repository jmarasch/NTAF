using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Serialization;
using NTAF.Core;
using NTAF.PrintEngine;

namespace NTAF.PlugInFramework {
    /// <summary>
    /// The lowest level complex type for NewTerra applications
    /// Interfaces:
    /// IAboutMe, ICloneable, IComparable, IMemberCopy
    /// INTId, INTName, IOwner, IPrintable, 
    /// IValidate, 
    /// </summary>
    public class ObjectClassBase : INTId, INTName,
        IOwner, ICloneable, IComparable, IAboutMe,
        IValidate, IPrintable, IMemberCopy{

        /// <summary>
        /// Occurs when data has changed
        /// </summary>
        public virtual event NTEventHandler MyDataChanged;

        //#region fields
        private String 
            _ID, 
            _Name;

        /// <summary>
        /// Gets or sets the objects Unique ID. All ids need to consist of a 4 alpha numeric prefix
        /// controlled by its file data prefix id and typically an 8 Hex ID the prefix/id
        /// is used to make sure that even if two items should ever share the same id in different files
        /// they will never be cross confused due to the prefix.
        /// <example>
        /// BSDS54D457A6
        /// BSDS is the id prefix and 54D457A6 is the id inside the file
        /// </example>
        /// </summary>
        [XmlAttribute( ), Category( TagCategory.ID ),
        Description( TagDef.ID )]
        public string ID {
            get { return _ID; }
            set { _ID = value; }
        }

        /// <summary>
        /// Gets the objects prefix identifier, see ID for more information
        /// </summary>
        [XmlIgnore( ), Category( TagCategory.ID ),
        Description( TagDef.IDPreFix )]
        public string IDPreFix {
            get { return _ID.Substring( 0, 4 ); }
        }

        /// <summary>
        /// Fires when a name change occurs
        /// </summary>
        public event NTAF.Core.NTEventHandler<NTAF.Core.NameChangeArgs> EventNameChanged;

        /// <summary>
        /// Gets or sets the name of the object
        /// </summary>
        [XmlAttribute( ), Category( TagCategory.ID ),
        Description( TagDef.Name )]
        public string Name {
            get { return _Name; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                String
                    oldName = _Name;

                _Name = value;
                if ( !NTAF.Core.Properties.Settings.Default.Loading &&
                    !NTAF.Core.Properties.Settings.Default.Updating ) {
                    if ( EventNameChanged != null )
                        EventNameChanged( new NameChangeArgs( _Name, oldName, _ID ) );
                    if ( MyDataChanged != null )
                        MyDataChanged( );
                }
            }
        }

        private Object owner = null;

        /// <summary>
        /// The data file that this object belongs to
        /// </summary>
        [XmlIgnore( ), Browsable( false )]
        public object myOwner {
            get { return owner; }
            set { owner = value; }
        }

        /// <summary>
        /// creates a shallow clone of this object
        /// </summary>
        /// <returns>An object identical to this one</returns>
        public Object Clone() {
            object retVal = this.MemberwiseClone( );
            ( ( ObjectClassBase )retVal ).clearMyEvents( );
            return retVal;
        }

        //todo need to find a way to make this work
        /// <summary>
        /// Clears all the basic events this object could have, you must override
        /// is basic operation if you plan on adding custom events, when overriding
        /// this method you should call base.clearMyEvents() prior to any event clearing
        /// your method will do.
        /// </summary>
        protected virtual void clearMyEvents() {
            MyDataChanged = null;
            EventNameChanged = null;
        }

        int IComparable.CompareTo( object obj ) {
            ObjectClassBase chk = ( ObjectClassBase )obj;

            //return string compare
            //asc
            return String.Compare( this.Name, chk.Name );

        }

        /// <summary>
        /// Gets a string of root basic details about the object.
        /// Override this method and first call base.aboutMe and then
        /// add your extra field information. This method is important
        /// in that if print is unavailable, this will typically get called
        /// in its place.
        /// </summary>
        [XmlIgnore( ), Browsable( false )]
        public virtual string aboutMe {
            get {

                String retVal = String.Empty;
                //if ( NTAF.DataCore.Properties.Settings.Default.VerboseToString )
                if ( this.ID != "" )
                    retVal += "ID: " + this.ID + "\n";
                if ( this.Name != "" )
                    retVal += "Name: " + this.Name + "\n";

                return retVal;
            }
        }

        /// <summary>
        /// Override this method call the base version then simply finish validating the
        /// item by checking for null referenced fields, when a field/property
        /// thats required for the object to work properly is found blank/empty/null
        /// or what ever state that would not be good or the object do a
        /// throw new ValidationExecption("this is whats wrong");
        /// </summary>
        /// <exception cref="ValidationException">Sends back information on whats
        /// wrong with the object</exception>
        public virtual void Valid() {

            List<FieldAndMessage>
                ThrowList = new List<FieldAndMessage>( );

            if ( _ID == "" ) ThrowList.Add( new FieldAndMessage( "ID", "Cannot be blank" ) );
            if ( _Name == "" ) ThrowList.Add( new FieldAndMessage( "Name", "Cannot be blank" ) );

            if ( ThrowList.Count >= 1 )
                throw new ValidationException( ThrowList.ToArray() );
        }

        /// <summary>
        /// Needs to be overridden if you want the object to print anything
        /// see NewTerra Print Framework documentation for details
        /// </summary>
        /// <param name="element"></param>
        public virtual void Print( PrintElement element ) { }

        /// <summary>
        /// copies all read/write fields from one object to this one
        /// </summary>
        /// <param name="members">The object thats having its properties copied</param>
        public void CopyMembers( Object members ) {
            List<PropertyInfo>
                MyProps = new List<PropertyInfo>( this.GetType( ).GetProperties( ) ),
                ObjProps = new List<PropertyInfo>( members.GetType( ).GetProperties( ) );

            foreach ( PropertyInfo PI in ObjProps ) {
                if ( MyProps.Contains( PI ) && ( PI.CanRead && PI.CanWrite ) )
                    MyProps[MyProps.IndexOf( PI )].SetValue( this, PI.GetValue( members, null ), null );
            }

            List<FieldInfo>
                MyFields = new List<FieldInfo>( this.GetType().GetFields() ),
                ObjFields = new List<FieldInfo>( members.GetType().GetFields() );

            foreach ( FieldInfo FI in ObjFields ) {
                if ( MyFields.Contains( FI ) && FI.IsPublic && !FI.IsStatic )
                    try {
                        MyFields[MyFields.IndexOf( FI )].SetValue( this, FI.GetValue( members ) );
                    }
                    catch ( ReadOnlyException ) {
                        
                    }
            }
            clearMyEvents( );
        }

        /// <summary>
        /// Must be overridden if the object level is 1 or higher for the ClassCollector
        /// this instructs the object on how to find complex reference objects and create
        /// a link to them
        /// </summary>
        /// <param name="DataMaster"></param>
        public virtual void Link( ILink DataMaster ) { }

        /// <summary>
        /// Must be overridden if the object level is 1 or higher for the ClassCollector
        /// this instructs the object on how to find complex reference objects and create
        /// a link to them
        /// </summary>
        /// <param name="ObjectToReplace"></param>
        /// <param name="ReplaceWith"></param>
        public virtual void ReplaceReferences( ObjectClassBase ObjectToReplace, ObjectClassBase ReplaceWith ) { }

        /// <summary>
        /// Must be overridden if the object level is 1 or higher for the ClassCollector,
        /// Checks to find out if a reference to the passed object exists in this object
        /// </summary>
        /// <param name="Item">Object were looking for references for</param>
        /// <returns>True if a reference exists, False if no reference is found</returns>
        public virtual bool CheckForReferences( ObjectClassBase Item ) { return false; }

        /// <summary>
        /// Gets the name of the class based on the ObjectClassPlugIn attribute
        /// </summary>
        public string CollectionName {
            get {
                String
                    retval = "";

                object[]
                    myAtts = this.GetType( ).GetCustomAttributes( typeof( ObjectClassPlugIn ), true );

                if ( myAtts.Length >= 1 )
                    retval = ( ( ObjectClassPlugIn )myAtts[0] ).Name;

                return retval;
            }
        }

        /// <summary>
        /// Gets the type of this object
        /// </summary>
        /// <returns>This objects type</returns>
        public virtual Type CollectionType {
            get { return this.GetType(); }
        }
        
        /// <summary>
        /// checks to see if this object is of testing type
        /// </summary>
        /// <param name="obj">Object to test</param>
        /// <returns>true if they match false if the don't match</returns>
        public virtual bool IsOfType( object obj ) {
            throw new NotImplementedException( );
        }

        /// <summary>
        /// checks to see if this object is of testing type
        /// </summary>
        /// <param name="T">Type to test</param>
        /// <returns>true if they match false if the don't match</returns>
        public virtual bool IsOfType( Type T ) {
            throw new NotImplementedException( );
        }

        /// <summary>
        /// Gets the type of this object
        /// </summary>
        /// <returns>This objects type</returns>
        public virtual Type MyType() { return this.GetType(); }

        /// <summary>
        /// not implemented not used, may be used in the future
        /// </summary>
        /// <param name="toSerial"></param>
        /// <param name="tempPath"></param>
        /// <returns></returns>
        public virtual string Serialize( object toSerial, string tempPath ) {
            throw new NotImplementedException( );
        }
        
        /// <summary>
        /// not implemented not used, may be used in the future
        /// </summary>
        /// <param name="PathOfSavedObject"></param>
        /// <returns></returns>
        public virtual object Deserialize( string PathOfSavedObject ) {
            throw new NotImplementedException( );
        }

        public virtual DataMember[] getDataMembers() {
            throw new NotImplementedException();
        }
    }
}


