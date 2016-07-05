using System;
using System.Windows.Forms;
using NTAF.Core;
using NTAF.Permissions;
using NTAF.PlugInFramework;
using System.Collections.Generic;

namespace NTAF.ObjectClasses {
    using Stats = StatsMod.Stats;
    //using RaceEnu = Race.Species;

    [EditorPlugIn( "GUI Skill Editor", "0.0.0.0", true,
    new Type[] { typeof( Item ) } )]
    public partial class ItemEditor : OCEditorBase {
        public ItemEditor() {
            MyObject = ( Item )Activator.CreateInstance( typeof( Item ) );
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
            List<StatsMod>
                tmpSM = new List<StatsMod>();

            foreach ( StatsMod sm in listBox_StatMods.Items )
                tmpSM.Add( sm );

            ( ( Item )MyObject ).ModifiesStats = tmpSM.ToArray();

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
                    if ( ( ( Control )sender ).Name == textBox_Description.Name ) ( ( Item )MyObject ).Description = textBox_Description.Text;
                    if ( ( ( Control )sender ).Name == textBox_Cost.Name ) {
                        UInt16.TryParse( textBox_Cost.Text, out tmpParse );
                        ( ( Item )MyObject ).Cost = tmpParse;
                    }
                    if ( ( ( Control )sender ).Name == comboBox_ForGroup.Name ) ( ( Item )MyObject ).RaceCanEquip = ( Race )comboBox_ForGroup.SelectedItem;
                }
                catch ( Exception ex ) { MessageBox.Show( ex.Message ); }
            }
        }

        protected override void Enter_field( object sender, EventArgs e ) {
            base.Enter_field( sender, e );
        }

        protected override void PopulateFields() {

            base.PopulateFields();
            Item
                item = ( Item )MyObject;

            if ( item.RaceCanEquip != null )
                foreach ( Race race in comboBox_ForGroup.Items )
                    if ( race.ID == item.RaceCanEquip.ID )
                        comboBox_ForGroup.SelectedItem = race;

            textBox_Description.Text = item.Description;
            textBox_Cost.Text = item.Cost.ToString();

            foreach ( StatsMod sm in item.ModifiesStats )
                listBox_StatMods.Items.Add( sm );

            if ( ( ( Item )MyObject ).SpeciesCanEquip.Is( Species.All ) )
                rcAll.Checked = true;

            else {
                rcAngelic.Checked = ( ( Item )MyObject ).SpeciesCanEquip.Is( Species.Angelic );
                rcAquatic.Checked = ( ( Item )MyObject ).SpeciesCanEquip.Is( Species.Aquatic );
                rcDemonic.Checked = ( ( Item )MyObject ).SpeciesCanEquip.Is( Species.Demonic );
                rcFun.Checked     = ( ( Item )MyObject ).SpeciesCanEquip.Is( Species.Fun );
                rcGenicA.Checked  = ( ( Item )MyObject ).SpeciesCanEquip.Is( Species.Genics_A );
                rcGenicB.Checked  = ( ( Item )MyObject ).SpeciesCanEquip.Is( Species.Genics_B );
                rcHuman.Checked   = ( ( Item )MyObject ).SpeciesCanEquip.Is( Species.Human );
                rcMutant.Checked  = ( ( Item )MyObject ).SpeciesCanEquip.Is( Species.Mutant );
                rcUndead.Checked  = ( ( Item )MyObject ).SpeciesCanEquip.Is( Species.Undead );
            }
        }

        protected override void PopulateComboboxes() {
            foreach ( OCCBase col in Collectors ) {
                if ( col.IsOfType( typeof( Race ) ) )
                    comboBox_ForGroup.Items.AddRange( col.Objects );
            }

            foreach ( Stats st in GeneralOperations.EnumToList<Stats>() )
                comboBox_StatToMod.Items.Add( GeneralOperations.GetDescription( st ) );


            comboBox_ForGroup.Sorted = true;
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
                Item
                    item = ( Item )MyObject;

                item.SpeciesCanEquip = 0;
                foreach ( Control ctrl in groupBox1.Controls ) {
                    if ( ( ( CheckBox )ctrl ).Checked ) {
                        if ( ctrl.Name == rcAll.Name ) item.SpeciesCanEquip = Species.All;
                        if ( ctrl.Name == rcHuman.Name ) item.SpeciesCanEquip |= Species.Human;
                        if ( ctrl.Name == rcMutant.Name ) item.SpeciesCanEquip |= Species.Mutant;
                        if ( ctrl.Name == rcUndead.Name ) item.SpeciesCanEquip |= Species.Undead;
                        if ( ctrl.Name == rcAquatic.Name ) item.SpeciesCanEquip |= Species.Aquatic;
                        if ( ctrl.Name == rcGenicA.Name ) item.SpeciesCanEquip |= Species.Genics_A;
                        if ( ctrl.Name == rcGenicB.Name ) item.SpeciesCanEquip |= Species.Genics_B;
                        if ( ctrl.Name == rcAngelic.Name ) item.SpeciesCanEquip |= Species.Angelic;
                        if ( ctrl.Name == rcDemonic.Name ) item.SpeciesCanEquip |= Species.Demonic;
                        if ( ctrl.Name == rcFun.Name ) item.SpeciesCanEquip |= Species.Fun;
                    }
                }
            }
        }

        private void NumOnly_KeyPress( object sender, KeyPressEventArgs e ) {
            if ( ( e.KeyChar > '9' || e.KeyChar < '0' ) &&
                e.KeyChar != '\b' && e.KeyChar != '-' )
                e.Handled = true;
        }

    }
}
