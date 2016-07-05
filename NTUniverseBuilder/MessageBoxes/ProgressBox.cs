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
    public partial class ProgressBox : Form
    {
        private Label LastAction;
        private Label current;
        private Label PercentCompleete;
        private ProgressBar progressBar1;

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

        private void InitializeComponent()
        {
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.LastAction = new System.Windows.Forms.Label();
            this.current = new System.Windows.Forms.Label();
            this.PercentCompleete = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 92);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(536, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // LastAction
            // 
            this.LastAction.AutoSize = true;
            this.LastAction.Location = new System.Drawing.Point(12, 9);
            this.LastAction.Name = "LastAction";
            this.LastAction.Size = new System.Drawing.Size(35, 13);
            this.LastAction.TabIndex = 1;
            this.LastAction.Text = "label1";
            // 
            // current
            // 
            this.current.AutoSize = true;
            this.current.Location = new System.Drawing.Point(12, 35);
            this.current.Name = "current";
            this.current.Size = new System.Drawing.Size(35, 13);
            this.current.TabIndex = 2;
            this.current.Text = "label1";
            // 
            // PercentCompleete
            // 
            this.PercentCompleete.AutoSize = true;
            this.PercentCompleete.Location = new System.Drawing.Point(12, 59);
            this.PercentCompleete.Name = "PercentCompleete";
            this.PercentCompleete.Size = new System.Drawing.Size(35, 13);
            this.PercentCompleete.TabIndex = 3;
            this.PercentCompleete.Text = "label1";
            // 
            // ProgressBox
            // 
            this.ClientSize = new System.Drawing.Size(560, 127);
            this.Controls.Add(this.PercentCompleete);
            this.Controls.Add(this.current);
            this.Controls.Add(this.LastAction);
            this.Controls.Add(this.progressBar1);
            this.Name = "ProgressBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
