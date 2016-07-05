using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using NTAF.Core;

namespace NTAF.PlugInFramework {
    /// <summary>
    /// This class is to be used as a base inheritance over "Form" when creating an ObjectClass editor
    /// class will also require the EditroPlugIn attribute
    /// </summary>
    public class OCEditorBase : Form {
        EditorMode
            _Mode = EditorMode.New;

        ObjectClassBase
            i_MyObject = null;

        List<OCCBase>
            i_Collectors = new List<OCCBase>();

        EditorExitCode
            i_ExitCode = EditorExitCode.Cancel;

        Boolean
            i_Loading = false;

        /// <summary>
        /// Easy way to tell events not to fire without unsubscribing and then re-subscribing
        /// </summary>
        /// <returns>true - if form is loading, false - if form is not loading</returns>
        protected Boolean FormLoading() {
            return i_Loading;
        }

        /// <summary>
        /// Checks the EditorPlugin atribute and reports if the editor is a graphical editor
        /// </summary>
        public bool Graphical {
            get {
                Boolean
                    retVal = false;

                List<Object>
                    myAtts = new List<object>( this.GetType( ).GetCustomAttributes( typeof( EditorPlugIn ), true ) );

                if ( myAtts.Count >= 1 )
                    retVal = ( ( EditorPlugIn )myAtts[0] ).isGUI;

                return retVal;
            }
        }

        /// <summary>
        /// Gets and sets the object currently contained within the editor.
        /// 
        /// when an object is sent it needs to be striped of subscribed events
        /// and have just the values copied using the IMemberCopy interface
        /// </summary>
        public ObjectClassBase MyObject {
            get {
                return i_MyObject;
            }
            set {
                if(!IEdit(value.GetType()))
                    throw new InvalidParameter( "Object is not of the correct type, setting of \"MyObject\" aborted" );

                i_MyObject = ( ObjectClassBase )Activator.CreateInstance( value.GetType( ) );

                ( ( IMemberCopy )i_MyObject ).CopyMembers( value );
            }
        }

        /// <summary>
        /// Gets and sets the Collectors that an editor can read from to pull needed reference information
        /// </summary>
        public OCCBase[] Collectors {
            get { return i_Collectors.ToArray(); }
            set {
                if ( i_Collectors.Count <= 0 )
                    i_Collectors.Clear( );
                i_Collectors.AddRange( value );
            }
        }

        /// <summary>
        /// this will run the editor, using the object previously attached to it using the MyObject 
        /// </summary>
        /// <param name="mode">editing mode</param>
        /// <returns>Editors exit code</returns>
        public virtual EditorExitCode RunEditor( EditorMode mode ) {
            
            Mode = mode;

            FormLoad( );

            this.ShowDialog( );

            return i_ExitCode;
        }

        /// <summary>
        /// Sets the editors object and runs the editor
        /// </summary>
        /// <param name="setObject">Object to set</param>
        /// <param name="mode">editing mode</param>
        /// <returns>Editors exit code</returns>
        public virtual EditorExitCode RunEditor( ObjectClassBase setObject, EditorMode mode ) {
            MyObject = setObject;

            return RunEditor( mode );
        }

        /// <summary>
        /// Tests to see if the editor can edit a specific type
        /// </summary>
        /// <param name="thisType">Type to test for editing</param>
        /// <returns>true if the type can be edited using this editor</returns>
        public Boolean IEdit( Type thisType ) {
            Object[]
                atts = this.GetType( ).GetCustomAttributes( typeof( EditorPlugIn ), true );

            if ( atts.Length >= 1 )
                foreach ( Type typ in ( ( EditorPlugIn )atts[0] ).IEdit )
                    if ( typ == thisType )
                        return true;

            return false;
        }

        /// <summary>
        /// Override Required
        /// Returns the type of the collection
        /// </summary>
        [ReadOnly( true )]
        public virtual Type CollectionType {
            get { return this.GetType(); }
        }

        /// <summary>
        /// Gets or sets( proected ) the editors exits status
        /// </summary>
        public EditorExitCode MyResult {
            get { return i_ExitCode; }
            protected set { i_ExitCode = value; }
        }

        /// <summary>
        /// Gets or sets the current mode of the editor
        /// </summary>
        public EditorMode Mode {
            get {
                return _Mode;
            }
            set {
                if ( Mode == EditorMode.ReadOnly )
                    throw new NTAF.Core.ReadOnlyException( "Editor has been set to Read-Only and cannot be changed" );

                if ( value == EditorMode.New || value == EditorMode.Edit )
                    editing( true );
                if ( value == EditorMode.ReadOnly || value == EditorMode.View )
                    editing( false );
            }
        }

