using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using NTAF.Core.Properties;
using NTAF.Core;
using NTAF.Core;
using NTAF.PrintEngine;
using NTAF.PlugInFramework;
using NTAF.Core;

namespace NTAF.ObjectClasses {
    using PrintEngine = NTAF.PrintEngine.PrintEngine;
    using NTAF.Permissions;

    [ObjectClassPlugIn( "BaseUnit", "0.0.0.0" ),
     Serializable()]
    public class BaseUnit : ObjectClassBase {

        #region AboutMe

        [XmlIgnore(), Browsable( false )]
        public override string aboutMe {
            get {
                String retVal = base.aboutMe;

                retVal += String.Format( "Race: {0}\n\n", this.BaseRace.Name );
                retVal += String.Format( "Unit Type: {0}\n\n", this.archetype.Name );
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

                retVal += String.Format( "\nDescription: \n{0}\n", GeneralOperations.WrapLength( this.Description, 50 ) );

                retVal += String.Format( "\nCost: {0}\n", this.Cost );

                return retVal;
            }
        }

        #endregion

        #region Clone

        protected override void clearMyEvents() {
            base.clearMyEvents();

            EventCostChanged = null;
            EventDescriptionChanged = null;
            EventItemsChanged = null;
            MyDataChanged = null;
            EventPsyPermissionsChanged = null;
            EventPsysChanged = null;
            EventRaceChanged = null;
            EventSkillPermissionsChanged = null;
            EventSkillsChanged = null;
            EventSpaecialChanged = null;
            EventStatChanged = null;
            EventUnitTypeChanged = null;
            EventWeaponPermissionsChanged = null;
            EventWeaponsChanged = null;
        }

        #endregion

        #region Compairers

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

                if ( val1.ActionPoints > val2.ActionPoints )
                    retVal = 1;
                if ( val1.ActionPoints < val2.ActionPoints )
                    retVal = -1;

