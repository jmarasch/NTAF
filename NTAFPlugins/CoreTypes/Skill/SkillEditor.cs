using System;
using System.Windows.Forms;
using NTAF.Core;
using NTAF.Permissions;
using NTAF.PlugInFramework;

namespace NTAF.ObjectClasses {
    using Stats = StatsMod.Stats;
    //using RaceEnumFunctions = NTAF.DataCore.ObjectClasses.Extentions.RaceEnumFunctions;
    [EditorPlugIn( "GUI Skill Editor", "0.0.0.0", true,
        new Type[] { typeof( Skill ) } )]
    public partial class SkillEditor : OCEditorBase {

        public SkillEditor() {
            MyObject = ( Skill )Activator.CreateInstance( typeof( Skill ) );
        }

        public override EditorExitCode RunEditor( EditorMode mode ) {
            InitializeComponent();
            return base.RunEditor( mode );
        }

        protected override void button_Cancel_Click( object sender, EventArgs e ) {
            base.button_Cancel_Click( sender, e );
        }

        protected override void button_Save_Click( object sender, EventArgs e ) {
            //add all stat modifiers to the object
            ( ( Skill )MyObject ).ModifiesStats.Clear();

            foreach ( StatsMod sm in listBox_StatMods.Items )
                ( ( Skill )MyObject ).ModifiesStats.Add( sm );

            base.button_Save_Click( sender, e );
        }
        
        protected override void button_Edit_Click( object sender, EventArgs e ) {
            base.button_Edit_Click( sender, e );
        }

        protected override void Leave_field( object sender, EventArgs e ) {
            base.Leave_field( sender, e );

            if ( sender is Control ) {
                try {
                    ushort tmpParse = 0;
                    if ( ( ( Control )sender ).Name == textBox_Description.Name ) ( ( Skill )MyObject ).Description = textBox_Description.Text;
                    if ( ( ( Control )sender ).Name == textBox_Cost.Name ) {
                        UInt16.TryParse( textBox_Cost.Text, out tmpParse );
                        ( ( Skill )MyObject ).Cost = tmpParse;
                    }
                    if ( ( ( Control )sender ).Name == comboBox_Permission.Name ) ( ( Skill )MyObject ).RequiresPermission = ( Permission )comboBox_Permission.SelectedItem;
                    if ( ( ( Control )sender ).Name == comboBox_SkillGroup.Name ) ( ( Skill )MyObject ).Group = ( SkillGroupFlag )Enum.Parse( typeof( SkillGroupFlag ), comboBox_SkillGroup.SelectedItem.ToString() );
                }
                catch ( Exception ex ) { MessageBox.Show( ex.Message ); }
            }
        }

        protected override void Enter_field( object sender, EventArgs e ) {
            base.Enter_field( sender, e );
        }

        protected override void PopulateFields() {

            base.PopulateFields();
            Skill
                _Skill = ( Skill )MyObject;

            if ( _Skill.RequiresPermission != null )
                foreach ( SkillPermission SKP in comboBox_Permission.Items )
                    if ( SKP.ID == _Skill.RequiresPermission.ID )
                        comboBox_Permission.SelectedItem = SKP;

            if ( comboBox_Permission.SelectedItem == null && _Skill.RequiresPermission != null ) {
                comboBox_Permission.Items.Add( _Skill.RequiresPermission );
                comboBox_Permission.SelectedItem = _Skill.RequiresPermission;
            }

            foreach ( StatsMod sm in _Skill.ModifiesStats )
                listBox_StatMods.Items.Add( sm );

            if ( _Skill.Group != 0 )
                comboBox_SkillGroup.SelectedItem = _Skill.Group.ToString();

            if ( ( ( Skill )MyObject ).SpeciesCanUseSkill.Is( Species.All ) )
                rcAll.Checked = true;

            else {
                rcAngelic.Checked = ( ( Skill )MyObject ).SpeciesCanUseSkill.Is( Species.Angelic );
                rcAquatic.Checked = ( ( Skill )MyObject ).SpeciesCanUseSkill.Is( Species.Aquatic );
                rcDemonic.Checked = ( ( Skill )MyObject ).SpeciesCanUseSkill.Is( Species.Demonic );
                rcFun.Checked = ( ( Skill )MyObject ).SpeciesCanUseSkill.Is( Species.Fun );
                rcGenicA.Checked = ( ( Skill )MyObject ).SpeciesCanUseSkill.Is( Species.Genics_A );
                rcGenicB.Checked = ( ( Skill )MyObject ).SpeciesCanUseSkill.Is( Species.Genics_B );
                rcHuman.Checked = ( ( Skill )MyObject ).SpeciesCanUseSkill.Is( Species.Human );
                rcMutant.Checked = ( ( Skill )MyObject ).SpeciesCanUseSkill.Is( Species.Mutant );
                rcUndead.Checked = ( ( Skill )MyObject ).SpeciesCanUseSkill.Is( Species.Undead );
            }

            textBox_Description.Text = _Skill.Description;

            textBox_Cost.Text = _Skill.Cost.ToString();
        }

