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
    public partial class ComboSelectorForm : Form {
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

        //todo fix
        public string[] SelectionObjArray {
            set {
                this.cbxSelected.Items.Clear();
                this.cbxSelected.Items.AddRange( value );
            }
        }

        public object Selected {
            get { return cbxSelected.SelectedItem; }
        }
            
        public ComboSelectorForm() {
            InitializeComponent();

            btnOKval = "OK";
            btnCancleval = "Cancle";
            Message = "Select one...";
            Title = "Please make a selection";
        }

        public ComboSelectorForm(string OKVal, string CancleVal,
                                 string message, string title,
                                 List<string> selectionVals) {
            InitializeComponent();
            btnOKval = OKVal;
            btnCancleval = CancleVal;
            Message = message;
            Title = title;
            SelectionObjArray = selectionVals.ToArray();
            Size newSize = new Size();
            newSize.Width = this.lblMessage.Width + 30 <= 197 ? 197 : this.lblMessage.Width + 30;
            newSize.Height = this.lblMessage.Height + 120 <= 131 ? 131 : this.lblMessage.Height + 120;
            this.Size = newSize;
            this.MinimumSize = newSize;

        }

        private void cbxSelected_SelectedIndexChanged( object sender, EventArgs e ) {
            btnOK.Enabled = true;
        }
    }
}
