using System;
using System.IO;
using System.Text;
using System.Xml;

namespace NTAF.Core
{
	public class XmlTextWriterFormattedNoDeclaration : XmlTextWriter
	{
		public XmlTextWriterFormattedNoDeclaration(TextWriter w) : base(w)
		{
			base.Formatting = System.Xml.Formatting.Indented;
		}

		public XmlTextWriterFormattedNoDeclaration(Stream s) : base(s, Encoding.UTF8)
		{
			base.Formatting = System.Xml.Formatting.Indented;
		}

		public override void WriteStartDocument()
		{
		}
	}
}