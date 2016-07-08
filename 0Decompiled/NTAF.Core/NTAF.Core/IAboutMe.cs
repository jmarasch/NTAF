using System;
using System.Xml.Serialization;

namespace NTAF.Core
{
	public interface IAboutMe
	{
		[XmlIgnore]
		string aboutMe
		{
			get;
		}
	}
}