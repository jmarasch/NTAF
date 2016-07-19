using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using NTAF.Core;
using NTAF.Core.Properties;
using NTAF.PlugInFramework;
using NTAF.PrintEngine;

namespace NTAF.ObjectClasses {

    [ObjectClassPlugIn( "Archetype", "0.0.0.0"), Serializable()]
    public class Archetype : ObjectClassBase {

        #region fields

        private ArchetypeBaseEnu 
            _BaseType;

        #endregion

        #region events
        public event NTEventHandler EventBaseTypeChanged;
        public override event NTEventHandler  MyDataChanged;
        #endregion

        #region Properties

        [XmlAttribute(), Category( TagCategory.Base ),
        Description( "The major type that defines the Archetype" )]
        public  ArchetypeBaseEnu BaseType {
            get { return _BaseType; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _BaseType = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventBaseTypeChanged != null )
                        EventBaseTypeChanged();
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [XmlIgnore(), Category( TagCategory.Base ),
        Description( "This is what it costs to be this archetype" )]
        public int CostModifier {
            get { return Int16.Parse( GeneralOperations.GetDescription<ArchetypeBaseEnu>( BaseType ).Split( ',' )[1] ); }
        }

        [XmlIgnore(), Browsable(false)]
        public String BaseToStirng {
            get { return GeneralOperations.GetDescription<ArchetypeBaseEnu>( BaseType ).Split( ',' )[0]; }
        }

        [XmlIgnore(), Category( TagCategory.Base ),
        Description( "The archetypes starting experience value" )]
        public String StartingEXP {
            get { return GeneralOperations.GetDescription<ArchetypeBaseEnu>( BaseType ).Split( ',' )[2]; }
        }
        #endregion

        #region Methods
        public override string ToString() {
            string retVal = BaseType.ToString() + ":" + Name;
            if ( NTAF.Core.Properties.Settings.Default.VerboseToString )
                retVal = ID + ": " + Name;
            return retVal;
        }

        public override DataMember[] getDataMembers() {
            return new[] {
                             new DataMember( "Name", Name ),
                             new DataMember( "Type", BaseToStirng )
                         };
        }
        #endregion

        #region IClone Members
        protected override void clearMyEvents(){
            base.clearMyEvents( );
            EventBaseTypeChanged = null;
        }
        #endregion

        #region Constructors
        //create default constructor
        public Archetype() {
            ID = "";
            Name = "";
            BaseType = ArchetypeBaseEnu.New;
        }

        //create constructor to accept current class object
        public Archetype( Archetype var ) {
            ID = var.ID;
            Name = var.Name;
            BaseType = var.BaseType;
        }

        //create constructor that takes all vars
        public Archetype( string utID, string utName, ArchetypeBaseEnu utBaseType ) {
            ID = utID;
            Name = utName;
            BaseType = utBaseType;
        } 
        #endregion

        #region ICompare Members
        //extended compairers
        private class sortNameHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                Archetype val1 = ( Archetype )a;
                Archetype val2 = ( Archetype )b;

                //dsc
                return String.Compare( val2.Name, val1.Name );

            }
        }

        private class sortIDHelper_ASC : IComparer {
            int IComparer.Compare( object a, object b ) {
                Archetype val1 = ( Archetype )a;
                Archetype val2 = ( Archetype )b;

                //return string compair
                //asc
                return String.Compare( val1.ID, val2.ID );

            }
        }

        private class sortIDHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                Archetype val1 = ( Archetype )a;
                Archetype val2 = ( Archetype )b;

