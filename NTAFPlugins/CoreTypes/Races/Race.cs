using System;
using System.Collections;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using NTAF.Core;
using NTAF.Core.Properties;
using NTAF.PlugInFramework;
using NTAF.PrintEngine;

namespace NTAF.ObjectClasses {

    [ObjectClassPlugIn( "Race", "0.9.0.0" ), Serializable()]
    public class Race : ObjectClassBase {
        #region Fields
        private Species _species;
        #endregion

        #region Special Events
        public event NTEventHandler EventSpeciesChange;
        public override event NTEventHandler  MyDataChanged;
        #endregion

        #region Properties
        [XmlAttribute(), Category( TagCategory.Base ),
        Description( "" )]
        public Species species {
            get { return _species; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _species = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventSpeciesChange != null )
                        EventSpeciesChange();
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [XmlIgnore(), Browsable( false )]
        public string SpeciesToString {
            get { return _species.ToString(); }
        }

        #endregion

        #region Methods
        //override functions
        public override string ToString() {
            string retVal = species + ":" + Name;
            if ( NTAF.Core.Properties.Settings.Default.VerboseToString )
                retVal = ID + ", " + species + ":" + Name;
            return retVal;
        }

        public override DataMember[] getDataMembers() {
            return new[] {
                new DataMember("Name", Name),
                new DataMember("Species", species.ToString())
            };
        }
        #endregion

        protected override void clearMyEvents() {
            base.clearMyEvents( );
            EventSpeciesChange = null;
        }

        #region Constructors
        //default constructor
        public Race() {
            ID = "";
            Name = "";
            species = Species.All;
        }

        //constructor to accept current class object
        public Race( Race var ) {
            ID = var.ID;
            Name = var.Name;
            species = var.species;
        }

        //constructor that takes all vars
        public Race( string rID, string rName, Species rBaseRace ) {
            ID = rID;
            Name = rName;
            species = rBaseRace;
        }
        #endregion

        //extended compairers
        private class sortNameHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                Race val1 = ( Race )a;
                Race val2 = ( Race )b;

                //return string compair
                //dsc
                return String.Compare( val2.Name, val1.Name );

            }
        }

        private class sortIDHelper_ASC : IComparer {
            int IComparer.Compare( object a, object b ) {
                Race val1 = ( Race )a;
                Race val2 = ( Race )b;

                //return string compair
                //asc
                return String.Compare( val1.ID, val2.ID );

            }
        }

        private class sortIDHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                Race val1 = ( Race )a;
                Race val2 = ( Race )b;

                //return string compair
                //dsc
                return String.Compare( val2.ID, val1.ID );

            }
        }

        //extended compairers link
        public static IComparer sortName_Dsc() {
            return ( IComparer )new sortNameHelper_DSC( );
        }
        public static IComparer sortID_Asc() {
            return ( IComparer )new sortIDHelper_ASC( );
        }
        public static IComparer sortID_Dsc() {
            return ( IComparer )new sortIDHelper_DSC( );
        }

        public override string aboutMe {
            get {
                String
                    retVal = base.aboutMe;

                retVal += String.Format( "Base Race: {0}", this.species );

                return retVal;
            }
        }

        public override bool Equals( object obj ) {
            bool retVal = true;
            if ( obj == null || !( obj is Race ) )
                retVal = false;

            if ( retVal )
                retVal = this.ID == ( ( Race )obj ).ID;
            if ( retVal )
                retVal = this.Name == ( ( Race )obj ).Name;
            if ( retVal )
                retVal = this.species == ( ( Race )obj ).species;

            return retVal;
        }

        static public bool operator ==( Race a, Race b ) {
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

        static public bool operator !=( Race a, Race b ) {
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
            return base.GetHashCode( );
        }

        public override void Print( PrintElement element ) {
            element.AddTitleText( "Race: " + Name );
            element.AddCategoryText( "Species: ", species.ToString( ) );
            element.AddBlankLine( );
        }

        public override Type CollectionType {
            get { return typeof( Race ); }
        }

        public override bool IsOfType( object obj ) {
            return obj is Race;
        }

        public override bool IsOfType( Type T ) {
            return T == typeof( Race );
        }

        public override Type MyType() { return CollectionType; }

    }

    [OCCPlugIn( "Races", "0.5.0.0")]
    public class RaceCollector : OCCBase{ 
        //IObjectClassCollector {

        public override Type CollectionType {
            get {
                return typeof( Race );
            }
        }

    }

    [TreeNodePlugIn( "Race Tree","Races", "0.4.0.0", typeof(Race) )]
    public class RaceTree : OCTreeNodeBase{

        //public override Type CollectionType {
        //    get {
        //        return typeof( Race );
        //    }
        //}

    }
}