using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using UniverseBuilderSingle.Properties;
using NTAF.PlugInFramework;

namespace UniverseBuilderSingle {
    partial class LoadedModuleBox : Form {
        public LoadedModuleBox() {
            InitializeComponent();
            tbLoadedMods.Lines = getAssembliesAndPlugins();
        }

        private String[] getAssembliesAndPlugins() {
            List<String>
                RetVal = new List<string>();

            Assembly[]
                currentAssemblies = PluginEngine.LoadedAssemblies();

            foreach ( Assembly asm in currentAssemblies ) {
                Object[]
                    asmAttribs = asm.GetCustomAttributes(true);

                String
                    Designer = String.Empty,
                    Contact = String.Empty,
                    URL = String.Empty,
                    Title = String.Empty,
                    Description = String.Empty,
                    Company = String.Empty,
                    CopyRight = String.Empty,
                    TM = String.Empty,
                    ASMVersion = String.Empty,
                    FileVersion = String.Empty;

                foreach ( Object obj in asmAttribs ) {
                    if ( obj is PluginDesigner )
                        Designer = obj.ToString();
                    if ( obj is PluginDesignerContact )
                        Contact = obj.ToString();
                    if ( obj is PluginDesignerWebUrl )
                        URL = obj.ToString();
                    if ( obj is AssemblyTitleAttribute )
                        Title = ( ( AssemblyTitleAttribute )obj ).Title;
                    if ( obj is AssemblyDescriptionAttribute )
                        Description = ( ( AssemblyDescriptionAttribute )obj ).Description;
                    if ( obj is AssemblyCompanyAttribute )
                        Company = ( ( AssemblyCompanyAttribute )obj ).Company;
                    if ( obj is AssemblyCopyrightAttribute )
                        CopyRight = ( ( AssemblyCopyrightAttribute )obj ).Copyright;
                    if ( obj is AssemblyTrademarkAttribute )
                        TM = ( ( AssemblyTrademarkAttribute )obj ).Trademark;
                    if ( obj is AssemblyVersionAttribute )
                        ASMVersion = ( ( AssemblyVersionAttribute )obj ).Version;
                    if ( obj is AssemblyFileVersionAttribute )
                        FileVersion = ( ( AssemblyFileVersionAttribute )obj ).Version;
                }

                if ( Title != "" )
                    RetVal.Add( "Assembly File: " + Title);
                if ( Designer != "" )
                    RetVal.Add( "   Designer: " + Designer );
                if ( Contact != "" )
                    RetVal.Add( "   Contact: " + Contact );
                if ( URL != "" )
                    RetVal.Add( "   Designer URL:" + URL );
                if ( ASMVersion != "" )
                    RetVal.Add( "   Assembly Version: " + ASMVersion );
                if ( FileVersion != "" )
                    RetVal.Add( "   File Version: " + FileVersion );
                if ( Company != "" )
                    RetVal.Add( "   Company: " + Company );
                if ( CopyRight != "" )
                    RetVal.Add( "   CopyRight: " + CopyRight );
                if ( TM != "" )
                    RetVal.Add( "   TradeMark: " + ASMVersion );
                if ( Description != "" )
                    RetVal.Add( "   Description: " + Description );

                RetVal.Add( "" );
                RetVal.Add( "   Contained Plugins" );
                RetVal.Add( "" );

                Type[]
                    asmTypes = asm.GetTypes();

                List<String>
                    PluginTypes = new List<string>();

                foreach ( Type typ in asmTypes ) {
                    Object[]
                        asmTypAttribs = typ.GetCustomAttributes( typeof( ObjectClassPlugIn ), true );

                    if ( asmTypAttribs.Length >= 1 )
                        foreach ( Object asmTypAttrib in asmTypAttribs ) {
                            PluginTypes.Add( "      " + ( ( ObjectClassPlugIn )asmTypAttrib ).ToString() );
                        }

                    asmTypAttribs = typ.GetCustomAttributes( typeof( OCCPlugIn ), true );

                    if ( asmTypAttribs.Length >= 1 )
                        foreach ( Object asmTypAttrib in asmTypAttribs ) {
                            PluginTypes.Add( "      " + ( ( OCCPlugIn )asmTypAttrib ).ToString() );
                        }

                    asmTypAttribs = typ.GetCustomAttributes( typeof( EditorPlugIn ), true );

                    if ( asmTypAttribs.Length >= 1 )
                        foreach ( Object asmTypAttrib in asmTypAttribs ) {
                            PluginTypes.Add( "      " + ( ( EditorPlugIn )asmTypAttrib ).ToString() );
                        }

                    asmTypAttribs = typ.GetCustomAttributes( typeof( TreeNodePlugIn ), true );

                    if ( asmTypAttribs.Length >= 1 )
                        foreach ( Object asmTypAttrib in asmTypAttribs ) {
                            PluginTypes.Add( "      " + ( ( TreeNodePlugIn )asmTypAttrib ).ToString() );
                        }

                }

                PluginTypes.Sort();
                RetVal.AddRange( PluginTypes.ToArray() );
                RetVal.Add( "" );
            }
            return RetVal.ToArray();
        }
    }
}
