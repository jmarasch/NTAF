using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using NTAF.DataCore.ObjectClasses.Extensions;
using NTAF.PrintEngine;
using NTAF.Interfaces;
using NTAF.Events;

namespace NTAF.DataCore.ObjectClasses {
    using NTAF.DataCore.Properties;
    using RaceEnu = NTAF.DataCore.ObjectClasses.Race.Species;
    using TypeEnu = NTAF.DataCore.ObjectClasses.Archetype.ArchetypeBaseEnu;

    [Serializable()]
    public class CrewUnit : INTId, INTName, IOwner, ICloneable,
        IComparable, IAboutMe, IValidate, IPrintable, IMemberCopy{

        #region Fields
        BaseUnit
            _baseunit = new BaseUnit();

        Byte
            _Movement,
            _HitPoints, 
            _HandToHand, 
            _AttackPoints,
            _PsyPoints,
            _RangedWeapons,
            _Might,
            _Toughness,
            _Agility,
            _Willpower,
            _HorrorFactor;

        String 
            _ID,
            _Name;

        internal List<WeaponPermission> 
            _WPerms;

        internal List<SkillPermission> 
            _SPerms;

        internal List<PsyPermission> 
            _PPerms;

        internal List<Weapon> 
            _Weapons;

        internal List<Skill> 
            _Skills;

        internal List<Psy> 
            _Psys;

        internal List<Item> 
            _Item;

        #endregion

        #region Properties Events

        public delegate void StatChanged( StatsMod.Stats Stat );
        public event StatChanged EventStatChanged;
        public event NTEventHandler EventNameChanged;
        public event NTEventHandler EventBaseUnitChanged;
        public event NTEventHandler EventMyDataChanged;
        public event NTEventHandler EventCostChanged;

        #endregion

        #region Properties

        [XmlAttribute(), Category( TagCategory.ID ),
        Description( TagDef.ID )]
        public string ID {
            get { return _ID; }
            set { _ID = value; }
        }

        [XmlIgnore(), ReadOnly( true ), Category( TagCategory.ID ),
        Description( TagDef.IDPreFix )]
        public string IDPreFix {
            get { return _ID.Substring( 0, 4 ); }
        }

        [XmlAttribute(), Category( TagCategory.ID ),
        Description( TagDef.Name )]
        public string Name {
            get { return _Name; }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _Name = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventNameChanged != null )
                        EventNameChanged();
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        [Category( TagCategory.Base ),
        Description( "" )]
        public BaseUnit Baseunit {
            get { return _baseunit; }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _baseunit = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventBaseUnitChanged != null )
                        EventBaseUnitChanged();
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte Movement {
            get { return _Movement; }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _Movement = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.Movement );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        //todo check on how stat mods are captured
        [XmlIgnore(), Category( TagCategory.StatModifiers ),
        Description( "" )]
        public int MovementMod {
            get { return getStatMod( StatsMod.Stats.Movement ); }
        }

        [XmlIgnore(), Category( TagCategory.StatTotals ),
        Description( "" )]
        public int MovementTotal {
            get { return ( int )Movement + MovementMod + Baseunit.MovementTotal; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte HitPoints {
            get { return _HitPoints; }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _HitPoints = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.HitPoints );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        [XmlIgnore(), Category( TagCategory.StatModifiers ),
        Description( "" )]
        public int HitPointsMod {
            get { return getStatMod( StatsMod.Stats.HitPoints ); }
        }

        [XmlIgnore(), Category( TagCategory.StatTotals ),
        Description( "" )]
        public int HitPointsTotal {
            get { return ( int )HitPoints + HitPointsMod + Baseunit.HitPointsTotal; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte HandToHand {
            get { return _HandToHand; }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _HandToHand = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.HandToHand );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        [XmlIgnore(), Category( TagCategory.StatModifiers ),
        Description( "" )]
        public int HandToHandMod {
            get { return getStatMod( StatsMod.Stats.HandToHand ); }
        }

        [XmlIgnore(), Category( TagCategory.StatTotals ),
        Description( "" )]
        public int HandToHandTotal {
            get { return ( int )HandToHand + HandToHandMod + Baseunit.HandToHandTotal; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte AttackPoints {
            get { return _AttackPoints; }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _AttackPoints = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.AttackPoints );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        [XmlIgnore(), Category( TagCategory.StatModifiers ),
        Description( "" )]
        public int AttackPointsMod {
            get { return getStatMod( StatsMod.Stats.AttackPoints ); }
        }

        [XmlIgnore(), Category( TagCategory.StatTotals ),
        Description( "" )]
        public int AttackPointsTotal {
            get { return ( int )AttackPoints + AttackPointsMod + Baseunit.AttackPointsTotal; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte PsyPoints {
            get { return _PsyPoints; }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _PsyPoints = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.PsyPoints );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        [XmlIgnore(), Category( TagCategory.StatModifiers ),
        Description( "" )]
        public int PsyPointsMod {
            get { return getStatMod( StatsMod.Stats.PsyPoints ); }
        }

        [XmlIgnore(), Category( TagCategory.StatTotals ),
        Description( "" )]
        public int PsyPointsTotal {
            get { return ( int )PsyPoints + PsyPointsMod + Baseunit.PsyPointsTotal; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte RangedWeapons {
            get { return _RangedWeapons; }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _RangedWeapons = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.RangedWeapons );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        [XmlIgnore(), Category( TagCategory.StatModifiers ),
        Description( "" )]
        public int RangedWeaponsMod {
            get { return getStatMod( StatsMod.Stats.RangedWeapons ); }
        }

        [XmlIgnore(), Category( TagCategory.StatTotals ),
        Description( "" )]
        public int RangedWeaponsTotal {
            get { return ( int )RangedWeapons + RangedWeaponsMod + Baseunit.RangedWeaponsTotal; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte Might {
            get { return _Might; }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _Might = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.Might );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        [XmlIgnore(), Category( TagCategory.StatModifiers ),
        Description( "" )]
        public int MightMod {
            get { return getStatMod( StatsMod.Stats.Might ); }
        }

        [XmlIgnore(), Category( TagCategory.StatTotals ),
        Description( "" )]
        public int MightTotal {
            get { return ( int )Might + MightMod+ Baseunit.MightTotal; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte Toughness {
            get { return _Toughness; }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _Toughness = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.Toughness );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        [XmlIgnore(), Category( TagCategory.StatModifiers ),
        Description( "" )]
        public int ToughnessMod {
            get { return getStatMod( StatsMod.Stats.Toughness ); }
        }

        [XmlIgnore(), Category( TagCategory.StatTotals ),
        Description( "" )]
        public int ToughnessTotal {
            get { return ( int )Toughness + ToughnessMod + Baseunit.ToughnessTotal; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte Agility {
            get { return _Agility; }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _Agility = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.Agility );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        [XmlIgnore(), Category( TagCategory.StatModifiers ),
        Description( "" )]
        public int AgilityMod {
            get { return getStatMod( StatsMod.Stats.Agility ); }
        }

        [XmlIgnore(), Category( TagCategory.StatTotals ),
        Description( "" )]
        public int AgilityTotal {
            get { return ( int )Agility + AgilityMod + Baseunit.AgilityTotal; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte Willpower {
            get { return _Willpower; }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _Willpower = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.Willpower );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        [XmlIgnore(), Category( TagCategory.StatModifiers ),
        Description( "" )]
        public int WillpowerMod {
            get { return getStatMod( StatsMod.Stats.Willpower ); }
        }

        [XmlIgnore(), Category( TagCategory.StatTotals ),
        Description( "" )]
        public int WillpowerTotal {
            get { return ( int )Willpower + WillpowerMod + Baseunit.WillpowerTotal; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte HorrorFactor {
            get { return _HorrorFactor; }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _HorrorFactor = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.HorrorFactor );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        [XmlIgnore(), Category( TagCategory.StatModifiers ),
        Description( "" )]
        public int HorrorFactorMod {
            get { return getStatMod( StatsMod.Stats.HorrorFactor ); }
        }

        [XmlIgnore(), Category( TagCategory.StatTotals ),
        Description( "" )]
        public int HorrorFactorTotal {
            get { return ( int )HorrorFactor + HorrorFactorMod + Baseunit.HorrorFactorTotal; }
        }

        [XmlIgnore(), Category( TagCategory.Cost ),
        Description( "" )]
        public Int32 Cost {
            get {
                Int32 retVal = 0;
                retVal += ( Int32 )( Movement * 2 );
                retVal += ( Int32 )( HandToHand * 2 );
                retVal += ( Int32 )( RangedWeapons * 3 );
                retVal += ( Int32 )( Might * 3 );
                retVal += ( Int32 )( Toughness * 4 );
                retVal += ( Int32 )( HitPoints * 5 );
                retVal += ( Int32 )( Agility * 1 );
                retVal += ( Int32 )( HorrorFactor * 10 );
                retVal += ( Int32 )( PsyPoints * 10 );
                retVal += ( Int32 )( Willpower * 1 );
                retVal += ( Int32 )( AttackPoints * 5 );
                retVal += Baseunit.Cost;

                foreach ( Weapon wpn in Weapons )
                    retVal += ( Int32 )( wpn.Cost );
                foreach ( Skill skl in SkillsAndAbilities )
                    retVal += ( Int32 )( skl.Cost );
                foreach ( Item itm in Items )
                    retVal += ( Int32 )itm.Cost;

                return retVal;
            }
        }

        [Category( TagCategory.Permissions ),
        Description( "" )]
        public WeaponPermission[] WPermissions {
            get { return this._WPerms.ToArray(); }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( _WPerms == null )
                    _WPerms = new List<WeaponPermission>();
                _WPerms.Clear();
                _WPerms.AddRange( value );
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {

                    if ( EventWeaponPermissionsChanged != null )
                        EventWeaponPermissionsChanged( null );
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        [Category( TagCategory.Permissions ),
        Description( "" )]
        public SkillPermission[] SPermissions {
            get { return this._SPerms.ToArray(); }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( _SPerms == null )
                    _SPerms = new List<SkillPermission>();
                _SPerms.Clear();
                _SPerms.AddRange( value );
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {

                    if ( EventSkillPermissionsChanged != null )
                        EventSkillPermissionsChanged( null );
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        [Category( TagCategory.Permissions ),
        Description( "" )]
        public PsyPermission[] PPermissions {
            get { return this._PPerms.ToArray(); }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( _PPerms == null )
                    _PPerms = new List<PsyPermission>();
                _PPerms.Clear();
                _PPerms.AddRange( value );
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {

                    if ( EventPsyPermissionsChanged != null )
                        EventPsyPermissionsChanged( null );
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        [XmlIgnore(), Browsable( false )]
        public IPermission[] permissions {
            get {
                List<IPermission> retVal = new List<IPermission>(); ;
                retVal.AddRange( WPermissions );
                retVal.AddRange( SPermissions );
                retVal.AddRange( PPermissions );
                return retVal.ToArray();
            }
        }

        [Category( TagCategory.ItmesAbilities ),
        Description( "" )]
        public Weapon[] Weapons {
            get { return this._Weapons.ToArray(); }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( _Weapons == null )
                    _Weapons = new List<Weapon>();
                _Weapons.Clear();
                _Weapons.AddRange( value );
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {

                    if ( EventWeaponsChanged != null )
                        EventWeaponsChanged( null );
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        [Category( TagCategory.ItmesAbilities ),
        Description( "" )]
        public Skill[] SkillsAndAbilities {
            get { return this._Skills.ToArray(); }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( _Skills == null )
                    _Skills = new List<Skill>();
                _Skills.Clear();
                _Skills.AddRange( value );
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {

                    if ( EventSkillsChanged != null )
                        EventSkillsChanged( null );
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        [Category( TagCategory.ItmesAbilities ),
        Description( "" )]
        public Psy[] Psys {
            get { return this._Psys.ToArray(); }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( _Psys == null )
                    _Psys = new List<Psy>();
                _Psys.Clear();
                _Psys.AddRange( value );
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {

                    if ( EventPsysChanged != null )
                        EventPsysChanged( null );
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        [Category( TagCategory.ItmesAbilities ),
        Description( "" )]
        public Item[] Items {
            get { return this._Item.ToArray(); }
            set {
                if ( !Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( _Item == null )
                    _Item = new List<Item>();
                _Item.Clear();
                _Item.AddRange( value );
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {

                    if ( EventItemsChanged != null )
                        EventItemsChanged( null );
                    if ( EventMyDataChanged != null )
                        EventMyDataChanged();
                }
            }
        }

        #endregion

        #region Methods

        public override string ToString() {
            string retVal = Name;
            if ( Properties.Settings.Default.VerboseToString ) {
                retVal = "";
                if ( Properties.Settings.Default.VerboseID )
                    retVal += ID + ": ";
                if ( Properties.Settings.Default.VerboseName )
                    retVal += Name;
                if ( Properties.Settings.Default.VerboseOther ) {
                    retVal += "( " + Baseunit.Name + " )";
                }
            }
            return retVal;
        }

        internal void clearWPermissions() {
            _WPerms.Clear();
        }
        internal void AddRangeWPerms( WeaponPermission[] perms ) {
            foreach ( WeaponPermission perm in perms )
                AddWeaponPermission( perm );
        }

        internal void clearSPermissions() {
            _SPerms.Clear();
        }
        internal void AddRangeSPerms( SkillPermission[] perms ) {
            foreach ( SkillPermission perm in perms )
                AddSkillPermission( perm );
        }

        internal void clearPPermissions() {
            _PPerms.Clear();
        }
        internal void AddRangePPerms( PsyPermission[] perms ) {
            foreach ( PsyPermission perm in perms )
                AddPsyPermission( perm );
        }

        internal void clearWeapons() {
            _Weapons.Clear();
        }
        internal void AddRangeWeapons( Weapon[] items ) {
            foreach ( Weapon item in items )
                AddWeapon( item );
        }

        internal void clearSkills() {
            _Skills.Clear();
        }
        internal void AddRangeSkills( Skill[] items ) {
            foreach ( Skill item in items )
                AddSkill( item );
        }

        internal void clearPsys() {
            _Psys.Clear();
        }
        internal void AddRangePsys( Psy[] items ) {
            foreach ( Psy item in items )
                AddPsy( item );
        }

        internal void clearItems() {
            _Item.Clear();
        }
        internal void AddRangeItems( Item[] items ) {
            foreach ( Item item in items )
                AddItem( item );
        }

        #endregion

        #region IClone Members

        public Object Clone() {
            object retVal = this.MemberwiseClone();
            ( ( CrewUnit )retVal ).clearMyEvents();
            ( ( CrewUnit )retVal ).myOwner = null;//new
            return retVal;
        }

        private void clearMyEvents() {
            EventCostChanged = null;
            EventBaseUnitChanged = null;
            //EventDescriptionChanged = null;
            EventItemsChanged = null;
            EventMyDataChanged = null;
            EventNameChanged = null;
            EventPsyPermissionsChanged = null;
            EventPsysChanged = null;
            //EventRaceChanged = null;
            EventSkillPermissionsChanged = null;
            EventSkillsChanged = null;
            //EventSpaecialChanged = null;
            EventStatChanged = null;
            //EventUnitTypeChanged = null;
            EventWeaponPermissionsChanged = null;
            EventWeaponsChanged = null;
        }
        #endregion

        #region IComparable Members
        //default sorter obj.sort()
        int IComparable.CompareTo( object obj ) {
            BaseUnit chk = ( BaseUnit )obj;

            //return string compair
            //asc
            return String.Compare( this.Name, chk.Name );
        }

        //extended compairers
        private class sortNameHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;

                //return string compair
                //dsc
                return String.Compare( val2.Name, val1.Name );

            }
        }

        private class sortIDHelper_ASC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;

                //return string compair
                //asc
                return String.Compare( val1.ID, val2.ID );

            }
        }

        private class sortIDHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;

                //return string compair
                //dsc
                return String.Compare( val2.ID, val1.ID );

            }
        }

        //Movement,
        private class sortMovementHelper_ASC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.Movement > val2.Movement )
                    retVal = 1;
                if ( val1.Movement < val2.Movement )
                    retVal = -1;

                return retVal;

            }
        }

        private class sortMovementHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.Movement < val2.Movement )
                    retVal = 1;
                if ( val1.Movement > val2.Movement )
                    retVal = -1;

                return retVal;
            }
        }
        //HitPoints, 
        private class sortHitPointsHelper_ASC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.HitPoints > val2.HitPoints )
                    retVal = 1;
                if ( val1.HitPoints < val2.HitPoints )
                    retVal = -1;

                return retVal;

            }
        }

        private class sortHitPointsHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.HitPoints < val2.HitPoints )
                    retVal = 1;
                if ( val1.HitPoints > val2.HitPoints )
                    retVal = -1;

                return retVal;
            }
        }
        //HandToHand,
        private class sortHandToHandHelper_ASC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.HandToHand > val2.HandToHand )
                    retVal = 1;
                if ( val1.HandToHand < val2.HandToHand )
                    retVal = -1;

                return retVal;

            }
        }

        private class sortHandToHandHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.HandToHand < val2.HandToHand )
                    retVal = 1;
                if ( val1.HandToHand > val2.HandToHand )
                    retVal = -1;

                return retVal;
            }
        }
        //AttackPoints,
        private class sortAttackPointsHelper_ASC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.AttackPoints > val2.AttackPoints )
                    retVal = 1;
                if ( val1.AttackPoints < val2.AttackPoints )
                    retVal = -1;

                return retVal;
            }
        }

        private class sortAttackPointsHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.AttackPoints < val2.AttackPoints )
                    retVal = 1;
                if ( val1.AttackPoints > val2.AttackPoints )
                    retVal = -1;

                return retVal;
            }
        }
        //PsyPoints,
        private class sortPsyPointsHelper_ASC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.PsyPoints > val2.PsyPoints )
                    retVal = 1;
                if ( val1.PsyPoints < val2.PsyPoints )
                    retVal = -1;

                return retVal;
            }
        }

        private class sortPsyPointsHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.PsyPoints < val2.PsyPoints )
                    retVal = 1;
                if ( val1.PsyPoints > val2.PsyPoints )
                    retVal = -1;

