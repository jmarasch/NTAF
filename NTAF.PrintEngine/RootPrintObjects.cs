using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTAF.PrintEngine {
    public class PrintBreakLine : IPrintable {
        #region IPrintable Members

        public PrintBreakLine() {}

        public void Print( PrintElement element ) {
            element.AddHorizontalRule();
        }

        #endregion
    }
}
