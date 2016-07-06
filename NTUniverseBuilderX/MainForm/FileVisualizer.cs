using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NTAF.UniverseBuilder.WinGui {
    public partial class FileVisualizer : Form {
        public FileVisualizer() {
            InitializeComponent();
        }

        public void pasteData() { MessageBox.Show( "You pasted some shit!" ); }
    }
}