                return retVal;
            }
        }

        private class sortAttackPointsHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                BaseUnit val1 = ( BaseUnit )a;
                BaseUnit val2 = ( BaseUnit )b;
                int retVal = 0;

                if ( val1.ActionPoints < val2.ActionPoints )
                    retVal = 1;
                if ( val1.ActionPoints > val2.ActionPoints )
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

        #region Constructors
        public BaseUnit() {
            Name = "";
            ID = "";
            archetype = new Archetype();
            BaseRace = new Race();
            Movement = 1;
            HitPoints = 1;
            HandToHand = 1;
            ActionPoints = 1;
            PsyPoints = 0;
            RangedWeapons = 1;
            Might = 1;
            Toughness = 1;
            Agility = 1;
            Willpower = 1;
            HorrorFactor = 0;
            CostMod = 0;

            Description = "";
            Special = "";

            _WPerms = new List<WeaponPermission>();
            _SPerms = new List<SkillPermission>();
            _PPerms = new List<PsyPermission>();

            _Weapons = new List<Weapon>();
            _Skills = new List<Skill>();
            _Psys = new List<Psy>();
            _Item = new List<Item>();
        }

        //constructor to accept current class object
        public BaseUnit( BaseUnit var ) {
            Name = var.Name;
            ID = var.ID;
            archetype = var.archetype;
            BaseRace = var.BaseRace;
            Movement = var.Movement;
            HitPoints = var.HitPoints;
            HandToHand = var.HandToHand;
            ActionPoints = var.ActionPoints;
            PsyPoints = var.PsyPoints;
            RangedWeapons = var.RangedWeapons;
            Might = var.Might;
            Toughness = var.Toughness;
            Agility = var.Agility;
            Willpower = var.Willpower;
            HorrorFactor = var.HorrorFactor;
            CostMod = var.CostMod;

            Description = var.Description;
            Special = var.Special;

            WPermissions = var.WPermissions;
            SPermissions = var.SPermissions;
            PPermissions = var.PPermissions;

            Weapons = var.Weapons;
            SkillsAndAbilities = var.SkillsAndAbilities;
            Psys = var.Psys;
            Items = var.Items;
        }

        //constructor that takes all vars
        public BaseUnit( string buName, string buBaseID, Archetype buBaseType, Race buBaseRace,
           byte buMovement, byte buHitPoints, byte buHandToHand, byte buAttackPoints, byte buPsyPoints,
           byte buRangedWeapons, byte buMight, byte buToughness, byte buAgility, byte buWillpower,
           Int32 buCostMod, byte buHorrorFactor, string buStartingEXP, string buDescription,
           string buSpecial, WeaponPermission[] buWPermissions, SkillPermission[] buSPermissions,
           PsyPermission[] buPPermissions, Weapon[] buWeapons, Skill[] buSkillsAndAbilities,
           Psy[] buPsys, Item[] buItems ) {

            Name = buName;
            ID = buBaseID;
            archetype = buBaseType;
            BaseRace = buBaseRace;
            Movement = buMovement;
            HitPoints = buHitPoints;
            HandToHand = buHandToHand;
            ActionPoints = buAttackPoints;
            PsyPoints = buPsyPoints;
            RangedWeapons = buRangedWeapons;
            Might = buMight;
            Toughness = buToughness;
            Agility = buAgility;
            Willpower = buWillpower;
            HorrorFactor = buHorrorFactor;
            CostMod = buCostMod;

            Description = buDescription;
            Special = buSpecial;

            WPermissions = buWPermissions;
            SPermissions = buSPermissions;
            PPermissions = buPPermissions;

            Weapons = buWeapons;
            SkillsAndAbilities = buSkillsAndAbilities;
            Psys = buPsys;
            Items = buItems;
        }

        #endregion

        #region Events
        
        public delegate void StatChanged( StatsMod.Stats Stat );
        public event StatChanged EventStatChanged;
        public event NTEventHandler EventCostChanged;
        public event NTEventHandler EventUnitTypeChanged;
        public event NTEventHandler EventRaceChanged;
        public event NTEventHandler EventDescriptionChanged;
        public event NTEventHandler EventSpaecialChanged;
        public override event NTEventHandler MyDataChanged;

        #endregion

        #region Fields
        public const Byte
            MAX_PERCENT_STAT = 12,
            MIN_PERCENT_STAT = 1;

        public const Byte
            MAX_POINT_STAT = 255,
            MIN_POINT_STAT = 0;

        Byte
            _Movement,
            _HitPoints, 
            _HandToHand, 
            _ActionPoints,
            _PsyPoints,
            _RangedWeapons,
            _Might,
            _Toughness,
            _Agility,
            _Willpower,
            _HorrorFactor;

        Int32 
            _CostMod;

        Race 
            _Race;

        Archetype 
            _Archetype;

        String
            _Description,
            _Special;

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
            if ( this.BaseRace.ID == "" )
                throw new RaceException( "Race hasnot ben set" );

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
            bool 
                ProperArchietype = false,
                ProperSpeciesOrRace = false;

            //Check based on archietype
            if ( item.TypesCanUse != ArchetypeBaseEnu.All ) {//its for a specfic type
                if ( CheckUnitTypeFlag( item.TypesCanUse, this.archetype.BaseType ) )
                    ProperArchietype = true;
            }
            else
                ProperArchietype = true;

            //check based on race and species
            if ( item.SpeciesCanEquip != 0 )
                if ( CheckRaceFlag( item.SpeciesCanEquip, this.BaseRace.species ) )
                    ProperSpeciesOrRace = true;
                else {
                    if ( item.RaceCanEquip == this.BaseRace )
                        ProperSpeciesOrRace = true;
                }

            return ProperArchietype & ProperSpeciesOrRace;
        }

        //returns true if the permission can be used by the currently selected race
        private bool PermissionFilter( object PERM ) {
            if ( this.BaseRace.ID == "" )
                throw new RaceException( "Race hasnot ben set" );

            bool retval = false;
            Race SubRaceCanEquip = new Race();
            Species BaseRacesCanEquip=new Species();
            if ( PERM is WSPPermission ) {
                SubRaceCanEquip = ( ( WSPPermission )PERM ).RaceCanEquip;
                BaseRacesCanEquip = ( ( WSPPermission )PERM ).SpeciesCanEquip;
            }

            if ( BaseRacesCanEquip != Species.All ) {
                if ( SubRaceCanEquip != null )
                    if ( SubRaceCanEquip.ID != "" ) {
                        if ( SubRaceCanEquip.ID == this.BaseRace.ID )
                            retval = true;
                    }
                    else {
                        if ( CheckRaceFlag( BaseRacesCanEquip, this.BaseRace.species ) )//enumerates possible flags list
                            retval = true;
                    }
                else {
                    if ( CheckRaceFlag( BaseRacesCanEquip, this.BaseRace.species ) )//enumerates possible flags list
                        retval = true;
                }
            }
            else
                retval = true;
            return retval;
        }
        
        //enumerates possible flags list
        private bool CheckRaceFlag( Species FlagsToLookFor, Species MyFlag ) {
            bool retval = false;
            List<Species> ContainingFlag = new List<Species>();
            foreach ( Species RE in GeneralOperations.EnumToList<Species>() )
                if ( RE.Is( FlagsToLookFor ) & RE != Species.All )
                    ContainingFlag.Add( RE );
            foreach ( Species RE in ContainingFlag )
                if ( RE.Is( MyFlag ) )
                    retval = true;
            return retval;
        }

        private bool CheckUnitTypeFlag( ArchetypeBaseEnu FlagsToLookFor, ArchetypeBaseEnu MyFlag ) {
            bool retval = false;
            List<ArchetypeBaseEnu> ContainingFlag = new List<ArchetypeBaseEnu>();
            foreach ( ArchetypeBaseEnu TE in GeneralOperations.EnumToList<ArchetypeBaseEnu>() )
                if ( TE.Is( FlagsToLookFor ) )
                    ContainingFlag.Add( TE );
            foreach ( ArchetypeBaseEnu TE in ContainingFlag )
                if ( TE.Is( MyFlag ) )
                    retval = true;
            return retval;
        }

        #endregion

        #region Linker

        public override void Link( ILink DataMaster ) {
            archetype = ( Archetype )DataMaster.FindObject( archetype );
            BaseRace = ( Race )DataMaster.FindObject( BaseRace );

            List<Item>
                CurrentItems = new List<Item>( Items );

            clearItems();

            foreach ( Item itm in CurrentItems )
                AddItem( ( Item )DataMaster.FindObject( itm ) );

            List<WSPPermission>
                CurrentPerms = new List<WSPPermission>( permissions );

            clearWPermissions();
            clearSPermissions();
            clearPPermissions();

            foreach ( WSPPermission itm in CurrentPerms ){
                if ( itm is WeaponPermission )
                    AddWeaponPermission( ( WeaponPermission )DataMaster.FindObject( itm ) );
                if ( itm is SkillPermission )
                    AddSkillPermission( ( SkillPermission )DataMaster.FindObject( itm ) );
                if ( itm is PsyPermission )
                    AddPsyPermission( ( PsyPermission )DataMaster.FindObject( itm ) );
            }
        }

        public override void ReplaceReferences( ObjectClassBase ObjectToReplace, ObjectClassBase ReplaceWith ) {
            if ( ObjectToReplace is Archetype )
                archetype = ( Archetype )ReplaceWith;

            if ( ObjectToReplace is Race )
                BaseRace = ( Race )ReplaceWith;

            if ( ObjectToReplace is Item )
                if ( _Item.Contains( ( Item )ObjectToReplace ) ) {
                    _Item.Remove( ( Item )ObjectToReplace );
                    _Item.Add( ( Item )ReplaceWith );
                }
            
            if ( ObjectToReplace is SkillPermission )
                if ( _SPerms.Contains( ( SkillPermission )ObjectToReplace ) ) {
                    _SPerms.Remove( ( SkillPermission )ObjectToReplace );
                    _SPerms.Add( ( SkillPermission )ReplaceWith );
                }

            if ( ObjectToReplace is WeaponPermission )
                if ( _WPerms.Contains( ( WeaponPermission )ObjectToReplace ) ) {
                    _WPerms.Remove( ( WeaponPermission )ObjectToReplace );
                    _WPerms.Add( ( WeaponPermission )ReplaceWith );
                }

            if ( ObjectToReplace is PsyPermission )
                if ( _PPerms.Contains( ( PsyPermission ) ObjectToReplace ) ) {
                    _PPerms.Remove( ( PsyPermission )ObjectToReplace );
                    _PPerms.Add( ( PsyPermission )ReplaceWith );
                }
        }

        public override bool CheckForReferences( ObjectClassBase item ) {
            if ( archetype == item )
                return true;

            if ( BaseRace == item )
                return true;

            if ( item is Item )
                if ( _Item.Contains( ( Item )item ) )
                    return true;

            if ( item is SkillPermission )
                if ( _SPerms.Contains( ( SkillPermission )item ) )
                    return true;

            if ( item is WeaponPermission )
                if ( _WPerms.Contains( ( WeaponPermission )item ) )
                    return true;

            if ( item is PsyPermission )
                if ( _PPerms.Contains( ( PsyPermission )item ) )
                    return true;

            return false;
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
            if ( this.BaseRace.ID == "" ) {
                throw new RaceException( "You have not selected a race" );
            }

            if ( this.archetype.ID == "" ) {
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

        #region Methods

        public override string ToString() {
            string retVal = Name;
            if ( NTAF.Core.Properties.Settings.Default.VerboseToString ) {
                retVal = "";
                if ( NTAF.Core.Properties.Settings.Default.VerboseID )
                    retVal += ID + ": ";
                if ( NTAF.Core.Properties.Settings.Default.VerboseName )
                    retVal += Name;
                if ( NTAF.Core.Properties.Settings.Default.VerboseOther ) {
                    retVal += "( " + BaseRace.Name + ", " + archetype.Name + " )";
                }
                if ( NTAF.Core.Properties.Settings.Default.VerboseDescription ) {
                    string DescriptionShort = ( Description.Length < 120 ? ( Description.Length > 0 ? Description.Substring( 0, Description.Length ) : "" ) : Description.Substring( 0, 120 ) + "..." );
                    retVal += ", " + DescriptionShort;
                }

            }
            return retVal;
        }

        public override DataMember[] getDataMembers() {
            string wperms = "";
            foreach ( WeaponPermission wpm in _WPerms )
                wperms += wpm.Name + ", ";
            wperms = wperms.TrimEnd( new[] { ',', ' ' } );

            string sperms = "";
            foreach ( SkillPermission wpm in _SPerms )
                sperms += wpm.Name + ", ";
            sperms = sperms.TrimEnd( new[] { ',', ' ' } );

            string pperms = "";
            foreach ( PsyPermission wpm in _PPerms )
                pperms += wpm.Name + ", ";
            pperms = pperms.TrimEnd( new[] { ',', ' ' } );

            string weps = "";
            foreach ( Weapon wpm in _Weapons )
                weps += wpm.Name + ", ";
            weps = weps.TrimEnd( new[] { ',', ' ' } );

            string skls = "";
            foreach ( Skill wpm in _Skills )
                skls += wpm.Name + ", ";
            skls = skls.TrimEnd( new[] { ',', ' ' } );

            string psys = "";
            foreach ( Psy wpm in _Psys )
                psys += wpm.Name + ", ";
            psys = psys.TrimEnd( new[] { ',', ' ' } );

            string itms = "";
            foreach ( Item wpm in _Item )
                itms += wpm.Name + ", ";
            itms = itms.TrimEnd( new[] { ',', ' ' } );

            return new[] {
                new DataMember("Name", Name),
                new DataMember("Description", _Race.Name),
                new DataMember("Description", _Archetype.Name),
                new DataMember("Description", Description),
                new DataMember("Movement", Movement),
                new DataMember("HitPoints", HitPoints),
                new DataMember("HandToHand", HandToHand),
                new DataMember("ActionPoints", ActionPoints),
                new DataMember("PsyPoints", PsyPoints),
                new DataMember("RangedWeapons", RangedWeapons),
                new DataMember("Might", Might),
                new DataMember("Toughness", Toughness),
                new DataMember("Agility", Agility),
                new DataMember("Willpower", Willpower),
                new DataMember("HorrorFactor", HorrorFactor),
                new DataMember("Weapon Permissions", wperms),
                new DataMember("Skill Permissions", sperms),
                new DataMember("Psy Permissions", pperms),
                new DataMember("Weapons", weps),
                new DataMember("Skills", skls),
                new DataMember("Psy Ablilites", psys),
                new DataMember("Items", itms),
                new DataMember("Special", Special),
                new DataMember("Cost Modifier",CostMod),
                new DataMember("Cost", Cost)
            };
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

        #region operator overloads

        public override bool Equals( object obj ) {
            bool retVal = true;
            if ( obj == null || !( obj is BaseUnit ) )
                retVal = false;

            if ( retVal )
                retVal = this.ID == ( ( BaseUnit )obj ).ID;

            if ( retVal )
                retVal = this.Name == ( ( BaseUnit )obj ).Name;

            //test stats
            if ( retVal )
                retVal = this.MovementTotal == ( ( BaseUnit )obj ).MovementTotal;
            if ( retVal )
                retVal = this.HitPointsTotal == ( ( BaseUnit )obj ).HitPointsTotal;
            if ( retVal )
                retVal = this.AttackPointsTotal == ( ( BaseUnit )obj ).AttackPointsTotal;
            if ( retVal )
                retVal = this.HandToHandTotal == ( ( BaseUnit )obj ).HandToHandTotal;
            if ( retVal )
                retVal = this.PsyPointsTotal == ( ( BaseUnit )obj ).PsyPointsTotal;
            if ( retVal )
                retVal = this.RangedWeaponsTotal == ( ( BaseUnit )obj ).RangedWeaponsTotal;
            if ( retVal )
                retVal = this.MightTotal == ( ( BaseUnit )obj ).MightTotal;
            if ( retVal )
                retVal = this.ToughnessTotal == ( ( BaseUnit )obj ).ToughnessTotal;
            if ( retVal )
                retVal = this.AgilityTotal == ( ( BaseUnit )obj ).AgilityTotal;
            if ( retVal )
                retVal = this.WillpowerTotal == ( ( BaseUnit )obj ).WillpowerTotal;
            if ( retVal )
                retVal = this.HorrorFactorTotal == ( ( BaseUnit )obj ).HorrorFactorTotal;
            if ( retVal )
                retVal = this.BaseRace == ( ( BaseUnit )obj ).BaseRace;
            if ( retVal )
                retVal = this.Description == ( ( BaseUnit )obj ).Description;
            if ( retVal )
                retVal = this.Special == ( ( BaseUnit )obj ).Special;

            if ( retVal )
                if ( this.WPermissions.Count() == ( ( BaseUnit )obj ).WPermissions.Count() ) {
                    foreach ( WeaponPermission item in ( ( BaseUnit )obj ).WPermissions )
                        if ( !this.WPermissions.Contains( item ) ) {
                            retVal = false;
                            break;
                        }
                }
                else { retVal = false; }

            if ( retVal )
                if ( this.SPermissions.Count() == ( ( BaseUnit )obj ).SPermissions.Count() ) {
                    foreach ( SkillPermission item in ( ( BaseUnit )obj ).SPermissions )
                        if ( !this.SPermissions.Contains( item ) ) {
                            retVal = false;
                            break;
                        }
                }
                else { retVal = false; }

            if ( retVal )
                if ( this.PPermissions.Count() == ( ( BaseUnit )obj ).PPermissions.Count() ) {
                    foreach ( PsyPermission item in ( ( BaseUnit )obj ).PPermissions )
                        if ( !this.PPermissions.Contains( item ) ) {
                            retVal = false;
                            break;
                        }
                }
                else { retVal = false; }

            if ( retVal )
                if ( this.Weapons.Count() == ( ( BaseUnit )obj ).Weapons.Count() ) {
                    foreach ( Weapon item in ( ( BaseUnit )obj ).Weapons )
                        if ( !this.Weapons.Contains( item ) ) {
                            retVal = false;
                            break;
                        }
                }
                else { retVal = false; }

            if ( retVal )
                if ( this.SkillsAndAbilities.Count() == ( ( BaseUnit )obj ).SkillsAndAbilities.Count() ) {
                    foreach ( Skill item in ( ( BaseUnit )obj ).SkillsAndAbilities )
                        if ( !this.SkillsAndAbilities.Contains( item ) ) {
                            retVal = false;
                            break;
                        }
                }
                else { retVal = false; }

            if ( retVal )
                if ( this.Psys.Count() == ( ( BaseUnit )obj ).Psys.Count() ) {
                    foreach ( Psy item in ( ( BaseUnit )obj ).Psys )
                        if ( !this.Psys.Contains( item ) ) {
                            retVal = false;
                            break;
                        }
                }
                else { retVal = false; }

            if ( retVal )
                if ( this.Items.Count() == ( ( BaseUnit )obj ).Items.Count() ) {
                    foreach ( Item item in ( ( BaseUnit )obj ).Items )
                        if ( !this.Items.Contains( item ) ) {
                            retVal = false;
                            break;
                        }
                }
                else { retVal = false; }


            return retVal;

        }

        static public bool operator ==( BaseUnit a, BaseUnit b ) {
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

        static public bool operator !=( BaseUnit a, BaseUnit b ) {
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

        #region Printing

        public override void Print( PrintElement element ) {
            string tmpString = "";

            element.AddTitleText( "Base Unit Type: " + Name );

            element.AddText( String.Format( "Archetype: {0}  Race: {1}", archetype.Name, BaseRace.Name ) );

            element.AddPrimitive( new PrintStatGrid( MovementTotal, HitPointsTotal, HandToHandTotal, AttackPointsTotal, PsyPointsTotal, RangedWeaponsTotal, MightTotal, ToughnessTotal, AgilityTotal, WillpowerTotal, HorrorFactorTotal ) );

            element.AddText( String.Format( "Cost: {0}   Starting EXP: {1}", Cost, StartingEXP ) );

            foreach ( Permission item in permissions )
                tmpString += item.Name + ", ";
            tmpString = tmpString.TrimEnd( ',' );
            element.AddBlankLine();
            element.AddCategoryText( "Permissions:" );
            element.AddMText( tmpString );

            tmpString = "";

            foreach ( Weapon item in Weapons )
                tmpString += item.Name + ", ";
            tmpString = tmpString.TrimEnd( ',' );
            element.AddBlankLine();
            element.AddCategoryText( "Weapons:" );
            element.AddMText( tmpString );

            tmpString = "";

            foreach ( Psy item in Psys )
                tmpString += item.Name + ", ";
            tmpString = tmpString.TrimEnd( ',' );
            element.AddBlankLine();
            element.AddCategoryText( "Psys:" );
            element.AddMText( tmpString );

            tmpString = "";

            foreach ( Skill item in SkillsAndAbilities )
                tmpString += item.Name + ", ";
            tmpString = tmpString.TrimEnd( ',' );
            element.AddBlankLine();
            element.AddCategoryText( "Skills And Abilities:" );
            element.AddMText( tmpString );

            tmpString = "";

            foreach ( Item item in Items )
                tmpString += item.Name + ", ";
            tmpString = tmpString.TrimEnd( ',' );
            element.AddBlankLine();
            element.AddCategoryText( "Items:" );
            element.AddMText( tmpString );

            tmpString = "";

            element.AddBlankLine();
            element.AddCategoryText( "Description:" );
            element.AddMText( Description );

            element.AddBlankLine();
            element.AddCategoryText( "Special notes about unit:" );
            element.AddMText( Special );

            element.AddBlankLine();
        }

        #region Specialty PrintPrimitives
        public class PrintStatGrid : IPrintPrimitive {
            const float
            buffer = 4;

            private String[] StatValues = new String[11];

            String[]
                StatHeaderText = new String[] { 
                    "M", 
                    "HP", 
                    "H+H", 
                    "AP",
                    "PP", 
                    "RW",
                    "MT", 
                    "T",
                    "AG", 
                    "WP", 
                    "HF" };

            //String[]
            //    StatHeaderText = new String[] { 
            //        "Movement", 
            //        "Hit Points", 
            //        "Hand to Hand", 
            //        "Actions",
            //        "Psy Points", 
            //        "Ranged Weapons",
            //        "Might", 
            //        "Toughness",
            //        "Agility", 
            //        "Willpower", 
            //        "Horror Factor" };


            public PrintStatGrid( int M, int HP, int HH, int AP, int PP, int RW, int MT, int T, int AG, int WP, int HF ) {
                StatValues[0] = M.ToString();
                StatValues[1] = HP.ToString();
                StatValues[2] = HH.ToString();
                StatValues[3] = AP.ToString();
                StatValues[4] = PP.ToString();
                StatValues[5] = RW.ToString();
                StatValues[6] = MT.ToString();
                StatValues[7] = T.ToString();
                StatValues[8] = AG.ToString();
                StatValues[9] = WP.ToString();
                StatValues[10] = HF.ToString();
            }

            public float CalculateHeight( PrintEngine engine, Graphics graphics ) {
                float 
                retVal = 0;

                retVal += engine.PrintFont.GetHeight( graphics ) * 2;
                retVal += buffer * 4;
                return retVal;
            }

            public void Draw( PrintEngine engine, float yPos, Graphics graphics, Rectangle elementBounds ) {
                List<float>
                HorizontalLineSpacing  = new List<float>(),
                HorizontalTextSpacing = new List<float>();

                Single[]
                colWidths = new Single[11];

                HorizontalLineSpacing.Add( ( float )elementBounds.Left );
                for ( int space = 0; space <= 10; space++ )
                    if ( graphics.MeasureString( StatHeaderText[space], engine.PrintFont ).Width
                        >=
                        graphics.MeasureString( StatValues[space], engine.PrintFont ).Width )
                        HorizontalLineSpacing.Add( ( buffer * 2 ) + HorizontalLineSpacing[space] + graphics.MeasureString( StatHeaderText[space], engine.PrintFont ).Width );
                    else
                        HorizontalLineSpacing.Add( ( buffer * 2 ) + HorizontalLineSpacing[space] + graphics.MeasureString( StatValues[space], engine.PrintFont ).Width );

                for ( int space = 0; space <= 10; space++ )
                    HorizontalTextSpacing.Add( ( ( HorizontalLineSpacing[space + 1] - HorizontalLineSpacing[space] ) / 2 ) + HorizontalLineSpacing[space] );

                Pen pen = new Pen( engine.PrintBrush, 1 );
                foreach ( float x in HorizontalLineSpacing )
                    graphics.DrawLine( pen, x, yPos, x, yPos + this.CalculateHeight( engine, graphics ) );

                graphics.DrawLine( pen, HorizontalLineSpacing[0], yPos, HorizontalLineSpacing[11], yPos );
                graphics.DrawLine( pen, HorizontalLineSpacing[0], yPos + this.CalculateHeight( engine, graphics ) / 2, HorizontalLineSpacing[11], yPos + this.CalculateHeight( engine, graphics ) / 2 );
                graphics.DrawLine( pen, HorizontalLineSpacing[0], yPos + this.CalculateHeight( engine, graphics ), HorizontalLineSpacing[11], yPos + this.CalculateHeight( engine, graphics ) );

                for ( int space = 0; space <= 10; space++ ) {
                    graphics.DrawString( StatHeaderText[space], engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing[space], yPos + buffer + ( float )( ( float )engine.PrintFont.Height * 0.5 ), StringHelper.AlignTC() );
                    graphics.DrawString( StatValues[space], engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing[space], yPos + ( buffer * 3 ) + pen.Width + ( float )( ( float )engine.PrintFont.Height * 1.5 ), StringHelper.AlignTC() );

                }
            }
        }
        #endregion

        #endregion

        #region Properties
        [Category( TagCategory.Base ),
        Description( "" )]
        public Archetype archetype {
            get { return _Archetype; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _Archetype = value;
                if ( EventUnitTypeChanged != null )
                    EventUnitTypeChanged();

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [Category( TagCategory.Base ),
        Description( "" )]
        public Race BaseRace {
            get { return _Race; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( _Race == null ) _Race = value;
                else {
                    _Race = value;
                    if ( !Settings.Default.Loading )
                        ValidatePermAndItems();
                }
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {

                    if ( EventRaceChanged != null )
                        EventRaceChanged();
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte Movement {
            get { return _Movement; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( value < MIN_POINT_STAT )
                    throw new StatException( StatsMod.Stats.Movement, StatException.StatExceptionType.ToSmall, MIN_POINT_STAT, MAX_POINT_STAT );

                _Movement = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.Movement );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [XmlIgnore(), Category( TagCategory.StatModifiers ),
        Description( "" )]
        public int MovementMod {
            get { return getStatMod( StatsMod.Stats.Movement ); }
        }

        [XmlIgnore(), Category( TagCategory.StatTotals ),
        Description( "" )]
        public int MovementTotal {
            get { return ( int )Movement + MovementMod; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte HitPoints {
            get { return _HitPoints; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( value < MIN_POINT_STAT )
                    throw new StatException( StatsMod.Stats.HitPoints, StatException.StatExceptionType.ToSmall, MIN_POINT_STAT, MAX_POINT_STAT );

                _HitPoints = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.HitPoints );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( MyDataChanged != null )
                        MyDataChanged();
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
            get { return ( int )HitPoints + HitPointsMod; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte HandToHand {
            get { return _HandToHand; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( value > MAX_PERCENT_STAT )
                    throw new StatException( StatsMod.Stats.HandToHand, StatException.StatExceptionType.ToLarge, MIN_PERCENT_STAT, MAX_PERCENT_STAT );

                if ( value < MIN_PERCENT_STAT )
                    throw new StatException( StatsMod.Stats.HandToHand, StatException.StatExceptionType.ToSmall, MIN_PERCENT_STAT, MAX_PERCENT_STAT );

                _HandToHand = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.HandToHand );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( MyDataChanged != null )
                        MyDataChanged();
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
            get { return ( int )HandToHand + HandToHandMod; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte ActionPoints {
            get { return _ActionPoints; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( value < MIN_POINT_STAT )
                    throw new StatException( StatsMod.Stats.AttackPoints, StatException.StatExceptionType.ToSmall, MIN_POINT_STAT, MAX_POINT_STAT );
                _ActionPoints = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.AttackPoints );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( MyDataChanged != null )
                        MyDataChanged();
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
            get { return ( int )ActionPoints + AttackPointsMod; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte PsyPoints {
            get { return _PsyPoints; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( value < MIN_POINT_STAT )
                    throw new StatException( StatsMod.Stats.PsyPoints, StatException.StatExceptionType.ToSmall, MIN_POINT_STAT, MAX_POINT_STAT );
                _PsyPoints = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.PsyPoints );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( MyDataChanged != null )
                        MyDataChanged();
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
            get { return ( int )PsyPoints + PsyPointsMod; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte RangedWeapons {
            get { return _RangedWeapons; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( value > MAX_PERCENT_STAT )
                    throw new StatException( StatsMod.Stats.RangedWeapons, StatException.StatExceptionType.ToLarge, MIN_PERCENT_STAT, MAX_PERCENT_STAT );

                if ( value < MIN_PERCENT_STAT )
                    throw new StatException( StatsMod.Stats.RangedWeapons, StatException.StatExceptionType.ToSmall, MIN_PERCENT_STAT, MAX_PERCENT_STAT );

                _RangedWeapons = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.RangedWeapons );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( MyDataChanged != null )
                        MyDataChanged();
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
            get { return ( int )RangedWeapons + RangedWeaponsMod; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte Might {
            get { return _Might; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( value > MAX_PERCENT_STAT )
                    throw new StatException( StatsMod.Stats.Might, StatException.StatExceptionType.ToLarge, MIN_PERCENT_STAT, MAX_PERCENT_STAT );

                if ( value < MIN_PERCENT_STAT )
                    throw new StatException( StatsMod.Stats.Might, StatException.StatExceptionType.ToSmall, MIN_PERCENT_STAT, MAX_PERCENT_STAT );

                _Might = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.Might );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( MyDataChanged != null )
                        MyDataChanged();
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
            get { return ( int )Might + MightMod; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte Toughness {
            get { return _Toughness; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( value > MAX_PERCENT_STAT )
                    throw new StatException( StatsMod.Stats.Toughness, StatException.StatExceptionType.ToLarge, MIN_PERCENT_STAT, MAX_PERCENT_STAT );

                if ( value < MIN_PERCENT_STAT )
                    throw new StatException( StatsMod.Stats.Toughness, StatException.StatExceptionType.ToSmall, MIN_PERCENT_STAT, MAX_PERCENT_STAT );

                _Toughness = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.Toughness );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( MyDataChanged != null )
                        MyDataChanged();
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
            get { return ( int )Toughness + ToughnessMod; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte Agility {
            get { return _Agility; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( value > MAX_PERCENT_STAT )
                    throw new StatException( StatsMod.Stats.Agility, StatException.StatExceptionType.ToLarge, MIN_PERCENT_STAT, MAX_PERCENT_STAT );

                if ( value < MIN_PERCENT_STAT )
                    throw new StatException( StatsMod.Stats.Agility, StatException.StatExceptionType.ToSmall, MIN_PERCENT_STAT, MAX_PERCENT_STAT );

                _Agility = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.Agility );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( MyDataChanged != null )
                        MyDataChanged();
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
            get { return ( int )Agility + AgilityMod; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte Willpower {
            get { return _Willpower; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( value > MAX_PERCENT_STAT )
                    throw new StatException( StatsMod.Stats.Willpower, StatException.StatExceptionType.ToLarge, MIN_PERCENT_STAT, MAX_PERCENT_STAT );

                if ( value < MIN_PERCENT_STAT )
                    throw new StatException( StatsMod.Stats.Willpower, StatException.StatExceptionType.ToSmall, MIN_PERCENT_STAT, MAX_PERCENT_STAT );

                _Willpower = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.Willpower );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( MyDataChanged != null )
                        MyDataChanged();
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
            get { return ( int )Willpower + WillpowerMod; }
        }

        [XmlAttribute(), Category( TagCategory.BaseStat ),
        Description( "" )]
        public byte HorrorFactor {
            get { return _HorrorFactor; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _HorrorFactor = value;

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatChanged != null )
                        EventStatChanged( StatsMod.Stats.HorrorFactor );

                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( MyDataChanged != null )
                        MyDataChanged();
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
            get { return ( int )HorrorFactor + HorrorFactorMod; }
        }

        [XmlAttribute(), Category( TagCategory.Cost ),
        Description( "" )]
        public Int32 CostMod {
            get { return _CostMod; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _CostMod = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventCostChanged != null )
                        EventCostChanged();
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
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
                retVal += ( Int32 )( ActionPoints * 5 );
                retVal += CostMod;

                foreach ( Weapon wpn in Weapons )
                    retVal += ( Int32 )( wpn.Cost );
                foreach ( Skill skl in SkillsAndAbilities )
                    retVal += ( Int32 )( skl.Cost );
                foreach ( Item itm in Items )
                    retVal += ( Int32 )itm.Cost;

                if ( archetype != null )
                    retVal += archetype.CostModifier;
                return retVal;
            }
        }

        [XmlAttribute(), Category( TagCategory.Experience ),
        Description( "" )]
        public string StartingEXP {
            get {
                string retval = "";
                if ( archetype != null )
                    retval = archetype.StartingEXP;
                return retval;
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

                    if ( EventSpaecialChanged != null )
                        EventSpaecialChanged();
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [Category( TagCategory.Permissions ),
        Description( "" )]
        public WeaponPermission[] WPermissions {
            get { return this._WPerms.ToArray(); }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
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
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [Category( TagCategory.Permissions ),
        Description( "" )]
        public SkillPermission[] SPermissions {
            get { return this._SPerms.ToArray(); }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
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
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [Category( TagCategory.Permissions ),
        Description( "" )]
        public PsyPermission[] PPermissions {
            get { return this._PPerms.ToArray(); }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
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
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [XmlIgnore(), Browsable( false )]
        public WSPPermission[] permissions {
            get {
                List<WSPPermission> retVal = new List<WSPPermission>(); ;
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
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
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
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [Category( TagCategory.ItmesAbilities ),
        Description( "" )]
        public Skill[] SkillsAndAbilities {
            get { return this._Skills.ToArray(); }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
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
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [Category( TagCategory.ItmesAbilities ),
        Description( "" )]
        public Psy[] Psys {
            get { return this._Psys.ToArray(); }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
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
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [Category( TagCategory.ItmesAbilities ),
        Description( "" )]
        public Item[] Items {
            get { return this._Item.ToArray(); }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
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
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }


        #endregion

        #region Type methods

        public override Type CollectionType {
            get { return typeof( BaseUnit ); }
        }

        public override bool IsOfType( object obj ) {
            return obj is BaseUnit;
        }

        public override bool IsOfType( Type T ) {
            return T == typeof( BaseUnit );
        }

        public override Type MyType() { return CollectionType; }

        #endregion

        #region Vallidation
        public override void Valid() {
            List<FieldAndMessage>
                ErrorList = new List<FieldAndMessage>(),
                WarningList = new List<FieldAndMessage>();

            try {
                base.Valid();
            }
            catch ( ValidationException VE ) {
                ErrorList.AddRange( VE.Errors.ToArray() );
            }
            finally {

                if ( archetype == null ) ErrorList.Add( new FieldAndMessage( "Archetype", "Base unit must have an Archietype associated with it." ) );
                if ( BaseRace == null ) ErrorList.Add( new FieldAndMessage( "Race", "Base unit must have a race associated with it." ) );

                //Will return validation errors
                if ( ErrorList.Count >= 1 )
                    throw new ValidationException( ErrorList.ToArray() );
                //Will return validation warnings
                if ( WarningList.Count >= 1 )
                    throw new ValidationWarning( WarningList.ToArray() );
            }
        }

        #endregion
    }
}