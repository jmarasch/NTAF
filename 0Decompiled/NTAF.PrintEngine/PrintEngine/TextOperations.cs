using System;
using System.Collections.Generic;

namespace NTAF.PrintEngine
{
	internal static class TextOperations
	{
		public static string[] WrapLength(string str, int len)
		{
			List<string> retVal = new List<string>();
			string buffer = "";
			int curPos = 0;
			for (int i = 0; i <= str.Length; i++)
			{
				if (i + 1 < str.Length)
				{
					if ((curPos < len ? true : !(str.Substring(i, 1) == " ")))
					{
						buffer = string.Concat(buffer, str.Substring(i, 1));
					}
					else
					{
						retVal.Add(buffer);
						buffer = "";
						curPos = 0;
					}
					curPos++;
				}
			}
			if (str != string.Empty)
			{
				retVal.Add(str);
			}
			return retVal.ToArray();
		}
	}
}