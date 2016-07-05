using System;
using System.Windows.Forms;
using NTAF.Core;
using NTAF.Permissions;
using NTAF.PlugInFramework;

namespace NTAF.ObjectClasses {
    //using PsyGroup = NTAF.DataCore.ObjectClasses.Psy.PsyGroup;
    //using template = NTAF.DataCore.ObjectClasses.Psy.template;

    [EditorPlugIn( "GUI Psy Editor", "0.0.0.0", true,
        new Type[] { typeof( Psy ) } )]
    public partial class PsyEditor : OCEditorBase {

        public PsyEditor() {
            MyObject = ( Psy )Activator.CreateInstance( typeof( Psy ) );
        }

        protected override void button_Cancel_Click( object sender, EventArgs e ) {
            base.button_Cancel_Click( sender, e );
        }

        protected override void button_Save_Click( object sender, EventArgs e ) {
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
                    if ( ( ( Control )sender ).Name == "textBox_Description" ) ( ( Psy )MyObject ).Description = textBox_Description.Text;
                    if ( ( ( Control )sender ).Name == "textBox_Effect" ) ( ( Psy )MyObject ).Effect = textBox_Effect.Text;
                    if ( ( ( Control )sender ).Name == "textBox_PPCost" ) {
                        ushort.TryParse( textBox_PPCost.Text, out tmpParse );
                        ( ( Psy )MyObject ).ppCost = tmpParse;
                    }
                    if ( ( ( Control )sender ).Name == "textBox_Range" ) {
                        ushort.TryParse( textBox_Range.Text, out tmpParse );
                        ( ( Psy )MyObject ).Range = tmpParse;
                    }
                    if ( ( ( Control )sender ).Name == "textBox_TemplateSize" ) ( ( Psy )MyObject ).TemplateSize = textBox_TemplateSize.Text;
                    if ( ( ( Control )sender ).Name == "comboBox_Permission" ) ( ( Psy )MyObject ).RequiresPermission = ( PsyPermission )comboBox_Permission.SelectedItem;
                    if ( ( ( Control )sender ).Name == "comboBox_PsyType" ) ( ( Psy )MyObject ).PsyTypes = ( PsyGroup )Enum.Parse( typeof( PsyGroup ), comboBox_PsyType.SelectedItem.ToString( ) );
                    if ( ( ( Control )sender ).Name == "comboBox_TemplateType" ) ( ( Psy )MyObject ).Template = ( template )Enum.Parse( typeof( template ), comboBox_TemplateType.SelectedItem.ToString( ) );
                }
                catch ( Exception ex ) { MessageBox.Show( ex.Message ); }
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

            foreach ( PsyGroup PSG in GeneralOperations.EnumToList<PsyGroup>( ) )
                comboBox_PsyType.Items.Add( GeneralOperations.GetDescription( PSG ) );

            foreach ( template TP in GeneralOperations.EnumToList<template>( ) )
                comboBox_TemplateType.Items.Add( GeneralOperations.GetDescription( TP ) );

            foreach ( OCCBase col in Collectors ) {
                if ( col.IsOfType( typeof( PsyPermission ) ) )
                    comboBox_Permission.Items.AddRange( col.Objects );
            }

            comboBox_PsyType.Sorted = true;
            comboBox_TemplateType.Sorted = true;
            comboBox_Permission.Sorted = true;
        }

        protected override void PopulateFields() {
            base.PopulateFields( );

            //check if the permission type was loaded if not add it to the list
            if ( ( ( Psy )MyObject ).RequiresPermission != null ) {
                bool permLoaded = false;
                foreach ( object obj in comboBox_Permission.Items )
                    if ( obj.GetType( ) == typeof( WSPPermission ) )
                        if ( ( ( WSPPermission )obj ).ID == ( ( Psy )MyObject ).RequiresPermission.ID )
                            permLoaded = true;
                if ( !( permLoaded ) && ( ( ( Psy )MyObject ).RequiresPermission.Name != "" ) )
                    comboBox_Permission.Items.Add( ( ( Psy )MyObject ).RequiresPermission );

                if ( ( ( Psy )MyObject ).RequiresPermission.Name != "" )
                    comboBox_Permission.SelectedItem = ( ( Psy )MyObject ).RequiresPermission;
            }

            //preload all data to the fields

            comboBox_PsyType.SelectedItem = ( ( Psy )MyObject ).PsyTypes.ToString( );
            textBox_PPCost.Text = ( ( Psy )MyObject ).ppCost.ToString( );
            textBox_Range.Text = ( ( Psy )MyObject ).Range.ToString( );
            comboBox_TemplateType.SelectedItem = ( ( Psy )MyObject ).Template.ToString( );
            textBox_TemplateSize.Text = ( ( Psy )MyObject ).TemplateSize.ToString( );
            textBox_Effect.Text = ( ( Psy )MyObject ).Effect;
            textBox_Description.Text = ( ( Psy )MyObject ).Description;

        }

        protected override void editing( bool editing ) {
            base.editing( editing );
            foreach ( System.Windows.Forms.Control ctrl in this.Controls ) {
                if ( ctrl.Name.Split( '_' )[0] == "comboBox" )
                    ( ( ComboBox )ctrl ).Enabled = editing;
            }
        }
    }
}
