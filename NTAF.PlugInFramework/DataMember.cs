using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTAF.PlugInFramework {
    public class DataMember {
        public String Field { get; private set; }
        public Object Data { get; private set; }

        public DataMember(String field, Object data) {
            Field = field;
            Data = data;
        }
    }
}
