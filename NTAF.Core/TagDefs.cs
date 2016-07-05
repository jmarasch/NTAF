using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTAF.Core {
    #region Attribute Tag Deffs
    public static class TagCategory {
        //categorys
        public const string ID = "ID";
        public const string About = "About";
        public const string Permissions = "Permissions";
        public const string PermissionRequirements = "Requires Permission";
        public const string ItmesAbilities = "Items/Abilities";
        public const string Cost = "Cost";
        public const string StatModifiers = "Stat Modifiers";
        public const string BaseStat = "Base Stats";
        public const string Stats = "Stats";
        public const string StatTotals = "Stat Totals";
        public const string Experience = "Experience";
        public const string Base = "Base";
        public const string Template = "Type";
        public const string TemplateSize = "Size";
        public const string Race = "Race";
    }
    public static class TagDef {
        //categorys
        public const string ID = "Unique ID used to distiguish this items from others like it, Must contain the 4 digit Prefix and an 8 digit hex number after it";
        public const string Name = "Common Name given to this object";
        public const string IDPreFix = "4 digit alpha numeric identifier so the objecs origen can be traced";
    }
    #endregion
}