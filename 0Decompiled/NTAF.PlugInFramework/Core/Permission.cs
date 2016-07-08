using NTAF.Core.Properties;
using NTAF.PlugInFramework;
using System;
using System.ComponentModel;
using System.Threading;
using System.Xml.Serialization;

namespace NTAF.Core
{
	[Serializable]
	public class Permission : ObjectClassBase
	{
		private Species _BaseRacesCanEquip = 0;

		[Category("Base")]
		[Description("")]
		[XmlAttribute]
		public virtual Species SpeciesCanEquip
		{
			get
			{
				return this._BaseRacesCanEquip;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._BaseRacesCanEquip = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		public Permission()
		{
		}

		public override event NTEventHandler MyDataChanged;
	}
}