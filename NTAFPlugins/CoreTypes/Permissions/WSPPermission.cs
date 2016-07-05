using System;
using System.ComponentModel;
using NTAF.Core;
using NTAF.Core.Properties;
using NTAF.ObjectClasses;

namespace NTAF.Permissions {
    [Serializable()]
    public class WSPPermission : Permission {

        public virtual event NTEventHandler EventRaceChange;
        
        public override event NTEventHandler MyDataChanged;

        private Race  _SubRaceCanEquip = null;

        [Category( TagCategory.Base ),
        Description( "" )]
        public virtual Race RaceCanEquip {
            get { return _SubRaceCanEquip; }
            set {
                if ( !NTAF.Core.Properties.Settings.Default.Loading )
                    if ( myOwner is ILockable )
                        if ( ( ( ILockable )myOwner ).FileLocked )
                            throw new FileLockedException( "File is locked, and cannot be edited." );

                if ( !( value is Race ) && value != null )
                    throw new ArgumentException( "When setting the value RaceCanEquip you must pass a Race value" );

                _SubRaceCanEquip = ( Race )value;
                if ( !Settings.Default.Loading && !Settings.Default.Updating ) {
                    if ( EventRaceChange != null )
                        EventRaceChange( );
                    if ( MyDataChanged != null )
                        MyDataChanged( );
                }
            }
        }

        protected virtual new void clearMyEvents() {
            base.clearMyEvents();

            EventRaceChange = null;
        }
    }
}
