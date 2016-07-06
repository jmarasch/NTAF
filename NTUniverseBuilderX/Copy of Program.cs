using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Security.Principal;
using Microsoft.Win32;
using System.Collections.Generic;



namespace NTAF.UniverseBuilder.WinGui {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main( string[] args ) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            string ass = Process.GetCurrentProcess().ProcessName;

            //get this applications name for the process list and querry the systems running process and return the results
            Process[] runningProcess = Process.GetProcessesByName( Process.GetCurrentProcess().ProcessName );

#if !DEBUG
            //todo get install path from registry if not in debug mode set InstallLocation to that location.
            RegistryKey
                NT = Registry.LocalMachine.OpenSubKey("NewTerra_Studios"),
                UB = NT.OpenSubKey("Universe_Builder");

            InstallLocation = ( String )UB.GetValue( "InstallLocation", @"C:\Program Files\NewTerra Studios\Universe Builder" );
#else
            NTAF.UniverseBuilder.WinGui.Properties.Settings.Default.InstallLocation = System.AppDomain.CurrentDomain.BaseDirectory;
#endif
            if ( runningProcess.Length >= 2 ) {
                //2 or more instances running
                if ( args.Length >= 1 )
                    //code to start sending args to the originaly running application
                    sendFileOpen( args );
                else
                    MessageBox.Show( "Application is allreaddy running" );
            }
            else {
                Form MainForm = null;

                if ( args.Length > 0 ) {
                    //program was started by opening a file
                    MainForm = new MDIMain( args );
                }
                else {
                    //no files to open with while loading app
                    MainForm = new MDIMain();
                }

                //creat a new thread to the listening method and start running the thread
                Thread SocketListen = new Thread( new ThreadStart( ( ( MDIMain )MainForm ).CommandListener ) );
                SocketListen.Start();

                //code to run main window

                Application.Run( MainForm );

                //clean up threads so the process can exit
                ( ( MDIMain )MainForm ).StopListeners();

                SocketListen.Abort();

            }
        }

        private static void sendFileOpen( string[] args ) {
            //code to send to main program that should have the listener up

            //setup strams and clients
            NetworkStream mainAppStream = default( NetworkStream );
            TcpClient sendSocket = new TcpClient();
            NetworkStream sendStream = default( NetworkStream );

            //command string that will get sent to the main program
            //start by attaching the command as text and end with some char that will not in way be used for anyhing else
            //or found in the string in this example i used the exlimation point(!) but a pipe(|) could work just as well
            string SendData = "Open!";

            //add all the args as a long string and again use a delimiter here i used the semi colon(;)
            foreach ( string str in args )
                SendData += str + ';';

            //remove the last delimiter char so we dont get a bum value at the other end
            SendData = SendData.TrimEnd( new char[] { ';' } );

            //connect to the main app
            sendSocket.Connect( "127.0.0.1", 44444 );
            //attach to the sending stram
            sendStream = sendSocket.GetStream();

            //encode the data to send as a byte array
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes( SendData );

            //send the byte array
            sendStream.Write( outStream, 0, outStream.Length );

            //clear the stram
            sendStream.Flush();

            //this will hold the sending program open untill the reciveing program has finished the transfer and sent back
            //a terminiation signal
            bool finished = false;
            while ( !finished ) {
                //same idea for sending data as above but switch to recive data
                mainAppStream = sendSocket.GetStream();
                int buffsize = 0;
                byte[] inStream = new byte[10025];
                buffsize = sendSocket.ReceiveBufferSize;
                mainAppStream.Read( inStream, 0, buffsize );
                string returndata = System.Text.Encoding.ASCII.GetString( inStream );
                returndata = returndata.TrimEnd( '\0' );

                if ( returndata == "finished" )
                    //main app has sent a finished command and now the secondary app can cleanup and close
                    finished = true;
            }
        }
    }
}

