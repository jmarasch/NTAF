using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTerra.UniverseBuilder.WinGui {
    partial class MainMDI {

        TcpListener listenSocket = new TcpListener( new IPAddress( new byte[] { 127, 0, 0, 1 } ), 44444 );
        TcpClient sendingAppSocket = default( TcpClient );

        public void CommandListener() {
            //start up the listener in the main app
            listenSocket.Start();

            //really just for my debugging isnt really neaded
            Console.WriteLine( "TCP Listener Started" );

            //continuously listen for new commands being sent
            while ( ( true ) ) {
                //encase in a try so when app is closing it doesnt crash
                try {
                    //when a client connects attach to sending app socket
                    sendingAppSocket = listenSocket.AcceptTcpClient();

                    //setup buffers
                    byte[] bytesFrom = new byte[10025];
                    string dataFromClient = null;

                    //get the network stream
                    NetworkStream NS = sendingAppSocket.GetStream();

                    //read network buffer
                    NS.Read( bytesFrom, 0, ( int )sendingAppSocket.ReceiveBufferSize );

                    //decode bytes to a string
                    dataFromClient = System.Text.Encoding.ASCII.GetString( bytesFrom );

                    //trimoff excess garbage data
                    dataFromClient = dataFromClient.TrimEnd( '\0' );

                    //flush the stream
                    NS.Flush();

                    List<string> filesToOpen = new List<string>();
                    List<string> operation = new List<string>();

                    //operation to split the command suffexed by the selected delimiter
                    //in this example it was the exlimation(!)
                    operation.AddRange( dataFromClient.Split( '!' ) );
                    //get the file path strings and store them in an array
                    if ( operation.Count >= 2 )
                        filesToOpen.AddRange( operation[1].Split( ';' ) );

                    //clear byte buffer
                    byte[] returnBytes = null;

                    //create the command to tell the other app it can close
                    returnBytes = Encoding.ASCII.GetBytes( "finished" );

                    //send the return command
                    NS.Write( returnBytes, 0, returnBytes.Length );

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
                    if ( operation[0] == "Open" ) {
                        foreach ( String str in filesToOpen )
                            flenfo.Add( new FileInfo( str ) );

                        loadedData.Load( flenfo.ToArray() );

                    }

                }
                //fornow generic catch will be utilized unless new routeens are added that can fail without handeling their own errors
                catch { }
            }
        }

        public void StopListeners() {
            if ( sendingAppSocket != null )
                //close the app socket if its in use a try may be inorder so you dont kill your program i havent had any problems yet thoe
                sendingAppSocket.Close();
            //stop the listening socket
            listenSocket.Stop();
        }
    
    }
}
