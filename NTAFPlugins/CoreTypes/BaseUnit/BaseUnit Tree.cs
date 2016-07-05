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

    [TreeNodePlugIn( "BaseUnit Tree","Base Units", "0.0.0.0", typeof( BaseUnit ) )]
    public class BaseUnitTree : OCTreeNodeBase {
        //public override Type CollectionType {
        //    get {
        //        return typeof( BaseUnit );
        //    }
        //}
    }
}