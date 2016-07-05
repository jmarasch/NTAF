using System;
using System.Windows.Forms;
using NTAF.Core;
using NTAF.PlugInFramework;
using System.Collections.Generic;

namespace NTAF.ObjectClasses {

    [EditorPlugIn( "GUI Race Editor", "0.8.0.0", true, typeof( Race ) )]
    public partial class RaceEditor : OCEditorBase {
        public RaceEditor() {
            MyObject = ( Race )Activator.CreateInstance( typeof( Race ) );
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
                if ( ( ( Control )sender ).Name == comboBox_BaseRace.Name )
                    ( ( Race )MyObject ).species = ( Species )Enum.Parse( typeof( Species ), comboBox_BaseRace.SelectedItem.ToString( ) );
            }
        }

        public override EditorExitCode RunEditor( EditorMode mode ) {
            InitializeComponent( );
            return base.RunEditor( mode );
        }

        protected override void Enter_field( object sender, EventArgs e ) {
            base.Enter_field( sender, e );
        }

        protected override void PopulateComboboxes() {
            foreach ( Species RE in GeneralOperations.EnumToList<Species>( ) )
                comboBox_BaseRace.Items.Add( GeneralOperations.GetDescription( RE ) );

            comboBox_BaseRace.Sorted = true;
        }
        
        protected override void PopulateFields() {
            FIELD_Name.Text = MyObject.Name;

            comboBox_BaseRace.SelectedItem = ((Race)MyObject).species.ToString( );
        }

        protected override void editing( bool editing ) {
            base.editing( editing );
            comboBox_BaseRace.Enabled = editing;
        }

        public override Type CollectionType {
            get {
                return typeof( Race );
            }
        }
    }
}
