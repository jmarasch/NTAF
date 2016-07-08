using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace UniverseBuilder.Properties
{
	[CompilerGenerated]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal class Resources
	{
		private static System.Resources.ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		internal static Bitmap effekt1
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("effekt1", Resources.resourceCulture);
			}
		}

		internal static Bitmap effekt2
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("effekt2", Resources.resourceCulture);
			}
		}

		internal static Icon File
		{
			get
			{
				return (Icon)Resources.ResourceManager.GetObject("File", Resources.resourceCulture);
			}
		}

		internal static Bitmap Galaxy
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Galaxy", Resources.resourceCulture);
			}
		}

		internal static Bitmap Galaxy_AboutScreen
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Galaxy_AboutScreen", Resources.resourceCulture);
			}
		}

		internal static Bitmap locked
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("locked", Resources.resourceCulture);
			}
		}

		internal static Icon Program
		{
			get
			{
				return (Icon)Resources.ResourceManager.GetObject("Program", Resources.resourceCulture);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static System.Resources.ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					Resources.resourceMan = new System.Resources.ResourceManager("UniverseBuilder.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		internal static Bitmap unlocked
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("unlocked", Resources.resourceCulture);
			}
		}

		internal Resources()
		{
		}
	}
}