using System;
using System.Windows.Forms;
using NTAF.Core;
using NTAF.Permissions;
using NTAF.PlugInFramework;

namespace NTAF.ObjectClasses {

    [EditorPlugIn( "GUI WPSPermission Editor", "0.0.0.0", true,
        new Type[] { typeof( WeaponPermission ), typeof( PsyPermission ),
            typeof( SkillPermission ) } )]
    public partial class PermEditor : OCEditorBase {

        public PermEditor() {
            MyObject = ( WeaponPermission )Activator.CreateInstance( typeof( WeaponPermission ) );
        }

        protected override void button_Cancel_Click( object sender, EventArgs e ) {
            base.button_Cancel_Click( sender, e );
        }

        protected override void button_Save_Click( object sender, EventArgs e ) {
            MyObject = ( ObjectClassBase )PermissionFormatter.changeType( 
               (WSPPermission) MyObject, ( PermissionType )Enum.Parse( typeof( PermissionType ), 
                    comboBox_PermType.SelectedItem.ToString() ) );

            base.button_Save_Click( sender, e );
        }

        protected override void button_Edit_Click( object sender, EventArgs e ) {
            base.button_Edit_Click( sender, e );
        }

        protected override void Leave_field( object sender, EventArgs e ) {
            base.Leave_field( sender, e );

            if ( sender is Control ) {
                if ( ( ( Control )sender ).Name == comboBox_ForGroup.Name )
                    ( ( WSPPermission )MyObject ).RaceCanEquip = ( Race )comboBox_ForGroup.SelectedItem;
            }
        }

        protected override void Enter_field( object sender, EventArgs e ) {
            base.Enter_field( sender, e );
        }

        public override EditorExitCode RunEditor( EditorMode mode ) {
            InitializeComponent( );
            return base.RunEditor( mode );
        }

        protected override void PopulateComboboxes() {
            foreach ( OCCBase col in Collectors ) {
                if ( col.IsOfType(typeof( Race) ) )
                    comboBox_ForGroup.Items.AddRange( col.Objects );
            }

            comboBox_ForGroup.Sorted = true;

            foreach ( PermissionType item in GeneralOperations.EnumToList<PermissionType>() )
                comboBox_PermType.Items.Add( GeneralOperations.GetDescription( item ) );
        }

        protected override void PopulateFields() {
            //Populate Fields
            base.PopulateFields();

            if ( MyObject is WeaponPermission )
                comboBox_PermType.SelectedItem = PermissionType.Weapon.ToString( );

            if ( MyObject is SkillPermission )
                comboBox_PermType.SelectedItem = PermissionType.Skill.ToString( );

            if ( MyObject is PsyPermission )
                comboBox_PermType.SelectedItem = PermissionType.Psy.ToString( );

            if ( comboBox_PermType.SelectedItem == null )
                comboBox_PermType.SelectedItem = PermissionType.Weapon.ToString( );

            if ( MyObject != null ) {
                if ( ( ( WSPPermission )MyObject ).RaceCanEquip != null )
                    try {
                        foreach ( Race race in comboBox_ForGroup.Items )
                            if ( race.ID == ( ( WSPPermission )MyObject ).RaceCanEquip.ID ) {
                                comboBox_ForGroup.SelectedItem = race;
                                break;
                            }
                        if ( comboBox_ForGroup.SelectedItem == null ) {
                            comboBox_ForGroup.Items.Add( ( ( WSPPermission )MyObject ).RaceCanEquip );
                            comboBox_ForGroup.SelectedItem = ( ( WSPPermission )MyObject ).RaceCanEquip;
                        }
                    }
                    catch {
                        comboBox_ForGroup.SelectedItem = null;
                        MessageBox.Show( "Race not found possible data error...", "Error...", MessageBoxButtons.OK );
                    }



                if ( ( ( WSPPermission )MyObject ).SpeciesCanEquip.Is( Species.All ) ) {
                    rcAll.Checked = true;
                }
                else {
                    if ( ( ( WSPPermission )MyObject ).SpeciesCanEquip.Is( Species.Angelic ) )
                        rcAngelic.Checked = true;
                    else
                        rcAngelic.Checked = false;

                    if ( ( ( WSPPermission )MyObject ).SpeciesCanEquip.Is( Species.Aquatic ) )
                        rcAquatic.Checked = true;
                    else
                        rcAquatic.Checked = false;

                    if ( ( ( WSPPermission )MyObject ).SpeciesCanEquip.Is( Species.Demonic ) )
                        rcDemonic.Checked = true;
                    else
                        rcDemonic.Checked = false;

                    if ( ( ( WSPPermission )MyObject ).SpeciesCanEquip.Is( Species.Fun ) )
                        rcFun.Checked = true;
                    else
                        rcFun.Checked = false;

                    if ( ( ( WSPPermission )MyObject ).SpeciesCanEquip.Is( Species.Genics_A ) )
                        rcGenicA.Checked = true;
                    else
                        rcGenicA.Checked = false;

                    if ( ( ( WSPPermission )MyObject ).SpeciesCanEquip.Is( Species.Genics_B ) )
                        rcGenicB.Checked = true;
                    else
                        rcGenicB.Checked = false;

                    if ( ( ( WSPPermission )MyObject ).SpeciesCanEquip.Is( Species.Human ) )
                        rcHuman.Checked = true;
                    else
                        rcHuman.Checked = false;

                    if ( ( ( WSPPermission )MyObject ).SpeciesCanEquip.Is( Species.Mutant ) )
                        rcMutant.Checked = true;
                    else
                        rcMutant.Checked = false;

                    if ( ( ( WSPPermission )MyObject ).SpeciesCanEquip.Is( Species.Undead ) )
                        rcUndead.Checked = true;
                    else
                        rcUndead.Checked = false;
                }
            }
        }