                //dsc
                return String.Compare( val2.ID, val1.ID );
            }
        }


        //extended compairers links
        public static IComparer sortName_Dsc() {
            return ( IComparer )new sortNameHelper_DSC();
        }

        public static IComparer sortID_Asc() {
            return ( IComparer )new sortIDHelper_ASC();
        }

        public static IComparer sortID_Dsc() {
            return ( IComparer )new sortIDHelper_DSC();
        } 
        #endregion

        #region IAboutMe Members

        [XmlIgnore(), Browsable(false)]
        public override string aboutMe {
            get {
                String retVal = base.aboutMe;
                retVal += String.Format( "Base Type: {0}", this.BaseType );

                return retVal;
            }
        }

        #endregion

        #region IValidate Members

        public override void Valid() {
            List<FieldAndMessage>
                ErrorList = new List<FieldAndMessage>( ),
                WarningList = new List<FieldAndMessage>( );

            try {
                base.Valid( );
            }
            catch ( ValidationException VE ) {
                ErrorList.AddRange( VE.Errors.ToArray( ) );
            }
            finally {
                
            //if ( _BaseType == ArchetypeBaseEnu.New ) throw new ValidationException( "Base type needs to be set" );
                if ( _BaseType.Is( ArchetypeBaseEnu.New ) ) ErrorList.Add( new FieldAndMessage( "Base Type", "Has not been set" ) );
                
                if ( ErrorList.Count >= 1 )
                    throw new ValidationException( ErrorList.ToArray( ) );

                if ( WarningList.Count >= 1 )
                    throw new ValidationWarning( WarningList.ToArray( ) );
            }
        }

        #endregion

        #region IPrintable Members

        public override void Print( PrintElement element ) {
            element.AddTitleText( "Archetype: " + Name );
            element.AddCategoryText( "Base Type: ", BaseToStirng );
            element.AddCategoryText( "Cost Modifier: ", CostModifier.ToString() );
            element.AddCategoryText( "Starting EXP: ", StartingEXP );

            //element.AddHorizontalRule();
            element.AddBlankLine();
        }

        #endregion

        #region operator overrides
        public override bool Equals( object obj ) {
            bool retVal = true;
            if ( obj == null || !( obj is Archetype ) )
                retVal = false;

            if ( retVal )
                retVal = this.ID == ( ( Archetype )obj ).ID;
            if ( retVal )
                retVal = this.Name == ( ( Archetype )obj ).Name;
            if ( retVal )
                retVal = this.BaseType == ( ( Archetype )obj ).BaseType;
            return retVal;
        }

        static public bool operator ==( Archetype a, Archetype b ) {
            bool retval = false;
            try {
                retval = a.Equals( b );
            }
            catch ( NullReferenceException ) {
                if ( ( Object )a == null && ( Object )b == null )
                    retval = true;
                else
                    retval = false;
            }
            catch ( Exception ex ) { throw ex; }
            return retval;
        }

        static public bool operator !=( Archetype a, Archetype b ) {
            bool retval = false;
            try {
                retval = a.Equals( b );
            }
            catch ( NullReferenceException ) {
                if ( ( Object )a == null && ( Object )b == null )
                    retval = true;
                else
                    retval = false;
            }
            catch ( Exception ex ) { throw ex; }
            return !retval;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        } 
        #endregion

        #region IObjectClass Members

        public override bool IsOfType( object obj ) {
            return obj is Archetype;
        }

        public override bool IsOfType( Type T ) {
            return T == typeof( Archetype );
        }

        public override Type MyType() { return CollectionType; }

        public override string Serialize( object toSerial, string tempPath ) {
            throw new NotImplementedException();
        }

        public override object Deserialize( string PathOfSavedObject ) {
            throw new NotImplementedException();
        }

        public override void Link( ILink DataMaster ) { }

        #endregion

    }

    [OCCPlugIn( "Archetypes", "0.0.0.0")]
    public class ArchetypeCollector : OCCBase{ 

        public override Type CollectionType {
            get {
                return typeof( Archetype );
            }
        }

    }

    [TreeNodePlugIn( "Archetypes Tree", "Archetypes", "0.0.0.0", typeof( Archetype ) )]
    public class ArchetypeTree : OCTreeNodeBase{
        //public override Type CollectionType {
        //    get {
        //        return typeof( Archetype );
        //    }
        //}
    }
}