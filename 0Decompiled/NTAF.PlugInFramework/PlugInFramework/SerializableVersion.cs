using System;
using System.Xml.Serialization;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// Use anywhere an Xml serializable version number is required
	/// </summary>
	[Serializable]
	public class SerializableVersion
	{
		private string PluginName = "";

		private string PluginType = "";

		private int i_major = 0;

		private int i_minor = 0;

		private int i_revision = 0;

		private int i_build = 0;

		/// <summary>
		/// Current build for this Major/Minor/Revion
		/// </summary>
		[XmlAttribute]
		public int Build
		{
			get
			{
				return this.i_build;
			}
			set
			{
				this.i_build = value;
			}
		}

		/// <summary>
		/// Major Build Number
		/// </summary>
		[XmlAttribute]
		public int Major
		{
			get
			{
				return this.i_major;
			}
			set
			{
				this.i_major = value;
			}
		}

		/// <summary>
		/// Minor build number
		/// </summary>
		[XmlAttribute]
		public int Minor
		{
			get
			{
				return this.i_minor;
			}
			set
			{
				this.i_minor = value;
			}
		}

		/// <summary>
		/// Name of the versioned item
		/// </summary>
		[XmlAttribute]
		public string Name
		{
			get
			{
				return this.PluginName;
			}
			set
			{
				this.PluginName = value;
			}
		}

		/// <summary>
		/// Revision build number
		/// </summary>
		[XmlAttribute]
		public int Revision
		{
			get
			{
				return this.i_revision;
			}
			set
			{
				this.i_revision = value;
			}
		}

		/// <summary>
		/// Type of the versioned item
		/// </summary>
		[XmlAttribute]
		public string Type
		{
			get
			{
				return this.PluginType;
			}
			set
			{
				this.PluginType = value;
			}
		}

		/// <summary>
		/// Creates an empty SerializableVersion object
		/// </summary>
		public SerializableVersion()
		{
		}

		/// <summary>
		/// Creates a SerializableVersion object
		/// </summary>
		/// <param name="name">Name of the versioned item</param>
		/// <param name="type">Type of the versioned item</param>
		/// <param name="major">Major Build Number</param>
		/// <param name="minor">Minor build number</param>
		/// <param name="revision">Revision build number</param>
		/// <param name="build">Current build for this Major/Minor/Revion</param>
		public SerializableVersion(string name, string type, int major, int minor, int revision, int build)
		{
			this.Name = name;
			this.Type = type;
			this.Major = major;
			this.Minor = minor;
			this.Revision = revision;
			this.Build = build;
		}

		/// <summary>
		/// Creates a SerializableVersion object
		/// </summary>
		/// <param name="name">Name of the versioned item</param>
		/// <param name="type">Type of the versioned item</param>
		/// <param name="version">Version in string format "Major#.Minor#.Revision#.Build#"</param>
		public SerializableVersion(string name, string type, string version)
		{
			this.Name = name;
			this.Type = type;
			try
			{
				char[] chrArray = new char[] { '.' };
				this.i_major = int.Parse(version.Split(chrArray)[0]);
				chrArray = new char[] { '.' };
				this.i_minor = int.Parse(version.Split(chrArray)[1]);
				chrArray = new char[] { '.' };
				this.i_revision = int.Parse(version.Split(chrArray)[2]);
				chrArray = new char[] { '.' };
				this.i_build = int.Parse(version.Split(chrArray)[3]);
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Creates a SerializableVersion object
		/// </summary>
		/// <param name="version">Version in string format "Major#.Minor#.Revision#.Build#"</param>
		public SerializableVersion(SerializableVersion version)
		{
			this.Name = version.Name;
			this.Type = version.Type;
			this.Major = version.Major;
			this.Minor = version.Minor;
			this.Revision = version.Revision;
			this.Build = version.Build;
		}

		/// <summary>
		/// Checks if this object equals the tested object
		/// </summary>
		/// <param name="obj">Object to test</param>
		/// <returns>True if the objects match</returns>
		public override bool Equals(object obj)
		{
			bool flag;
			bool retval = false;
			try
			{
				if (obj is SerializableVersion)
				{
					retval = (!(this.Name == ((SerializableVersion)obj).Name) || !(this.Type == ((SerializableVersion)obj).Type) || this.Major != ((SerializableVersion)obj).Major || this.Minor != ((SerializableVersion)obj).Minor || this.Revision != ((SerializableVersion)obj).Revision ? false : this.Build == ((SerializableVersion)obj).Build);
				}
			}
			catch (NullReferenceException nullReferenceException)
			{
				flag = true;
				return flag;
			}
			catch (Exception exception)
			{
				throw exception;
			}
			flag = retval;
			return flag;
		}

		/// <summary>
		/// gets the objects hash code
		/// </summary>
		/// <returns>Integer object</returns>
		public override int GetHashCode()
		{
			return this.GetHashCode();
		}

		/// <summary>
		/// checks if one object is equal to another
		/// </summary>
		/// <param name="a">Subject A</param>
		/// <param name="b">Subject B</param>
		/// <returns>True if a <!--==--> b</returns>
		public static bool operator ==(SerializableVersion a, SerializableVersion b)
		{
			bool flag;
			bool retval = false;
			try
			{
				retval = a.Equals(b);
			}
			catch (NullReferenceException nullReferenceException)
			{
				flag = true;
				return flag;
			}
			catch (Exception exception)
			{
				throw exception;
			}
			flag = retval;
			return flag;
		}

		/// <summary>
		/// checks if one object is greater than to another
		/// </summary>
		/// <param name="a">Subject A</param>
		/// <param name="b">Subject B</param>
		/// <returns>True if a <!-->--> b</returns>
		public static bool operator >(SerializableVersion a, SerializableVersion b)
		{
			bool retval = false;
			try
			{
				retval = a.Major > b.Major;
				if (!retval)
				{
					retval = a.Minor > b.Minor;
				}
				if (!retval)
				{
					retval = a.Revision > b.Revision;
				}
				if (!retval)
				{
					retval = a.Build > b.Build;
				}
			}
			catch (NullReferenceException nullReferenceException)
			{
				throw;
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return retval;
		}

		/// <summary>
		/// checks if one object is greater than or equal to another
		/// </summary>
		/// <param name="a">Subject A</param>
		/// <param name="b">Subject B</param>
		/// <returns>True if a <!-->=--> b</returns>
		public static bool operator >=(SerializableVersion a, SerializableVersion b)
		{
			bool retval = false;
			try
			{
				retval = a.Major >= b.Major;
				if (retval)
				{
					retval = a.Minor >= b.Minor;
				}
				if (retval)
				{
					retval = a.Revision >= b.Revision;
				}
				if (retval)
				{
					retval = a.Build >= b.Build;
				}
			}
			catch (NullReferenceException nullReferenceException)
			{
				throw;
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return retval;
		}

		/// <summary>
		/// checks if one object is not equal to another
		/// </summary>
		/// <param name="a">Subject A</param>
		/// <param name="b">Subject B</param>
		/// <returns>True if a <!--!=--> b</returns>
		public static bool operator !=(SerializableVersion a, SerializableVersion b)
		{
			bool retval = false;
			try
			{
				retval = a == b;
			}
			catch (NullReferenceException nullReferenceException)
			{
				throw;
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return !retval;
		}

		/// <summary>
		/// checks if one object is less than another
		/// </summary>
		/// <param name="a">Subject A</param>
		/// <param name="b">Subject B</param>
		/// <returns>True if a <!--<--> b</returns>
		public static bool operator <(SerializableVersion a, SerializableVersion b)
		{
			bool retval = false;
			try
			{
				retval = a.Major < b.Major;
				if (!retval)
				{
					retval = a.Minor < b.Minor;
				}
				if (!retval)
				{
					retval = a.Revision < b.Revision;
				}
				if (!retval)
				{
					retval = a.Build < b.Build;
				}
			}
			catch (NullReferenceException nullReferenceException)
			{
				throw;
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return retval;
		}

		/// <summary>
		/// checks if one object is lessthan than or equal to another
		/// </summary>
		/// <param name="a">Subject A</param>
		/// <param name="b">Subject B</param>
		/// <returns>True if a <!--<=--> b</returns>
		public static bool operator <=(SerializableVersion a, SerializableVersion b)
		{
			bool retval = false;
			try
			{
				retval = a.Major <= b.Major;
				if (retval)
				{
					retval = a.Minor <= b.Minor;
				}
				if (retval)
				{
					retval = a.Revision <= b.Revision;
				}
				if (retval)
				{
					retval = a.Build <= b.Build;
				}
			}
			catch (NullReferenceException nullReferenceException)
			{
				throw;
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return retval;
		}

		/// <summary>
		/// Gets a string object representing the version of the object as "Type:Name Major.Minor.Revision.Build"
		/// </summary>
		/// <returns>"Type:Name Major.Minor.Revision.Build"</returns>
		public override string ToString()
		{
			object[] name = new object[] { this.Name, this.Type, this.Major, this.Minor, this.Revision, this.Build };
			return string.Format("{1}:{0} {2}.{3}.{4}.{5}", name);
		}

		/// <summary>
		/// gets string representing the version of this object as "Major.Minor.Revision.Build"
		/// </summary>
		/// <returns>"Major.Minor.Revision.Build"</returns>
		public string Version()
		{
			object[] major = new object[] { this.Major, this.Minor, this.Revision, this.Build };
			return string.Format("{0}.{1}.{2}.{3}", major);
		}
	}
}