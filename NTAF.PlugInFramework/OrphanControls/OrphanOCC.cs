using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTAF.PlugInFramework;
using NTAF.Core;

namespace NTAF.PlugInFramework.OrphanControls {
    [OCCPlugIn("Orphans", "0.0.0.0")]
    public class OrphanCollector : OCCBase {
        public override Type CollectionType {
            get {
                return typeof(ObjectClassBase);
            }
        }

        /// <summary>
        /// Checks a passed in obj and determines if it matches the type of this collector
        /// </summary>
        /// <param name="obj">Type of the class to check</param>
        /// <returns>true/false</returns>
        public override bool IsOfType(Object obj) {
            Type objT = obj.GetType();
            //return objT.IsSubclassOf(typeof(ObjectClassBase)) || objT == typeof(ObjectClassBase);
            return IsOfType(objT);
        }

        /// <summary>
        /// Checks a passed in obj and determines if it matches the type of this collector
        /// </summary>
        /// <param name="T">Type of the class to check</param>
        /// <returns>true/false</returns>
        public override bool IsOfType(Type T) {
            //return obj.GetType() == CollectionType;
            return T.IsSubclassOf(typeof(ObjectClassBase)) || T == typeof(ObjectClassBase);
            //return T.IsSubclassOf(CollectionType.GetGenericTypeDefinition());
        }
    }
}
