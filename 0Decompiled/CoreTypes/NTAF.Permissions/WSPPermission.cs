using NTAF.Core;
using NTAF.Core.Properties;
using NTAF.ObjectClasses;
using NTAF.PlugInFramework;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace NTAF.Permissions
{
	[Serializable]
	public class WSPPermission : Permission
	{
		private Race _SubRaceCanEquip = null;

		[Category("Base")]
		[Description("")]
		public virtual Race RaceCanEquip
		{
			get
			{
				return this._SubRaceCanEquip;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if ((value != null ? false : value != null))
				{
					throw new ArgumentException("When setting the value RaceCanEquip you must pass a Race value");
				}
				this._SubRaceCanEquip = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventRaceChange != null)
					{
						this.EventRaceChange();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		public WSPPermission()
		{
		}

		protected virtual new void clearMyEvents()
		{
			base.clearMyEvents();
			this.EventRaceChange = null;
		}

		public virtual event NTEventHandler EventRaceChange;

		public override event NTEventHandler MyDataChanged;
	}
}