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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ProtoGears_World_Builder {
    /// <summary>
    /// Interaction logic for NewFile.xaml
    /// </summary>
    public partial class NewFile : Window {
        public NewFile() {
            InitializeComponent();
            }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var dlg = new CommonOpenFileDialog();

            dlg.Title = "My Title";
            dlg.IsFolderPicker = true;

            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;

            if(dlg.ShowDialog() == CommonFileDialogResult.Ok) {
                this.txtFilePathText.Text = dlg.FileName;
                }
            }

        public CommonFileDialogResult Result { get; private set; }

        private void Button_OK(object sender, RoutedEventArgs e) {
            if (!System.IO.Directory.Exists(txtFilePathText.Text)) {
                MessageBox.Show("File needs to be saved to a valid directory please...", "Try again...", MessageBoxButton.OK);
                return;
                }
            if (txtFileNameText.Text.Replace(" ", "") == "") {
                MessageBox.Show("You are required by law to enter a name for this file", "Try again...", MessageBoxButton.OK);
                return;
                }
            if (txtFileDataSetText.Text.Replace(" ", "") == "") {
                MessageBox.Show("You are required by law to enter a name for the data set", "Try again...", MessageBoxButton.OK);
                return;
                }
            if (txtFileIDText.Text.Replace(" ", "").Length < 4) {
                MessageBox.Show("File IDs must contain 4 characters no more, no less", "Try again...", MessageBoxButton.OK);
                return;
                }
            if (txtAuthorText.Text.Replace(" ", "") == "") {
                MessageBox.Show("You are required by law to enter a name as the author of this file", "Try again...", MessageBoxButton.OK);
                return;
                }
            if (!txtAuthorEmailText.Text.Contains("@")) {
                MessageBox.Show("You are required by law to enter a Valid e-mail as the author of this file", "Try again...", MessageBoxButton.OK);
                return;
                }
            //txtAuthorWebText
            //txtDescriptionText

            Result = CommonFileDialogResult.Ok;
            this.Close();
            }

        private void Button_Cancel(object sender, RoutedEventArgs e) {
            Result = CommonFileDialogResult.Cancel;
            this.Close();
            }
        }
    }
