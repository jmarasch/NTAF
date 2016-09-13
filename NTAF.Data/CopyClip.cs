using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using NTAF.Core;
using NTAF.PlugInFramework;

namespace NTAF.Core {
    public static class CopyClip {
        public static void CopyToClipboard(object obj) {
            try {
                String ClipData = "Error occurred while copying data to clip board";

                if ((obj is OCCBase) || (obj is ObjectClassBase) || (obj is NTDataFileExt)) {
                    MemoryStream memoryStream = new MemoryStream();

                    XmlSerializer xs = new XmlSerializer(obj.GetType(), PluginEngine.GetLoadedPluginTypes());

                    XmlTextWriter xmlTextWriter = new XmlTextWriterFormattedNoDeclaration(memoryStream); // new XmlTextWriter( memoryStream, Encoding.UTF8 );

                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

                    ns.Add("", "");

                    xs.Serialize(xmlTextWriter, obj, ns);

                    memoryStream = (MemoryStream)xmlTextWriter.BaseStream;

                    ClipData = UTF8ByteArrayToString(memoryStream.ToArray());
                    }

                Clipboard.Clear();

                Clipboard.SetText(ClipData);
                } catch { throw; }
            }

        public static void CopyFromClipboard(Object file){// NTDataFile file ) {
            try {
                if ( Clipboard.ContainsText() ) {
                    Object
                        itemToAdd = null;

                    String 
                        FromClipBoard = Clipboard.GetText();

                    List<String>
                        IDs = new List<string>();

                    XmlSerializer 
                        xs = null;

                    MemoryStream 
                        memoryStream = new MemoryStream( StringToUTF8ByteArray( FromClipBoard ) );

                    XmlTextWriter
                        xmlTextWriter = new XmlTextWriterFormattedNoDeclaration( memoryStream );// new XmlTextWriter( memoryStream, Encoding.UTF8 );

                    XmlTextReader
                        xmlTextReader = new XmlTextReader( memoryStream );

                    List<Type>
                        ObjectTypes = new List<Type>(NTReflection.GetObjectClassTypes());

                    Boolean
                        deserialized = false;

                    Int32
                        count = 0;

                    do {
                        try {

                            xs = new XmlSerializer( ObjectTypes[count] );
                            if ( xs.CanDeserialize( xmlTextReader ) ) {
                                memoryStream.Position = 0;
                                itemToAdd = xs.Deserialize( memoryStream );
                                deserialized = true;
                            }
                            else {
                                count++;
                            }
                        }
                        catch { throw; }
                    } while ( !deserialized && count <= ObjectTypes.Count - 1 );

                    if ( itemToAdd != null && itemToAdd is INTId ) {
                        ( ( INTId )itemToAdd ).ID = ( ( NTDataFile )file ).GenerateIDCode();

                        ( ( NTDataFile )file ).Add( ( ObjectClassBase )itemToAdd );
                    }
                    else
                        throw new ClipperException( "Clipboard data is either corrupt or the proper plugin is not loaded" );
                }
            }
            catch { throw; }
        }

        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        private static String UTF8ByteArrayToString( Byte[] characters ) {

            UTF8Encoding encoding = new UTF8Encoding();

            String constructedString = encoding.GetString( characters );

            return ( constructedString );

        }

        private static String UTF32ByteArrayToString( Byte[] characters ) {

            UTF32Encoding encoding = new UTF32Encoding();

            String constructedString = encoding.GetString( characters );

            return ( constructedString );

        }



        /// <summary>
        /// Converts the String to UTF8 Byte array and is used in De serialization
        /// </summary>
        /// <param name="pXmlString"></param>
        /// <returns></returns>
        private static Byte[] StringToUTF8ByteArray( String pXmlString ) {

            UTF8Encoding encoding = new UTF8Encoding();

            Byte[ ] byteArray = encoding.GetBytes( pXmlString );

            return byteArray;

        }

        private static Byte[] StringToUTF32ByteArray( String pXmlString ) {

            UTF32Encoding encoding = new UTF32Encoding();

            Byte[ ] byteArray = encoding.GetBytes( pXmlString );

            return byteArray;

        } 
    }
}