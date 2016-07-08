using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NTAF.Core
{
	public static class EnumExt
	{
		public static string GetDescription<T>(this Enum type, T EnumValue)
		{
			FieldInfo fi = EnumValue.GetType().GetField(EnumValue.ToString());
			DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
			return ((int)attributes.Length > 0 ? attributes[0].Description : EnumValue.ToString());
		}

		public static bool Is<T>(this Enum type, T value)
		{
			bool flag;
			try
			{
				flag = ((int)type & (int)(object)value) == (int)(object)value;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}
	}
}