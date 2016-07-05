using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NTAF.Core;
using NTAF.PlugInFramework;
using NTAF.UniverseBuilder.WinGui.MessageBoxes;

namespace NTAF.ObjectClasses {

    [EditorPlugIn( "GUI BaseUnit Editor", "0.0.0.0", true,
        new Type[] { typeof( BaseUnit ) } )]
    public partial class BaseUnitEditor : OCEditorBase {
        //ErrorDisplay
        //    ErrDisp = new ErrorDisplay();

        private Race _PreviouslySelectedRace = null;

        private char[] trimChars = { '\u0027', ' ', '"' };

        public BaseUnitEditor() {
            MyObject = ( BaseUnit )Activator.CreateInstance( typeof( BaseUnit ) );


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
                    byte tryParse = 1;
                    Byte.TryParse( ( ( TextBox )sender ).Text, out tryParse );

                    int intTryParse = 0;
                    Int32.TryParse( ( ( TextBox )sender ).Text, out intTryParse );

                    if ( sender is TextBox )
                        switch ( ( ( TextBox )sender ).Name ) {
                            case "txtM":
                                ((BaseUnit)MyObject).Movement = tryParse;
                                break;
                            case "txtHP":
                                ((BaseUnit)MyObject).HitPoints = tryParse;
                                break;
                            case "txtAP":
                                ((BaseUnit)MyObject).ActionPoints = tryParse;
                                break;
                            case "txtHH":
                                ((BaseUnit)MyObject).HandToHand = tryParse;
                                break;
                            case "txtPP":
                                ((BaseUnit)MyObject).PsyPoints = tryParse;
                                break;
                            case "txtRW":
                                ((BaseUnit)MyObject).RangedWeapons = tryParse;
                                break;
                            case "txtMT":
                                ((BaseUnit)MyObject).Might = tryParse;
                                break;
                            case "txtT":
                                ((BaseUnit)MyObject).Toughness = tryParse;
                                break;
                            case "txtAG":
                                ((BaseUnit)MyObject).Agility = tryParse;
                                break;
                            case "txtWP":
                                ((BaseUnit)MyObject).Willpower = tryParse;
                                break;
                            case "txtHF":
                                ((BaseUnit)MyObject).HorrorFactor = tryParse;
                                break;
                            case "txtCostMod":
                                ((BaseUnit)MyObject).CostMod = intTryParse;
                                break;
                            case "txtName":
                                ((BaseUnit)MyObject).Name = ( ( TextBox )sender ).Text;
                                break;
                            case "txtDes":
                                ((BaseUnit)MyObject).Description = ( ( TextBox )sender ).Text;
                                break;
                            case "txtSpec":
                                ((BaseUnit)MyObject).Special = ( ( TextBox )sender ).Text;
                                break;
                        }
                }
                catch ( Exception ex ) { 
                    //ErrDisp.ShowMsgDialog( ex );
                }
            }
        }

        protected override void Enter_field( object sender, EventArgs e ) {
            base.Enter_field( sender, e );
        }

        public override EditorExitCode RunEditor( EditorMode mode ) {
            InitializeComponent();

            this.nrM.Minimum = BaseUnit.MIN_POINT_STAT; this.nrM.Maximum = BaseUnit.MAX_POINT_STAT;
            this.nrAP.Minimum = BaseUnit.MIN_POINT_STAT; this.nrAP.Maximum = BaseUnit.MAX_POINT_STAT;
            this.nrHP.Minimum = BaseUnit.MIN_POINT_STAT; this.nrHP.Maximum = BaseUnit.MAX_POINT_STAT;
            this.nrPP.Minimum = BaseUnit.MIN_POINT_STAT; this.nrPP.Maximum = BaseUnit.MAX_POINT_STAT;


            this.nrAGIL.Minimum = BaseUnit.MIN_PERCENT_STAT; this.nrAGIL.Maximum = BaseUnit.MAX_PERCENT_STAT;
            this.nrHF.Minimum = BaseUnit.MIN_PERCENT_STAT; this.nrHF.Maximum = BaseUnit.MAX_PERCENT_STAT;
            this.nrHH.Minimum = BaseUnit.MIN_PERCENT_STAT; this.nrHH.Maximum = BaseUnit.MAX_PERCENT_STAT;
            this.nrMGT.Minimum = BaseUnit.MIN_PERCENT_STAT; this.nrMGT.Maximum = BaseUnit.MAX_PERCENT_STAT;
            this.nrRW.Minimum = BaseUnit.MIN_PERCENT_STAT; this.nrRW.Maximum = BaseUnit.MAX_PERCENT_STAT;
            this.nrT.Minimum = BaseUnit.MIN_PERCENT_STAT; this.nrT.Maximum = BaseUnit.MAX_PERCENT_STAT;
            this.nrWP.Minimum = BaseUnit.MIN_PERCENT_STAT; this.nrWP.Maximum = BaseUnit.MAX_PERCENT_STAT;

            ( ( BaseUnit )MyObject ).EventWeaponPermissionsChanged += new BaseUnit.WeaponPermissionsChanged( WeaponPermissionsChanged );
            ( ( BaseUnit )MyObject ).EventSkillPermissionsChanged += new BaseUnit.SkillPermissionsChanged( SkillPermissionsChanged );
            ( ( BaseUnit )MyObject ).EventPsyPermissionsChanged += new BaseUnit.PsyPermissionsChanged( PsyPermissionsChanged );
            ( ( BaseUnit )MyObject ).EventItemsChanged += new BaseUnit.ItemsChanged( ItemsChanged );
            ( ( BaseUnit )MyObject ).EventWeaponsChanged += new BaseUnit.WeaponsChanged( WeaponsChanged );
            ( ( BaseUnit )MyObject ).EventSkillsChanged += new BaseUnit.SkillsChanged( SkillsChanged );
            ( ( BaseUnit )MyObject ).EventPsysChanged += new BaseUnit.PsysChanged( PsysChanged );
            ( ( BaseUnit )MyObject ).EventStatChanged += new BaseUnit.StatChanged( EventStatChanged );
            ( ( BaseUnit )MyObject ).EventCostChanged += new NTEventHandler( EventCostChanged );
            ( ( BaseUnit )MyObject ).EventUnitTypeChanged += new NTEventHandler( EventUnitTypeChanged );

            return base.RunEditor( mode );
        }

        protected override void PopulateComboboxes() {
            foreach ( OCCBase col in Collectors ) {
                if ( col.IsOfType( typeof( Race ) ) )
                    cbxRace.Items.AddRange( col.Objects );
            }

            foreach ( OCCBase col in Collectors ) {
                if ( col.IsOfType( typeof( Archetype ) ) )
                    cbxArchatype.Items.AddRange( col.Objects );
            }

            cbxRace.Sorted = true;
            cbxArchatype.Sorted = true;
        }

        protected override void PopulateFields() {
            base.PopulateFields();

            if ( ( ( BaseUnit )MyObject ).archetype != null ) {
                bool unitTypeLoaded = false;
                foreach ( object obj in cbxArchatype.Items )
                    if ( obj.GetType() == typeof( Archetype ) )
                        if ( ( ( Archetype )obj ).ID == ( ( BaseUnit )MyObject ).archetype.ID )
                            unitTypeLoaded = true;
                if ( !( unitTypeLoaded ) && ( ( ( BaseUnit )MyObject ).archetype.Name != "" ) )
                    cbxArchatype.Items.Add( ( ( BaseUnit )MyObject ).archetype );
            }

            if ( ( ( BaseUnit )MyObject ).archetype.Name != "" )
                cbxArchatype.SelectedItem = ( ( BaseUnit )MyObject ).archetype;

            //check if the race was loaded with data if not add it
            if ( ( ( BaseUnit )MyObject ).BaseRace != null ) {
                bool raceLoaded = false;
                foreach ( object obj in cbxRace.Items )
                    if ( obj.GetType() == typeof( Race ) )
                        if ( ( ( Race )obj ).ID == ( ( BaseUnit )MyObject ).BaseRace.ID )
                            raceLoaded = true;
                if ( !( raceLoaded ) && ( ( ( BaseUnit )MyObject ).BaseRace.Name != "" ) )
                    cbxRace.Items.Add( ( ( BaseUnit )MyObject ).BaseRace );
            }

            if ( ( ( BaseUnit )MyObject ).BaseRace.Name != "" )
                cbxRace.SelectedItem = ( ( BaseUnit )MyObject ).BaseRace;

            //preload data
            //txtM.Text = ( ( BaseUnit )MyObject ).Movement.ToString();
            //txtHP.Text = ( ( BaseUnit )MyObject ).HitPoints.ToString();
            //txtHH.Text = ( ( BaseUnit )MyObject ).HandToHand.ToString();
            //txtAP.Text = ( ( BaseUnit )MyObject ).ActionPoints.ToString();
            //txtPP.Text = ( ( BaseUnit )MyObject ).PsyPoints.ToString();
            //txtRW.Text = ( ( BaseUnit )MyObject ).RangedWeapons.ToString();
            //txtMT.Text = ( ( BaseUnit )MyObject ).Might.ToString();
            //txtT.Text = ( ( BaseUnit )MyObject ).Toughness.ToString();
            //txtAG.Text = ( ( BaseUnit )MyObject ).Agility.ToString();
            //txtWP.Text = ( ( BaseUnit )MyObject ).Willpower.ToString();
            //txtCostMod.Text = ( ( BaseUnit )MyObject ).CostMod.ToString();
            //txtHF.Text = ( ( BaseUnit )MyObject ).HorrorFactor.ToString();
            //lblEXP.Text = ( ( BaseUnit )MyObject ).StartingEXP;
            //txtDes.Text = ( ( BaseUnit )MyObject ).Description;
            //txtSpec.Text = ( ( BaseUnit )MyObject ).Special;

            if ( ( ( BaseUnit )MyObject ).WPermissions != null )
                lstWepRes.Items.AddRange( ( ( BaseUnit )MyObject ).WPermissions );
            if ( ( ( BaseUnit )MyObject ).SPermissions != null )
                lstSkillRes.Items.AddRange( ( ( BaseUnit )MyObject ).SPermissions );
            if ( ( ( BaseUnit )MyObject ).PPermissions != null )
                lstPsyRes.Items.AddRange( ( ( BaseUnit )MyObject ).PPermissions );
            if ( ( ( BaseUnit )MyObject ).Weapons != null )
                lstWeapons.Items.AddRange( ( ( BaseUnit )MyObject ).Weapons );
            if ( ( ( BaseUnit )MyObject ).SkillsAndAbilities != null )
                lstSkils.Items.AddRange( ( ( BaseUnit )MyObject ).SkillsAndAbilities );
            if ( ( ( BaseUnit )MyObject ).Psys != null )
                lstPsys.Items.AddRange( ( ( BaseUnit )MyObject ).Psys );
            if ( ( ( BaseUnit )MyObject ).Items != null )
                lstItems.Items.AddRange( ( ( BaseUnit )MyObject ).Items );

            lblCost.Text = ( ( BaseUnit )MyObject ).Cost.ToString();
        }

        protected override void editing( bool editing ) {
            base.editing( editing );

            foreach ( System.Windows.Forms.Control ctrl in this.Controls ) {
                if ( ctrl.Name.Substring( 0, 3 ) == "txt" )
                    ( ( TextBox )ctrl ).ReadOnly = !editing;
                if ( ctrl.Name.Substring( 0, 3 ) == "cbx" )
                    ( ( ComboBox )ctrl ).Enabled = editing;
                if ( ctrl.Name.Substring( 0, 3 ) == "lst" )
                    ( ( ListBox )ctrl ).Enabled = editing;
                if ( ctrl.Name.Substring( 0, 3 ) == "btn" )
                    ( ( Button )ctrl ).Visible = editing;
                if ( ctrl.Name == "ButtonEdit" )
                    ( ( Button )ctrl ).Visible = !editing;
            }
        }

        void EventUnitTypeChanged() {
            lblEXP.Text = ( ( BaseUnit )MyObject ).StartingEXP;
        }

        void EventStatChanged( StatsMod.Stats Stat ) {
            switch ( Stat ) {
                case StatsMod.Stats.Movement:
                    MMod.Text = ( ( BaseUnit )MyObject ).MovementMod.ToString();
                    MTot.Text = ( ( BaseUnit )MyObject ).MovementTotal.ToString();
                    break;
                case StatsMod.Stats.HitPoints:
                    HPMod.Text = ( ( BaseUnit )MyObject ).HitPointsMod.ToString();
                    HPTot.Text = ( ( BaseUnit )MyObject ).HitPointsTotal.ToString();
                    break;
                case StatsMod.Stats.AttackPoints:
                    APMod.Text = ( ( BaseUnit )MyObject ).AttackPointsMod.ToString();
                    APTot.Text = ( ( BaseUnit )MyObject ).AttackPointsTotal.ToString();
                    break;
                case StatsMod.Stats.HandToHand:
                    HHMod.Text = ( ( BaseUnit )MyObject ).HandToHandMod.ToString();
                    HHTot.Text = ( ( BaseUnit )MyObject ).HandToHandTotal.ToString();
                    break;
                case StatsMod.Stats.PsyPoints:
                    PPMod.Text = ( ( BaseUnit )MyObject ).PsyPointsMod.ToString();
                    PPTot.Text = ( ( BaseUnit )MyObject ).PsyPointsTotal.ToString();
                    break;
                case StatsMod.Stats.RangedWeapons:
                    RWMod.Text = ( ( BaseUnit )MyObject ).RangedWeaponsMod.ToString();
                    RWTot.Text = ( ( BaseUnit )MyObject ).RangedWeaponsTotal.ToString();
                    break;
                case StatsMod.Stats.Might:
                    MTMod.Text = ( ( BaseUnit )MyObject ).MightMod.ToString();
                    MTTot.Text = ( ( BaseUnit )MyObject ).MightTotal.ToString();
                    break;
                case StatsMod.Stats.Toughness:
                    TMod.Text = ( ( BaseUnit )MyObject ).ToughnessMod.ToString();
                    TTot.Text = ( ( BaseUnit )MyObject ).ToughnessTotal.ToString();
                    break;
                case StatsMod.Stats.Agility:
                    AGMod.Text = ( ( BaseUnit )MyObject ).AgilityMod.ToString();
                    AGTot.Text = ( ( BaseUnit )MyObject ).AgilityTotal.ToString();
                    break;
                case StatsMod.Stats.Willpower:
                    WPMod.Text = ( ( BaseUnit )MyObject ).WillpowerMod.ToString();
                    WPTot.Text = ( ( BaseUnit )MyObject ).WillpowerTotal.ToString();
                    break;
                case StatsMod.Stats.HorrorFactor:
                    HFMod.Text = ( ( BaseUnit )MyObject ).HorrorFactorMod.ToString();
                    HFTot.Text = ( ( BaseUnit )MyObject ).HorrorFactorTotal.ToString();
                    break;
            }
            Console.WriteLine( Stat.ToString() + " Updated!" );
        }

        void EventCostChanged() {
            Console.WriteLine( "Cost changed!" );
            lblCost.Text = ( ( BaseUnit )MyObject ).Cost.ToString();
        }

        #region Weapon Actions
        private void btnAddWpnRest_Click( object sender, EventArgs e ) {
            try {
                List<WeaponPermission>
                    WpnPerms = new List<WeaponPermission>();

                foreach ( OCCBase col in Collectors ) {
                    if ( col.IsOfType( typeof( WeaponPermission ) ) && col.Objects.Length != 0 )
                        WpnPerms.AddRange( Array.ConvertAll<Object, WeaponPermission>( col.Objects,
                                delegate( object Obj ) {
                                    return Obj is WeaponPermission ? ( WeaponPermission )Obj : null;
                                }
                            )
                        );
                }

                SelectorForm CSF = new SelectorForm( "Add", "Cancel", "Select a Weapon Permission to add...", "Add Weapon Permission",
                                    ( ( BaseUnit )MyObject ).getAllowedPermissions<WeaponPermission>( WpnPerms.ToArray() ) );
                switch ( CSF.ShowDialog() ) {
                    case DialogResult.OK:
                        ( ( BaseUnit )MyObject ).AddWeaponPermission( ( WeaponPermission )CSF.SelectedObject );
                        break;
                }
            }
            catch ( RaceException ex ) {
                //ErrDisp.ShowMsgDialog( ex );
                //MessageBox.Show( ex.Message );
            }
            catch ( Exception ex ) {
                //ErrDisp.ShowMsgDialog( ex );
                //MessageBox.Show( ex.Message );
            }
        }

        private void btnRmvWpnRest_Click( object sender, EventArgs e ) {
            try {
                if ( lstWepRes.SelectedItem != null )
                    ( ( BaseUnit )MyObject ).DropWeaponPermission( ( WeaponPermission )lstWepRes.SelectedItem );

            }
            catch ( Exception ex ) {
                //ErrDisp.ShowMsgDialog( ex );
                //MessageBox.Show( ex.Message );
            }
        }

        private void btnAddWeapon_Click( object sender, EventArgs e ) {
            try {
                List<Weapon>
                    Wpn = new List<Weapon>();

                foreach ( OCCBase col in Collectors ) {
                    if ( col.IsOfType( typeof( Weapon ) ) )
                        Wpn.AddRange( col.Objects as Weapon[] );
                }

                SelectorForm CSF = new SelectorForm( "Add", "Cancel", "Select a Weapon to add...", "Add Weapon",
                                                    ( ( BaseUnit )MyObject ).getAvalableWeapons( Wpn.ToArray() ) );
                switch ( CSF.ShowDialog() ) {
                    case DialogResult.OK:
                        ( ( BaseUnit )MyObject ).AddWeapon( ( Weapon )CSF.SelectedObject );
                        break;
                }
            }
            catch ( Exception ex ) {
                //ErrDisp.ShowMsgDialog( ex );
                //MessageBox.Show( ex.Message );
            }
        }

        private void btnRemWeapon_Click( object sender, EventArgs e ) {
            try {
                if ( lstWeapons.SelectedItem != null )
                    ( ( BaseUnit )MyObject ).DropWeapon( ( Weapon )lstWepRes.SelectedItem );
            }
            catch ( Exception ex ) {
                //ErrDisp.ShowMsgDialog( ex );
                //MessageBox.Show( ex.Message );
            }
        }

        void WeaponPermissionsChanged( WeaponPermission WPerm ) {
            Console.WriteLine( "Weapon Permissions changed, updating lists" );
            lstWepRes.Items.Clear();
            lstWepRes.Items.AddRange( ( ( BaseUnit )MyObject ).WPermissions );
        }

        void WeaponsChanged( Weapon weapon ) {
            Console.WriteLine( "Weapons changed, Updating List" );
            lstWeapons.Items.Clear();
            lstWeapons.Items.AddRange( ( ( BaseUnit )MyObject ).Weapons );

        }
        #endregion

        #region Skill Actions
        private void btnAddSkillRest_Click( object sender, EventArgs e ) {
            try {
                List<SkillPermission>
                    Perms = new List<SkillPermission>();

                foreach ( OCCBase col in Collectors ) {
                    if ( col.IsOfType( typeof( SkillPermission ) ) )
                        Perms.AddRange(
                            Array.ConvertAll<Object, SkillPermission>(
                            col.Objects,
                                delegate( object Obj ) {
                                    return Obj is SkillPermission ? ( SkillPermission )Obj : null;
                                }
                            )
                        );
                }

                SelectorForm CSF = new SelectorForm( "Add", "Cancel", "Select a Skill Permission to add...", "Add Skill Permission",
                                                    ( ( BaseUnit )MyObject ).getAllowedPermissions<SkillPermission>( Perms.ToArray() ) );
                switch ( CSF.ShowDialog() ) {
                    case DialogResult.OK:
                        ( ( BaseUnit )MyObject ).AddSkillPermission( ( SkillPermission )CSF.SelectedObject );
                        break;
                }
            }
            catch ( RaceException ex ) {
                //ErrDisp.ShowMsgDialog( ex );
                //MessageBox.Show( ex.Message );
            }
            catch ( Exception ex ) {
                //ErrDisp.ShowMsgDialog( ex );
                //MessageBox.Show( ex.Message );
            }
        }

        private void btnRmvSkillRest_Click( object sender, EventArgs e ) {
            try {
                if ( lstSkillRes.SelectedItem != null )
                    ( ( BaseUnit )MyObject ).DropSkillPermission( ( SkillPermission )lstSkillRes.SelectedItem );

            }
            catch ( Exception ex ) {
                //ErrDisp.ShowMsgDialog( ex );
                //MessageBox.Show( ex.Message );
            }
        }

        private void btnAddSkill_Click( object sender, EventArgs e ) {
            try {
                List<Skill>
                    Skills = new List<Skill>();

                foreach ( OCCBase col in Collectors ) {
                    if ( col.IsOfType( typeof( Skill ) ) )
                        Skills.AddRange( col.Objects as Skill[] );
                }
                SelectorForm CSF = new SelectorForm( "Add", "Cancel", "Select a Skill to add...", "Add Skill",
                                                    ( ( BaseUnit )MyObject ).getAvalableSkills( Skills.ToArray() ) );
                switch ( CSF.ShowDialog() ) {
                    case DialogResult.OK:
                        ( ( BaseUnit )MyObject ).AddSkill( ( Skill )CSF.SelectedObject );
                        break;
                }
            }
            catch ( Exception ex ) {
                //ErrDisp.ShowMsgDialog( ex );
                //MessageBox.Show( ex.Message );
            }
        }

        private void btnRemSkill_Click( object sender, EventArgs e ) {
            try {
                if ( lstSkils.SelectedItem != null )
                    ( ( BaseUnit )MyObject ).DropSkill( ( Skill )lstSkils.SelectedItem );
            }
            catch ( Exception ex ) {
                //ErrDisp.ShowMsgDialog( ex );
                //MessageBox.Show( ex.Message );
            }
        }

        void SkillPermissionsChanged( SkillPermission PPerm ) {
            Console.WriteLine( "Skill Permission changed, Updating List" );
            lstSkillRes.Items.Clear();
            lstSkillRes.Items.AddRange( ( ( BaseUnit )MyObject ).SPermissions );
        }

        void SkillsChanged( Skill Skil ) {
            Console.WriteLine( "Skills changed, Updating List" );
            lstSkils.Items.Clear();
            lstSkils.Items.AddRange( ( ( BaseUnit )MyObject ).SkillsAndAbilities );

            UpdateStats();
        }


        #endregion

        #region Psy Actions
        private void btnAddPsyRest_Click( object sender, EventArgs e ) {
            try {
                List<PsyPermission>
                    Perms = new List<PsyPermission>();

                foreach ( OCCBase col in Collectors ) {
                    if ( col.IsOfType( typeof( PsyPermission ) ) )
                        Perms.AddRange( Array.ConvertAll<Object, PsyPermission>( col.Objects,
                            delegate( object Obj ) {
                                return Obj is PsyPermission ? ( PsyPermission )Obj : null;
                            }
                        ));
                        Perms.AddRange( col.Objects as PsyPermission[] );
                }

                SelectorForm CSF = new SelectorForm( "Add", "Cancel", "Select a Psy Permission to add...", "Add Psy Permission",
                                                    ( ( BaseUnit )MyObject ).getAllowedPermissions<PsyPermission>( Perms.ToArray() ) );
                switch ( CSF.ShowDialog() ) {
                    case DialogResult.OK:
                        ( ( BaseUnit )MyObject ).AddPsyPermission( ( PsyPermission )CSF.SelectedObject );
                        break;
                }
            }
            catch ( RaceException ex ) {
                //ErrDisp.ShowMsgDialog( ex );
                //MessageBox.Show( ex.Message );
            }
            catch ( Exception ex ) {
                //ErrDisp.ShowMsgDialog( ex );
                //MessageBox.Show( ex.Message );
            }
        }

        private void btnRmvPsyRest_Click( object sender, EventArgs e ) {
            try {
                if ( lstPsyRes.SelectedItem != null )
                    ( ( BaseUnit )MyObject ).DropPsyPermission( ( PsyPermission )lstPsyRes.SelectedItem );

            }
            catch ( Exception ex ) {
                //ErrDisp.ShowMsgDialog( ex );
                //MessageBox.Show( ex.Message );
            }
        }

        private void btnAddPsy_Click( object sender, EventArgs e ) {
            try {
                List<Psy>
                    Psys = new List<Psy>();

                foreach ( OCCBase col in Collectors ) {
                    if ( col.IsOfType( typeof( Psy ) ) )
                        Psys.AddRange( col.Objects as Psy[] );
                }

                SelectorForm CSF = new SelectorForm( "Add", "Cancel", "Select a Psy to add...", "Add Psy",
                                                    ( ( BaseUnit )MyObject ).getAvalablPsys( Psys.ToArray() ) );

                switch ( CSF.ShowDialog() ) {
                    case DialogResult.OK:
                        ( ( BaseUnit )MyObject ).AddPsy( ( Psy )CSF.SelectedObject );
                        break;
                }
            }
            catch ( Exception ex ) {
                //ErrDisp.ShowMsgDialog( ex );
                //MessageBox.Show( ex.Message );
            }
        }

        private void btnRemPsy_Click( object sender, EventArgs e ) {
            try {
                if ( lstPsys.SelectedItem != null )
                    ( ( BaseUnit )MyObject ).DropPsy( ( Psy )lstPsys.SelectedItem );
            }
            catch ( Exception ex ) {
                //ErrDisp.ShowMsgDialog( ex );
                //MessageBox.Show( ex.Message );
            }
        }

        void PsyPermissionsChanged( PsyPermission PPerm ) {
            Console.WriteLine( "Psy Permission changed, Updating List" );
            lstPsyRes.Items.Clear();
            lstPsyRes.Items.AddRange( ( ( BaseUnit )MyObject ).PPermissions );
        }

        void PsysChanged( Psy psy ) {
            Console.WriteLine( "Psys changed, Updating List" );
            lstPsys.Items.Clear();
            lstPsys.Items.AddRange( ( ( BaseUnit )MyObject ).Psys );

        }

        #endregion

        #region Item Actions
        private void btnAddItem_Click( object sender, EventArgs e ) {
            try {
                List<Item>
                    Items = new List<Item>();

                foreach ( OCCBase col in Collectors ) {
                    if ( col.IsOfType( typeof( Item ) ) )
                        Items.AddRange( Array.ConvertAll<Object, Item>( col.Objects,
                            delegate( object Obj ) {
                                return Obj is Item ? ( Item )Obj : null;
                            }
                        ) );
                }

                SelectorForm CSF = new SelectorForm( "Add", "Cancel", "Select an Item to add...", "Add Item",
                                                    ( ( BaseUnit )MyObject ).getAvalablItems( Items.ToArray() ) );
                switch ( CSF.ShowDialog() ) {
                    case DialogResult.OK:
                        ( ( BaseUnit )MyObject ).AddItem( ( Item )CSF.SelectedObject );
                        break;
                }
            }
            catch ( Exception ex ) {
                //ErrDisp.ShowMsgDialog( ex );
                //MessageBox.Show( ex.Message );
            }
        }

        private void btnRmvItem_Click( object sender, EventArgs e ) {
            try {
                if ( lstItems.SelectedItem != null )
                    ( ( BaseUnit )MyObject ).DropItem( ( Item )lstItems.SelectedItem );

            }
            catch ( Exception ex ) {
                //ErrDisp.ShowMsgDialog( ex );
                //MessageBox.Show( ex.Message );
            }
        }

        void ItemsChanged( Item item ) {
            Console.WriteLine( "Items changed, updating list" );
            lstItems.Items.Clear();
            lstItems.Items.AddRange( ( ( BaseUnit )MyObject ).Items );

            UpdateStats();
        }

        #endregion

        private void Race_Changed( object sender, EventArgs e ) {
            if ( ( _PreviouslySelectedRace != null ) && ( _PreviouslySelectedRace != ( Race )cbxRace.SelectedItem ) )
                switch ( MessageBox.Show( "Changing your race will change your allowable permissions and could result in " + Environment.NewLine +
                                       "permissions, weapons, skills, and psy abilities to be removed from the current base unit" + Environment.NewLine +
                                       "Do you wish to continue?", "Change Race?", MessageBoxButtons.YesNo ) ) {
                    case DialogResult.Yes:
                        try {
                            //switch the race
                            ( ( BaseUnit )MyObject ).BaseRace = ( Race )cbxRace.SelectedItem;
                            _PreviouslySelectedRace = ( Race )cbxRace.SelectedItem;

                        }
                        catch ( Exception ex ) {
                            //ErrDisp.ShowMsgDialog( ex );
                            //MessageBox.Show( ex.Message );
                        }

                        break;
                    case DialogResult.No:
                        cbxRace.SelectedItem = _PreviouslySelectedRace;
                        break;
                }
            else {
                _PreviouslySelectedRace = ( Race )cbxRace.SelectedItem;
                ( ( BaseUnit )MyObject ).BaseRace = ( Race )cbxRace.SelectedItem;
            }

        }

        private void UpdateStats() {
            MMod.Text = ( ( BaseUnit )MyObject ).MovementMod.ToString();
            MTot.Text = ( ( BaseUnit )MyObject ).MovementTotal.ToString();
            HPMod.Text = ( ( BaseUnit )MyObject ).HitPointsMod.ToString();
            HPTot.Text = ( ( BaseUnit )MyObject ).HitPointsTotal.ToString();
            APMod.Text = ( ( BaseUnit )MyObject ).AttackPointsMod.ToString();
            APTot.Text = ( ( BaseUnit )MyObject ).AttackPointsTotal.ToString();
            HHMod.Text = ( ( BaseUnit )MyObject ).HandToHandMod.ToString();
            HHTot.Text = ( ( BaseUnit )MyObject ).HandToHandTotal.ToString();
            PPMod.Text = ( ( BaseUnit )MyObject ).PsyPointsMod.ToString();
            PPTot.Text = ( ( BaseUnit )MyObject ).PsyPointsTotal.ToString();
            RWMod.Text = ( ( BaseUnit )MyObject ).RangedWeaponsMod.ToString();
            RWTot.Text = ( ( BaseUnit )MyObject ).RangedWeaponsTotal.ToString();
            MTMod.Text = ( ( BaseUnit )MyObject ).MightMod.ToString();
            MTTot.Text = ( ( BaseUnit )MyObject ).MightTotal.ToString();
            TMod.Text = ( ( BaseUnit )MyObject ).ToughnessMod.ToString();
            TTot.Text = ( ( BaseUnit )MyObject ).ToughnessTotal.ToString();
            AGMod.Text = ( ( BaseUnit )MyObject ).AgilityMod.ToString();
            AGTot.Text = ( ( BaseUnit )MyObject ).AgilityTotal.ToString();
            WPMod.Text = ( ( BaseUnit )MyObject ).WillpowerMod.ToString();
            WPTot.Text = ( ( BaseUnit )MyObject ).WillpowerTotal.ToString();
            HFMod.Text = ( ( BaseUnit )MyObject ).HorrorFactorMod.ToString();
            HFTot.Text = ( ( BaseUnit )MyObject ).HorrorFactorTotal.ToString();
        }

        private void cbxUnitType_SelectedIndexChanged( object sender, EventArgs e ) {
            ( ( BaseUnit )MyObject ).archetype = ( Archetype )cbxArchatype.SelectedItem;
        }

        private void txt_KeyPress( object sender, KeyPressEventArgs e ) {
            if ( e.KeyChar > '9' || e.KeyChar < '0' &&
                e.KeyChar != '\b' )
                e.Handled = true;
        }

        private void StatNR_ValueChanged( object sender, EventArgs e ) {

            if ( sender is NumericUpDown ) {
                byte value = (byte) Math.Round( ( ( NumericUpDown )sender ).Value, 0, MidpointRounding.AwayFromZero ) ;
                switch ( ( ( NumericUpDown )sender ).Name ) {
                    case "nrM":
                        ( ( BaseUnit )MyObject ).Movement = value;
                        break;
                    case "nrHP":
                        ( ( BaseUnit )MyObject ).HitPoints = value;
                        break;
                    case "nrAP":
                        ( ( BaseUnit )MyObject ).ActionPoints = value;
                        break;
                    case "nrHH":
                        ( ( BaseUnit )MyObject ).HandToHand = value;
                        break;
                    case "nrPP":
                        ( ( BaseUnit )MyObject ).PsyPoints = value;
                        break;
                    case "nrRW":
                        ( ( BaseUnit )MyObject ).RangedWeapons = value;
                        break;
                    case "nrMGT":
                        ( ( BaseUnit )MyObject ).Might = value;
                        break;
                    case "nrT":
                        ( ( BaseUnit )MyObject ).Toughness = value;
                        break;
                    case "nrAGIL":
                        ( ( BaseUnit )MyObject ).Agility = value;
                        break;
                    case "nrWP":
                        ( ( BaseUnit )MyObject ).Willpower = value;
                        break;
                    case "nrHF":
                        ( ( BaseUnit )MyObject ).HorrorFactor = value;
                        break;
                }
            }
        }

    }
}