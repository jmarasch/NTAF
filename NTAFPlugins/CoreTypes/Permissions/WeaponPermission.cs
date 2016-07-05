using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using NTAF.Core;
using NTAF.Permissions;
using NTAF.PlugInFramework;
using NTAF.PrintEngine;

namespace NTAF.ObjectClasses {

    [ObjectClassPlugIn( "WeaponPermission", "0.0.0.0" ), Serializable()]
    public class WeaponPermission : WSPPermission {

        //public override event NTEventHandler  EventRaceChange;

        #region methods
        //override functions
        public override string ToString() {
            string retVal = Name;
            if ( NTAF.Core.Properties.Settings.Default.VerboseToString )
                retVal = ID + ": " + Name;
            return retVal;
        }

        public override DataMember[] getDataMembers() {
            return new[] {
                new DataMember("Name", Name),
                new DataMember("Type", "Weapon Permission"),
                new DataMember("Race Can Equip", RaceCanEquip),
                new DataMember("Species Can Equip", SpeciesCanEquip)
            };
        }
        #endregion

        #region constructors
        public WeaponPermission() { }
        public WeaponPermission( string pID, string pName, Species MyBaseRace ) { ID = pID; Name = pName; SpeciesCanEquip = MyBaseRace; }
        public WeaponPermission( string pID, string pName, Race MyRace ) { ID = pID; Name = pName; RaceCanEquip = MyRace; }
        public WeaponPermission( WeaponPermission permission ) { ID = permission.ID; Name = permission.Name; SpeciesCanEquip = permission.SpeciesCanEquip; RaceCanEquip = permission.RaceCanEquip; }
        public WeaponPermission( string pID, string pName, Species MyBaseRace, Race MyRace ) { ID = pID; Name = pName; SpeciesCanEquip = MyBaseRace; RaceCanEquip = MyRace; }
        #endregion

        #region operator overrides
        public override bool Equals( object obj ) {
            bool retVal = true;
            if ( obj == null || !( obj is WeaponPermission ) )
                retVal = false;

            if ( retVal )
                retVal = this.ID == ( ( WeaponPermission )obj ).ID;
            if ( retVal )
                retVal = this.Name == ( ( WeaponPermission )obj ).Name;
            if ( retVal )
                retVal = this.RaceCanEquip == ( ( WeaponPermission )obj ).RaceCanEquip;
            if ( retVal )
                retVal = this.SpeciesCanEquip == ( ( WeaponPermission )obj ).SpeciesCanEquip;

            return retVal;
        }

        static public bool operator ==( WeaponPermission a, WeaponPermission b ) {
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

        static public bool operator !=( WeaponPermission a, WeaponPermission b ) {
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

        public override Type CollectionType {
            get { return typeof( WeaponPermission ); }
        }

        public override bool IsOfType( object obj ) {
            return obj is WeaponPermission;
        }

        public override bool IsOfType( Type T ) {
            return T == typeof( WeaponPermission );
        }

        public override Type MyType() { return CollectionType; }

        public override void Link( ILink DataMaster ) {
            if ( RaceCanEquip != null )
                RaceCanEquip = ( Race )DataMaster.FindObject( RaceCanEquip );
        }

        public override void ReplaceReferences( ObjectClassBase ObjectToReplace, ObjectClassBase ReplaceWith ) {
            if ( RaceCanEquip == ObjectToReplace )
                RaceCanEquip = ( Race )ReplaceWith;
        }

        public override bool CheckForReferences( ObjectClassBase Item ) {
            if ( RaceCanEquip == Item )
                return true;

            return false;
        }
        #endregion

        #region ICloneable Members
        //cloning operations

        protected override void clearMyEvents() {
            base.clearMyEvents( );
            //EventRaceChange = null;
        }

        #endregion

        #region IAboutMe Members

        [XmlIgnore(), Browsable( false )]
        public override string aboutMe {
            get {
                String retVal = base.aboutMe;
                if ( SpeciesCanEquip != 0 )
                    retVal += "Required Base Race(s): " + SpeciesCanEquip.ToString( ) + "\n";
                else
                    if ( RaceCanEquip != null )
                        retVal += "Required Race: " + ( ( Race )this.RaceCanEquip ).Name + "\n";

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
            //if ( ( ( Species )SpeciesCanEquip ) == 0 && RaceCanEquip == null ) throw new ValidationException( "A useable race or base race needs to be selected" );
                if ( SpeciesCanEquip == 0 && RaceCanEquip == null ) ErrorList.Add( new FieldAndMessage( "Race/Species", "A useable race or base race needs to be selected" ) );

                if ( ErrorList.Count >= 1 )
                    throw new ValidationException( ErrorList.ToArray( ) );

                if ( WarningList.Count >= 1 )
                    throw new ValidationWarning( WarningList.ToArray( ) );
            }
        }

        #endregion

        #region IComparable Members

        //nested sorting classes
        private class sortNameHelpe_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                Permission val1 = ( Permission )a;
                Permission val2 = ( Permission )b;

                //dsc
                return String.Compare( val2.Name, val1.Name );
            }
        }

        private class sortIDHelper_ASC : IComparer {
            int IComparer.Compare( object a, object b ) {
                Permission val1 = ( Permission )a;
                Permission val2 = ( Permission )b;

                //return string compair
                //asc
                return String.Compare( val1.ID, val2.ID );
            }
        }

        private class sortIDHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                Permission val1 = ( Permission )a;
                Permission val2 = ( Permission )b;

                //dsc
                return String.Compare( val2.ID, val1.ID );
            }
        }

        //extended compairers
        public static IComparer sortNameDsc() {
            return ( IComparer )new sortNameHelpe_DSC();
        }

        public static IComparer sortIDAsc() {
            return ( IComparer )new sortIDHelper_ASC();
        }

        public static IComparer sortIDDsc() {
            return ( IComparer )new sortIDHelper_DSC();
        }

        #endregion

        #region IPrintable Members

        public override void Print( PrintElement element ) {
            element.AddTitleText( "Weapon Permission: " + Name );
            if ( RaceCanEquip != null )
                element.AddCategoryText( "For race", ( ( Race )RaceCanEquip ).Name );
            else
                element.AddCategoryText( "For Species", SpeciesCanEquip.ToString() );
            //element.AddHorizontalRule();
            element.AddBlankLine();
        }

        #endregion
    }

    [OCCPlugIn( "WeaponPermissions", "0.0.0.0" )]
    public class WeaponPermissionCollector : OCCBase {
        public override byte objectLayer {
            get {
                return 1;
            }
        }
        public override Type CollectionType {
            get {
                return typeof( WeaponPermission );
            }
        }

    }

    //[TreeNodePlugIn( "WeaponPermission Tree","Weapon Permissions", "0.0.0.0", typeof( WeaponPermission ) )]
    //public class WeaponPermissionTree : OCTreeNodeBase {
    //    //public override Type CollectionType {
    //    //    get {
    //    //        return typeof( WeaponPermission );
    //    //    }
    //    //}
    //}
}