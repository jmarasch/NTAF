using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace NTAF.Core.Properties
{
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
	public sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance;

		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		[UserScopedSetting]
		public bool Loading
		{
			get
			{
				return (bool)this["Loading"];
			}
			set
			{
				this["Loading"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		[UserScopedSetting]
		public bool PerformingAction
		{
			get
			{
				return (bool)this["PerformingAction"];
			}
			set
			{
				this["PerformingAction"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		[UserScopedSetting]
		public bool Updating
		{
			get
			{
				return (bool)this["Updating"];
			}
			set
			{
				this["Updating"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		[UserScopedSetting]
		public bool VerboseDescription
		{
			get
			{
				return (bool)this["VerboseDescription"];
			}
			set
			{
				this["VerboseDescription"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		[UserScopedSetting]
		public bool VerboseID
		{
			get
			{
				return (bool)this["VerboseID"];
			}
			set
			{
				this["VerboseID"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		[UserScopedSetting]
		public bool VerboseName
		{
			get
			{
				return (bool)this["VerboseName"];
			}
			set
			{
				this["VerboseName"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		[UserScopedSetting]
		public bool VerboseOther
		{
			get
			{
				return (bool)this["VerboseOther"];
			}
			set
			{
				this["VerboseOther"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		[UserScopedSetting]
		public bool VerboseToString
		{
			get
			{
				return (bool)this["VerboseToString"];
			}
			set
			{
				this["VerboseToString"] = value;
			}
		}

		static Settings()
		{
			Settings.defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
		}

		public Settings()
		{
		}

		private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
		{
		}

		private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
		{
		}
	}
}