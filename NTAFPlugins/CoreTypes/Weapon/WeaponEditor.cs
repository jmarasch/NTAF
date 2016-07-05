using System;
using System.Windows.Forms;
using NTAF.Core;
using NTAF.Permissions;
using NTAF.PlugInFramework;

namespace NTAF.ObjectClasses {
    [EditorPlugIn( "GUI Psy Editor", "0.0.0.0", true,
    new Type[] { typeof( Weapon ) } )]
    public partial class WeaponEditor : OCEditorBase {

        public WeaponEditor() {
            MyObject = ( Weapon )Activator.CreateInstance( typeof( Weapon ) );
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
                Control ctrl = sender as Control;
                try {
                    Weapon _Weapon = MyObject as Weapon;

                    if ( ctrl.Name == comboBox_Permission.Name ) _Weapon.RequiresPermission = ( WeaponPermission )comboBox_Permission.SelectedItem;

                    if ( ctrl.Name == comboBox_BaseWeaponType.Name ) _Weapon.BaseType = ( WeaponBaseType )Enum.Parse( typeof( WeaponBaseType ), comboBox_BaseWeaponType.SelectedItem.ToString( ) );

                    if ( ctrl.Name == textBox_Cost.Name ) {
                        UInt16 tmp = 0;
                        UInt16.TryParse( textBox_Cost.Text, out tmp );
                        _Weapon.Cost = tmp;
                    }
                    if ( ctrl.Name == textBox_Description.Name ) _Weapon.Description = textBox_Description.Text;

                    if ( ctrl.Name == textBox_Special.Name ) _Weapon.Special = textBox_Special.Text;

                    if ( ctrl.Name == textBox_Range.Name ) _Weapon.Range = textBox_Range.Text;

                    if ( ctrl.Name == textBox_MvsP.Name ) {
                        SByte tmp=0;
                        SByte.TryParse( textBox_MvsP.Text, out tmp );
                        _Weapon.MvsP = tmp;
                    }

                    if ( ctrl.Name == textBox_MvsA.Name ) {
                        SByte tmp=0;
                        SByte.TryParse( textBox_MvsA.Text, out tmp );
                        _Weapon.MvsA = tmp;
                    }

                    if ( ctrl.Name == textBox_SIOR.Name ) {
                        SByte tmp=0;
                        SByte.TryParse( textBox_SIOR.Text, out tmp );
                        _Weapon.SIOR = tmp;
                    }

                    if ( ctrl.Name == textBox_Shots.Name ) {
                        Byte tmp=0;
                        Byte.TryParse( textBox_Shots.Text, out tmp );
                        _Weapon.Shots = tmp;
                    }

                    if ( ctrl.Name == textBox_SaveMod.Name ) {
                        SByte tmp=0;
                        SByte.TryParse( textBox_SaveMod.Text, out tmp );
                        _Weapon.SvMod = tmp;
                    }
                }
                catch ( Exception ) { throw; }
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
            foreach ( WeaponBaseType BWT in GeneralOperations.EnumToList<WeaponBaseType>( ) )
                comboBox_BaseWeaponType.Items.Add( GeneralOperations.GetDescription( BWT ) );

            foreach ( OCCBase col in Collectors ) {
                if ( col.IsOfType( typeof( WeaponPermission ) ) )
                    comboBox_Permission.Items.AddRange( col.Objects );
            }

            comboBox_BaseWeaponType.Sorted = true;
            comboBox_Permission.Sorted = true;

        }

        protected override void PopulateFields() {
            base.PopulateFields( );

            try {
                Weapon _Weapon = MyObject as Weapon;
                comboBox_BaseWeaponType.SelectedItem = _Weapon.BaseType.ToString( );
                textBox_Range.Text = _Weapon.Range;
                textBox_MvsP.Text = _Weapon.MvsP.ToString( );
                textBox_MvsA.Text = _Weapon.MvsA.ToString( );
                textBox_SIOR.Text = _Weapon.SIOR.ToString( );
                textBox_Shots.Text = _Weapon.Shots.ToString( );
                textBox_SaveMod.Text = _Weapon.SvMod.ToString( );
                textBox_Special.Text = _Weapon.Special;
                textBox_Description.Text = _Weapon.Description;
                if ( _Weapon.RequiresPermission != null )
                    foreach ( WeaponPermission WP in comboBox_Permission.Items )
                        if ( WP.ID == _Weapon.RequiresPermission.ID )
                            comboBox_Permission.SelectedItem = WP;

                textBox_Cost.Text = _Weapon.Cost.ToString( );
            }
            catch ( Exception ) { throw; }

        }

        protected override void editing( bool editing ) {
            base.editing( editing );
            foreach ( System.Windows.Forms.Control ctrl in this.Controls ) {
                if ( ctrl is ComboBox )
                    ( ( ComboBox )ctrl ).Enabled = editing;
            }
        }

    }
}
