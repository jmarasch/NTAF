using NTAF.PlugInFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace NTAF.Core
{
	public static class NTReflection
	{
		public static Type[] GetObjectClassTypes()
		{
			string InstallLocation = AppDomain.CurrentDomain.BaseDirectory;
			List<string> dllsToLoad = new List<string>(Directory.GetFiles(string.Concat(InstallLocation, "Plugins"), "*.dll"));
			List<Type> reflectedTypes = new List<Type>();
			List<Type> objectClassTypes = new List<Type>();
			foreach (string str in dllsToLoad)
			{
				reflectedTypes.AddRange(Assembly.LoadFile(str).GetTypes());
			}
			foreach (Type typ in reflectedTypes)
			{
				if (((int)typ.GetCustomAttributes(typeof(ObjectClassPlugIn), true).Length == 0 ? false : typ.IsSerializable))
				{
					objectClassTypes.Add(typ);
				}
			}
			return objectClassTypes.ToArray();
		}
	}
}