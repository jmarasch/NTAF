using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTAF.Core {
    public class XmlTextWriterFormattedNoDeclaration : System.Xml.XmlTextWriter {
        public XmlTextWriterFormattedNoDeclaration( System.IO.TextWriter w )
            : base( w ) {
            Formatting = System.Xml.Formatting.Indented;
        }
        public XmlTextWriterFormattedNoDeclaration( System.IO.Stream s )
            : base( s, Encoding.UTF8 ) {
            Formatting = System.Xml.Formatting.Indented;
        }

        public override void WriteStartDocument() { } // suppress
    }
}
