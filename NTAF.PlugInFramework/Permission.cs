using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using NTAF.Core.Properties;
using NTAF.PlugInFramework;


namespace NTAF.Core {

    public interface IRequiresPermission {
        //event EventHandler 

        event NTEventHandler EventRequiredPermissionChanged;

        Permission RequiresPermission { get; set; }

    }

    [Serializable()]
    public class Permission : ObjectClassBase {

        public override event NTAF.Core.NTEventHandler MyDataChanged;

        private Species _BaseRacesCanEquip = 0;

        [XmlAttribute( ), Category( TagCategory.Base ),
        Description( "" )]
        public virtual Species SpeciesCanEquip {
            get { return _BaseRacesCanEquip; }
            set {
                
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                _BaseRacesCanEquip = ( Species )value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( MyDataChanged != null )
                        MyDataChanged( );
                }
            }
        }


    }
}
