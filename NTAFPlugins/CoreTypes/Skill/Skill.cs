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
    [ObjectClassPlugIn( "Skill", "0.0.0.0" ), Serializable(), XmlInclude( typeof( SkillPermission ) )]
    public class Skill : ObjectClassBase, IRequiresPermission {
        #region Constructors
        public Skill() {
            ID = "";
            Name = "";
            Cost = 0;
            Description = "";
            SpeciesCanUseSkill = 0;
            Group = SkillGroupFlag.Generic;
            ModifiesStats = new List<StatsMod>();
            RequiresPermission = null;
        }

        //constructor to accept current class object
        public Skill( Skill var ) {
            ID = var.ID;
            Name = var.Name;
            Cost = var.Cost;
            Description = var.Description;
            SpeciesCanUseSkill = var.SpeciesCanUseSkill;
            Group = var.Group;
            ModifiesStats = var.ModifiesStats;
            RequiresPermission = var.RequiresPermission;
        }

        //constructor that takes all vars
        public Skill( string skID, string skName, UInt16 skCost, string skDescription,
        Species skRacesCanUse, SkillGroupFlag skGroup, List<StatsMod> skModifysStat,
            SkillPermission skRequiresPermission ) {
            ID = skID;
            Name = skName;
            Cost = skCost;
            Description = skDescription;
            SpeciesCanUseSkill = skRacesCanUse;
            Group = skGroup;
            ModifiesStats = skModifysStat;
            RequiresPermission = skRequiresPermission;
        }
        #endregion

        #region Events
        public event NTEventHandler EventDescriptionChange;
        public event NTEventHandler EventRequiredPermissionChange;
        public event NTEventHandler EventCostChange;
        public event NTEventHandler EventSpeciesChange;
        public event NTEventHandler EventGroupChange;
        public event NTEventHandler EventModifyStatChange;
        public override event NTEventHandler MyDataChanged;
        #endregion

        #region fields
        private string 
            _Description;

        private SkillPermission
            _RequiresPermission;

        private UInt16 
            _Cost;

        private Species
            _SpeciesCanUse;

        private SkillGroupFlag 
            _Group;

        private List<StatsMod> 
            _modifyStat = new List<StatsMod>();

        #endregion

        #region IAboutMe Members

        [XmlIgnore(), Browsable( false )]
        public override string aboutMe {
            get {
                String retVal = base.aboutMe;

                if ( this.ModifiesStats.Count != 0 )
                    retVal += "\n";

                foreach ( StatsMod SM in this.ModifiesStats )
                    retVal += String.Format( "{0}\n", SM );

                if ( this.Description != "" )
                    retVal += String.Format( "\nDescription: \n{0}\n\n", GeneralOperations.WrapLength( this.Description, 50 ) );

                retVal += String.Format( "Skill Group: {0}\n\n", this.Group );

                retVal += String.Format( "Avalible to: {0}\n\n", this.SpeciesCanUseSkill );

                if ( this.RequiresPermission != null )
                    retVal += String.Format( "Requires Permission: {0}\n\n", this.RequiresPermission.Name );

                retVal += String.Format( "Cost: {0}\n\n", this.Cost );

                return retVal;
            }
        }

        #endregion

        #region IClone Members

        protected override void clearMyEvents() {
            base.clearMyEvents();

            EventCostChange = null;
            EventDescriptionChange = null;
            EventGroupChange = null;
            EventModifyStatChange = null;
            EventSpeciesChange = null;
            EventRequiredPermissionChange = null;
        }
        #endregion

        #region ICompare Members

        //extended compairers
        private class sortNameHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                Skill val1 = ( Skill )a;
                Skill val2 = ( Skill )b;

                //return string compair
                //dsc
                return String.Compare( val2.Name, val1.Name );

            }
        }

        private class sortIDHelper_ASC : IComparer {
            int IComparer.Compare( object a, object b ) {
                Skill val1 = ( Skill )a;
                Skill val2 = ( Skill )b;

                //return string compair
                //asc
                return String.Compare( val1.ID, val2.ID );

            }
        }

        private class sortIDHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                Skill val1 = ( Skill )a;
                Skill val2 = ( Skill )b;

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
            get { return typeof( Skill ); }
        }

        public override bool IsOfType( object obj ) {
            return obj is Skill;
        }

        public override bool IsOfType( Type T ) {
            return T == typeof( Skill );
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
        #endregion

        #region IPrintable Members

        public override void Print( PrintElement element ) {
            String tmpString = String.Empty;
            element.AddTitleText( "Skill: " + Name );
            element.AddCategoryText( "Modifies Stats:" );
            foreach ( StatsMod SM in ModifiesStats )
                tmpString += SM.ToString() + ", ";
            element.AddMText( tmpString.TrimEnd( new char[] { ',', ' ' } ) );
            element.AddBlankLine();
            element.AddCategoryText( "Description:" );
            element.AddMText( Description );
            element.AddBlankLine();
            element.AddCategoryText( "Requires Permission: ", RequiresPermission.Name );
            element.AddBlankLine();
            element.AddCategoryText( "Species avalibility: ", SpeciesCanUseSkill.ToString() );
            element.AddBlankLine();
            element.AddCategoryText( "Skill Group: ", Group.ToString() );
            element.AddBlankLine();
            element.AddCategoryText( "Cost: ", Cost.ToString() );
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
                if ( Group == 0 ) ErrorList.Add( new FieldAndMessage( "Skill Group", "Skill must be associated with a group" ) );
                if ( SpeciesCanUseSkill == 0 ) ErrorList.Add( new FieldAndMessage( "Species", "At least one species need to be able to use the skill" ) );
                if ( RequiresPermission == null ) ErrorList.Add( new FieldAndMessage( "Required Permission", "Requires a value" ) );
               

                if ( Description == "" ) WarningList.Add( new FieldAndMessage( "Description", "Should not be blank" ) );
                if ( Cost == 0 ) WarningList.Add( new FieldAndMessage( "Cost", "Cost is 0" ) );
                
                if ( ErrorList.Count >= 1 )
                    throw new ValidationException( ErrorList.ToArray() );

                if ( WarningList.Count >= 1 )
                    throw new ValidationWarning( WarningList.ToArray() );
            }
        }

        #endregion

        #region methods
        public override string ToString() {
            string retVal = Name;
            if ( NTAF.Core.Properties.Settings.Default.VerboseToString ) {
                retVal = "";
                if ( NTAF.Core.Properties.Settings.Default.VerboseID )
                    retVal += ID + ": ";
                if ( NTAF.Core.Properties.Settings.Default.VerboseName )
                    retVal += Name + ", ";
                if ( NTAF.Core.Properties.Settings.Default.VerboseDescription ) {
                    string DescriptionShort = ( Description.Length < 120 ? ( Description.Length > 0 ? Description.Substring( 0, Description.Length ) : "" ) : Description.Substring( 0, 120 ) + "..." );
                    retVal += DescriptionShort;
                }
                if ( NTAF.Core.Properties.Settings.Default.VerboseOther ) {
                    retVal += "(" + " ( " + SpeciesCanUseSkill.ToString() + " ) " + Group.ToString() + " " + Cost.ToString() + " " + RequiresPermission.Name + " )";
                }
            }
            return retVal;
        }

        public override DataMember[] getDataMembers() {
            string smods = "";

            foreach ( StatsMod mod in ModifiesStats )
                smods += mod.ToString() + ", ";

            smods = smods.TrimEnd( new[] { ',', ' ' } );

            return new[] {
                new DataMember("Name", Name),
                new DataMember("Description", Description),
                new DataMember("Modifies Stats", smods),
                new DataMember("Skill Group", Group.ToString()),
                new DataMember("Avalable to", SpeciesCanUseSkill.ToString()),
                new DataMember("Permission", RequiresPermission.Name),
                new DataMember("Cost", Cost)
            };
        }

        #endregion

        #region operator overrides
        public override bool Equals( object obj ) {
            bool retVal = true;
            if ( obj == null || !( obj is Skill ) )
                retVal = false;

            if ( retVal )
                retVal = this.RequiresPermission == ( ( Skill )obj ).RequiresPermission;
            if ( retVal )
                retVal = this.Name == ( ( Skill )obj ).Name;
            if ( retVal )
                retVal = this.ID == ( ( Skill )obj ).ID;
            if ( retVal )
                retVal = this.Description == ( ( Skill )obj ).Description;
            if ( retVal )
                retVal = this.Cost == ( ( Skill )obj ).Cost;
            if ( retVal )
                retVal = this.SpeciesCanUseSkill == ( ( Skill )obj ).SpeciesCanUseSkill;
            if ( retVal )
                retVal = this.Group == ( ( Skill )obj ).Group;
            if ( retVal )
                retVal = this.ModifiesStats == ( ( Skill )obj ).ModifiesStats;

            return retVal;
        }

        static public bool operator ==( Skill a, Skill b ) {
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

        static public bool operator !=( Skill a, Skill b ) {
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

        #region Properties

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
                    if ( EventDescriptionChange != null )
                        EventDescriptionChange();
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [XmlAttribute(), Category( TagCategory.Cost ),
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
                    if ( EventCostChange != null )
                        EventCostChange();
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [XmlAttribute(), Category( TagCategory.Base ),
        Description( "" )]
        public Species SpeciesCanUseSkill {
            get { return _SpeciesCanUse; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _SpeciesCanUse = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventSpeciesChange != null )
                        EventSpeciesChange();
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [XmlAttribute(), Category( TagCategory.Base ),
        Description( "" )]
        public SkillGroupFlag Group {
            get { return _Group; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _Group = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventGroupChange != null )
                        EventGroupChange();
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        [Category( TagCategory.StatModifiers ),
        Description( "" )]
        public List<StatsMod> ModifiesStats {
            get { return _modifyStat; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _modifyStat = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventModifyStatChange != null )
                        EventModifyStatChange();
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }
        #endregion

        #region IRequiresPermission Members

        public event NTEventHandler EventRequiredPermissionChanged;

        [Category( TagCategory.Base ),
        Description( "" )]
        public Permission RequiresPermission {
            get { return _RequiresPermission; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( !( value is SkillPermission ) && value != null )
                    throw new ArgumentException( "When setting this permission it must be of the PsyPermission Type" );

                _RequiresPermission = ( SkillPermission )value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventRequiredPermissionChanged != null )
                        EventRequiredPermissionChanged();
                    if ( MyDataChanged != null )
                        MyDataChanged();
                }
            }
        }

        #endregion

    }

    [OCCPlugIn( "Skills", "0.0.0.0" )]
    public class SkillCollector : OCCBase {
        public override byte objectLayer {
            get {
                return 2;
            }
        }

        public override Type CollectionType {
            get {
                return typeof( Skill );
            }
        }
    }

    [TreeNodePlugIn( "Skill Tree","Skills", "0.0.0.0", typeof( Skill ) )]
    public class SkillTree : OCTreeNodeBase {
        //public override Type CollectionType {
        //    get {
        //        return typeof( Skill );
        //    }
        //}
    }
}