        protected override void editing( bool editing ) {
            base.editing( editing );
            comboBox_ForGroup.Enabled = editing;
            comboBox_PermType.Enabled = editing;
        }

        public override Type CollectionType {
            get {
                return typeof( WSPPermission );
            }
        }

        private void rcAll_CheckedChanged( object sender, EventArgs e ) {
            if ( !FormLoading() ) {
                if ( rcAll.Checked ) {
                    bool val = false;
                    rcHuman.Enabled = val;
                    rcMutant.Enabled = val;
                    rcUndead.Enabled = val;
                    rcAquatic.Enabled = val;
                    rcGenicA.Enabled = val;
                    rcGenicB.Enabled = val;
                    rcAngelic.Enabled = val;
                    rcDemonic.Enabled = val;
                    rcFun.Enabled = val;
                    rcHuman.Checked = val;
                    rcMutant.Checked = val;
                    rcUndead.Checked = val;
                    rcAquatic.Checked = val;
                    rcGenicA.Checked = val;
                    rcGenicB.Checked = val;
                    rcAngelic.Checked = val;
                    rcDemonic.Checked = val;
                    rcFun.Checked = val;
                }
                else {
                    bool val = true;
                    rcHuman.Enabled = val;
                    rcMutant.Enabled = val;
                    rcUndead.Enabled = val;
                    rcAquatic.Enabled = val;
                    rcGenicA.Enabled = val;
                    rcGenicB.Enabled = val;
                    rcAngelic.Enabled = val;
                    rcDemonic.Enabled = val;
                    rcFun.Enabled = val;
                }
                CheckForChecks();
            }
        }

