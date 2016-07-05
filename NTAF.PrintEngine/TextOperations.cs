using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTAF.PrintEngine {
    static class TextOperations {
        public static string[] WrapLength( string str, int len ) {
            List<string> retVal = new List<string>();

            string buffer = "";

            int curPos = 0;
            for ( int i = 0; i <= str.Length; i++ ) {
                if ( i + 1 < str.Length ) {
                    if ( ( curPos >= len ) && ( str.Substring( i, 1 ) == " " ) ) {
                        retVal.Add(buffer);
                        buffer = "";
                        curPos = 0;
                    }
                    else
                        buffer += str.Substring( i, 1 );
                    curPos++;
                }
            }
            if ( str != String.Empty )
                retVal.Add(str);
            return retVal.ToArray();

        }
    }
}
