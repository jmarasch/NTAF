using System;
using System.Windows.Forms;

namespace NTAF.PlugInFramework {

    #region ICreatorInfo
    /// <summary>
    /// DONT USE THIS
    /// </summary>
    [Obsolete("Use attributes o define info", true)]
    public interface ICreatorInfo {
        /// <summary>
        /// Returns the classes creators name
        /// </summary>
        String Creator { get; }
        /// <summary>
        /// Returns the classes creators contact info, ie e-mail, phone, or mailing address
        /// </summary>
        String Contact { get; }
        /// <summary>
        /// Returns the classes creators Web address
        /// </summary>
        String WebUrl { get; }
        /// <summary>
        /// Returns the classes Version info, not the same as the plugins, dll, or file version neccicarrily.
        /// </summary>
        String Version { get; }
    } 
    #endregion    

    //#region IEditorPlugin
    //[Obsolete("use new base class for this", false)]
    //public interface IEditorPlugin{//: ICreatorInfo {
    //    Boolean Graphical { get; }

    //    Object MyObject { get; set; }

    //    void RunEditor( EditorMode mode );
    //    void RunEditor( Object setObject, EditorMode mode );

    //    Boolean IEdit( Type thisType );
    //    //Boolean IEdit( Type[] theseTypes );

    //    Type CollectionType { get; }

    //    EditorExitCode MyResult { get; }

    //    EditorMode Mode { get; set; }
    //}
    //#endregion

    //#region IObjectClass

    ///// <summary>
    ///// 
    ///// </summary>
    //public interface IObjectClass : INTId, INTName{//, ICreatorInfo {
    //    event NTEventHandler MyDataChanged;

    //    /// <summary>
    //    /// Returns the name of the collection.
    //    /// </summary>
    //    String CollectionName { get; }

    //    /// <summary>
    //    /// Returns the type of the collection
    //    /// </summary>
    //    Type CollectionType { get; }

    //    /// <summary>
    //    /// Checks a passed in obj and determins if it matches the type of this collector
    //    /// </summary>
    //    /// <param name="obj">Type of the class to check</param>
    //    /// <returns>true/false</returns>
    //    Boolean IsOfType( Object obj );

    //    /// <summary>
    //    /// Checks a passed in obj and determins if it matches the type of this collector
    //    /// </summary>
    //    /// <param name="T">Type to check</param>
    //    /// <returns>true/false</returns>
    //    Boolean IsOfType( Type T );

    //    /// <summary>
    //    /// Returns the type of the object
    //    /// </summary>
    //    /// <returns>This objects type</returns>
    //    Type MyType( );

    //    /// <summary>
    //    /// Serializes a passed object provided it is of the correct type
    //    /// </summary>
    //    /// <param name="toSerial">Object being serialized or turned into saveable data</param>
    //    /// <param name="tempPath">Temporary location where all objects are being saved to</param>
    //    /// <returns>The path of the saved object to be manipulated later</returns>
    //    String Serialize( Object toSerial, String tempPath );

    //    /// <summary>
    //    /// Deserializes an object passed to it provided it can be converte to this type of object
    //    /// </summary>
    //    /// <param name="PathOfSavedObject">Location where the deserializer will find the saved object</param>
    //    /// <returns>The deseralized object</returns>
    //    Object Deserialize( String PathOfSavedObject );
        
    //    /// <summary>
    //    /// Tells the object to link back to original data items so when they are updaed the used items in the higher level objects are also updated
    //    /// </summary>
    //    /// <param name="DataMaster">The data master that the object can poll from to get the orginal collection item</param>
    //    void Link( ILink DataMaster );

    //}
    //#endregion

    #region ILink
    /// <summary>
    /// Provides an interface forwhich to allow the linking of lowerlevel objects to higherlevel objects.
    /// </summary>
    public interface ILink {
        /// <summary>
        /// Links data among all files, this is an important setep when loading files objects with a 
        /// higher object level reset their 'shallow pointers' back to the original items or root and/or
        /// mid level objects. basically it uses the shallow copy method so that when an original object changes
        /// the changes are reflected throughout the object(s) and file(s) that reference that(those) object(s) 
        /// </summary>
        void LinkData();

        /// <summary>
        /// Searches for and returns a traceable object
        /// </summary>
        /// <param name="obj">Object to be found in original data location</param>
        /// <returns>The original object</returns>
        Object FindObject( ObjectClassBase obj );

    }
    #endregion 
}