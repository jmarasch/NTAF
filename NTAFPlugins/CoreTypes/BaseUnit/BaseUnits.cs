using System;
using System.Collections;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using NTAF.Core;
using NTAF.Core.Properties;
using NTAF.Core;
using NTAF.Core;
using NTAF.PlugInFramework;
using NTAF.PrintEngine;
using NTAF.Permissions;
using System.Collections.Generic;

namespace NTAF.ObjectClasses {

    [OCCPlugIn( "BaseUnits", "0.0.0.0" )]
    public class BaseUnits : OCCBase {
        public override byte objectLayer {
            get {

                return 3;
            }
        }

        public override Type CollectionType {
            get {
                return typeof( BaseUnit );
            }
        }
    }
}