        private void CheckForChecks() {
            if ( !FormLoading() ) {
                ( ( WSPPermission )MyObject ).SpeciesCanEquip = 0;
                //Species
                //    tmpSpecies = ( ( WSPPermission )MyObject ).SpeciesCanEquip;

                bool cbChecked = false;
                foreach ( Control ctrl in groupBox1.Controls ) {
                    if ( ( ( CheckBox )ctrl ).Checked ) {
                        cbChecked = true;
                        if ( ctrl.Name == rcAll.Name )
                            if ( rcAll.Checked )
                                ( ( WSPPermission )MyObject ).SpeciesCanEquip = Species.All;
                        if ( ctrl.Name == rcHuman.Name )
                            if ( rcHuman.Checked )
                                ( ( WSPPermission )MyObject ).SpeciesCanEquip |= Species.Human;
                        if ( ctrl.Name == rcMutant.Name )
                            if ( rcMutant.Checked )
                                ( ( WSPPermission )MyObject ).SpeciesCanEquip |= Species.Mutant;
                        if ( ctrl.Name == rcUndead.Name )
                            if ( rcUndead.Checked )
                                ( ( WSPPermission )MyObject ).SpeciesCanEquip |= Species.Undead;
                        if ( ctrl.Name == rcAquatic.Name )
                            if ( rcAquatic.Checked )
                                ( ( WSPPermission )MyObject ).SpeciesCanEquip |= Species.Aquatic;
                        if ( ctrl.Name == rcGenicA.Name )
                            if ( rcGenicA.Checked )
                                ( ( WSPPermission )MyObject ).SpeciesCanEquip |= Species.Genics_A;
                        if ( ctrl.Name == rcGenicB.Name )
                            if ( rcGenicB.Checked )
                                ( ( WSPPermission )MyObject ).SpeciesCanEquip |= Species.Genics_B;
                        if ( ctrl.Name == rcAngelic.Name )
                            if ( rcAngelic.Checked )
                                ( ( WSPPermission )MyObject ).SpeciesCanEquip |= Species.Angelic;
                        if ( ctrl.Name == rcDemonic.Name )
                            if ( rcDemonic.Checked )
                                ( ( WSPPermission )MyObject ).SpeciesCanEquip |= Species.Demonic;
                        if ( ctrl.Name == rcFun.Name )
                            if ( rcFun.Checked )
                                ( ( WSPPermission )MyObject ).SpeciesCanEquip |= Species.Fun;
                    }
                }

                //((WSPPermission)MyObject).SpeciesCanEquip = tmpSpecies;

                if ( Mode == EditorMode.Edit || Mode == EditorMode.New ) {
                    comboBox_ForGroup.Enabled = !cbChecked;
                    if ( cbChecked ) comboBox_ForGroup.SelectedItem = null;
                }
            }
        }
    }
}







//if ( MyObject != null ) {
            //    if ( SpeciesFunctions.IsSpecies( ( ( WSPPermission )MyObject ).SpeciesCanEquip, Species.All ) )
            //        rcAll.Checked = true;

            //    else {
            //        if ( SpeciesFunctions.IsSpecies( ( ( WSPPermission )MyObject ).SpeciesCanEquip, Species.Angelic ) )
            //            rcAngelic.Checked = true;
            //        else
            //            rcAngelic.Checked = false;

            //        if ( SpeciesFunctions.IsSpecies( ( ( WSPPermission )MyObject ).SpeciesCanEquip, Species.Aquatic ) )
            //            rcAquatic.Checked = true;
            //        else
            //            rcAquatic.Checked = false;

            //        if ( SpeciesFunctions.IsSpecies( ( ( WSPPermission )MyObject ).SpeciesCanEquip, Species.Demonic ) )
            //            rcDemonic.Checked = true;
            //        else
            //            rcDemonic.Checked = false;

            //        if ( SpeciesFunctions.IsSpecies( ( ( WSPPermission )MyObject ).SpeciesCanEquip, Species.Fun ) )
            //            rcFun.Checked = true;
            //        else
            //            rcFun.Checked = false;

            //        if ( SpeciesFunctions.IsSpecies( ( ( WSPPermission )MyObject ).SpeciesCanEquip, Species.Genics_A ) )
            //            rcGenicA.Checked = true;
            //        else
            //            rcGenicA.Checked = false;

            //        if ( SpeciesFunctions.IsSpecies( ( ( WSPPermission )MyObject ).SpeciesCanEquip, Species.Genics_B ) )
            //            rcGenicB.Checked = true;
            //        else
            //            rcGenicB.Checked = false;

            //        if ( SpeciesFunctions.IsSpecies( ( ( WSPPermission )MyObject ).SpeciesCanEquip, Species.Human ) )
            //            rcHuman.Checked = true;
            //        else
            //            rcHuman.Checked = false;

            //        if ( SpeciesFunctions.IsSpecies( ( ( WSPPermission )MyObject ).SpeciesCanEquip, Species.Mutant ) )
            //            rcMutant.Checked = true;
            //        else
            //            rcMutant.Checked = false;

            //        if ( SpeciesFunctions.IsSpecies( ( ( WSPPermission )MyObject ).SpeciesCanEquip, Species.Undead ) )
            //            rcUndead.Checked = true;
            //        else
            //            rcUndead.Checked = false;
            //    }
            //}