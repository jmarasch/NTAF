using System;

namespace NTAF.Core
{
	public static class ExIAboutMe
	{
		public static string getIAboutMe(object sender)
		{
			string str;
			try
			{
				string retVal = "";
				IAboutMe AMe = null;
				if (sender is IAboutMe)
				{
					AMe = (IAboutMe)sender;
				}
				retVal = (AMe == null ? "Details not avalable..." : AMe.aboutMe);
				str = retVal;
			}
			catch
			{
				str = "Details not avalable...";
			}
			return str;
		}
	}
}