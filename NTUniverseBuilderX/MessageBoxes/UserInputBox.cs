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
    public partial class UserInputBox : Form {
        public string btnOKval {
            set { btnOK.Text = value; }
        }

        public string btnCancleval {
            set { btnCancle.Text = value; }
        }

        public string Message {
            set { lblMessage.Text = value; }
        }

        public string Title {
            set { this.Text = value; }
        }

        public string UserInput {
            get { return txtUserInput.Text; }
        }

        private bool
            i_EscNonANChars = true;

        public bool EscapeNonAlphaNumChars {
            set { i_EscNonANChars = value; }
        }

        public UserInputBox() {
            InitializeComponent();

            btnOKval = "OK";
            btnCancleval = "Cancle";
            Message = "Provide Input...";
            Title = "Need input...";
        }

        public UserInputBox( string OKVal, string CancleVal,
                                 string message, string title) {
            InitializeComponent();
            btnOKval = OKVal;
            btnCancleval = CancleVal;
            Message = message;
            Title = title;
            Size newSize = new Size();
            newSize.Width = this.lblMessage.Width + 30 <= 197 ? 197 : this.lblMessage.Width + 30;
            newSize.Height = this.lblMessage.Height + 120 <= 131 ? 131 : this.lblMessage.Height + 120;
            this.Size = newSize;
            this.MinimumSize = newSize;
        }

        public UserInputBox( string OKVal, string CancleVal,
                                 string message, string title, bool passwordInput ) {
            InitializeComponent();
            btnOKval = OKVal;
            btnCancleval = CancleVal;
            Message = message;
            Title = title;
            Size newSize = new Size();
            newSize.Width = this.lblMessage.Width + 30 <= 197 ? 197 : this.lblMessage.Width + 30;
            newSize.Height = this.lblMessage.Height + 120 <= 131 ? 131 : this.lblMessage.Height + 120;
            this.Size = newSize;
            this.MinimumSize = newSize;
            if ( passwordInput ) {
                txtUserInput.PasswordChar = '*';
                //txtUserInput.PasswordChar = true;
            }
        }

        private void txtUserInput_KeyPress( object sender, KeyPressEventArgs e ) {
            if ( i_EscNonANChars )
                if ( ( e.KeyChar < '0' || e.KeyChar > '9' ) &
                     ( e.KeyChar < 'A' || e.KeyChar > 'Z' ) &
                     ( e.KeyChar < 'a' || e.KeyChar > 'z' ) &
                       e.KeyChar != '\b')
                    e.Handled = true;

        }
    }
}
