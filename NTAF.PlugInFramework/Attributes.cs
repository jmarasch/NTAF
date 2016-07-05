using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTAF.PlugInFramework {
    /// <summary>
    /// Attach this to an Object Class Collector and make it a plugin, sets the version info, and the plugin name
    /// </summary>
    [AttributeUsage( AttributeTargets.Class, AllowMultiple = false, Inherited = false )]
    public class OCCPlugIn : Attribute {
        /// <summary>
        /// returns the version of the plugin
        /// </summary>
        public readonly SerializableVersion
            version = null;

        /// <summary>
        /// gets the name of the plugin
        /// </summary>
        public readonly String
            Name = "";
        
        /// <summary>
        /// Creates an instance of the Object Class Collector attribute class, defines basic information
        /// about the object types name, and version
        /// </summary>
        /// <param name="plugInName">Name of this plugin</param>
        /// <param name="plugInVersion">Version of this plugin can be entered as a
        /// "0.0.0.0" string, '*' is current not implimented</param>
        public OCCPlugIn( string plugInName, string plugInVersion ) {
            version = new SerializableVersion(plugInName, "OCC", plugInVersion );
            Name = plugInName;
        }

        /// <summary>
        /// retrieves version informtion as a string
        /// </summary>
        /// <returns>version as a string</returns>
        public override string ToString() {
            return version.ToString();
        }
    }

    /// <summary>
    /// Used to define an editor plugin
    /// </summary>
    [AttributeUsage( AttributeTargets.Class, AllowMultiple = false, Inherited = false )]
    public class EditorPlugIn : Attribute {
        /// <summary>
        /// returns the version of the plugin
        /// </summary>
        public readonly SerializableVersion
            version = null;// new PlugInVersion( );

        /// <summary>
        /// gets the name of the plugin
        /// </summary>
        public readonly String
            Name = "";
        
        /// <summary>
        /// used to define if the editor is graphical
        /// </summary>
        public readonly bool
            isGUI = false;

        /// <summary>
        /// defined the types the editor can edit
        /// </summary>
        public readonly Type[]
            IEdit = new Type[] { };

        /// <summary>
        /// Creates an instance of the EditorPlugIn attribute, defines basic information about the editor
        /// including what it can edit, its name and version info
        /// </summary>
        /// <param name="plugInName">Name of the plugin</param>
        /// <param name="plugInVersion">Version of this plugin can be entered as a
        /// "0.0.0.0" string, '*' is current not implimented</param>
        /// <param name="IsGUI">Is the gui graphical</param>
        /// <param name="iEdit">what type does it edit</param>
        public EditorPlugIn( string plugInName, string plugInVersion, bool IsGUI, Type iEdit ) {
            version = new SerializableVersion( plugInName, "Editor", plugInVersion );
            Name = plugInName;
            isGUI = IsGUI;
            IEdit = new Type[] { iEdit };
        }

        /// <summary>
        /// Creates an instance of the EditorPlugIn attribute, defines basic information about the editor
        /// including what it can edit, its name and version info
        /// </summary>
        /// <param name="plugInName">Name of the plugin</param>
        /// <param name="plugInVersion">Version of this plugin can be entered as a
        /// "0.0.0.0" string, '*' is current not implimented</param>
        /// <param name="IsGUI">Is the gui graphical</param>
        /// <param name="iEdit">what types does it edit</param>
        public EditorPlugIn( string plugInName, string plugInVersion, bool IsGUI, Type[] iEdit ) {
            version = new SerializableVersion( plugInName, "Editor", plugInVersion );
            Name = plugInName;
            isGUI = IsGUI;
            IEdit = iEdit;
        }

        /// <summary>
        /// retrieves version informtion as a string
        /// </summary>
        /// <returns>version as a string</returns>
        public override string ToString() {
            return version.ToString();
        }
    }
    
    /// <summary>
    /// Used to define a TreeNode plugn
    /// </summary>
    [AttributeUsage( AttributeTargets.Class, AllowMultiple = false, Inherited = false )]
    public class TreeNodePlugIn : Attribute {
        /// <summary>
        /// returns the version of the plugin
        /// </summary>
        public  readonly SerializableVersion
            version = null;//new PlugInVersion( );

        /// <summary>
        /// gets the name of the plugin
        /// </summary>
        public readonly String
            Name = "";

        /// <summary>
        /// gets the name of the plugin
        /// </summary>
        public readonly String
            DisplayName = "";

        /// <summary>
        /// defined the types the tree can display
        /// </summary>
        public readonly Type[]
            IDisplay = new Type[] { };

        /// <summary>
        /// Creates an instance of the TreeNodePlugIn attribute, defines basic information about the node
        /// including its name and version info
        /// </summary>
        /// <param name="plugInName">Name of the plugin</param>
        /// <param name="displayName">Name to display on the main branch of the tree</param>
        /// <param name="plugInVersion">Version of this plugin can be entered as a
        /// "0.0.0.0" string, '*' is current not implimented</param>
        /// <param name="iDisplay">the type this tree displays</param>
        public TreeNodePlugIn( string plugInName, string displayName, string plugInVersion, Type iDisplay ) {
            version = new SerializableVersion( plugInName, "TreeNode", plugInVersion );
            Name = plugInName;
            IDisplay = new Type[] { iDisplay };
            DisplayName = displayName;
        }

        /// <summary>
        /// Creates an instance of the TreeNodePlugIn attribute, defines basic information about the node
        /// including its name and version info
        /// </summary>
        /// <param name="plugInName">Name of the plugin</param>
        /// <param name="displayName">Name to display on the main branch of the tree</param>
        /// <param name="plugInVersion">Version of this plugin can be entered as a
        /// "0.0.0.0" string, '*' is current not implimented</param>
        /// <param name="iDisplay">An array of types this tree can display</param>
        public TreeNodePlugIn( string plugInName, string displayName, string plugInVersion, Type[] iDisplay ) {
            version = new SerializableVersion( plugInName, "TreeNode", plugInVersion );
            Name = plugInName;
            IDisplay = iDisplay;
            DisplayName = displayName;
        }

        /// <summary>
        /// retrieves version informtion as a string
        /// </summary>
        /// <returns>version as a string</returns>        
        public override string ToString() {
            return version.ToString();
        }
    }

    /// <summary>
    /// Used to define a ObjectClass plugin
    /// </summary>
    [AttributeUsage( AttributeTargets.Class, AllowMultiple = false, Inherited = false )]
    public class ObjectClassPlugIn : Attribute {
        /// <summary>
        /// returns the version of the plugin
        /// </summary>
        public readonly SerializableVersion
            version =null;// new PlugInVersion( );

        /// <summary>
        /// gets the name of the plugin
        /// </summary>
        public  readonly String
            Name = "";

        /// <summary>
        /// Creates an instance of the ObjectClass attribute, defines basic information about the OC
        /// including its name and version info
        /// </summary>
        /// <param name="plugInName">Name of the plugin</param>
        /// <param name="plugInVersion">Version of this plugin can be entered as a
        /// "0.0.0.0" string, '*' is current not implimented</param>
        public ObjectClassPlugIn( string plugInName, string plugInVersion ) {
            version = new SerializableVersion( plugInName, "ObjectClass", plugInVersion );
            Name = plugInName;
        }

        /// <summary>
        /// retrieves version informtion as a string
        /// </summary>
        /// <returns>version as a string</returns>
        
        public override string ToString() {
            return version.ToString();
        }
    }

    /// <summary>
    /// Sets the plugin Designer
    /// </summary>
    [AttributeUsage( AttributeTargets.Assembly, AllowMultiple = false )]
    public sealed class PluginDesigner : Attribute {
        /// <summary>
        /// Designers Name
        /// </summary>
        private readonly string
            retVal;

        /// <summary>
        /// Creates an instance of the plugin designer attribute
        /// </summary>
        /// <param name="designer">Designers Name</param>
        public PluginDesigner( string designer ) {
            retVal = designer;
        }

        /// <summary>
        /// Returns the Designers Name
        /// </summary>
        public string Designer { get { return retVal; } }

        /// <summary>
        /// also returns the designers name
        /// </summary>
        /// <returns>String as the designers name</returns>
        public override string ToString() {
            return retVal;
        }
    }

    /// <summary>
    /// Use this attribute to define a method to contact the plugindesigner
    /// </summary>
    [AttributeUsage( AttributeTargets.Assembly, AllowMultiple = false )]
    public sealed class PluginDesignerContact : Attribute {
        /// <summary>
        /// gets contact info
        /// </summary>
        private readonly string
            retVal;

        /// <summary>
        /// creates instance of attribute
        /// </summary>
        /// <param name="designerContact">Designers contact info</param>
        public PluginDesignerContact( string designerContact ) {
            retVal = designerContact;
        }

        /// <summary>
        /// gets contact information
        /// </summary>
        public string DesignerContact { get { return retVal; } }
    
        /// <summary>
        /// gets contact information
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return retVal;
        }
    }

    /// <summary>
    /// attribute that defines the designers web url
    /// </summary>
    [AttributeUsage( AttributeTargets.Assembly, AllowMultiple = false )]
    public sealed class PluginDesignerWebUrl : Attribute {
        /// <summary>
        /// holds the designers web url
        /// </summary>
        private readonly string
            retVal;

        /// <summary>
        /// creates an instance of the PluginDesigner WebUrl
        /// </summary>
        /// <param name="designerWebUrl">URL that the designer owns or posts to</param>
        public PluginDesignerWebUrl( string designerWebUrl ) {
            retVal = designerWebUrl;
        }

        /// <summary>
        /// gets the url for the designer
        /// </summary>
        public string DesignerWebUrl { get { return retVal; } }

        /// <summary>
        /// also get the url for the designer
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return retVal;
        }
    }
}