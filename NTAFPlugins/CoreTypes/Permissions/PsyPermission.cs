using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using NTAF.Core;
using NTAF.Core.Properties;
using NTAF.Core;
using NTAF.Core;
using NTAF.Permissions;
using NTAF.PlugInFramework;
using NTAF.PrintEngine;

namespace NTAF.ObjectClasses {

    [ObjectClassPlugIn( "PsyPermission", "0.0.0.0" ), Serializable( )]
    public class PsyPermission : WSPPermission { // ObjectClassBase, IWSPPermission {

        public override event NTEventHandler  EventRaceChange;

        public override string ToString() {
            string retVal = Name;
            if ( NTAF.Core.Properties.Settings.Default.VerboseToString )
                retVal = ID + ": " + Name;
            return retVal;
        }

        public override DataMember[] getDataMembers() {
            return new[] {
                new DataMember("Name", Name),
                new DataMember("Type", "Psy Permission"),
                new DataMember("Race Can Equip", RaceCanEquip),
                new DataMember("Species Can Equip", SpeciesCanEquip)
            };
        }

        public PsyPermission() { }
        public PsyPermission( string pID, string pName, Species MyBaseRace ) { ID = pID; Name = pName; SpeciesCanEquip = MyBaseRace; }
        public PsyPermission( string pID, string pName, Race MyRace ) { ID = pID; Name = pName; RaceCanEquip = MyRace; }
        public PsyPermission( PsyPermission permission ) { ID = permission.ID; Name = permission.Name; SpeciesCanEquip = permission.SpeciesCanEquip; RaceCanEquip = permission.RaceCanEquip; }
        public PsyPermission( string pID, string pName, Species MyBaseRace, Race MyRace ) { ID = pID; Name = pName; SpeciesCanEquip = MyBaseRace; RaceCanEquip = MyRace; }

        public override bool Equals( object obj ) {
            bool retVal = true;
            if ( obj == null || !( obj is PsyPermission ) )
                retVal = false;

            if ( retVal )
                retVal = this.ID == ( ( PsyPermission )obj ).ID;
            if ( retVal )
                retVal = this.Name == ( ( PsyPermission )obj ).Name;
            if ( retVal )
                retVal = this.RaceCanEquip == ( ( PsyPermission )obj ).RaceCanEquip;
            if ( retVal )
                retVal = this.SpeciesCanEquip == ( ( PsyPermission )obj ).SpeciesCanEquip;

            return retVal;
        }

        static public bool operator ==( PsyPermission a, PsyPermission b ) {
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

        static public bool operator !=( PsyPermission a, PsyPermission b ) {
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

        public override Type CollectionType {
            get { return typeof( PsyPermission ); }
        }

        public override bool IsOfType( object obj ) {
            return obj is PsyPermission;
        }

        public override bool IsOfType( Type T ) {
            return T == typeof( PsyPermission );
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

        protected override void clearMyEvents() {
            base.clearMyEvents( );
            EventRaceChange = null;
        }

        [XmlIgnore( ), Browsable( false )]
        public override string aboutMe {
            get {
                String retVal = base.aboutMe;
                if ( SpeciesCanEquip != 0 )
                    retVal += "Required Base Race(s): " + SpeciesCanEquip.ToString( ) + "\n";
                else
                    if ( this.RaceCanEquip != null )
                        retVal += "Required Race: " + RaceCanEquip.Name + "\n";

                return retVal;
            }
        }

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
            return ( IComparer )new sortNameHelpe_DSC( );
        }

        public static IComparer sortIDAsc() {
            return ( IComparer )new sortIDHelper_ASC( );
        }

        public static IComparer sortIDDsc() {
            return ( IComparer )new sortIDHelper_DSC( );
        }

        public override void Print( PrintElement element ) {
            element.AddTitleText( "Psy Permission: " + Name );
            if ( RaceCanEquip != null )
                element.AddCategoryText( "For race", ( ( Race )RaceCanEquip ).Name );
            else
                element.AddCategoryText( "For Species", SpeciesCanEquip.ToString( ) );
            //element.AddHorizontalRule();
            element.AddBlankLine( );
        }

    }

    [OCCPlugIn( "PsyPermissions", "0.0.0.0" )]
    public class PsyPermissionCollector : OCCBase {

        public override byte objectLayer {
            get {
                return 1;
            }
        }

        public override Type CollectionType {
            get {
                return typeof( PsyPermission );
            }
        }
    }

    //[TreeNodePlugIn( "PsyPermission Tree","Psionic Permissions", "0.0.0.0", typeof( PsyPermission ) )]
    //public class PsyPermissionTree : OCTreeNodeBase {

    //    //public override Type CollectionType {
    //    //    get {
    //    //        return typeof( PsyPermission );
    //    //    }
    //    //}
    //}

    [TreeNodePlugIn( "WSPPermission Tree", "WSP Permissions", "0.0.0.0", new Type[]{typeof( PsyPermission ), typeof(WeaponPermission), typeof(SkillPermission)} )]
    public class PsyPermissionTree : OCTreeNodeBase {

        //public override Type CollectionType {
        //    get {
        //        return typeof( PsyPermission );
        //    }
        //}
    }
} 