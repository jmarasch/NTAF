using NTAF.Core;
using NTAF.Permissions;

namespace NTAF.ObjectClasses {

    public static class PermissionFormatter {
        public static WSPPermission changeType( WSPPermission permission, PermissionType toType ) {
            WSPPermission retVal = null;
            switch ( toType ) {
                case PermissionType.Weapon:
                    retVal = new WeaponPermission();
                    break;
                case PermissionType.Skill:
                    retVal = new SkillPermission();
                    break;
                case PermissionType.Psy:
                    retVal = new PsyPermission();
                    break;
            }
            retVal.ID = permission.ID;
            retVal.Name = permission.Name;
            retVal.RaceCanEquip = permission.RaceCanEquip;
            retVal.SpeciesCanEquip = permission.SpeciesCanEquip;
            retVal.myOwner = permission.myOwner;

            return retVal;
        }
    }


    //#region IWSPPermission
    //public interface IWSPPermission : IPermission {

    //    event NTEventHandler EventRaceChange;

    //    Race RaceCanEquip { get; set; } //todo check out later was Race

    //}
    //#endregion

    #region IRequireWSPPermission
    public interface IRequiresWSPPermission : IRequiresPermission {

        event NTEventHandler EventRequiredWSPPermissionChanged;

        WSPPermission RequiresWSPPermission { get; set; }

    } 
    #endregion
}
