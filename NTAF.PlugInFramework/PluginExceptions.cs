using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTAF.PlugInFramework {
    /// <summary>
    /// Exception used to denote that no plugins have been loaded for a particular type
    /// </summary>
    public class NoPluginsLoadedException : Exception {
        /// <summary>
        /// Creates an empty exception object
        /// </summary>
        public NoPluginsLoadedException() { }
        /// <summary>
        /// Creates an exception object
        /// </summary>
        /// <param name="message">Error message that accompanies the error</param>
        public NoPluginsLoadedException( string message ) : base( message ) { }
        /// <summary>
        /// Creates an exception object
        /// </summary>
        /// <param name="message">Error message that acompanies the error</param>
        /// <param name="innerException">Underlying error that that caused this error</param>
        public NoPluginsLoadedException( string message, Exception innerException ) : base( message, innerException ) { }
    }
    /// <summary>
    /// Eception used to denote the fact that a proper editor could not be found
    /// to edit the object.
    /// </summary>
    public class NoEditorFound : Exception {
        readonly String
            Creator = "",
            Contact = "",
            WebUrl = "",
            Version = "";

        /// <summary>
        /// Creates an exception object
        /// </summary>
        /// <param name="creator">Name of the person that made the ObjectClass</param>
        /// <param name="contacct">Contact information of the person that created the Object Class plugin</param>
        /// <param name="webUrl">WebLocation that the plugin shouldhave been goten from</param>
        /// <param name="version">Version of the current Object Class plugin</param>
        public NoEditorFound( String creator, String contacct, String webUrl, String version ) {
            Creator = creator;
            Contact = contacct;
            WebUrl = webUrl;
            Version = version;
        }
        /// <summary>
        /// Creates an exception object
        /// </summary>
        /// <param name="creator">Name of the person that made the ObjectClass</param>
        /// <param name="contacct">Contact information of the person that created the Object Class plugin</param>
        /// <param name="webUrl">WebLocation that the plugin shouldhave been goten from</param>
        /// <param name="version">Version of the current Object Class plugin</param>
        /// <param name="message">Error message that accompanies the error</param>
        public NoEditorFound( String creator, String contacct, String webUrl, String version, string message )
            : base( message ) {
            Creator = creator;
            Contact = contacct;
            WebUrl = webUrl;
            Version = version;
        }
        /// <summary>
        /// Creates an exception object
        /// </summary>
        /// <param name="creator">Name of the person that made the ObjectClass</param>
        /// <param name="contacct">Contact information of the person that created the Object Class plug in</param>
        /// <param name="webUrl">WebLocation that the plug in should have been goten from</param>
        /// <param name="version">Version of the current Object Class plugin</param>
        /// <param name="message">Error message that accompanies the error</param>
        /// <param name="innerException">Underlying error that caused this error</param>
        public NoEditorFound( String creator, String contacct, String webUrl, String version, string message, Exception innerException )
            : base( message, innerException ) {
            Creator = creator;
            Contact = contacct;
            WebUrl = webUrl;
            Version = version;
        }
    }
    /// <summary>
    /// Occurs when an ObjectClass plugin is available but a proper Collector could not be found
    /// </summary>
    public class NoOCCFound : Exception {
        /// <summary>
        /// Creates an empty exception
        /// </summary>
        public NoOCCFound() { }
        /// <summary>
        /// Creates an exception
        /// </summary>
        /// <param name="message">Message about the error</param>
        public NoOCCFound( string message ) : base( message ) { }
        /// <summary>
        /// Creates an exception
        /// </summary>
        /// <param name="message">Message about the error</param>
        /// <param name="innerException">hold the exception that steamrolled in to this execption</param>
        public NoOCCFound( string message, Exception innerException ) : base( message, innerException ) { }
    }

    /// <summary>
    /// Execption that is thrown when thier is a ObjectClass and a collector but no Tree plugin to display the data in the collector
    /// </summary>
    public class NoTreeSupportFound : Exception {
        /// <summary>
        /// Creates an empty execption
        /// </summary>
        public NoTreeSupportFound() { }
        /// <summary>
        /// Creates an exception
        /// </summary>
        /// <param name="message">Message about the error</param>
        public NoTreeSupportFound( string message ) : base( message ) { }
        /// <summary>
        /// Creates an exception
        /// </summary>
        /// <param name="message">Message about the error</param>
        /// <param name="innerException">hold the exception that steamrolled in to this execption</param>
        public NoTreeSupportFound( string message, Exception innerException ) : base( message, innerException ) { }
    }
    /// <summary>
    /// A more general execption that can be thrown dealing with missing plugin items or extentions
    /// </summary>
    public class PluginMissing : Exception {
        /// <summary>
        /// Creates an empty execption
        /// </summary>
        public PluginMissing() { }
        /// <summary>
        /// Creates an exception
        /// </summary>
        /// <param name="message">Message about the error</param>
        public PluginMissing( string message ) : base( message ) { }        
        /// <summary>
        /// Creates an exception
        /// </summary>
        /// <param name="message">Message about the error</param>
        /// <param name="innerException">hold the exception that steamrolled in to this execption</param>
        public PluginMissing( string message, Exception innerException ) : base( message, innerException ) { }
    }
}
