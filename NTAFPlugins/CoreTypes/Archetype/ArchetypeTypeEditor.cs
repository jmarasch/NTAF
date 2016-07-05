using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NTAF.Core;
using NTAF.PlugInFramework;


namespace NTAF.ObjectClasses {

    [EditorPlugIn( "GUI Archetype Editor", "0.0.0.0", true, typeof(Archetype) )]
    public partial class ArchetypeEditor :OCEditorBase {

        public ArchetypeEditor() {
            MyObject = ( Archetype )Activator.CreateInstance( typeof( Archetype ) );
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
                if ( ( ( Control )sender ).Name == comboBox_BaseUnitType.Name & comboBox_BaseUnitType.SelectedItem != null )
                    ( ( Archetype )MyObject ).BaseType = ( ArchetypeBaseEnu )Enum.Parse( typeof( ArchetypeBaseEnu ), comboBox_BaseUnitType.SelectedItem.ToString() );
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
            IEnumerable<ArchetypeBaseEnu> enums = GeneralOperations.EnumToList<ArchetypeBaseEnu>();
            foreach ( ArchetypeBaseEnu UTE in enums )
                if ( UTE != ArchetypeBaseEnu.All && UTE != ArchetypeBaseEnu.New )
                    comboBox_BaseUnitType.Items.Add( GeneralOperations.GetDescription( UTE ).Split( ',' )[0] );

            comboBox_BaseUnitType.Sorted = true;
        }
        
        protected override void PopulateFields() {
            FIELD_Name.Text = MyObject.Name;
            comboBox_BaseUnitType.SelectedItem = GeneralOperations.GetDescription( ( ( Archetype )MyObject ).BaseType ).Split( ',' )[0];
        }

        protected override void editing( bool editing ) {
            base.editing( editing );
            comboBox_BaseUnitType.Enabled = editing;
        }

        public override Type CollectionType {
            get {
                return typeof( Archetype );
            }
        }
    }
}