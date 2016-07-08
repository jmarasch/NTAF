using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.CompilerServices;

namespace NTAF.PrintEngine.Properties
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
		[UserScopedSetting]
		public PrinterSettings printerSettings
		{
			get
			{
				return (PrinterSettings)this["printerSettings"];
			}
			set
			{
				this["printerSettings"] = value;
			}
		}

		[DebuggerNonUserCode]
		[UserScopedSetting]
		public Font printFontSettings
		{
			get
			{
				return (Font)this["printFontSettings"];
			}
			set
			{
				this["printFontSettings"] = value;
			}
		}

		[DebuggerNonUserCode]
		[UserScopedSetting]
		public PageSettings printSettings
		{
			get
			{
				return (PageSettings)this["printSettings"];
			}
			set
			{
				this["printSettings"] = value;
			}
		}

		static Settings()
		{
			Settings.defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
		}

		public Settings()
		{
		}
	}
}