using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Collections;
using Microsoft.Win32;

namespace NTAF.PlugInFramework {
    /// <summary>
    /// This class provides the functionallity to use NewTerra Plugins
    /// in an application
    /// </summary>
    public static class PluginEngine {
        /// <summary>
        /// Current max limit of supported object layers
        /// </summary>
        public const Byte MAX_OBJECT_LAYER = 10;

#if !DEBUG
        //todo get install path from registry if not in debug mode set InstallLocation to that location.
       public static readonly RegistryKey
            NT = Registry.LocalMachine.OpenSubKey("NewTerra_Studios"),
            UB = NT.OpenSubKey("Universe_Builder");

        /// <summary>
        /// Directory for the current application plugin folder
        /// </summary>
        public static readonly String
            PLUGIN_PATH = ( String )UB.GetValue( "InstallLocation", @"C:\Program Files\NewTerra Studios\Universe Builder" );
#else
        /// <summary>
        /// Directory for the current application plugin folder
        /// </summary>
        public static readonly String
                PLUGIN_PATH = System.AppDomain.CurrentDomain.BaseDirectory + "Plugins";
#endif

        /// <summary>
        /// Directory path for plugins stored in users personal folders
        /// </summary>
        public static readonly String
            USER_PLUGIN_PATH = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.Personal ), "NewTerra\\Plugins" );

        /// <summary>
        /// Holds all plugin assemblies
        /// </summary>
        private static List<Assembly>
            i_LoadedAssemblies;

        /// <summary>
        /// Gets the currently loaded assemblies
        /// </summary>
        /// <returns>An array of assemblies that meet the plugin assemblies
        /// expectations of what a Plugin assembly needs</returns>
        public static Assembly[] LoadedAssemblies() {
            if ( i_LoadedAssemblies == null || i_LoadedAssemblies.Count == 0 )
                i_LoadedAssemblies = new List<Assembly>( LoadPlugInAssemblies( ) );

            return i_LoadedAssemblies.ToArray( );
        }

        /// <summary>
        /// Rrfreshes the current list of loaded plugins but doesnot report back of whats loaded
        /// </summary>
        public static void Reload() {
            i_LoadedAssemblies = new List<Assembly>( LoadPlugInAssemblies() );
        }

        /// <summary>
        /// Checks assemblies in proper folder if the meet the requirements it will load the assembly and add it to the return array
        /// </summary>
        /// <returns>List of proper plugin assemblies</returns>
        private static Assembly[] LoadPlugInAssemblies() {
            if ( !Directory.Exists( PLUGIN_PATH ) )
                Directory.CreateDirectory( PLUGIN_PATH );

            if ( !Directory.Exists( USER_PLUGIN_PATH ) )
                Directory.CreateDirectory( USER_PLUGIN_PATH );

            DirectoryInfo
                AppDirInfo = new DirectoryInfo( PLUGIN_PATH ),
                UsrDirInfo = new DirectoryInfo( USER_PLUGIN_PATH );

            List<FileInfo>
                files = new List<FileInfo>( AppDirInfo.GetFiles( "*.dll" ) );

            files.AddRange( UsrDirInfo.GetFiles( "*.dll" ) );

            List<Assembly>
                plugInAssemblyList = new List<Assembly>( ),
                retVal = new List<Assembly>( );

            if ( null != files ) {
                foreach ( FileInfo file in files ) {
                    plugInAssemblyList.Add( Assembly.LoadFile( file.FullName ) );
                }
            }

            foreach ( Assembly asm in plugInAssemblyList ) {
                List<Object>
                    asmAtts = new List<object>( asm.GetCustomAttributes( typeof( PluginDesigner ), true ) );

                if ( asmAtts.Count == 0 ) asmAtts.Add( asm.GetCustomAttributes( typeof( PluginDesignerContact ), true ) );

                if ( asmAtts.Count == 0 ) asmAtts.Add( asm.GetCustomAttributes( typeof( PluginDesignerWebUrl ), true ) );

                if ( asmAtts.Count != 0 ) retVal.Add( asm );

            }

            retVal.Sort(SortAssembly);

            return retVal.ToArray( );

        }

        private static int SortAssembly( Assembly a, Assembly z ) {
            if ( a == null ) {
                if ( z == null ) {
                    return 0;
                }
                else {
                    return -1;
                }
            }
            else {
                if ( z == null ) {
                    return 1;
                }
                else {
                    String.Compare( a.ToString(), z.ToString() );
                }
            }
            return 0;
        }

        /// <summary>
        /// Returns all plugin types that are marked as Serializable
        /// </summary>
        /// <returns>Serializable class types</returns>
        public static Type[] GetSerailPlugins() {
            List<Type>
                availableTypes  = new List<Type>(),
                ApprovedPlugins = new List<Type>(),
                InterfaceTypes  = new List<Type>();

            foreach ( Assembly currentAssembly in LoadedAssemblies() )
                availableTypes.AddRange( currentAssembly.GetTypes() );

            foreach ( Type typ in availableTypes ) {
                List<Object>
                    attributes = new List<object>( typ.GetCustomAttributes( typeof( SerializableAttribute ), true ) );
                if ( attributes.Count >= 1 )
                    ApprovedPlugins.Add( typ );
            }

            ApprovedPlugins.Sort( SortPlugins );

            return ApprovedPlugins.ToArray();

        }

        ///// <summary>
        ///// Returns all plugin types that are marked as Serializable
        ///// </summary>
        ///// <returns>Serializable class types</returns>
        //public static Type[] GetSerailPlugins(int LayerLevel) {
        //    GetSerailPlugins().Where<Type>()

        //    return ApprovedPlugins.ToArray();

        //}

        /// <summary>
        /// Scans through all loaded assemblies and returns NTTreeNode Plugins        
        /// </summary>
        /// <returns>Array of loaded OCTreeNodeBase inheritied classes</returns>
        public static OCTreeNodeBase[] GetTreePlugIns() {
            List<Type>
                availableTypes  = new List<Type>( ),
                ApprovedPlugins = new List<Type>( ),
                InterfaceTypes  = new List<Type>( );

            foreach ( Assembly currentAssembly in LoadedAssemblies() )
                availableTypes.AddRange( currentAssembly.GetTypes( ) );

            foreach ( Type typ in availableTypes ) {
                List<Object>
                    attributes = new List<object>( typ.GetCustomAttributes( typeof( TreeNodePlugIn ), true ) );
                if ( attributes.Count >= 1 )
                    ApprovedPlugins.Add( typ );
            }

            ApprovedPlugins.Sort( SortPlugins );
            
            return ApprovedPlugins.ConvertAll<OCTreeNodeBase>( delegate( Type t ) { return Activator.CreateInstance( t ) as OCTreeNodeBase; } ).ToArray( );
        }

        /// <summary>
        /// Scans through all loaded assemblies and returns Editor Plugins        
        /// </summary>
        /// <returns>Array of loaded OCEditorBase inheritied classes</returns>
        public static OCEditorBase[] GetEditorPlugIns() {
            List<Type>
                availableTypes  = new List<Type>( ),
                ApprovedPlugins = new List<Type>( ),
                InterfaceTypes  = new List<Type>( );

            foreach ( Assembly currentAssembly in LoadedAssemblies ())
                availableTypes.AddRange( currentAssembly.GetTypes( ) );

            foreach ( Type typ in availableTypes ) {
                List<Object>
                    attributes = new List<object>( typ.GetCustomAttributes( typeof( EditorPlugIn ), true ) );
                if ( attributes.Count >= 1 )
                    ApprovedPlugins.Add( typ );
            }

            ApprovedPlugins.Sort( SortPlugins );

            return ApprovedPlugins.ConvertAll<OCEditorBase>( delegate( Type t ) { return Activator.CreateInstance( t ) as OCEditorBase; } ).ToArray();

        }

        /// <summary>
        /// Scans through all loaded assemblies and returns Class Collector Plugins        
        /// </summary>
        /// <returns>Array of loaded OCCBase inheritied classes</returns>
        public static OCCBase[] GetOCCPlugIns() {
            List<Type>
                availableTypes  = new List<Type>( ),
                ApprovedPlugins = new List<Type>( ),
                InterfaceTypes  = new List<Type>( );

            foreach ( Assembly currentAssembly in LoadedAssemblies() )
                availableTypes.AddRange( currentAssembly.GetTypes( ) );

            foreach ( Type typ in availableTypes ) {
                List<Object>
                    attributes = new List<object>( typ.GetCustomAttributes( typeof( OCCPlugIn ), true ) );
                if ( attributes.Count >= 1 )
                    ApprovedPlugins.Add( typ );
            }

            ApprovedPlugins.Sort( SortPlugins );

            return ApprovedPlugins.ConvertAll<OCCBase>( delegate( Type t ) { return Activator.CreateInstance( t ) as OCCBase; } ).ToArray();
        }

        /// <summary>
        /// Scans through all loaded assemblies and returns Class Collector Plugins in order by layer level stating at 0.       
        /// </summary>
        /// <returns>Array of loaded OCCBase inheritied classes</returns>
        public static OCCBase[] GetOCCPlugInsByLayer() {
            return GetOCCPlugIns().OrderBy(occ => occ.objectLayer).ToArray();
        }
        //

        /// <summary>
        /// Scans through all loaded assemblies and returns Object Class Plugins        
        /// </summary>
        /// <returns>Array of loaded IObjectClass inheritied classes</returns>
        public static ObjectClassBase[] GetObjectClasses() {
            List<Type>
                availableTypes  = new List<Type>( ),
                ApprovedPlugins = new List<Type>( ),
                InterfaceTypes  = new List<Type>( );

            foreach ( Assembly currentAssembly in LoadedAssemblies() )
                availableTypes.AddRange( currentAssembly.GetTypes( ) );

            foreach ( Type typ in availableTypes ) {
                List<Object>
                    attributes = new List<object>( typ.GetCustomAttributes( typeof( ObjectClassPlugIn ), true ) );
                if ( attributes.Count >= 1 )
                    ApprovedPlugins.Add( typ );
            }

            ApprovedPlugins.Sort( SortPlugins );

            return ApprovedPlugins.ConvertAll<ObjectClassBase>( delegate( Type t ) { return Activator.CreateInstance( t ) as ObjectClassBase; } ).ToArray();
        }

        private static int SortPlugins( Type a, Type z ) {
            if ( a == null ) {
                if ( z == null ) {
                    return 0;
                }
                else {
                    return -1;
                }
            }
            else {
                if ( z == null ) {
                    return 1;
                }
                else {
                    return String.Compare( a.Name, z.Name );
                }
            }
        }
    }
}