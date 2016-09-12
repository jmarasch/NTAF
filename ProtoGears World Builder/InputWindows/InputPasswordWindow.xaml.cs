using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProtoGears_World_Builder.InputWindows {
    /// <summary>
    /// Interaction logic for InputPasswordWindow.xaml
    /// </summary>
    public partial class InputPasswordWindow : Window {
        bool ConfirmRequired;
        public InputPasswordWindow(string question, bool ConfirmationRequired = false) {
            InitializeComponent();
            lblQuestion.Content = question;
            ConfirmRequired = ConfirmationRequired;
            if (ConfirmRequired) {
                this.MinHeight += 50;
                lblConfirm.Visibility = Visibility.Visible;
                txtAnswerConFirm.Visibility = Visibility.Visible;
                btnViewConfirm.Visibility = Visibility.Visible;
                }
            }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e) {
            if(ConfirmRequired)
                if(txtAnswer.Password != txtAnswerConFirm.Password) {
                    MessageBox.Show("Passwords don't match...");
                    return;
                    }
            this.DialogResult = true;
            }

        private void Window_ContentRendered(object sender, EventArgs e) {
            txtAnswer.SelectAll();
            txtAnswer.Focus();
            }

        public string Answer {
            get { return txtAnswer.Password; }
            }

        private void btnViewConfirm_Click(object sender, RoutedEventArgs e) {
            //txtAnswerConFirm.PasswordChar = '';
            }

        private void btnViewPassword(object sender, RoutedEventArgs e) {

            }
        }
    }
