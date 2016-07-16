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
    public partial class ProgressBox : Form {
        public ProgressBox(int Max, string StartingMessage) {
            InitializeComponent();

            progressBar1.Maximum = Max;
            
            progressBar1.Value = 0;

            LastAction.Text = StartingMessage;
        }

        public void ResetBox( UpdaterEventArgs args, string StartingMessage ) {
            progressBar1.Maximum = args.NumberOfItems;

            progressBar1.Value = 0;

            LastAction.Text = StartingMessage;
        }

        public void UpdateBox(UpdateProgressEventArgs args){
            if ( args.NumberOfItems != progressBar1.Maximum )
                progressBar1.Maximum = args.NumberOfItems;
            progressBar1.Value = args.current;
            LastAction.Text += Environment.NewLine + args.verb + " " + args.lastItem;
            current.Text = args.OfCount;
            PercentCompleete.Text = args.PercentCompleeted;
        }

        public void closeMe() {
            this.Close();
        }
    }
}
