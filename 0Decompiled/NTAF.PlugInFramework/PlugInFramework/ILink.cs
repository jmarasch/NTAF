using System;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// Provides an interface forwhich to allow the linking of lowerlevel objects to higherlevel objects.
	/// </summary>
	public interface ILink
	{
		/// <summary>
		/// Searches for and returns a traceable object
		/// </summary>
		/// <param name="obj">Object to be found in original data location</param>
		/// <returns>The original object</returns>
		object FindObject(ObjectClassBase obj);

		/// <summary>
		/// Links data among all files, this is an important setep when loading files objects with a 
		/// higher object level reset their 'shallow pointers' back to the original items or root and/or
		/// mid level objects. basically it uses the shallow copy method so that when an original object changes
		/// the changes are reflected throughout the object(s) and file(s) that reference that(those) object(s) 
		/// </summary>
		void LinkData();
	}
}