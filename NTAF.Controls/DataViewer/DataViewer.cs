using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NTAF.Core;
using NTAF.PlugInFramework;

namespace NTAF.DataViewer {
    public class DataViewer : UserControl {
        NTData data = new NTData();

        private System.Windows.Forms.ListView listViewControl;


        public DataViewer() {
            this.listViewControl = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listViewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewControl.Location = new System.Drawing.Point(0, 0);
            this.listViewControl.Name = "listView1";
            this.listViewControl.TabIndex = 0;
            this.listViewControl.UseCompatibleStateImageBehavior = false;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BuildView() {
            foreach(NTDataFile file in data.LoadedData) {
                NTListViewItem listviewitem = new NTListViewItem(file.FileName);
                foreach (OCCBase collector in file.Collectors) {
                    NTListViewGroup collectoritem = new NTListViewGroup(listviewitem,collector.CollectionName);
                    collectoritem.
                    collectoritem.SubItems.Add()
                    collector.Objects
                }
                listViewControl.Items.Add(listviewitem);
            }
        }

        #region controls code
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null; 
        #endregion
    }

    public class NTListViewItem : ListViewItem {
        public object NTObjectRef { get; set; }
        
        public NTListViewItem(string text, object thing) : base(text) {
            //if type is a collector show as folder
            //if type is a base class show as a file
            if(thing is PlugInFramework.ObjectClassBase) {
                //show as file object
            }
            else if (thing is PlugInFramework.OCCBase) {
                //show as a folder
            }
        }
    }

    //public class NTListViewItem  : ListViewItem.ListViewSubItem {
    //    public object NTObjectRef { get; set; }

    //    public NTListViewItem(NTListViewItem owner, string text) : base() { this.i }
    //}
}

/*
    root file object
        \collecotrs
            \subcollectors
            \items
        \items
     root file data
     -NTData File
        -holds list of collectors
            -collectors hold items
 */
  