//        /// <summary>
//        /// The main entry point for the application.
//        /// </summary>
//        [STAThread]
//        static void Main( string[] args ) {
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault( false );
//            string ass = Process.GetCurrentProcess().ProcessName;

//            //get this applications name for the process list and querry the systems running process and return the results
//            Process[] runningProcess = Process.GetProcessesByName( Process.GetCurrentProcess().ProcessName );

//#if !DEBUG
//            //todo get install path from registry if not in debug mode set InstallLocation to that location.
//            RegistryKey
//                NT = Registry.LocalMachine.OpenSubKey("NewTerra_Studios"),
//                UB = NT.OpenSubKey("Universe_Builder");

//            InstallLocation = ( String )UB.GetValue( "InstallLocation", @"C:\Program Files\NewTerra Studios\Universe Builder" );
//#else
//            NTAF.UniverseBuilder.WinGui.Properties.Settings.Default.InstallLocation = System.AppDomain.CurrentDomain.BaseDirectory;
//#endif
//            if ( runningProcess.Length >= 2 ) {
//                //2 or more instances running
//                if ( args.Length >= 1 )
//                    //code to start sending args to the originaly running application
//                    sendFileOpen( args );
//                else
//                    MessageBox.Show( "Application is allreaddy running" );
//            }
//            else {
//                Form MainForm = null;

//                if ( args.Length > 0 ) {
//                    //program was started by opening a file
//                    MainForm = new Main( args );
//                }
//                else {
//                    //no files to open with while loading app
//                    MainForm = new Main();
//                }

//                //creat a new thread to the listening method and start running the thread
//                Thread SocketListen = new Thread( new ThreadStart( ( ( Main )MainForm ).CommandListener ) );
//                SocketListen.Start();

//                //code to run main window

//                Application.Run( MainForm );

//                //clean up threads so the process can exit
//                ( ( Main )MainForm ).StopListeners();

//                SocketListen.Abort();

//            }
//        }

//        private static void sendFileOpen( string[] args ) {
//            //code to send to main program that should have the listener up

//            //setup strams and clients
//            NetworkStream mainAppStream = default( NetworkStream );
//            TcpClient sendSocket = new TcpClient();
//            NetworkStream sendStream = default( NetworkStream );

//            //command string that will get sent to the main program
//            //start by attaching the command as text and end with some char that will not in way be used for anyhing else
//            //or found in the string in this example i used the exlimation point(!) but a pipe(|) could work just as well
//            string SendData = "Open!";

//            //add all the args as a long string and again use a delimiter here i used the semi colon(;)
//            foreach ( string str in args )
//                SendData += str + ';';

//            //remove the last delimiter char so we dont get a bum value at the other end
//            SendData = SendData.TrimEnd( new char[] { ';' } );

//            //connect to the main app
//            sendSocket.Connect( "127.0.0.1", 44444 );
//            //attach to the sending stram
//            sendStream = sendSocket.GetStream();

//            //encode the data to send as a byte array
//            byte[] outStream = System.Text.Encoding.ASCII.GetBytes( SendData );

//            //send the byte array
//            sendStream.Write( outStream, 0, outStream.Length );

//            //clear the stram
//            sendStream.Flush();

//            //this will hold the sending program open untill the reciveing program has finished the transfer and sent back
//            //a terminiation signal
//            bool finished = false;
//            while ( !finished ) {
//                //same idea for sending data as above but switch to recive data
//                mainAppStream = sendSocket.GetStream();
//                int buffsize = 0;
//                byte[] inStream = new byte[10025];
//                buffsize = sendSocket.ReceiveBufferSize;
//                mainAppStream.Read( inStream, 0, buffsize );
//                string returndata = System.Text.Encoding.ASCII.GetString( inStream );
//                returndata = returndata.TrimEnd( '\0' );

//                if ( returndata == "finished" )
//                    //main app has sent a finished command and now the secondary app can cleanup and close
//                    finished = true;
//            }
//        }