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
    /*todo: modify this for hand to hand, create new classification for ranged weapons, you will need
     *  to be able to define frame types, ammo types, and then build stock weapons from those types
     * 
     */ 

    [ObjectClassPlugIn( "Weapon", "0.0.0.0" ), Serializable(), XmlInclude( typeof( WeaponPermission ) )]
    public class Weapon : ObjectClassBase, IRequiresPermission {
        private string _Description, _Range, _Special;
        private WeaponPermission _RequiresPermission;
        private sbyte _MvsP, _MvsA, _SIOR, _SvMod;
        private byte _Shots;
        private UInt16 _Cost;
        private WeaponBaseType _BaseType;

        public override event NTEventHandler MyDataChanged;

        public event NTEventHandler EventCostChanged;
        public event NTEventHandler EventDescriptionChanged;
        public event NTEventHandler EventMvsPChanged;
        public event NTEventHandler EventMvsAChanged;
        public event NTEventHandler EventRangeChanged;
        public event NTEventHandler EventShotsChanged;
        public event NTEventHandler EventSIORChanged;
        public event NTEventHandler EventSpecialChanged;
        public event NTEventHandler EventSvModChanged;
        public event NTEventHandler EventBaseTypeChanged;

        [Category( TagCategory.PermissionRequirements ),
        Description( "" )]
        public Permission RequiresPermission {
            get { return _RequiresPermission; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( !NTAF.Core.Properties.Settings.Default.Loading )
                        if ( myOwner is ILockable )
                            if ( ( ( ILockable )myOwner ).FileLocked )
                                throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( !( value is WeaponPermission ) && value != null )
                    throw new ItemException( "Permission is not of the proper type, only Weapon Permissions can be assigned" );

                _RequiresPermission =(WeaponPermission) value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {

                    if ( EventRequiredPermissionChanged != null )
                        EventRequiredPermissionChanged();
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [Category( TagCategory.About ),
        Description( "" )]
        public string Description {
            get { return _Description; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _Description = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventDescriptionChanged != null )
                        EventDescriptionChanged();

                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [XmlAttribute(), Category( TagCategory.Stats ),
        Description( "" )]
        public string Range {
            get { return _Range; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _Range = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventRangeChanged != null )
                        EventRangeChanged();

                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }
      
        [Category( TagCategory.About ),
        Description( "" )]
        public string Special {
            get { return _Special; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _Special = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventSpecialChanged != null )
                        EventSpecialChanged();

                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [XmlAttribute(), Category( TagCategory.Stats ),
        Description( "" )]
        public sbyte MvsP {
            get { return _MvsP; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _MvsP = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventMvsPChanged != null )
                        EventMvsPChanged();

                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [XmlAttribute(), Category( TagCategory.Stats ),
        Description( "" )]
        public sbyte MvsA {
            get { return _MvsA; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _MvsA = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventMvsAChanged != null )
                        EventMvsAChanged();

                    if ( MyDataChanged != null )
                        MyDataChanged( );
                }
            }
        }

        [XmlAttribute(), Category( TagCategory.Stats ),
        Description( "" )]
        public sbyte SvMod {
            get { return _SvMod; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _SvMod = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventSvModChanged != null )
                        EventSvModChanged();

                    if ( MyDataChanged != null )
                        MyDataChanged( );
                }
            }
        }

        [XmlAttribute(), Category( TagCategory.Stats ),
        Description( "" )]
        public sbyte SIOR {
            get { return _SIOR; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _SIOR = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventSIORChanged != null )
                        EventSIORChanged();

                    if ( MyDataChanged != null )
                        MyDataChanged( );
                }
            }
        }

        [XmlAttribute(), Category( TagCategory.Stats ),
        Description( "" )]
        public byte Shots {
            get { return _Shots; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _Shots = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventShotsChanged != null )
                        EventShotsChanged();

                    if ( MyDataChanged != null )
                        MyDataChanged( );
                }
            }
        }

        [XmlAttribute(), Category( TagCategory.Stats ),
        Description( "" )]
        public UInt16 Cost {
            get { return _Cost; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _Cost = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventCostChanged != null )
                        EventCostChanged();

                    if ( MyDataChanged != null )
                        MyDataChanged( );
                }
            }
        }

        [XmlAttribute(), Category( TagCategory.Stats ),
        Description( "" )]
        public WeaponBaseType BaseType {
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
                        MyDataChanged( );
                }
            }
        }

        public Weapon() {
            ID = "";
            Name = "";
            Description = "";
            Range = "";
            Special = "";
            MvsP = 0;
            MvsA = 0;
            SIOR = 0;
            Shots = 0;
            Cost = 0;
            SvMod = 0;
            BaseType = WeaponBaseType.NoBase;
            RequiresPermission = null;
        }

        public Weapon( Weapon weapon ) {
            ID = weapon.ID;
            Name = weapon.Name;
            Description = weapon.Description;
            Range = weapon.Range;
            Special = weapon.Special;
            MvsP = weapon.MvsP;
            MvsA = weapon.MvsA;
            SIOR = weapon.SIOR;
            Shots = weapon.Shots;
            Cost = weapon.Cost;
            SvMod = weapon.SvMod;
            BaseType = weapon.BaseType;
            RequiresPermission = weapon.RequiresPermission;
        }

        public Weapon( string wpID, string wpName, string wpDescription, string wpRange, string wpSpecial,
        sbyte wpMvsP, sbyte wpMvsA, sbyte wpSIOR, byte wpShots, UInt16 wpCost, sbyte wpSvMod,
        WeaponBaseType wpBaseType, WeaponPermission wpRequiresPermission ) {
            ID = wpID;
            Name = wpName;
            Description = wpDescription;
            Range = wpRange;
            Special = wpSpecial;
            MvsP = wpMvsP;
            MvsA = wpMvsA;
            SIOR = wpSIOR;
            Shots = wpShots;
            Cost = wpCost;
            SvMod = wpSvMod;
            BaseType = wpBaseType;
            RequiresPermission = wpRequiresPermission;
        } 

        [XmlIgnore(), Browsable( false )]
        public override string aboutMe {
            get {
                String retVal = base.aboutMe;

                if ( this.Range != "" )
                    retVal += "Range: " + this.Range.ToString() + "\n";

                if ( this.SvMod != 0 )
                    retVal += String.Format( "Save Modifier: {0}\n", this.SvMod );

                retVal += String.Format( "MvsP: {0} MvsA: {1}\n", new object[] { this.MvsP, this.MvsA } );

                retVal += String.Format( "Shots: {0}    SIOR: {1}\n", new object[] { this.Shots, this.SIOR } );

                if ( this.BaseType != WeaponBaseType.NoBase )
                    retVal += String.Format( "Base Weapon Type: {0}\n", this.BaseType );

                if ( this.Special != "" )
                    retVal += "Special:\n" +
                        GeneralOperations.WrapLength( this.Special, 50 ) + "\n";

                if ( Description != "" )
                    retVal += "Description:\n" +
                        GeneralOperations.WrapLength( this.Description, 50 ) + "\n";

                if ( this.RequiresPermission != null )
                    retVal += "Required Permission: " + this.RequiresPermission.Name + "\n";

                retVal += String.Format( "Cost: {0}", this.Cost );

                return retVal;
            }
        }

        protected override void clearMyEvents() {
            base.clearMyEvents();

            EventBaseTypeChanged = null;
            EventCostChanged = null;
            EventDescriptionChanged = null;
            EventMvsAChanged = null;
            EventMvsPChanged = null;
            EventRangeChanged = null;
            EventRequiredPermissionChanged = null;
            EventShotsChanged = null;
            EventSIORChanged = null;
            EventSpecialChanged = null;
            EventSvModChanged = null;
            
        }

        public override Type CollectionType {
            get { return typeof( Weapon ); }
        }

        public override bool IsOfType( object obj ) {
            return obj is Weapon;
        }

        public override bool IsOfType( Type T ) {
            return T == CollectionType;
        }

        public override Type MyType() { return CollectionType; }

        public override void Link( ILink DataMaster ) {
            RequiresPermission = ( Permission )DataMaster.FindObject( RequiresPermission );
        }

        public override void ReplaceReferences( ObjectClassBase ObjectToReplace, ObjectClassBase ReplaceWith ) {
            if ( RequiresPermission == ObjectToReplace )
                RequiresPermission = ( Permission )ReplaceWith;
        }

        public override bool CheckForReferences( ObjectClassBase Item ) {
            if ( RequiresPermission == Item )
                return true;

            return false;
        }

        public override void Print( PrintElement element ) {
            element.AddTitleText( "Weapon: " + Name );
            element.AddCategoryText( "Range: ", Range != "0" ? Range + '"' : "Direct Contact" );
            element.AddText( String.Format( "Save Modifier: {0}    MvsP/MvsA: {1}/{2}", SvMod, MvsP, MvsA ) );
            element.AddText( String.Format( "SIOR/Shots: {0}/{1}", SIOR, Shots ) );
            element.AddBlankLine();
            element.AddCategoryText( "Description:" );
            element.AddMText( Description );
            element.AddCategoryText( "Base Type: ", BaseType.ToString() );
            element.AddBlankLine();
            element.AddCategoryText( "Requires Permission: ", RequiresPermission.Name );
            element.AddBlankLine();
            element.AddCategoryText( "Special:" );
            element.AddMText( Special );
            element.AddBlankLine();
            element.AddCategoryText( "Cost: ", Cost.ToString() );
            //element.AddHorizontalRule();
            element.AddBlankLine();
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
                
                if ( _Range == "" )ErrorList.Add( new FieldAndMessage( "Range", "Requires a value" ) );
                if ( _RequiresPermission == null ) ErrorList.Add( new FieldAndMessage( "Required Permission", "Requires a value" ) );
                if ( _MvsP <= -1 ) ErrorList.Add( new FieldAndMessage( "MvsP", "To Small" ) );
                if ( _MvsA <= -1 )ErrorList.Add( new FieldAndMessage( "MvsA", "To Small" ) );
                if ( _SIOR <= -1 ) ErrorList.Add( new FieldAndMessage( "SIOR", "Needs to be a positive number" ) );
                if ( _Cost == 0 )ErrorList.Add( new FieldAndMessage( "Cost", "Weapon must have a cost" ) );
                if ( _BaseType.Is( WeaponBaseType.NoBase ) )ErrorList.Add( new FieldAndMessage( "Base Type", "All weapons have a base type" ) );

                if ( ( _BaseType.Is( WeaponBaseType.Projectile ) | _BaseType.Is( WeaponBaseType.Gun ) | _BaseType.Is( WeaponBaseType.Thrown ) ) && _Shots == 0 )
                    ErrorList.Add( new FieldAndMessage( "Shots", _BaseType.GetDescription( _BaseType ) + " requires atleast 1 shot" ) );

                if ( _Description == "" ) WarningList.Add( new FieldAndMessage( "Description", "Should not be blank" ) );
                if ( _Special == "" ) WarningList.Add( new FieldAndMessage( "Special", "Should not be blank" ) );
                //if ( _SvMod == 0 ) WarningList.Add( new FieldAndMessage( "Save Modifier", "Should not be blank" ) );

                if ( ErrorList.Count >= 1 )
                    throw new ValidationException( ErrorList.ToArray( ) );

                if ( WarningList.Count >= 1 )
                    throw new ValidationWarning( WarningList.ToArray( ) );
            }
        }

        public override string ToString() {
            string retVal = Name;
            if ( NTAF.Core.Properties.Settings.Default.VerboseToString )
                retVal = ID + ": " + Name;
            return retVal;
        }

        public override DataMember[] getDataMembers() {
            return new[] {
                             new DataMember( "Name", Name ),
                             new DataMember( "Range", Range ),
                             new DataMember( "Description", Description ),
                             new DataMember( "Special", Special ),
                             new DataMember( "MvsP", MvsP ),
                             new DataMember( "MvsA", MvsA ),
                             new DataMember( "SIOR", SIOR ),
                             new DataMember( "SvMod", SvMod ),
                             new DataMember( "Shots", Shots ),
                             new DataMember( "Permission", RequiresPermission.Name ),
                             new DataMember( "Cost", Cost )
                         };
        }

        public override bool Equals( object obj ) {
            bool retVal = true;
            if ( obj == null || !( obj is Weapon ) )
                retVal = false;

            if ( retVal )
                retVal = this.RequiresPermission == ( ( Weapon )obj ).RequiresPermission;
            if ( retVal )
                retVal = this.ID == ( ( Weapon )obj ).ID;
            if ( retVal )
                retVal = this.Name == ( ( Weapon )obj ).Name;
            if ( retVal )
                retVal = this.Description == ( ( Weapon )obj ).Description;
            if ( retVal )
                retVal = this.Range == ( ( Weapon )obj ).Range;
            if ( retVal )
                retVal = this.Special == ( ( Weapon )obj ).Special;
            if ( retVal )
                retVal = this.MvsP == ( ( Weapon )obj ).MvsP;
            if ( retVal )
                retVal = this.MvsA == ( ( Weapon )obj ).MvsA;
            if ( retVal )
                retVal = this.SvMod == ( ( Weapon )obj ).SvMod;
            if ( retVal )
                retVal = this.SIOR == ( ( Weapon )obj ).SIOR;
            if ( retVal )
                retVal = this.Shots == ( ( Weapon )obj ).Shots;
            if ( retVal )
                retVal = this.Cost == ( ( Weapon )obj ).Cost;
            if ( retVal )
                retVal = this.BaseType == ( ( Weapon )obj ).BaseType;

            return retVal;
        }

        static public bool operator ==( Weapon a, Weapon b ) {
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

        static public bool operator !=( Weapon a, Weapon b ) {
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

        public event NTEventHandler  EventRequiredPermissionChanged;

        //nested sorting classes
        private class sortByNameHelper_Dec : IComparer {
            int IComparer.Compare( object a, object b ) {
                Weapon w1 = ( Weapon )a;
                Weapon w2 = ( Weapon )b;

                return String.Compare( w2.Name, w1.Name );
            }
        }

        private class sortByIDHelper_Asc : IComparer {
            int IComparer.Compare( object a, object b ) {
                Weapon w1 = ( Weapon )a;
                Weapon w2 = ( Weapon )b;

                return String.Compare( w1.ID, w2.ID );
            }
        }

        private class sortByIDHelper_Dsc : IComparer {
            int IComparer.Compare( object a, object b ) {
                Weapon w1 = ( Weapon )a;
                Weapon w2 = ( Weapon )b;

                return String.Compare( w2.ID, w1.ID );
            }
        }

        //extended compairers
        public static IComparer sortByNameDsc() {
            return ( IComparer )new sortByNameHelper_Dec();
        }

        public static IComparer sortByIDAsc() {
            return ( IComparer )new sortByIDHelper_Asc();
        }

        public static IComparer sortByIDDsc() {
            return ( IComparer )new sortByIDHelper_Dsc();
        }
    }

    [OCCPlugIn( "Weapons", "0.0.0.0" )]
    public class WeaponCollector : OCCBase {

        public override byte objectLayer {
            get {
                return 2;
            }
        }

        public override Type CollectionType {
            get {
                return typeof( Weapon );
            }
        }
    }

    [TreeNodePlugIn( "Weapon Tree", "Weapons", "0.0.0.0", typeof(Weapon) )]
    public class WeaponTree : OCTreeNodeBase {
        //public override Type CollectionType {
        //    get {
        //        return typeof( Weapon );
        //    }
        //}
    }
}