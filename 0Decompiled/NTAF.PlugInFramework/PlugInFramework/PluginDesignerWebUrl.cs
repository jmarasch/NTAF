using System;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// attribute that defines the designers web url
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple=false)]
	public sealed class PluginDesignerWebUrl : Attribute
	{
		/// <summary>
		/// holds the designers web url
		/// </summary>
		private readonly string retVal;

		/// <summary>
		/// gets the url for the designer
		/// </summary>
		public string DesignerWebUrl
		{
			get
			{
				return this.retVal;
			}
		}

		/// <summary>
		/// creates an instance of the PluginDesigner WebUrl
		/// </summary>
		/// <param name="designerWebUrl">URL that the designer owns or posts to</param>
		public PluginDesignerWebUrl(string designerWebUrl)
		{
			this.retVal = designerWebUrl;
		}

		/// <summary>
		/// also get the url for the designer
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.retVal;
		}
	}
}