                return retVal;
            }
        }
        //RangedWeapons,
        private class sortRangedWeaponsHelper_ASC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.RangedWeapons > val2.RangedWeapons )
                    retVal = 1;
                if ( val1.RangedWeapons < val2.RangedWeapons )
                    retVal = -1;

                return retVal;
            }
        }

        private class sortRangedWeaponsHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.RangedWeapons < val2.RangedWeapons )
                    retVal = 1;
                if ( val1.RangedWeapons > val2.RangedWeapons )
                    retVal = -1;

                return retVal;
            }
        }
        //Might,
        private class sortMightHelper_ASC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.Might > val2.Might )
                    retVal = 1;
                if ( val1.Might < val2.Might )
                    retVal = -1;

                return retVal;
            }
        }

        private class sortMightHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.Might < val2.Might )
                    retVal = 1;
                if ( val1.Might > val2.Might )
                    retVal = -1;

                return retVal;
            }
        }
        //Toughness,
        private class sortToughnessHelper_ASC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.Toughness > val2.Toughness )
                    retVal = 1;
                if ( val1.Toughness < val2.Toughness )
                    retVal = -1;

                return retVal;
            }
        }

        private class sortToughnessHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.Toughness < val2.Toughness )
                    retVal = 1;
                if ( val1.Toughness > val2.Toughness )
                    retVal = -1;

                return retVal;
            }
        }
        //Agility,
        private class sortAgilityHelper_ASC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.Agility > val2.Agility )
                    retVal = 1;
                if ( val1.Agility < val2.Agility )
                    retVal = -1;

                return retVal;
            }
        }

        private class sortAgilityHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.Agility < val2.Agility )
                    retVal = 1;
                if ( val1.Agility > val2.Agility )
                    retVal = -1;

                return retVal;
            }
        }
        //Willpower,
        private class sortWillpowerHelper_ASC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.Willpower > val2.Willpower )
                    retVal = 1;
                if ( val1.Willpower < val2.Willpower )
                    retVal = -1;

                return retVal;
            }
        }

        private class sortWillpowerHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.Willpower < val2.Willpower )
                    retVal = 1;
                if ( val1.Willpower > val2.Willpower )
                    retVal = -1;

                return retVal;
            }
        }
        //HorrorFactor;
        private class sortHorrorFactorHelper_ASC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.HorrorFactor > val2.HorrorFactor )
                    retVal = 1;
                if ( val1.HorrorFactor < val2.HorrorFactor )
                    retVal = -1;

                return retVal;
            }
        }

        private class sortHorrorFactorHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.HorrorFactor < val2.HorrorFactor )
                    retVal = 1;
                if ( val1.HorrorFactor > val2.HorrorFactor )
                    retVal = -1;

                return retVal;
            }
        }

        //extended compairers link
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

        [XmlIgnore(), Browsable( false )]
        public string aboutMe {
            get {
                string retVal = String.Empty;
                if ( Properties.Settings.Default.VerboseToString )
                    if ( this.ID != "" )
                        retVal += "ID: " + this.ID + "\n";
                if ( this.Name != "" )
                    retVal += "Name: " + this.Name + "\n\n";
                retVal += String.Format( "BaseUnit: {0}\n\n", this.Baseunit.Name );
                retVal += String.Format( "Race: {0}\n\n", this.Baseunit.BaseRace.Name );
                retVal += String.Format( "Unit Type: {0}\n\n", this.Baseunit.archetype.Name );
                retVal += String.Format( "Movement: {0}\n", this.MovementTotal );
                retVal += String.Format( "HitPoints: {0}\n", this.HitPointsTotal );
                retVal += String.Format( "HandToHand: {0}\n", this.HandToHandTotal );
                retVal += String.Format( "AttackPoints: {0}\n", this.AttackPointsTotal );
                retVal += String.Format( "PsyPoints: {0}\n", this.PsyPointsTotal );
                retVal += String.Format( "RangedWeapons: {0}\n", this.RangedWeaponsTotal );
                retVal += String.Format( "Might: {0}\n", this.MightTotal );
                retVal += String.Format( "Toughness: {0}\n", this.ToughnessTotal );
                retVal += String.Format( "Agility: {0}\n", this.AgilityTotal );
                retVal += String.Format( "Willpower: {0}\n", this.WillpowerTotal );
                retVal += String.Format( "HorrorFactor: {0}\n", this.HorrorFactorTotal );


                retVal += String.Format( "\nCost: {0}\n", this.Cost );

                return retVal;
            }
        }

        #endregion

        #region ListOperations

        #region Weapon

        public delegate void WeaponPermissionsChanged( WeaponPermission WPerm );

        public event WeaponPermissionsChanged EventWeaponPermissionsChanged;

        public delegate void WeaponsChanged( Weapon Weapon );

        public event WeaponsChanged EventWeaponsChanged;

        #region permissions
        public void AddWeaponPermission( WeaponPermission WPerm ) {
            if ( WeaponPermissionExists( WPerm ) )
                throw new PermissionException( "This item already exists in the current list, no need to add it twice..." );

            _WPerms.Add( WPerm );

            if ( !Settings.Default.Loading )
                if ( EventWeaponPermissionsChanged != null )
                    EventWeaponPermissionsChanged( WPerm );
        }

        public void DropWeaponPermission( WeaponPermission WPerm ) {
            if ( !WeaponPermissionExists( WPerm ) )
                throw new PermissionException( "Could not find the item in the current list" );
            _WPerms.Remove( WPerm );
            Console.WriteLine( "Perm removed in class" );

            this.ValidatePermAndItems();

            if ( EventWeaponPermissionsChanged != null )
                EventWeaponPermissionsChanged( WPerm );
        }

        public bool WeaponPermissionExists( WeaponPermission WPerm ) {
            bool retVal = false;
            if ( _WPerms == null )
                retVal = false;
            else
                retVal = _WPerms.Contains( WPerm );
            return retVal;
        }
        #endregion

        #region items

        public Weapon[] getAvalableWeapons( Weapon[] weapons ) {
            if ( this._WPerms.Count <= 0 ) {
                throw new PermissionException( "You have not selected any weapon permissions" );
            }

            //start filtering out thaings that cannot be used
            List<Weapon> AvalibleWeapons = new List<Weapon>();
            foreach ( Weapon ITEM in weapons )
                foreach ( WeaponPermission PERM in this._WPerms )
                    if ( ( ( WeaponPermission )PERM ).ID == ( ( Weapon )ITEM ).RequiresPermission.ID )
                        AvalibleWeapons.Add( ITEM );
            return AvalibleWeapons.ToArray();

        }

        public void AddWeapon( Weapon Wepon ) {
            _Weapons.Add( Wepon );

            if ( !Settings.Default.Loading ) {
                if ( EventWeaponsChanged != null )
                    EventWeaponsChanged( Wepon );

                if ( EventCostChanged != null )
                    EventCostChanged();
            }
        }

        public void DropWeapon( Weapon Wepon ) {
            if ( !WeaponExists( Wepon ) )
                throw new ItemException( "Could not find the item in the current list" );
            _Weapons.Remove( Wepon );

            if ( EventWeaponsChanged != null )
                EventWeaponsChanged( Wepon );

            if ( EventCostChanged != null )
                EventCostChanged();
        }

        public bool WeaponExists( Weapon Wepon ) {
            return _Weapons.Contains( Wepon );
        }
        #endregion
        #endregion

        #region Skills

        public delegate void SkillPermissionsChanged( SkillPermission PPerm );

        public event SkillPermissionsChanged EventSkillPermissionsChanged;

        public delegate void SkillsChanged( Skill Skil );

        public event SkillsChanged EventSkillsChanged;

        #region permissions
        public void AddSkillPermission( SkillPermission SPerm ) {
            if ( SkillPermissionExists( SPerm ) )
                throw new PermissionException( "This permission already exists in the current list, no need to add it twice..." );

            _SPerms.Add( SPerm );

            if ( !Settings.Default.Loading )
                if ( EventSkillPermissionsChanged != null )
                    EventSkillPermissionsChanged( SPerm );
        }

        public void DropSkillPermission( SkillPermission SPerm ) {
            if ( !SkillPermissionExists( SPerm ) )
                throw new PermissionException( "Could not find the item in the current list" );
            _SPerms.Remove( SPerm );
            Console.WriteLine( "Perm removed in class" );

            this.ValidatePermAndItems();

            if ( EventSkillPermissionsChanged != null )
                EventSkillPermissionsChanged( SPerm );
        }

        public bool SkillPermissionExists( SkillPermission SPerm ) {
            bool retVal = false;
            if ( _SPerms == null )
                retVal = false;
            else
                retVal = _SPerms.Contains( SPerm );
            return retVal;
        }
        #endregion

        #region items

        public Skill[] getAvalableSkills( Skill[] skills ) {
            if ( this._SPerms.Count <= 0 ) {
                throw new PermissionException( "You have not selected any skillAbility permissions" );
            }

            //start filtering out thaings that cannot be used
            List<Skill> AvalibleSkills = new List<Skill>();
            foreach ( Skill ITEM in skills )
                foreach ( SkillPermission PERM in this._SPerms )
                    if ( PERM == ITEM.RequiresPermission )
                        //if ( ( ( SkillPermission )PERM ).ID == ( ( Skill )ITEM ).RequiresPermission.ID )
                        AvalibleSkills.Add( ITEM );
            return AvalibleSkills.ToArray();
        }

        public void AddSkill( Skill Skil ) {
            if ( SkillExists( Skil ) )
                throw new ItemException( "that item already exists in the lists" );
            _Skills.Add( Skil );

            if ( !Settings.Default.Loading ) {
                if ( EventSkillsChanged != null )
                    EventSkillsChanged( Skil );

                if ( EventCostChanged != null )
                    EventCostChanged();
            }
        }

        public void DropSkill( Skill Skil ) {
            if ( !SkillExists( Skil ) )
                throw new ItemException( "Could not find the item in the current list" );
            _Skills.Remove( Skil );

            if ( EventWeaponsChanged != null )
                EventSkillsChanged( Skil );

            if ( EventCostChanged != null )
                EventCostChanged();
        }

        public bool SkillExists( Skill Skil ) {
            return _Skills.Contains( Skil );
        }
        #endregion

        #endregion

        #region Psys

        public delegate void PsyPermissionsChanged( PsyPermission PPerm );

        public event PsyPermissionsChanged EventPsyPermissionsChanged;

        public delegate void PsysChanged( Psy psy );

        public event PsysChanged EventPsysChanged;

        #region permissions
        public void AddPsyPermission( PsyPermission PPerm ) {
            if ( PsyPermissionExists( PPerm ) )
                throw new PermissionException( "This permission already exists in the current list, no need to add it twice..." );

            _PPerms.Add( PPerm );

            if ( !Settings.Default.Loading )
                if ( EventPsyPermissionsChanged != null )
                    EventPsyPermissionsChanged( PPerm );
        }

        public void DropPsyPermission( PsyPermission PPerm ) {
            if ( !PsyPermissionExists( PPerm ) )
                throw new PermissionException( "Could not find the item in the current list" );
            _PPerms.Remove( PPerm );
            Console.WriteLine( "Perm removed in class" );

            this.ValidatePermAndItems();

            if ( EventPsyPermissionsChanged != null )
                EventPsyPermissionsChanged( PPerm );
        }

        public bool PsyPermissionExists( PsyPermission PPerm ) {
            bool retVal = false;
            if ( _PPerms == null )
                retVal = false;
            else
                retVal = _PPerms.Contains( PPerm );
            return retVal;
        }
        #endregion

        #region items

        public Psy[] getAvalablPsys( Psy[] psys ) {
            if ( this._PPerms.Count <= 0 ) {
                throw new PermissionException( "You have not selected any psy permissions" );
            }

            //start filtering out thaings that cannot be used
            List<Psy> AvaliblePsys = new List<Psy>();
            foreach ( Psy ITEM in psys )
                foreach ( PsyPermission PERM in this._PPerms )
                    if ( ( ( PsyPermission )PERM ).ID == ( ( Psy )ITEM ).RequiresPermission.ID )
                        AvaliblePsys.Add( ITEM );
            return AvaliblePsys.ToArray();
        }

        public void AddPsy( Psy psy ) {
            _Psys.Add( psy );

            if ( !Settings.Default.Loading ) {
                if ( EventPsysChanged != null )
                    EventPsysChanged( psy );

                if ( EventCostChanged != null )
                    EventCostChanged();
            }
        }

        public void DropPsy( Psy psy ) {
            if ( !PsyExists( psy ) )
                throw new ItemException( "Could not find the item in the current list" );
            _Psys.Remove( psy );

            if ( EventPsysChanged != null )
                EventPsysChanged( psy );

            if ( EventCostChanged != null )
                EventCostChanged();
        }

        public bool PsyExists( Psy psy ) {
            return _Psys.Contains( psy );
        }
        #endregion
        #endregion

        #region Items

        public delegate void ItemsChanged( Item item );

        public event ItemsChanged EventItemsChanged;

        #region items

        public Item[] getAvalablItems( Item[] items ) {
            if ( this.Baseunit.BaseRace.ID == "" ) {
                throw new RaceException( "You have not selected a race" );
            }

            if ( this.Baseunit.archetype.ID == "" ) {
                throw new RaceException( "You have not selected a Unit Type" );
            }

            //start filtering out thaings that cannot be used
            List<Item> AvalibleItems = new List<Item>();
            foreach ( Item ITEM in items ) {
                if ( checkItem( ITEM ) )
                    AvalibleItems.Add( ITEM );
            }
            return AvalibleItems.ToArray();
        }

        public void AddItem( Item item ) {
            _Item.Add( item );

            if ( !Settings.Default.Loading ) {
                if ( EventItemsChanged != null )
                    EventItemsChanged( item );

                if ( EventCostChanged != null )
                    EventCostChanged();
            }
        }

        public void DropItem( Item item ) {
            if ( !ItemExists( item ) )
                throw new ItemException( "Could not find the item in the current list" );
            _Item.Remove( item );

            if ( EventItemsChanged != null )
                EventItemsChanged( item );

            if ( EventCostChanged != null )
                EventCostChanged();
        }

        public bool ItemExists( Item item ) {
            return _Item.Contains( item );
        }
        #endregion
        #endregion

        #endregion

        #region functions
        private int getStatMod( StatsMod.Stats stat ) {
            int retVal = 0;
            List<StatsMod> mods = new List<StatsMod>();

            foreach ( Skill skl in SkillsAndAbilities )
                mods.AddRange( skl.ModifiesStats );

            foreach ( Item itm in Items )
                mods.AddRange( itm.ModifiesStats );

            foreach ( StatsMod sm in mods )
                if ( sm.StatToMod == stat )
                    retVal += sm.Modifier;

            return retVal;
        }

        public T[] getAllowedPermissions<T>( T[] LoadedPermissions ) {
            if ( this.Baseunit.ID == "" )
                throw new RaceException( "You have not selected a Unit Framework" );

            List<T> retVal = new List<T>();

            foreach ( T PERM in LoadedPermissions )
                if ( PermissionFilter( PERM ) )
                    retVal.Add( PERM );

            return retVal.ToArray();
        }
        
        private void ValidatePermAndItems() {
            List<Item> ItemsToRemove = new List<Item>();
            if ( this._Item.Count > 0 ) {
                foreach ( Item I in this.Items )
                    if ( !checkItem( I ) )
                        ItemsToRemove.Add( I );
                foreach ( Item I in ItemsToRemove )
                    this.DropItem( I );
            }

            //remove weapon permissions
            List<WeaponPermission> WPermToRemove = new List<WeaponPermission>();
            if ( this._WPerms.Count > 0 ) {
                foreach ( WeaponPermission PERM in this.WPermissions )
                    if ( !( PermissionFilter( PERM ) ) )
                        WPermToRemove.Add( ( WeaponPermission )PERM );
                foreach ( WeaponPermission PERM in WPermToRemove )
                    this.DropWeaponPermission( PERM );
            }

            //check Weapons list
            if ( this._Weapons.Count <= 0 ) {
                this._Weapons.Clear();
                if ( EventWeaponsChanged != null )
                    EventWeaponsChanged( null );
            }
            else {
                List<Weapon> WeaponsToRemove = new List<Weapon>();
                foreach ( Weapon ITEM in this.Weapons )
                    if ( !( this.WPermissions.Contains( ITEM.RequiresPermission ) ) )
                        WeaponsToRemove.Add( ITEM );

                foreach ( Weapon ITEM in WeaponsToRemove )
                    this.DropWeapon( ITEM );
            }

            //remove skillAbility perms
            List<SkillPermission> SPermToRemove = new List<SkillPermission>();
            if ( this._SPerms.Count > 0 ) {
                foreach ( SkillPermission PERM in this.SPermissions )
                    if ( !( PermissionFilter( PERM ) ) )
                        SPermToRemove.Add( ( SkillPermission )PERM );
                foreach ( SkillPermission PERM in SPermToRemove )
                    this.DropSkillPermission( PERM );
            }

            //check skills list
            if ( this._SPerms.Count <= 0 ) {
                this._Skills.Clear();
                if ( EventSkillsChanged != null )
                    EventSkillsChanged( null );
            }
            else {
                List<Skill> SkillsToRemove = new List<Skill>();
                foreach ( Skill ITEM in this.SkillsAndAbilities )
                    if ( !( this.SPermissions.Contains( ITEM.RequiresPermission ) ) )
                        SkillsToRemove.Add( ITEM );

                foreach ( Skill ITEM in SkillsToRemove )
                    this.DropSkill( ITEM );
            }

            //remove psy perms
            List<PsyPermission> PPermToRemove = new List<PsyPermission>();
            if ( this._PPerms.Count > 0 ) {
                foreach ( PsyPermission PERM in this.PPermissions )
                    if ( !( PermissionFilter( PERM ) ) )
                        PPermToRemove.Add( ( PsyPermission )PERM );
                foreach ( PsyPermission PERM in PPermToRemove )
                    this.DropPsyPermission( PERM );
            }

            //check Pys list
            if ( this._PPerms.Count <= 0 ) {
                this._Psys.Clear();
                if ( EventPsysChanged != null )
                    EventPsysChanged( null );
            }
            else {
                List<Psy> PsysToRemove = new List<Psy>();
                foreach ( Psy ITEM in this.Psys )
                    if ( !( this.PPermissions.Contains( ITEM.RequiresPermission ) ) )
                        PsysToRemove.Add( ITEM );

                foreach ( Psy ITEM in PsysToRemove )
                    this.DropPsy( ITEM );
            }

            if ( EventCostChanged != null )
                EventCostChanged();
        }

        private bool checkItem( Item item ) {
            bool allowed = false;
            if ( item.TypesCanUse != 0 ) {//its for a specfic type
                if ( CheckUnitTypeFlag( item.TypesCanUse, this.Baseunit.archetype.BaseType ) )
                    allowed = true;
            }
            else {//no specific type needed
                if ( item.SpeciesCanEquip != 0 )
                    if ( CheckRaceFlag( item.SpeciesCanEquip, this.Baseunit.BaseRace.species ) )
                        allowed = true;
                    else {
                        if ( item.RaceCanEquip == this.Baseunit.BaseRace )
                            allowed = true;
                    }
            }
            return allowed;
        }

        //returns true if the permission can be used by the currently selected race
        private bool PermissionFilter( object PERM ) {

            bool retval = false;
            Race SubRaceCanEquip = new Race();
            RaceEnu BaseRacesCanEquip=new RaceEnu();
            if ( PERM is IPermission ) {
                SubRaceCanEquip = ( ( IPermission )PERM ).RaceCanEquip;
                BaseRacesCanEquip = ( ( IPermission )PERM ).SpeciesCanEquip;
            }

            if ( BaseRacesCanEquip != RaceEnu.All ) {
                if ( SubRaceCanEquip != null )
                    if ( SubRaceCanEquip.ID != "" ) {
                        if ( SubRaceCanEquip.ID == this.Baseunit.BaseRace.ID )
                            retval = true;
                    }
                    else {
                        if ( CheckRaceFlag( BaseRacesCanEquip, this.Baseunit.BaseRace.species ) )//enumerates possible flags list
                            retval = true;
                    }
                else {
                    if ( CheckRaceFlag( BaseRacesCanEquip, this.Baseunit.BaseRace.species ) )//enumerates possible flags list
                        retval = true;
                }
            }
            else
                retval = true;
            return retval;
        }

        //enumerates possible flags list
        private bool CheckRaceFlag( RaceEnu FlagsToLookFor, RaceEnu MyFlag ) {
            bool retval = false;
            List<RaceEnu> ContainingFlag = new List<RaceEnu>();
            foreach ( RaceEnu RE in GeneralOperations.EnumToList<RaceEnu>() )
                if ( SpeciesFunctions.IsSpecies( FlagsToLookFor, RE ) )
                    ContainingFlag.Add( RE );
            foreach ( RaceEnu RE in ContainingFlag )
                if ( SpeciesFunctions.IsSpecies( MyFlag, RE ) )
                    retval = true;
            return retval;
        }

        private bool CheckUnitTypeFlag( TypeEnu FlagsToLookFor, TypeEnu MyFlag ) {
            bool retval = false;
            List<TypeEnu> ContainingFlag = new List<TypeEnu>();
            foreach ( TypeEnu TE in GeneralOperations.EnumToList<TypeEnu>() )
                if ( UnitTypeEnumFunctions.HasUnitType( FlagsToLookFor, TE ) )
                    ContainingFlag.Add( TE );
            foreach ( TypeEnu TE in ContainingFlag )
                if ( UnitTypeEnumFunctions.HasUnitType( MyFlag, TE ) )
                    retval = true;
            return retval;
        }

        #endregion

        #region Constructors

        //default constructor
        public CrewUnit() {
            Name = "";
            ID = "";
            Baseunit = new BaseUnit();
            Movement = 1;
            HitPoints = 1;
            HandToHand = 1;
            AttackPoints = 1;
            PsyPoints = 0;
            RangedWeapons = 1;
            Might = 1;
            Toughness = 1;
            Agility = 1;
            Willpower = 1;
            HorrorFactor = 0;

            _WPerms = new List<WeaponPermission>();
            _SPerms = new List<SkillPermission>();
            _PPerms = new List<PsyPermission>();

            _Weapons = new List<Weapon>();
            _Skills = new List<Skill>();
            _Psys = new List<Psy>();
            _Item = new List<Item>();
        }

        //constructor to accept current class object
        public CrewUnit( CrewUnit var ) {
            Name = var.Name;
            ID = var.ID;
            Baseunit = var.Baseunit;
            Movement = var.Movement;
            HitPoints = var.HitPoints;
            HandToHand = var.HandToHand;
            AttackPoints = var.AttackPoints;
            PsyPoints = var.PsyPoints;
            RangedWeapons = var.RangedWeapons;
            Might = var.Might;
            Toughness = var.Toughness;
            Agility = var.Agility;
            Willpower = var.Willpower;
            HorrorFactor = var.HorrorFactor;

            WPermissions = var.WPermissions;
            SPermissions = var.SPermissions;
            PPermissions = var.PPermissions;

            Weapons = var.Weapons;
            SkillsAndAbilities = var.SkillsAndAbilities;
            Psys = var.Psys;
            Items = var.Items;
        }

        //constructor that takes all vars
        public CrewUnit( string UnitName, string UnitID, BaseUnit UnitBaseTemplate,
           byte UnitMovement, byte UnitHitPoints, byte UnitHandToHand, byte UnitAttackPoints, byte UnitPsyPoints,
           byte UnitRangedWeapons, byte UnitMight, byte UnitToughness, byte UnitAgility, byte UnitWillpower,
           byte UnitHorrorFactor, 
           WeaponPermission[] UnitWPermissions, SkillPermission[] UnitSPermissions,
           PsyPermission[] UnitPPermissions, Weapon[] UnitWeapons, Skill[] UnitSkillsAndAbilities,
           Psy[] UnitPsys, Item[] UnitItems ) {

            Name = UnitName;
            ID = UnitID;
            Movement = UnitMovement;
            HitPoints = UnitHitPoints;
            HandToHand = UnitHandToHand;
            AttackPoints = UnitAttackPoints;
            PsyPoints = UnitPsyPoints;
            RangedWeapons = UnitRangedWeapons;
            Might = UnitMight;
            Toughness = UnitToughness;
            Agility = UnitAgility;
            Willpower = UnitWillpower;
            HorrorFactor = UnitHorrorFactor;

            WPermissions = UnitWPermissions;
            SPermissions = UnitSPermissions;
            PPermissions = UnitPPermissions;

            Weapons = UnitWeapons;
            SkillsAndAbilities = UnitSkillsAndAbilities;
            Psys = UnitPsys;
            Items = UnitItems;
        }

        #endregion

        #region IValidate Members

        public void Valid() {
            if ( _ID == "" ) throw new ValidationException( "ID cannot be blank" );
            if ( _Name == "" ) throw new ValidationException( "Name cannot be blank" );
        }

        #endregion

        #region IOwner Members

        private object owner = null;

        [XmlIgnore(), Browsable( false )]
        public object myOwner {
            get { return owner; }
            set { owner = value; }
        }

        #endregion

        #region IPrintable Members

        public void Print( PrintElement element ) {

        }

        #endregion

        #region operator overrides
        //not working
        public override bool Equals( object obj ) {
            bool retVal = true;
            if ( obj == null || !( obj is CrewUnit ) )
                retVal = false;

            if ( retVal )
                retVal = this.ID == ( ( CrewUnit )obj ).ID;

            if ( retVal )
                retVal = this.Name == ( ( CrewUnit )obj ).Name;

            //test stats
            if ( retVal )
                retVal = this.MovementTotal == ( ( CrewUnit )obj ).MovementTotal;
            if ( retVal )
                retVal = this.HitPointsTotal == ( ( CrewUnit )obj ).HitPointsTotal;
            if ( retVal )
                retVal = this.AttackPointsTotal == ( ( CrewUnit )obj ).AttackPointsTotal;
            if ( retVal )
                retVal = this.HandToHandTotal == ( ( CrewUnit )obj ).HandToHandTotal;
            if ( retVal )
                retVal = this.PsyPointsTotal == ( ( CrewUnit )obj ).PsyPointsTotal;
            if ( retVal )
                retVal = this.RangedWeaponsTotal == ( ( CrewUnit )obj ).RangedWeaponsTotal;
            if ( retVal )
                retVal = this.MightTotal == ( ( CrewUnit )obj ).MightTotal;
            if ( retVal )
                retVal = this.ToughnessTotal == ( ( CrewUnit )obj ).ToughnessTotal;
            if ( retVal )
                retVal = this.AgilityTotal == ( ( CrewUnit )obj ).AgilityTotal;
            if ( retVal )
                retVal = this.WillpowerTotal == ( ( CrewUnit )obj ).WillpowerTotal;
            if ( retVal )
                retVal = this.HorrorFactorTotal == ( ( CrewUnit )obj ).HorrorFactorTotal;
            if ( retVal )
                retVal = this.Baseunit == ( ( CrewUnit )obj ).Baseunit;

            if ( retVal )
                if ( this.WPermissions.Count() == ( ( CrewUnit )obj ).WPermissions.Count() ) {
                    foreach ( WeaponPermission item in ( ( CrewUnit )obj ).WPermissions )
                        if ( !this.WPermissions.Contains( item ) ) {
                            retVal = false;
                            break;
                        }
                }
                else { retVal = false; }

            if ( retVal )
                if ( this.SPermissions.Count() == ( ( CrewUnit )obj ).SPermissions.Count() ) {
                    foreach ( SkillPermission item in ( ( CrewUnit )obj ).SPermissions )
                        if ( !this.SPermissions.Contains( item ) ) {
                            retVal = false;
                            break;
                        }
                }
                else { retVal = false; }

            if ( retVal )
                if ( this.PPermissions.Count() == ( ( CrewUnit )obj ).PPermissions.Count() ) {
                    foreach ( PsyPermission item in ( ( CrewUnit )obj ).PPermissions )
                        if ( !this.PPermissions.Contains( item ) ) {
                            retVal = false;
                            break;
                        }
                }
                else { retVal = false; }

            if ( retVal )
                if ( this.Weapons.Count() == ( ( CrewUnit )obj ).Weapons.Count() ) {
                    foreach ( Weapon item in ( ( CrewUnit )obj ).Weapons )
                        if ( !this.Weapons.Contains( item ) ) {
                            retVal = false;
                            break;
                        }
                }
                else { retVal = false; }

            if ( retVal )
                if ( this.SkillsAndAbilities.Count() == ( ( CrewUnit )obj ).SkillsAndAbilities.Count() ) {
                    foreach ( Skill item in ( ( CrewUnit )obj ).SkillsAndAbilities )
                        if ( !this.SkillsAndAbilities.Contains( item ) ) {
                            retVal = false;
                            break;
                        }
                }
                else { retVal = false; }

            if ( retVal )
                if ( this.Psys.Count() == ( ( CrewUnit )obj ).Psys.Count() ) {
                    foreach ( Psy item in ( ( CrewUnit )obj ).Psys )
                        if ( !this.Psys.Contains( item ) ) {
                            retVal = false;
                            break;
                        }
                }
                else { retVal = false; }

            if ( retVal )
                if ( this.Items.Count() == ( ( CrewUnit )obj ).Items.Count() ) {
                    foreach ( Item item in ( ( CrewUnit )obj ).Items )
                        if ( !this.Items.Contains( item ) ) {
                            retVal = false;
                            break;
                        }
                }
                else { retVal = false; }


            return retVal;

        }

        static public bool operator ==( CrewUnit a, CrewUnit b ) {
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

        static public bool operator !=( CrewUnit a, CrewUnit b ) {
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

        #region IMemberCopy Members
        //new
        public void CopyMembers( object members ) {
            CrewUnit tmp;
            if ( members is CrewUnit )
                tmp = ( CrewUnit )members;
            else
                throw new InvalidParameter( "When copying members object type must be of type " + this.GetType().Name );
            if ( tmp != this ) {
                Settings.Default.Updating = true;
                ID = tmp.ID;
                Name = tmp.Name;
                Movement = tmp.Movement;
                HitPoints = tmp.HitPoints;
                HandToHand = tmp.HandToHand;
                AttackPoints = tmp.AttackPoints;
                PsyPoints = tmp.PsyPoints;
                RangedWeapons = tmp.RangedWeapons;
                Might = tmp.Might;
                Toughness = tmp.Toughness;
                Agility = tmp.Agility;
                Willpower = tmp.Willpower;
                HorrorFactor = tmp.HorrorFactor;
                WPermissions = tmp.WPermissions;
                SPermissions = tmp.SPermissions;
                PPermissions = tmp.PPermissions;
                Weapons = tmp.Weapons;
                SkillsAndAbilities = tmp.SkillsAndAbilities;
                Psys = tmp.Psys;
                Items = tmp.Items;
                Settings.Default.Updating = false;

                if ( EventMyDataChanged != null )
                    EventMyDataChanged();
            }
        }

        #endregion
    }
}

