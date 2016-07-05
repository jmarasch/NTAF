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
    //class serializable
    [ObjectClassPlugIn( "Item", "0.0.0.0" ), Serializable()]
    public class Item : ObjectClassBase {

        #region constructors
        //default constructor
        public Item() {
            ID = "";
            Name = "";
            SpeciesCanEquip = 0;
            RaceCanEquip = null;
            Description = "";
            Cost = 0;
        }

        //constructor to accept current class object
        public Item( Item var ) {
            ID = var.ID;
            Name = var.Name;
            SpeciesCanEquip = var.SpeciesCanEquip;
            RaceCanEquip = var.RaceCanEquip;
            Description = var.Description;
            ModifiesStats = var.ModifiesStats;
            Cost = var.Cost;
        }

        //constructor that takes all vars
        public Item( string iID, string iName, Species iBaseRacesCanEquip, Race iSubRaceCanEquip, string iDescription, StatsMod[] iStatMods, UInt16 iCost ) {
            ID = iID;
            Name = iName;
            SpeciesCanEquip = iBaseRacesCanEquip;
            RaceCanEquip = iSubRaceCanEquip;
            Description = iDescription;
            ModifiesStats = iStatMods;
            Cost = iCost;
        }

        #endregion

        #region Events
        public event NTEventHandler EventStatModsChanged;
        public event NTEventHandler EventDescriptionChanged;
        public event NTEventHandler EventCostChanged;
        public event NTEventHandler EventUseableTypeChanged;
        public event NTEventHandler EventRaceChange;
        public override event NTEventHandler MyDataChanged;
        #endregion

        #region Fields
        private String
            _Description;

        private List<StatsMod> 
            _modifyStat = new List<StatsMod>();

        private UInt32 
            _Cost;

        private ArchetypeBaseEnu 
            _UseableType = ArchetypeBaseEnu.All;

        private Race
            _UseableRace = null;

        private Species
            _RacesCanUse = 0;

        #endregion

        #region IAboutMe Members

        [XmlIgnore(), Browsable( false )]
        public override string aboutMe {
            get {
                String retVal = base.aboutMe;

                if ( this.SpeciesCanEquip != 0 )
                    retVal += "Required Base Race(s): " + this.SpeciesCanEquip.ToString() + "\n";
                else
                    if ( this.RaceCanEquip != null )
                        retVal += "Required Race: " + this.RaceCanEquip.Name + "\n";
                if ( this.ModifiesStats.Length > 0 )
                    foreach ( StatsMod SM in this.ModifiesStats )
                        retVal += SM.ToString() + "\n";
                if ( Description != "" )
                    retVal += "Description :" + "\n" +
                        GeneralOperations.WrapLength( this.Description, 50 ) + "\n";
                retVal += "Cost: " + this.Cost.ToString();

                return retVal;
            }
        }

        #endregion

        #region ICloneable
        protected override void clearMyEvents() {
            base.clearMyEvents();

            EventCostChanged = null;
            EventDescriptionChanged = null;
            EventRaceChange = null;
            EventStatModsChanged = null;
            EventUseableTypeChanged = null;
        }
        #endregion

        #region ICompare Members
        private class sortNameHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                Item val1 = ( Item )a;
                Item val2 = ( Item )b;

                //return string compair
                //dsc
                return String.Compare( val2.Name, val1.Name );

            }
        }

        private class sortIDHelper_ASC : IComparer {
            int IComparer.Compare( object a, object b ) {
                Item val1 = ( Item )a;
                Item val2 = ( Item )b;

                //return string compair
                //asc
                return String.Compare( val1.ID, val2.ID );

            }
        }

        private class sortIDHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                Item val1 = ( Item )a;
                Item val2 = ( Item )b;

                //return string compair
                //dsc
                return String.Compare( val2.ID, val1.ID );

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

        #region IObjectClass Members

        public override Type CollectionType {
            get { return typeof( Item ); }
        }

        public override bool IsOfType( object obj ) {
            return obj is Item;
        }

        public override bool IsOfType( Type T ) {
            return T == typeof( Item );
        }

        public override Type MyType() { return CollectionType; }

        public override void Link( ILink DataMaster ) {
            RaceCanEquip = ( Race )DataMaster.FindObject( RaceCanEquip );    
        }

        public override void ReplaceReferences( ObjectClassBase ObjectToReplace, ObjectClassBase ReplaceWith ) {
            if ( RaceCanEquip == ObjectToReplace )
                RaceCanEquip = ( Race )ReplaceWith;
        }

        public override bool CheckForReferences( ObjectClassBase item ) {
            if ( RaceCanEquip == item )
                return true;
            return false;
        }

        #endregion

        #region IPrintable Members

        public override void Print( PrintElement element ) {
            element.AddTitleText( "Item: " + Name );

            element.AddCategoryText( "For Type", TypesCanUse.ToString() );
            element.AddBlankLine();
            element.AddCategoryText( "Description:" );
            element.AddMText( Description );
            element.AddBlankLine();

            if ( ModifiesStats.Length >= 1 ) {
                element.AddCategoryText( "Modifies Stats:" );
                String tmpString = "";
                foreach ( StatsMod SM in ModifiesStats )
                    tmpString += SM.ToString() + ", ";
                element.AddMText( tmpString.TrimEnd( new char[] { ',', ' ' } ) );
                element.AddBlankLine();
            }

            element.AddCategoryText( "Cost", Cost.ToString() );
            //element.AddHorizontalRule();
            element.AddBlankLine();


        }

        #endregion

        #region IValidate Members

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

                if ( SpeciesCanEquip == 0 && RaceCanEquip == null ) ErrorList.Add( new FieldAndMessage( "Race/Species", "A useable race or base race needs to be selected" ) );
                if ( Cost == 0 ) ErrorList.Add( new FieldAndMessage( "Cost", "Item must cost something" ) );

                if ( Description == "" ) WarningList.Add( new FieldAndMessage( "Description", "No description given" ) );

                if ( ErrorList.Count >= 1 )
                    throw new ValidationException( ErrorList.ToArray() );

                if ( WarningList.Count >= 1 )
                    throw new ValidationWarning( WarningList.ToArray() );
            }
        }

        #endregion

        #region methods

        //override functions
        public override string ToString() {
            string retVal = Name;
            if ( NTAF.Core.Properties.Settings.Default.VerboseToString )
                retVal = ID + ": " + Name;
            return retVal;
        }

        public override DataMember[] getDataMembers() {
            string smods = "";

            foreach (StatsMod mod in ModifiesStats)
                smods += mod.ToString() + ", ";

            smods = smods.TrimEnd(new[] {',', ' '});

            return new[] {
                             new DataMember("Name", Name),
                             new DataMember("Description", Description),
                             new DataMember("Modifies Stats", smods),
                             new DataMember("Archatypes Can Use", TypesCanUse),
                             (_UseableRace == null
                                  ? new DataMember("Races Can Use", "")
                                  : new DataMember("Races Can Use", _UseableRace.Name)),
                             new DataMember("Species Can Use", _RacesCanUse),
                             new DataMember("Cost", Cost)
                         };
        }

        public void DropStatMod( StatsMod SM ) {
            if ( StatsModExists( SM ) )
                _modifyStat.Remove( SM );
            else
                throw new Exception( "Modifier not found" );

            if ( EventStatModsChanged != null )
                EventStatModsChanged();
        }

        public void AddStatMod( StatsMod SM ) {
            if ( StatsModExists( SM ) )
                throw new Exception( "Modifier allreaddy exists" );

            _modifyStat.Add( SM );

            if ( EventStatModsChanged != null )
                EventStatModsChanged();
        }

        public bool StatsModExists( StatsMod SM ) {
            bool retVal = false;
            if ( _modifyStat.Contains( SM ) )
                retVal = true;

            return retVal;
        }
        #endregion

        #region operator overrides
        public override bool Equals( object obj ) {
            bool retVal = true;
            if ( obj == null || !( obj is Item ) )
                retVal = false;

            if ( retVal )
                retVal = this.ID == ( ( Item )obj ).ID;
            if ( retVal )
                retVal = this.Name == ( ( Item )obj ).Name;
            if ( retVal )
                retVal = this.RaceCanEquip == ( ( Item )obj ).RaceCanEquip;
            if ( retVal )
                retVal = this.SpeciesCanEquip == ( ( Item )obj ).SpeciesCanEquip;
            if ( retVal )
                retVal = this.ModifiesStats.Length == ( ( Item )obj ).ModifiesStats.Length;
            if ( retVal )
                retVal = this.Description == ( ( Item )obj ).Description;
            if ( retVal )
                retVal = this.Cost == ( ( Item )obj ).Cost;
            if ( retVal )
                retVal = this.TypesCanUse == ( ( Item )obj ).TypesCanUse;
            return retVal;
        }

        static public bool operator ==( Item a, Item b ) {
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

        static public bool operator !=( Item a, Item b ) {
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

        #region properties

        [Category( TagCategory.Stats ),
        Description( "" )]
        public StatsMod[] ModifiesStats {
            get { return _modifyStat.ToArray(); }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _modifyStat.Clear();
                _modifyStat.AddRange( value );

                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventStatModsChanged != null )
                        EventStatModsChanged();
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

        [XmlAttribute(), Category( TagCategory.About ),
        Description( "" )]
        public UInt32 Cost {
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
                        MyDataChanged();
                }
            }
        }

        [XmlAttribute(), Category( TagCategory.About ),
        Description( "" )]
        public ArchetypeBaseEnu TypesCanUse {
            get { return _UseableType; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _UseableType = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventUseableTypeChanged != null )
                        EventUseableTypeChanged();
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [Category( "Permission Requirements" )]
        public Race RaceCanEquip {
            get { return _UseableRace; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _UseableRace = value;

                if ( !Settings.Default.Loading ) {
                    if ( EventRaceChange != null )
                        EventRaceChange();
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [XmlAttribute()]
        public Species SpeciesCanEquip {
            get { return _RacesCanUse; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _RacesCanUse = value;

                if ( EventRaceChange != null )
                    EventRaceChange();
                if ( MyDataChanged != null )
                    MyDataChanged();
            }
        }
        #endregion

    }

    [OCCPlugIn( "Items", "0.0.0.0" )]
    public class ItemCollector : OCCBase {
        public override byte objectLayer {
            get {
                return 2;
            }
        }

        public override Type CollectionType {
            get {
                return typeof( Item );
            }
        }
    }

    [TreeNodePlugIn( "Item Tree","Items", "0.0.0.0", typeof( Item ) )]
    public class ItemTree : OCTreeNodeBase {
        //public override Type CollectionType {
        //    get {
        //        return typeof( Item );
        //    }
        //}
    }
}