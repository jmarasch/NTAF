using NTAF.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NTAF.Core;

namespace NTAF.UniverseBuilder.WinGui.MessageBoxes {
    public partial class SelectorForm : Form {
        public string btnOKval {
            set { btnOK.Text = value; }
        }

        public string btnCancleval{
            set { btnCancle.Text = value; }
        }

        public string Message {
            set { lblMessage.Text = value; }
        }

        public string Title {
            set { this.Text = value; }
        }

        public object[] SelectionObjArray {
            set { this.cbxSelected.Items.AddRange( value.ToArray() ); }
        }

        public object SelectedObject {
            get { return cbxSelected.SelectedItem; }
        }

        public object Selected {
            get { return cbxSelected.SelectedItem; }
        }
            
        public SelectorForm() {
            InitializeComponent();

            btnOKval = "OK";
            btnCancleval = "Cancle";
            Message = "Select one...";
            Title = "Please make a selection";
        }

        public SelectorForm( string OKVal, string CancleVal,
                                 string message, string title,
                                 object[] selectionVals ) {
            InitializeComponent();
            btnOKval = OKVal;
            btnCancleval = CancleVal;
            Message = message;
            Title = title;
            SelectionObjArray = selectionVals;
            Size newSize = new Size();
            int top = lblMessage.Height + lblMessage.Top + 10;
            cbxSelected.Top = top;
            top += cbxSelected.Height + 10;

            btnCancle.Top = top;
            btnOK.Top = top;
            btnInfo.Top = top;

            Top += btnInfo.Height + 10;

            newSize.Width = this.lblMessage.Width + 30 <= 197 ? 197 : this.lblMessage.Width + 30;
            newSize.Height = Top; //this.lblMessage.Height + 120 <= 131 ? 131 : this.lblMessage.Height + 120;

            if ( newSize.Height < this.MinimumSize.Height )
                newSize.Height = this.MinimumSize.Height;

            if ( newSize.Width < this.MinimumSize.Width )
                newSize.Width = this.MinimumSize.Width;

            this.Size = newSize;
            this.MinimumSize = newSize;

        }

        private void cbxSelected_SelectedIndexChanged( object sender, EventArgs e ) {
            btnOK.Enabled = true;
            label1.Text = NTAF.Core.ExIAboutMe.getIAboutMe( ((ComboBox) sender).SelectedItem );

        }

        private void button1_Click( object sender, EventArgs e ) {
            switch(( ( Button )sender ).Text){
                case ">>":
                    ( ( Button )sender ).Text = "<<";
                    this.Size = new Size( 330, 390 );

                    break;
                case "<<":
                    ( ( Button )sender ).Text = ">>";
                    this.Size = this.MinimumSize;

                    break;

            }


        }
    }
}