        protected override void PopulateComboboxes() {
            foreach ( SkillGroupFlag SGF in GeneralOperations.EnumToList<SkillGroupFlag>() )
                comboBox_SkillGroup.Items.Add( GeneralOperations.GetDescription( SGF ) );

            foreach ( Stats st in GeneralOperations.EnumToList<Stats>() )
                comboBox_StatToMod.Items.Add( GeneralOperations.GetDescription( st ) );

            foreach ( OCCBase col in Collectors ) {
                if ( col.IsOfType( typeof( SkillPermission ) ) )
                    comboBox_Permission.Items.AddRange( col.Objects );
            }

            comboBox_SkillGroup.Sorted = true;
            comboBox_StatToMod.Sorted = true;
            comboBox_Permission.Sorted = true;
        }

        protected override void editing( bool editing ) {
            base.editing( editing );
            foreach ( System.Windows.Forms.Control ctrl in this.Controls ) {
                if ( ctrl.Name.Split( '_' )[0] == "comboBox" )
                    ( ( ComboBox )ctrl ).Enabled = editing;
            }
        }

        private void button_AddMod_Click( object sender, EventArgs e ) {
            int modAmount = 0;

            int.TryParse( textBox_StatModAmount.Text, out modAmount );

            listBox_StatMods.Items.Add( new StatsMod( ( Stats )Enum.Parse( typeof( Stats ), comboBox_StatToMod.SelectedItem.ToString() ), modAmount ) );
        }

        private void button_RemoveMod_Click( object sender, EventArgs e ) {
            listBox_StatMods.Items.Remove( listBox_StatMods.SelectedItem );
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
                Skill
                    _Skill = ( Skill )MyObject;

                _Skill.SpeciesCanUseSkill = 0;
                foreach ( Control ctrl in groupBox1.Controls ) {
                    if ( ( ( CheckBox )ctrl ).Checked ) {
                        if ( ctrl.Name == rcAll.Name ) _Skill.SpeciesCanUseSkill = Species.All;
                        if ( ctrl.Name == rcHuman.Name ) _Skill.SpeciesCanUseSkill |= Species.Human;
                        if ( ctrl.Name == rcMutant.Name ) _Skill.SpeciesCanUseSkill |= Species.Mutant;
                        if ( ctrl.Name == rcUndead.Name ) _Skill.SpeciesCanUseSkill |= Species.Undead;
                        if ( ctrl.Name == rcAquatic.Name ) _Skill.SpeciesCanUseSkill |= Species.Aquatic;
                        if ( ctrl.Name == rcGenicA.Name ) _Skill.SpeciesCanUseSkill |= Species.Genics_A;
                        if ( ctrl.Name == rcGenicB.Name ) _Skill.SpeciesCanUseSkill |= Species.Genics_B;
                        if ( ctrl.Name == rcAngelic.Name ) _Skill.SpeciesCanUseSkill |= Species.Angelic;
                        if ( ctrl.Name == rcDemonic.Name ) _Skill.SpeciesCanUseSkill |= Species.Demonic;
                        if ( ctrl.Name == rcFun.Name ) _Skill.SpeciesCanUseSkill |= Species.Fun;
                    }
                }
            }
        }

        private void textBox_Cost_KeyPress( object sender, KeyPressEventArgs e ) {
            if ( ( e.KeyChar > '9' || e.KeyChar < '0' ) &&
                e.KeyChar != '\b' )
                e.Handled = true;
        }

    }
}
