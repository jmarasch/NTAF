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
    [ObjectClassPlugIn( "Psy", "0.0.0.0" ), Serializable( ), XmlInclude( typeof( PsyPermission ) )]
    public class Psy : ObjectClassBase, IRequiresPermission {

        #region Constructors
        //default constructor
        public Psy() {
            ID = "";
            Name = "";
            ppCost = 0;
            Range = 0;
            Effect = "";
            Description = "";
            Template = template.None;
            TemplateSize = "";
            PsyTypes = PsyGroup.Generic;
            RequiresPermission = null;
        }

        //constructor to accept current class object
        public Psy( Psy var ) {
            ID = var.ID;
            Name = var.Name;
            ppCost = var.ppCost;
            Range = var.Range;
            Effect = var.Effect;
            Description = var.Description;
            Template = var.Template;
            TemplateSize = var.TemplateSize;
            PsyTypes = var.PsyTypes;
            RequiresPermission = var.RequiresPermission;
        }

        //constructor that takes all vars
        public Psy( string PsyID, string PsyDescription, string PsyEffect, string PsyName,
                    UInt16 PsyppCost, string PsyTemplateSize, UInt16 PsyRange,
                    template PsyTemplate, PsyGroup PsyPsyType, PsyPermission PsyRequiredPermission ) {
            ID = PsyID;
            Name = PsyName;
            ppCost = PsyppCost;
            Range = PsyRange;
            Effect = PsyEffect;
            Description = PsyDescription;
            Template = PsyTemplate;
            TemplateSize = PsyTemplateSize;
            PsyTypes = PsyPsyType;
            RequiresPermission = PsyRequiredPermission;
        }
        #endregion

        #region Events
        public event NTEventHandler EventDescriptionChange;
        public event NTEventHandler EventEffectChange;
        public event NTEventHandler EventTemplateSizeChange;
        public event NTEventHandler EventPPCostChange;
        public event NTEventHandler EventRangeChange;
        public event NTEventHandler EventTemplateChange;
        public event NTEventHandler EventPsyTypeChange;
        public override event NTEventHandler MyDataChanged;
        #endregion

        #region Fields
        private string 
            _Description,
            _Effect, 
            _TemplateSize;

        private PsyPermission 
            _RequiresPermission;

        private UInt16 
            _ppCost, 
            _Range;

        private template 
            _Template;

        private PsyGroup 
            _PsyType;

        #endregion

        #region IAboutMe Members

        [XmlIgnore( ), Browsable( false )]
        public override string aboutMe {
            get {
                String retVal = base.aboutMe;

                if ( this.Template != template.None )
                    retVal += this.Template.ToString( ) + "(" + this.TemplateSize + ")\n";

                if ( this.PsyTypes != 0 )
                    retVal += "PP: " + this.ppCost.ToString( ) + "    Type: " + this.PsyTypes.ToString( ) + "\n";
                else
                    retVal += "PP: " + this.ppCost.ToString( ) + "\n";

                retVal += "Range: " + ( this.Range == 0 ? "Base to Base" : this.Range.ToString( ) ) + "\n";

                if ( this.Effect != "" )
                    retVal += "Effect:\n" +
                        GeneralOperations.WrapLength( this.Effect, 50 ) + "\n\n";

                if ( Description != "" )
                    retVal += "Description:\n" +
                        GeneralOperations.WrapLength( this.Description, 50 ) + "\n";

                if ( this.RequiresPermission != null )
                    retVal += "Required Permission: " + this.RequiresPermission.Name + "\n";

                return retVal;
            }
        }

        #endregion

        #region ICloneable Members

        protected override void clearMyEvents() {
            base.clearMyEvents( );
            EventDescriptionChange = null;
            EventEffectChange = null;
            EventPPCostChange = null;
            EventPsyTypeChange = null;
            EventRangeChange = null;
            EventRequiredPermissionChanged = null;
            EventTemplateChange = null;
            EventTemplateSizeChange = null;
        }

        #endregion

        #region IComparable Members

        //extended compairers
        private class sortNameHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                Psy val1 = ( Psy )a;
                Psy val2 = ( Psy )b;

                //return string compair
                //dsc
                return String.Compare( val2.Name, val1.Name );

            }
        }

        private class sortIDHelper_ASC : IComparer {
            int IComparer.Compare( object a, object b ) {
                Psy val1 = ( Psy )a;
                Psy val2 = ( Psy )b;

                //return string compair
                //asc
                return String.Compare( val1.ID, val2.ID );

            }
        }

        private class sortIDHelper_DSC : IComparer {
            int IComparer.Compare( object a, object b ) {
                Psy val1 = ( Psy )a;
                Psy val2 = ( Psy )b;

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
        #endregion

        #region IObjectClass Members

        public override Type CollectionType {
            get { return typeof( Psy ); }
        }

        public override bool IsOfType( object obj ) {
            return obj is Psy;
        }

        public override bool IsOfType( Type T ) {
            return T == typeof( Psy );
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
            element.AddTitleText( "Psy: " + Name );
            element.AddText( "Range: " + ( Range != 0 ? Range.ToString( ) + '"' : "Direct Contact" ) );
            element.AddText( String.Format( "Template(Size): {0}{1}", Template, TemplateSize.ToString( ) + '"' ) );
            element.AddText( String.Format( "Psy Type: {0}    P.P. Cost: {1}", PsyTypes, ppCost ) );
            element.AddBlankLine( );
            element.AddCategoryText( "Description:" );
            element.AddMText( Description );
            element.AddBlankLine( );
            element.AddCategoryText( "Effect:" );
            element.AddMText( Effect );
            element.AddBlankLine( );
            element.AddCategoryText( "Requires Permission: ", RequiresPermission.Name );
            //element.AddHorizontalRule();
            element.AddBlankLine( );
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

                if ( _ppCost == 0 ) ErrorList.Add( new FieldAndMessage( "PP Cost", "All Psy abilities use some PP" ) );
                if ( _RequiresPermission == null ) ErrorList.Add( new FieldAndMessage( "Required Permission", "Requires a value" ) );
                if ( !_Template.Is( template.None ) & TemplateSize != "" ) ErrorList.Add( new FieldAndMessage( "Template Size", "Cannot have a value if a template is not used" ) );

                if ( _Effect == "" ) WarningList.Add( new FieldAndMessage( "Effect", "Should not be blank" ) );
                if ( _Description == "" ) WarningList.Add( new FieldAndMessage( "Description", "Should not be blank" ) );
                
                if ( ErrorList.Count >= 1 )
                    throw new ValidationException( ErrorList.ToArray( ) );

                if ( WarningList.Count >= 1 )
                    throw new ValidationWarning( WarningList.ToArray( ) );
            }
        }

        #endregion

        #region Methods
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
                new DataMember("PP Cost", ppCost),
                new DataMember("Range", Range),
                new DataMember("Template", Template),
                new DataMember("Template Size", TemplateSize),
                new DataMember("Psy Type", PsyTypes),
                new DataMember("Discription", Description),
                new DataMember("Effect", Effect),
                new DataMember("Requires Permission", RequiresPermission)
            };
        }
        #endregion

        #region operator overrides
        public override bool Equals( object obj ) {
            bool retVal = true;
            if ( obj == null || !( obj is Psy ) )
                retVal = false;

            if ( retVal )
                retVal = this.ID == ( ( Psy )obj ).ID;

            if ( retVal )
                retVal = this.Name == ( ( Psy )obj ).Name;
            if ( retVal )
                retVal = this.Description == ( ( Psy )obj ).Description;
            if ( retVal )
                retVal = this.RequiresPermission == ( ( Psy )obj ).RequiresPermission;
            if ( retVal )
                retVal = this.Effect == ( ( Psy )obj ).Effect;
            if ( retVal )
                retVal = this.ppCost == ( ( Psy )obj ).ppCost;
            if ( retVal )
                retVal = this.Range == ( ( Psy )obj ).Range;
            if ( retVal )
                retVal = this.Template == ( ( Psy )obj ).Template;
            if ( retVal )
                retVal = this.TemplateSize == ( ( Psy )obj ).TemplateSize;
            if ( retVal )
                retVal = this.PsyTypes == ( ( Psy )obj ).PsyTypes;

            return retVal;
        }

        static public bool operator ==( Psy a, Psy b ) {
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

        static public bool operator !=( Psy a, Psy b ) {
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
        #endregion

        #region Properties
        //properties based on fields
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
                        EventDescriptionChange( );
                    if ( MyDataChanged != null )
                        MyDataChanged( );
                }
            }
        }

        [Category( TagCategory.About ),
        Description( "" )]
        public string Effect {
            get { return _Effect; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _Effect = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventEffectChange != null )
                        EventEffectChange( );
                    if ( MyDataChanged != null )
                        MyDataChanged( );
                }
            }
        }

        [XmlAttribute( ), Category( TagCategory.Stats ),
        Description( "" )]
        public UInt16 ppCost {
            get { return _ppCost; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _ppCost = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventPPCostChange != null )
                        EventPPCostChange( );
                    if ( MyDataChanged != null )
                        MyDataChanged( );
                }
            }
        }

        [XmlIgnore( ), Category( TagCategory.Stats ),
        Description( "" )]
        public string Distance {
            get {
                string retVal;
                if ( _Range <= 0 )
                    retVal = "Touch Attack";
                else
                    retVal = "Ranged Attack(" + _Range.ToString( ) + '"' + ")";
                return retVal;
            }
        }

        [XmlAttribute( ), Category( TagCategory.Stats ),
        Description( "" )]
        public UInt16 Range {
            get { return _Range; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _Range = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventRangeChange != null )
                        EventRangeChange( );
                    if ( MyDataChanged != null )
                        MyDataChanged( );
                }
            }
        }

        [XmlAttribute( ), Category( TagCategory.Template ),
        Description( "" )]
        public template Template {
            get { return _Template; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _Template = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventTemplateChange != null )
                        EventTemplateChange( );
                    if ( MyDataChanged != null )
                        MyDataChanged( );
                }
            }
        }

        [XmlAttribute( ), Category( TagCategory.TemplateSize ),
        Description( "" )]
        public string TemplateSize {
            get { return _TemplateSize; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _TemplateSize = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventTemplateSizeChange != null )
                        EventTemplateSizeChange( );
                    if ( MyDataChanged != null )
                        MyDataChanged( );
                }
            }
        }

        [XmlAttribute( ), Category( TagCategory.Base ),
        Description( "" )]
        public PsyGroup PsyTypes {
            get { return _PsyType; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _PsyType = value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventPsyTypeChange != null )
                        EventPsyTypeChange( );
                    if ( MyDataChanged != null )
                        MyDataChanged( );
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
                
                if ( !( value is PsyPermission ) && value != null )
                    throw new ArgumentException( "When setting this permission it must be of the PsyPermission Type" );

                _RequiresPermission = ( PsyPermission )value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventRequiredPermissionChanged != null )
                        EventRequiredPermissionChanged( );
                    if ( MyDataChanged != null )
                        MyDataChanged( );
                }
            }
        }

        #endregion
    }

    [OCCPlugIn( "Psys", "0.0.0.0" )]
    public class PsyCollector : OCCBase {
        public override byte objectLayer {
            get {
                return 2;
            }
        }

        public override Type CollectionType {
            get {
                return typeof( Psy );
            }
        }
    }

    [TreeNodePlugIn( "Psy Tree", "Psionics", "0.0.0.0", typeof( Psy ) )]
    public class PsyTree : OCTreeNodeBase {
        //public override Type CollectionType {
        //    get {
        //        return typeof( Psy );
        //    }
        //}
    }
}