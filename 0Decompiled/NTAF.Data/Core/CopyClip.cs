using NTAF.PlugInFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace NTAF.Core
{
	public static class CopyClip
	{
		public static void CopyFromClipboard(object file)
		{
			bool flag;
			try
			{
				if (Clipboard.ContainsText())
				{
					object itemToAdd = null;
					string FromClipBoard = Clipboard.GetText();
					List<string> IDs = new List<string>();
					XmlSerializer xs = null;
					MemoryStream memoryStream = new MemoryStream(CopyClip.StringToUTF8ByteArray(FromClipBoard));
					XmlTextWriter xmlTextWriter = new XmlTextWriterFormattedNoDeclaration(memoryStream);
					XmlTextReader xmlTextReader = new XmlTextReader(memoryStream);
					List<Type> ObjectTypes = new List<Type>(NTReflection.GetObjectClassTypes());
					bool deserialized = false;
					int count = 0;
					do
					{
						try
						{
							xs = new XmlSerializer(ObjectTypes[count]);
							if (!xs.CanDeserialize(xmlTextReader))
							{
								count++;
							}
							else
							{
								memoryStream.Position = (long)0;
								itemToAdd = xs.Deserialize(memoryStream);
								deserialized = true;
							}
						}
						catch
						{
							throw;
						}
						flag = (deserialized ? false : count <= ObjectTypes.Count - 1);
					}
					while (flag);
					if ((itemToAdd == null ? true : !(itemToAdd is INTId)))
					{
						throw new ClipperException("Clipboard data is either corrupt or the proper plugin is not loaded");
					}
					((INTId)itemToAdd).ID = ((NTDataFile)file).GenerateIDCode();
					((NTDataFile)file).Add((ObjectClassBase)itemToAdd);
				}
			}
			catch
			{
				throw;
			}
		}

		public static void CopyToClipboard(INTId obj)
		{
			try
			{
				string ClipData = "Error occurred while copying data to clip board";
				MemoryStream memoryStream = new MemoryStream();
				XmlSerializer xs = new XmlSerializer(obj.GetType());
				XmlTextWriter xmlTextWriter = new XmlTextWriterFormattedNoDeclaration(memoryStream);
				XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
				ns.Add("", "");
				xs.Serialize(xmlTextWriter, obj, ns);
				memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
				ClipData = CopyClip.UTF8ByteArrayToString(memoryStream.ToArray());
				Clipboard.Clear();
				Clipboard.SetText(ClipData);
			}
			catch
			{
				throw;
			}
		}

		private static byte[] StringToUTF32ByteArray(string pXmlString)
		{
			return (new UTF32Encoding()).GetBytes(pXmlString);
		}

		private static byte[] StringToUTF8ByteArray(string pXmlString)
		{
			return (new UTF8Encoding()).GetBytes(pXmlString);
		}

		private static string UTF32ByteArrayToString(byte[] characters)
		{
			return (new UTF32Encoding()).GetString(characters);
		}

		private static string UTF8ByteArrayToString(byte[] characters)
		{
			return (new UTF8Encoding()).GetString(characters);
		}
	}
}