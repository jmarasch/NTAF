using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// This class provides the functionallity to use NewTerra Plugins
	/// in an application
	/// </summary>
	public static class PluginEngine
	{
		/// <summary>
		/// Current max limit of supported object layers
		/// </summary>
		public const byte MAX_OBJECT_LAYER = 10;

		/// <summary>
		/// Directory for the current application plugin folder
		/// </summary>
		public readonly static string PLUGIN_PATH;

		/// <summary>
		/// Directory path for plugins stored in users personal folders
		/// </summary>
		public readonly static string USER_PLUGIN_PATH;

		/// <summary>
		/// Holds all plugin assemblies
		/// </summary>
		private static List<Assembly> i_LoadedAssemblies;

		static PluginEngine()
		{
			PluginEngine.PLUGIN_PATH = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
			PluginEngine.USER_PLUGIN_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "NewTerra\\Plugins");
		}

		/// <summary>
		/// Scans through all loaded assemblies and returns Editor Plugins        
		/// </summary>
		/// <returns>Array of loaded OCEditorBase inheritied classes</returns>
		public static OCEditorBase[] GetEditorPlugIns()
		{
			List<Type> availableTypes = new List<Type>();
			List<Type> ApprovedPlugins = new List<Type>();
			List<Type> InterfaceTypes = new List<Type>();
			Assembly[] assemblyArray = PluginEngine.LoadedAssemblies();
			for (int i = 0; i < (int)assemblyArray.Length; i++)
			{
				availableTypes.AddRange(assemblyArray[i].GetTypes());
			}
			foreach (Type typ in availableTypes)
			{
				if ((new List<object>(typ.GetCustomAttributes(typeof(EditorPlugIn), true))).Count >= 1)
				{
					ApprovedPlugins.Add(typ);
				}
			}
			ApprovedPlugins.Sort(new Comparison<Type>(PluginEngine.SortPlugins));
			OCEditorBase[] array = ApprovedPlugins.ConvertAll<OCEditorBase>((Type t) => Activator.CreateInstance(t) as OCEditorBase).ToArray();
			return array;
		}

		/// <summary>
		/// Scans through all loaded assemblies and returns Object Class Plugins        
		/// </summary>
		/// <returns>Array of loaded IObjectClass inheritied classes</returns>
		public static ObjectClassBase[] GetObjectClasses()
		{
			List<Type> availableTypes = new List<Type>();
			List<Type> ApprovedPlugins = new List<Type>();
			List<Type> InterfaceTypes = new List<Type>();
			Assembly[] assemblyArray = PluginEngine.LoadedAssemblies();
			for (int i = 0; i < (int)assemblyArray.Length; i++)
			{
				availableTypes.AddRange(assemblyArray[i].GetTypes());
			}
			foreach (Type typ in availableTypes)
			{
				if ((new List<object>(typ.GetCustomAttributes(typeof(ObjectClassPlugIn), true))).Count >= 1)
				{
					ApprovedPlugins.Add(typ);
				}
			}
			ApprovedPlugins.Sort(new Comparison<Type>(PluginEngine.SortPlugins));
			ObjectClassBase[] array = ApprovedPlugins.ConvertAll<ObjectClassBase>((Type t) => Activator.CreateInstance(t) as ObjectClassBase).ToArray();
			return array;
		}

		/// <summary>
		/// Scans through all loaded assemblies and returns Class Collector Plugins        
		/// </summary>
		/// <returns>Array of loaded OCCBase inheritied classes</returns>
		public static OCCBase[] GetOCCPlugIns()
		{
			List<Type> availableTypes = new List<Type>();
			List<Type> ApprovedPlugins = new List<Type>();
			List<Type> InterfaceTypes = new List<Type>();
			Assembly[] assemblyArray = PluginEngine.LoadedAssemblies();
			for (int i = 0; i < (int)assemblyArray.Length; i++)
			{
				availableTypes.AddRange(assemblyArray[i].GetTypes());
			}
			foreach (Type typ in availableTypes)
			{
				if ((new List<object>(typ.GetCustomAttributes(typeof(OCCPlugIn), true))).Count >= 1)
				{
					ApprovedPlugins.Add(typ);
				}
			}
			ApprovedPlugins.Sort(new Comparison<Type>(PluginEngine.SortPlugins));
			OCCBase[] array = ApprovedPlugins.ConvertAll<OCCBase>((Type t) => Activator.CreateInstance(t) as OCCBase).ToArray();
			return array;
		}

		/// <summary>
		/// Returns all plugin types that are marked as Serializable
		/// </summary>
		/// <returns>Serializable class types</returns>
		public static Type[] GetSerailPlugins()
		{
			List<Type> availableTypes = new List<Type>();
			List<Type> ApprovedPlugins = new List<Type>();
			List<Type> InterfaceTypes = new List<Type>();
			Assembly[] assemblyArray = PluginEngine.LoadedAssemblies();
			for (int i = 0; i < (int)assemblyArray.Length; i++)
			{
				availableTypes.AddRange(assemblyArray[i].GetTypes());
			}
			foreach (Type typ in availableTypes)
			{
				if ((new List<object>(typ.GetCustomAttributes(typeof(SerializableAttribute), true))).Count >= 1)
				{
					ApprovedPlugins.Add(typ);
				}
			}
			ApprovedPlugins.Sort(new Comparison<Type>(PluginEngine.SortPlugins));
			return ApprovedPlugins.ToArray();
		}

		/// <summary>
		/// Scans through all loaded assemblies and returns NTTreeNode Plugins        
		/// </summary>
		/// <returns>Array of loaded OCTreeNodeBase inheritied classes</returns>
		public static OCTreeNodeBase[] GetTreePlugIns()
		{
			List<Type> availableTypes = new List<Type>();
			List<Type> ApprovedPlugins = new List<Type>();
			List<Type> InterfaceTypes = new List<Type>();
			Assembly[] assemblyArray = PluginEngine.LoadedAssemblies();
			for (int i = 0; i < (int)assemblyArray.Length; i++)
			{
				availableTypes.AddRange(assemblyArray[i].GetTypes());
			}
			foreach (Type typ in availableTypes)
			{
				if ((new List<object>(typ.GetCustomAttributes(typeof(TreeNodePlugIn), true))).Count >= 1)
				{
					ApprovedPlugins.Add(typ);
				}
			}
			ApprovedPlugins.Sort(new Comparison<Type>(PluginEngine.SortPlugins));
			OCTreeNodeBase[] array = ApprovedPlugins.ConvertAll<OCTreeNodeBase>((Type t) => Activator.CreateInstance(t) as OCTreeNodeBase).ToArray();
			return array;
		}

		/// <summary>
		/// Gets the currently loaded assemblies
		/// </summary>
		/// <returns>An array of assemblies that meet the plugin assemblies
		/// expectations of what a Plugin assembly needs</returns>
		public static Assembly[] LoadedAssemblies()
		{
			if ((PluginEngine.i_LoadedAssemblies == null ? true : PluginEngine.i_LoadedAssemblies.Count == 0))
			{
				PluginEngine.i_LoadedAssemblies = new List<Assembly>(PluginEngine.LoadPlugInAssemblies());
			}
			return PluginEngine.i_LoadedAssemblies.ToArray();
		}

		/// <summary>
		/// Checks assemblies in proper folder if the meet the requirements it will load the assembly and add it to the return array
		/// </summary>
		/// <returns>List of proper plugin assemblies</returns>
		private static Assembly[] LoadPlugInAssemblies()
		{
			if (!Directory.Exists(PluginEngine.PLUGIN_PATH))
			{
				Directory.CreateDirectory(PluginEngine.PLUGIN_PATH);
			}
			if (!Directory.Exists(PluginEngine.USER_PLUGIN_PATH))
			{
				Directory.CreateDirectory(PluginEngine.USER_PLUGIN_PATH);
			}
			DirectoryInfo AppDirInfo = new DirectoryInfo(PluginEngine.PLUGIN_PATH);
			DirectoryInfo UsrDirInfo = new DirectoryInfo(PluginEngine.USER_PLUGIN_PATH);
			List<FileInfo> files = new List<FileInfo>(AppDirInfo.GetFiles("*.dll"));
			files.AddRange(UsrDirInfo.GetFiles("*.dll"));
			List<Assembly> plugInAssemblyList = new List<Assembly>();
			List<Assembly> retVal = new List<Assembly>();
			if (null != files)
			{
				foreach (FileInfo file in files)
				{
					plugInAssemblyList.Add(Assembly.LoadFile(file.FullName));
				}
			}
			foreach (Assembly asm in plugInAssemblyList)
			{
				List<object> asmAtts = new List<object>(asm.GetCustomAttributes(typeof(PluginDesigner), true));
				if (asmAtts.Count == 0)
				{
					asmAtts.Add(asm.GetCustomAttributes(typeof(PluginDesignerContact), true));
				}
				if (asmAtts.Count == 0)
				{
					asmAtts.Add(asm.GetCustomAttributes(typeof(PluginDesignerWebUrl), true));
				}
				if (asmAtts.Count != 0)
				{
					retVal.Add(asm);
				}
			}
			retVal.Sort(new Comparison<Assembly>(PluginEngine.SortAssembly));
			return retVal.ToArray();
		}

		/// <summary>
		/// Rrfreshes the current list of loaded plugins but doesnot report back of whats loaded
		/// </summary>
		public static void Reload()
		{
			PluginEngine.i_LoadedAssemblies = new List<Assembly>(PluginEngine.LoadPlugInAssemblies());
		}

		private static int SortAssembly(Assembly a, Assembly z)
		{
			int num;
			if (a == null)
			{
				num = (!(z == null) ? -1 : 0);
			}
			else if (!(z == null))
			{
				string.Compare(a.ToString(), z.ToString());
				num = 0;
			}
			else
			{
				num = 1;
			}
			return num;
		}

		private static int SortPlugins(Type a, Type z)
		{
			int num;
			if (!(a == null))
			{
				num = (!(z == null) ? string.Compare(a.Name, z.Name) : 1);
			}
			else
			{
				num = (!(z == null) ? -1 : 0);
			}
			return num;
		}
	}
}