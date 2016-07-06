using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using NTAF.UniverseBuilder.WinGui.Properties;
using GDIDB;

namespace NTAF.UniverseBuilder.WinGui.MDIWindow {
    public partial class MDIMaster : Form {
        private int childFormNumber = 0;

        public MDIMaster() {
            InitializeComponent();
        }


        public void MDIMain(String[] args)
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e) {
            Form childForm = new FileVisualizer();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
            childForm.WindowState = FormWindowState.Maximized;
        }

        private void OpenFile(object sender, EventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        #region Socketing
        TcpListener listenSocket = new TcpListener(new IPAddress(new byte[] { 127, 0, 0, 1 }), 44444);
        TcpClient sendingAppSocket = default(TcpClient);

        public void CommandListener()
        {
            //start up the listener in the main app
            listenSocket.Start();

            //really just for my debugging isnt really neaded
            Console.WriteLine("TCP Listener Started");

            //continuously listen for new commands being sent
            while ((true))
            {
                //encase in a try so when app is closing it doesnt crash
                try
                {
                    //when a client connects attach to sending app socket
                    sendingAppSocket = listenSocket.AcceptTcpClient();

                    //setup buffers
                    byte[] bytesFrom = new byte[10025];
                    string dataFromClient = null;

                    //get the network stream
                    NetworkStream NS = sendingAppSocket.GetStream();

                    //read network buffer
                    NS.Read(bytesFrom, 0, (int)sendingAppSocket.ReceiveBufferSize);

                    //decode bytes to a string
                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);

                    //trimoff excess garbage data
                    dataFromClient = dataFromClient.TrimEnd('\0');

                    //flush the stream
                    NS.Flush();

                    List<string> filesToOpen = new List<string>();
                    List<string> operation = new List<string>();

                    //operation to split the command suffexed by the selected delimiter
                    //in this example it was the exlimation(!)
                    operation.AddRange(dataFromClient.Split('!'));
                    //get the file path strings and store them in an array
                    if (operation.Count >= 2)
                        filesToOpen.AddRange(operation[1].Split(';'));

                    //clear byte buffer
                    byte[] returnBytes = null;

                    //create the command to tell the other app it can close
                    returnBytes = Encoding.ASCII.GetBytes("finished");

                    //send the return command
                    NS.Write(returnBytes, 0, returnBytes.Length);

                    //clear the buffer
                    NS.Flush();

                    //close the network stream
                    NS.Close();

                    //make the sending app socket collectable by the garbage collector
                    sendingAppSocket = null;

                    //add code here to load files
                    //i typically creat an object libary or object that handles all data operations outside my app
                    //so it can be ported to other avenues just makesure that you use a lock or some other
                    //threadding control mech so your data doesnt get mangled

                    List<FileInfo> flenfo = new List<FileInfo>();
                    if (operation[0] == "Open")
                    {
                        foreach (String str in filesToOpen)
                            flenfo.Add(new FileInfo(str));

                        //loadedData.Load( flenfo.ToArray() );

                    }

                }
                //fornow generic catch will be utilized unless new routeens are added that can fail without handeling their own errors
                catch { }
            }
        }

        public void StopListeners()
        {
            if (sendingAppSocket != null)
                //close the app socket if its in use a try may be inorder so you dont kill your program i havent had any problems yet thoe
                sendingAppSocket.Close();
            //stop the listening socket
            listenSocket.Stop();
        }
        #endregion
    }
}
