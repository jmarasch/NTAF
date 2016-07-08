using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace NTAF.Core
{
	public static class GeneralOperations
	{
		public static List<T> EnumToList<T>()
		{
			Type enumType = typeof(T);
			if (enumType.BaseType != typeof(Enum))
			{
				throw new ArgumentException("T must be of type System.Enum");
			}
			Array enumValArray = Enum.GetValues(enumType);
			List<T> enumValList = new List<T>(enumValArray.Length);
			foreach (T val in enumValArray)
			{
				enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
			}
			return enumValList;
		}

		public static string GetDescription<T>(T EnumValue)
		{
			FieldInfo fi = EnumValue.GetType().GetField(EnumValue.ToString());
			DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
			return ((int)attributes.Length > 0 ? attributes[0].Description : EnumValue.ToString());
		}

		public static T GetEnumValueFromDescription<T>(string Description)
		{
			object retEnumValue = null;
			Type enumType = typeof(T);
			Array enumValueArray = Enum.GetValues(enumType);
			if (enumType.BaseType != typeof(Enum))
			{
				throw new ArgumentException("T must be of type System.Enum");
			}
			foreach (T val in enumValueArray)
			{
				if (GeneralOperations.GetDescription<T>(val).ToLower() == Description.ToLower())
				{
					retEnumValue = val;
				}
			}
			return (T)retEnumValue;
		}

		public static string WrapLength(string str, int len)
		{
			List<string> stringBuilder = new List<string>();
			string buffer = "";
			string retVal = "";
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
						stringBuilder.Add(buffer);
						buffer = "";
						curPos = 0;
					}
					curPos++;
				}
			}
			if (str != string.Empty)
			{
				stringBuilder.Add(str);
			}
			foreach (string tmpString in stringBuilder)
			{
				retVal = string.Concat(retVal, tmpString, "\n");
			}
			return retVal;
		}

		public static string[] WrapLength(int width, string str)
		{
			List<string> retVal = new List<string>();
			string buffer = "";
			int curPos = 0;
			for (int i = 0; i <= str.Length; i++)
			{
				if (i + 1 < str.Length)
				{
					if ((curPos < width ? true : !(str.Substring(i, 1) == " ")))
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