using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NTAF.PlugInFramework;

namespace NTAF.Core {
    public static class NTReflection {
        public static Type[] GetObjectClassTypes() {
            //todo can be updated to use a plugin directory later, after i start working on a plugin engine
            
#if !DEBUG
            //todo get install path from registry if not in debug mode set InstallLocation to that location.
            RegistryKey
                NT = Registry.LocalMachine.OpenSubKey("NewTerra_Studios"),
                UB = NT.OpenSubKey("Universe_Builder");

            String
                InstallLocation = ( String )UB.GetValue( "InstallLocation", @"C:\Program Files\NewTerra Studios\Universe Builder" );
#else
            String
                InstallLocation = System.AppDomain.CurrentDomain.BaseDirectory;
#endif

            List<String>
                dllsToLoad = new List<string>( Directory.GetFiles( InstallLocation + "Plugins", "*.dll" ) );
            //dllsToLoad = new List<string>( Directory.GetFiles( System.AppDomain.CurrentDomain.BaseDirectory, "*.dll" ) );

            //dllsToLoad.Add( System.AppDomain.CurrentDomain.BaseDirectory + "NTAF.DataCore.dll" );

            Assembly
                ass = null;

            List<Type>
                reflectedTypes = new List<Type>(),
                objectClassTypes = new List<Type>();

            foreach ( String str in dllsToLoad ) {
                ass = Assembly.LoadFile( str );
                reflectedTypes.AddRange( ass.GetTypes() );
            }

            foreach ( Type typ in reflectedTypes ) {
                Object[]
                    atts = typ.GetCustomAttributes( typeof( ObjectClassPlugIn ), true );
                if(atts.Length != 0 && typ.IsSerializable)
                    objectClassTypes.Add( typ );
            }
            return objectClassTypes.ToArray();
        }
    }
}