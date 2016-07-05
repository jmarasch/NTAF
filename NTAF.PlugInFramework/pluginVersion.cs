using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NTAF.PlugInFramework {
    /// <summary>
    /// Use anywhere an Xml serializable version number is required
    /// </summary>
    [Serializable( )]
    public class SerializableVersion {
        private string
            PluginName = "",
            PluginType = "";

        private int
            i_major = 0,
            i_minor = 0,
            i_revision = 0,
            i_build = 0;

        /// <summary>
        /// Name of the versioned item
        /// </summary>
        [XmlAttribute( )]
        public String Name {
            get { return PluginName; }
            set { PluginName = value; }
        }

        /// <summary>
        /// Type of the versioned item
        /// </summary>
        [XmlAttribute()]
        public String Type {
            get { return PluginType; }
            set { PluginType = value; }
        }

        /// <summary>
        /// Major Build Number
        /// </summary>
        [XmlAttribute( )]
        public int Major {
            get { return i_major; }
            set { i_major = value; }
        }

        /// <summary>
        /// Minor build number
        /// </summary>
        [XmlAttribute( )]
        public int Minor {
            get { return i_minor; }
            set { i_minor = value; }
        }

        /// <summary>
        /// Revision build number
        /// </summary>
        [XmlAttribute( )]
        public int Revision {
            get { return i_revision; }
            set { i_revision = value; }
        }

        /// <summary>
        /// Current build for this Major/Minor/Revion
        /// </summary>
        [XmlAttribute( )]
        public int Build {
            get { return i_build; }
            set { i_build = value; }
        }

        /// <summary>
        /// Creates an empty SerializableVersion object
        /// </summary>
        public SerializableVersion() { }


        /// <summary>
        /// Creates a SerializableVersion object
        /// </summary>
        /// <param name="name">Name of the versioned item</param>
        /// <param name="type">Type of the versioned item</param>
        /// <param name="major">Major Build Number</param>
        /// <param name="minor">Minor build number</param>
        /// <param name="revision">Revision build number</param>
        /// <param name="build">Current build for this Major/Minor/Revion</param>
        public SerializableVersion(string name, string type, int major, int minor, int revision, int build ) {
            Name = name;
            Type = type;
            Major = major;
            Minor = minor;
            Revision = revision;
            Build = build;
        }

        /// <summary>
        /// Creates a SerializableVersion object
        /// </summary>
        /// <param name="name">Name of the versioned item</param>
        /// <param name="type">Type of the versioned item</param>
        /// <param name="version">Version in string format "Major#.Minor#.Revision#.Build#"</param>
        public SerializableVersion( string name, string type, String version ) {
            Name = name;
            Type = type;
            try {
                i_major = Int32.Parse( version.Split( '.' )[0] );
                i_minor = Int32.Parse( version.Split( '.' )[1] );
                i_revision = Int32.Parse( version.Split( '.' )[2] );
                i_build = Int32.Parse( version.Split( '.' )[3] );
            }
            catch ( Exception ) {

                throw;
            }
        }

        /// <summary>
        /// Creates a SerializableVersion object
        /// </summary>
        /// <param name="version">Version in string format "Major#.Minor#.Revision#.Build#"</param>
        public SerializableVersion( SerializableVersion version ) {
            Name = version.Name;
            Type = version.Type;
            Major = version.Major;
            Minor = version.Minor;
            Revision = version.Revision;
            Build = version.Build;
        }

        /// <summary>
        /// Gets a string object representing the version of the object as "Type:Name Major.Minor.Revision.Build"
        /// </summary>
        /// <returns>"Type:Name Major.Minor.Revision.Build"</returns>
        public override string ToString() {
            Object[]
                tmpIntArr = new Object[] {Name, Type, Major, Minor, Revision, Build };
            return String.Format( "{1}:{0} {2}.{3}.{4}.{5}", tmpIntArr );
        }

        /// <summary>
        /// gets the objects hash code
        /// </summary>
        /// <returns>Integer object</returns>
        public override int GetHashCode() {
            return base.GetHashCode( );
        }

        /// <summary>
        /// gets string representing the version of this object as "Major.Minor.Revision.Build"
        /// </summary>
        /// <returns>"Major.Minor.Revision.Build"</returns>
        public string Version() {
            Object[]
                tmpIntArr = new Object[] { Major, Minor, Revision, Build };
            return String.Format( "{0}.{1}.{2}.{3}", tmpIntArr );
        }

        /// <summary>
        /// Checks if this object equals the tested object
        /// </summary>
        /// <param name="obj">Object to test</param>
        /// <returns>True if the objects match</returns>
        public override bool Equals( object obj ) {
            bool retval = false;
            try {
                if(obj is SerializableVersion)
                    retval = ( this.Name == ( ( SerializableVersion )obj ).Name ) && ( this.Type == ( ( SerializableVersion )obj ).Type ) &&
                         ( this.Major == ( ( SerializableVersion )obj ).Major ) && ( this.Minor == ( ( SerializableVersion )obj ).Minor ) &&
                         ( this.Revision == ( ( SerializableVersion )obj ).Revision ) && ( this.Build == ( ( SerializableVersion )obj ).Build );

                //retval = a.Major == b.Major;
                //if ( retval )
                //    retval = a.Minor == b.Minor;
                //if ( retval )
                //    retval = a.Revision == b.Revision;
                //if ( retval )
                //    retval = a.Build == b.Build;
            }
            catch ( NullReferenceException ) { return true; }
            catch ( Exception ex ) { throw ex; }
            return retval;
        }

        /// <summary>
        /// checks if one object is greater than or equal to another
        /// </summary>
        /// <param name="a">Subject A</param>
        /// <param name="b">Subject B</param>
        /// <returns>True if a <!-->=--> b</returns>
        public static bool operator >=( SerializableVersion a, SerializableVersion b ) {
            bool retval = false;
            try {
                retval = a.Major >= b.Major;
                if ( retval )
                    retval = a.Minor >= b.Minor;
                if ( retval )
                    retval = a.Revision >= b.Revision;
                if ( retval )
                    retval = a.Build >= b.Build;
            }
            catch ( NullReferenceException ) { throw; }
            catch ( Exception ex ) { throw ex; }
            return retval;
        }

        /// <summary>
        /// checks if one object is lessthan than or equal to another
        /// </summary>
        /// <param name="a">Subject A</param>
        /// <param name="b">Subject B</param>
        /// <returns>True if a <!--<=--> b</returns>
        public static bool operator <=( SerializableVersion a, SerializableVersion b ) {
            bool retval = false;
            try {
                retval = a.Major <= b.Major;
                if ( retval )
                    retval = a.Minor <= b.Minor;
                if ( retval )
                    retval = a.Revision <= b.Revision;
                if ( retval )
                    retval = a.Build <= b.Build;
            }
            catch ( NullReferenceException ) { throw; }
            catch ( Exception ex ) { throw ex; }
            return retval;
        }

        /// <summary>
        /// checks if one object is equal to another
        /// </summary>
        /// <param name="a">Subject A</param>
        /// <param name="b">Subject B</param>
        /// <returns>True if a <!--==--> b</returns>
        public static bool operator ==( SerializableVersion a, SerializableVersion b ) {
            bool retval = false;
            try {

                retval = a.Equals( b );

                //retval = a.Major == b.Major;
                //if ( retval )
                //    retval = a.Minor == b.Minor;
                //if ( retval )
                //    retval = a.Revision == b.Revision;
                //if ( retval )
                //    retval = a.Build == b.Build;
            }
            catch ( NullReferenceException ) { return true; }
            catch ( Exception ex ) { throw ex; }
            return retval;
        }

        /// <summary>
        /// checks if one object is not equal to another
        /// </summary>
        /// <param name="a">Subject A</param>
        /// <param name="b">Subject B</param>
        /// <returns>True if a <!--!=--> b</returns>
        public static bool operator !=( SerializableVersion a, SerializableVersion b ) {
            bool retval = false;
            try {
                retval = a == b;
            }
            catch ( NullReferenceException ) { throw; }
            catch ( Exception ex ) { throw ex; }
            return !retval;
        }
        /// <summary>
        /// checks if one object is greater than to another
        /// </summary>
        /// <param name="a">Subject A</param>
        /// <param name="b">Subject B</param>
        /// <returns>True if a <!-->--> b</returns>
        public static bool operator >( SerializableVersion a, SerializableVersion b ) {
            bool 
                retval = false;//,
                //rb = false,
                //rc = false,
                //rd = false;
            try {

                //retval = ( a.Major > b.Major ) || ( a.Minor > b.Minor ) ||
                //         ( a.Revision > b.Revision ) || ( a.Build > b.Build );


                retval = a.Major > b.Major; 
                if ( !retval )
                    retval = a.Minor > b.Minor;
                if ( !retval )
                    retval = a.Revision > b.Revision;
                if ( !retval )
                    retval = a.Build > b.Build;
            }
            catch ( NullReferenceException ) { throw; }
            catch ( Exception ex ) { throw ex; }
            return retval;
        }

        /// <summary>
        /// checks if one object is less than another
        /// </summary>
        /// <param name="a">Subject A</param>
        /// <param name="b">Subject B</param>
        /// <returns>True if a <!--<--> b</returns>
        public static bool operator <( SerializableVersion a, SerializableVersion b ) {
            bool retval = false;
            try {
                retval = a.Major < b.Major;
                if ( !retval )
                    retval = a.Minor < b.Minor;
                if ( !retval )
                    retval = a.Revision < b.Revision;
                if ( !retval )
                    retval = a.Build < b.Build;
            }
            catch ( NullReferenceException ) { throw; }
            catch ( Exception ex ) { throw ex; }
            return retval;
        }

    }
}