        /// <summary>
        /// Requires override, basse provides basic functionallity to set TextBoxes to readonly or not-readonly
        /// base will make a button named "btnEdit" visible or not visible depending on editing setting
        /// </summary>
        /// <param name="editing"></param>
        protected virtual void editing( bool editing ) {
            foreach ( Control ctr in this.Controls ) {
                if ( ctr is TextBox )
                    ( ( TextBox )ctr ).ReadOnly = !editing;
                if ( ctr is Button && ctr.Name == "btnEdit" )
                    ctr.Visible = !editing;
            }
        }

        /// <summary>
        /// will call PopulateComboboxes and PopulateFields when the form loads
        /// </summary>
        protected virtual void FormLoad() {
            i_Loading = true;
            PopulateComboboxes( );
            PopulateFields( );
            i_Loading = false;
        }

        /// <summary>
        /// Should be overridden base function will populate fields with proper names
        /// check documentation of current fields for this version
        /// </summary>
        protected virtual void PopulateFields() {
            foreach ( Control ctr in this.Controls ) {
                if ( ctr.Name == "FIELD_Name" )
                    if ( i_MyObject is INTName )
                        ( ( TextBox )ctr ).Text = ( ( INTName )i_MyObject ).Name;

                if ( ctr.Name == "FIELD_ID" )
                    if ( i_MyObject is INTId )
                        ( ( TextBox )ctr ).Text = ( ( INTId )i_MyObject ).ID;
            }
        }

        /// <summary>
        /// Should be overridden base function will populate comboboxes or lists with
        /// proper names check documentation of current fields for this version
        /// </summary>
        protected virtual void PopulateComboboxes() {

        }

        /// <summary>
        /// Calls for vaallidation of the object using IValidate sets the proper exit code
        /// and closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void button_Save_Click( object sender, EventArgs e ) {
            if ( ( Mode == EditorMode.New ) || ( Mode == EditorMode.Edit ) ) {
                try {
                    if ( i_MyObject is IValidate )
                        ( ( IValidate )i_MyObject ).Valid( );

                    i_ExitCode = EditorExitCode.OK;
                    DialogResult = DialogResult.OK;
                    this.Close( );
                }
                catch ( ValidationWarning VW ) {
                    switch( MessageBox.Show( VW.Message, "Possible object validation errors...", MessageBoxButtons.YesNo ) ){
                        case DialogResult.Yes:
                            i_ExitCode = EditorExitCode.OK;
                            DialogResult = DialogResult.OK;
                            this.Close( );
                            break;
                    }
                }
                catch ( ValidationException VE ) { MessageBox.Show( VE.Message, "Object validation errors...", MessageBoxButtons.OK ); }
                catch ( Exception ex ) { MessageBox.Show( ex.Message ); }
            }
            else {
                DialogResult = DialogResult.Ignore;
                i_ExitCode = EditorExitCode.Cancel;
                this.Close( );
            }
        }

        /// <summary>
        /// cancels the current editing by setting the result code
        /// and closing the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void button_Cancel_Click( object sender, EventArgs e ) {
            DialogResult = DialogResult.Cancel;
            i_ExitCode = EditorExitCode.Cancel;
            this.Close( );
        }

        /// <summary>
        /// sets the Mode of the form to edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void button_Edit_Click( object sender, EventArgs e ) {
            Mode = EditorMode.Edit;
        }

        /// <summary>
        /// Call base.Leave_field(sender,e) then overried the rest of the implementation
        /// if the field being left is a text box and of one of the standard fields
        /// it will auto set the field for the oject. check current documentation for
        /// what the current standard field names are and what they are for
        /// 
        /// you will need to override this if you are using custom fields and you should
        /// be using custom fields if your creating a plugin.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Leave_field( object sender, EventArgs e ) {
            if ( sender is Control ) {
                if ( sender is TextBox ) {
                    switch ( ( ( Control )sender ).Name ) {
                        case "FIELD_Name":
                            if ( i_MyObject is INTName )
                                ( ( INTName )i_MyObject ).Name = ( ( TextBox )sender ).Text;
                            break;
                        case "FIELD_ID":
                            if ( i_MyObject is INTId )
                                ( ( INTId )i_MyObject ).ID = ( ( TextBox )sender ).Text;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Call base.Enter_field(sender,e) then overried the rest of the implementation
        /// based on how you want it to work for what ever field your entering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Enter_field( object sender, EventArgs e ) {
            if ( sender is Control ) {
                if ( sender is TextBox )
                    ( ( TextBox )sender ).SelectAll( );
            }
        }
    }
}
