using System;
using System.Collections.Generic;
using System.ComponentModel;
using NTAF.Core;

namespace NTAF.PlugInFramework {
    /// <summary>
    /// The Object Collector Base Class provides all the functionality needed to create
    /// a functioning collector for any class that inherits IObjectClass.
    /// 
    /// to use this class you only need to override Collection Name, and CollectionType
    /// the only time its required to override objectLevel is when your 
    /// not creating a root or layer 0 IObjectClass  
    /// </summary>
    public class OCCBase {

        private List<Object>
            i_Colector = new List<Object>( );

        /// <summary>
        /// Returns an enumerator that iterates over the collection
        /// </summary>
        /// <returns>Returns an enumerator that iterates over the collection</returns>
        public List<Object>.Enumerator GetEnumerator() {
            return i_Colector.GetEnumerator();
        }

        /// <summary>
        /// fires when the collection is updated
        /// </summary>
        public event NTEventHandler<ItemChangedArgs>  CollectionUpdated;

        /// <summary>
        /// Override Required is level is above 0 or root
        /// Returns the layerd level of this object collection
        /// </summary>
        public virtual Byte objectLayer {
            get { return 0; } 
        }

        /// <summary>
        /// Override Required
        /// Returns the collection Name
        /// </summary>
        [ReadOnly( true )]
        public string CollectionName {
            get {
                object[]
                    typAtts = this.GetType().GetCustomAttributes( typeof( OCCPlugIn ), true );

                if ( typAtts.Length >= 1 )
                    return ( ( OCCPlugIn )typAtts[0] ).Name;

                return "UnKnow Collection Type";
            }
        }

        /// <summary>
        /// Override Required
        /// Returns the type of the collection
        /// </summary>
        [ReadOnly( true )]
        public virtual Type CollectionType {
            get { return typeof( Object ); }
        }
        
        ///// <summary>
        ///// Gathers information from the OCCPlugIn attribute and decides 
        ///// if it can collect the type that was passed to it.
        ///// </summary>
        ///// <param name="thisType">Type to check against the collector</param>
        ///// <returns>true if it can or does collect the type, false if it cannot</returns>
        //public Boolean ICollect( Type thisType ) {
        //    Object[]
        //        atts = this.GetType().GetCustomAttributes( typeof( OCCPlugIn ), true );

        //    if ( atts.Length >= 1 )
        //        foreach ( Type typ in ( ( OCCPlugIn )atts[0] ).ICollect )
        //            if ( typ == thisType )
        //                return true;

        //    return false;
        //}

        /// <summary>
        /// Checks a passed in obj and determins if it matches the type of this collector
        /// </summary>
        /// <param name="obj">Type of the class to check</param>
        /// <returns>true/false</returns>
        public virtual bool IsOfType( Object obj ) {
            return obj.GetType( ) == CollectionType;
        }
        
        /// <summary>
        /// Checks a passed in obj and determins if it matches the type of this collector
        /// </summary>
        /// <param name="T">Type of the class to check</param>
        /// <returns>true/false</returns>
        public virtual bool IsOfType( Type T ) {
            //return obj.GetType() == CollectionType;
            return T == CollectionType;
        }
        
        /// <summary>
        /// Returns all the object that this Collector holds
        /// </summary>
        public virtual Object[] Objects {
            get { return i_Colector.ToArray( ) as Object[]; }
        }
        
        /// <summary>
        /// Finds the index of the specified item
        /// </summary>
        /// <param name="Item">item to find index for</param>
        /// <returns>int index of the found item or -1 if it cannot find the item</returns>
        public virtual int FindIndex( object Item ) {
            if ( !IsOfType( Item ) )
                throw new InvalidParameter( "Item is not of the correct Type" );

            return i_Colector.IndexOf( Item );
        }
        
        /// <summary>
        /// Searched the collector based on a key and field.
        /// </summary>
        /// <param name="Key">Data to match</param>
        /// <param name="Field">Field to search</param>
        /// <returns>int index of the found item or -1 if it cannot find the item</returns>
        public virtual int FindIndex( string Key, SearchField Field ) {
            int
                retVal = -1;

            if ( Count >= 1 && ( i_Colector[0] is INTId && i_Colector[0] is INTName ) )
                for ( int i = 0; i < Count - 1; i++ )
                    if ( ( ( ( INTId )i_Colector[i] ).ID == Key && Field == SearchField.ID ) |
                        ( ( ( INTName )i_Colector[i] ).Name == Key && Field == SearchField.Name ) )
                        retVal = i;

            return retVal;
        }
        
        /// <summary>
        /// Returns the object at a specified index
        /// </summary>
        /// <param name="index">Index of the object</param>
        /// <returns>Object from the list</returns>
        public virtual object this[int index] {
            get {
                return i_Colector[index];
            }
            set {
                if ( !IsOfType( value ) )
                    throw new InvalidParameter( "Object type incorrect" );

                if ( index < 0 || index >= Count )
                    throw new IndexOutOfRangeException( "Index was out side the bounds of the collector" );

                i_Colector[index] = value;
            }
        }
        
        /// <summary>
        /// searches for the object by a passed object
        /// </summary>
        /// <param name="Item"></param>
        /// <returns>matching object from the Collector</returns>
        public virtual object this[object Item] {
            get {
                if ( !IsOfType( Item ) )
                    throw new InvalidParameter( "Object not of the correct type" );

                Object
                    retVal = null;

                if ( Count >= 1 ) {
                    int
                        index = FindIndex( Item );
                    if ( index != -1 )
                        retVal = i_Colector[index];
                }
                return retVal;
            }
        }
        
        /// <summary>
        /// searches for the object by a key field method
        /// </summary>
        /// <param name="Key">Search perammiter</param>
        /// <param name="Field">Filed to search in</param>
        /// <returns></returns>
        public virtual object this[string Key, SearchField Field] {
            get {
                Object
                    retVal = null;

                if ( Count >= 1 )
                    retVal = i_Colector[FindIndex( Key, Field )];

                return retVal;
            }
        }
        
        /// <summary>
        /// counts the number of objects in the collector
        /// </summary>
        /// <returns>number of objects in the collector</returns>
        [ReadOnly( true )]
        public virtual int Count {
            get { return i_Colector.Count; }
        }
        
        /// <summary>
        /// add an object to the collector
        /// </summary>
        /// <param name="Item">Item to add. Must be of the same type of collector or it will throw an invalid peramiter exception</param>
        public virtual void AddObject( object Item ) {
            if ( !IsOfType( Item ) )
                throw new InvalidParameter( "Object not of the correct type" );

            int
                newIndex = Count;

            i_Colector.Add( Item );

            if ( CollectionUpdated != null )//&& !NTAF.Core.Properties.Settings.Default.Loading )
                CollectionUpdated( new ItemChangedArgs( newIndex, Item, ArgAction.Add ) );
        }
        
        /// <summary>
        /// add multiple objects to the collector
        /// </summary>
        /// <param name="Items">Items to add. Must be of the same type of collector or it will throw an invalid peramiter exception</param>
        public virtual void AddObject( object[] Items ) {
            foreach ( Object obj in Items )
                if ( !IsOfType( obj ) )
                    throw new InvalidParameter( "Object not of the correct type" );

            foreach ( Object obj in Items ) {
                int
                newIndex = Count;

                i_Colector.Add( obj );

                if ( CollectionUpdated != null )
                    CollectionUpdated( new ItemChangedArgs( newIndex, obj, ArgAction.Add ) );
            }
        }
        
        /// <summary>
        /// add multiple objects to the collector
        /// </summary>
        /// <param name="Item">Items to add. Must be of the same type of collector or it will throw an invalid peramiter exception</param>
        /// <param name="atIndex">Index at which to insert the object</param>
        public virtual void AddObject( object Item, int atIndex ) {
            if ( !IsOfType( Item ) )
                throw new InvalidParameter( "Object not of the correct type" );

            i_Colector.Insert( atIndex, Item );

            if ( CollectionUpdated != null )
                CollectionUpdated( new ItemChangedArgs( atIndex, Item, ArgAction.Add ) );
        }
        
        /// <summary>
        /// add multiple objects to the collector
        /// </summary>
        /// <param name="Items">Items to add. Must be of the same type of collector or it will throw an invalid peramiter exception</param>
        /// <param name="atIndex">Index at which to insert the objects</param>
        public virtual void AddObject( object[] Items, int atIndex ) {
            foreach ( Object obj in Items )
                if ( !IsOfType( obj ) )
                    throw new InvalidParameter( "Object not of the correct type" );

            foreach ( Object obj in Items ) {
                int
                    newIndex = Count;

                i_Colector.Insert( atIndex, obj );

                if ( CollectionUpdated != null )
                    CollectionUpdated( new ItemChangedArgs( newIndex, obj, ArgAction.Add ) );

                atIndex++;
            }
        }
        
        /// <summary>
        /// removes an object from the collector
        /// </summary>
        /// <param name="Item">object to remove. Must be of the same type of collector or it will throw an invalid peramiter exception</param>
        public virtual void DropObject( ObjectClassBase Item ) {
            if ( !IsOfType( Item ) )
                throw new InvalidParameter( "Object not of the correct type" );

            if ( !Exists( Item ) )
                throw new ItemException( "Could not drop object, it doesnt exist in the list" );

            int
                oldIndex = i_Colector.IndexOf( Item );

            //if ( !CheckForReferences( Item ) ) {

                i_Colector.Remove( Item );

                if ( CollectionUpdated != null )
                    CollectionUpdated( new ItemChangedArgs( oldIndex, Item, ArgAction.Remove ) );
            //}
        }
        
        /// <summary>
        /// removes multiple objects from the collector
        /// </summary>
        /// <param name="Items">objects to remove. Must be of the same type of collector or it will throw an invalid peramiter exception</param>
        public virtual void DropObject( object[] Items ) {
            foreach ( Object obj in Items )
                if ( !IsOfType( obj ) )
                    throw new InvalidParameter( "Object not of the correct type" );

            foreach ( Object obj in Items ) {
                int
                    oldIndex = FindIndex( obj );

                i_Colector.Remove( obj );

                if ( CollectionUpdated != null )
                    CollectionUpdated( new ItemChangedArgs( oldIndex, obj, ArgAction.Remove ) );
            }
        }
        
        /// <summary>
        /// remove an object from the collector
        /// </summary>
        /// <param name="atIndex">Index at which to insert the object</param>
        public virtual void DropObject( int atIndex ) {
            if ( atIndex >= Count || atIndex < 0 )
                throw new IndexOutOfRangeException( "Index was outside the bounds of the collector" );

            Object
                Removed = i_Colector[atIndex];

            i_Colector.RemoveAt( atIndex );

            if ( CollectionUpdated != null )
                CollectionUpdated( new ItemChangedArgs( atIndex, Removed, ArgAction.Remove ) );
        }

        //todo add edit option into interface force the call to edit this

        /// <summary>
        /// Edits the object in the collector with the the passed in new values
        /// </summary>
        /// <param name="toEdit">Item to edit</param>
        /// <param name="newValues">Items New Values</param>
        public virtual void EditObject( Object toEdit, Object newValues ) {
            if ( !IsOfType( toEdit ) )
                throw new InvalidParameter( "editing object not of the correct type" );

            if ( !IsOfType( newValues ) )
                throw new InvalidParameter( "New Values object must be of the same type" );

            if ( !( this[toEdit] is IMemberCopy ) )
                throw new InvalidOperationException(
                    "Plugin doesnot support editing, IMemberCopy functionallity not found" );

            ( ( IMemberCopy )this[toEdit] ).CopyMembers( newValues );

            if ( CollectionUpdated != null )//add find index here
                CollectionUpdated( new ItemChangedArgs( 0, newValues, ArgAction.Edit ) );
        }
        
        /// <summary>
        /// clears all items from the collector.
        /// </summary>
        public virtual void Clear() {
            List<Object>
                PsyToRemove = new List<Object>( i_Colector );

            DropObject( PsyToRemove.ToArray( ) );

        }
        
        /// <summary>
        /// Checks to see of the collector allreaddy holds atleast one of the specified objects
        /// </summary>
        /// <param name="Item">Item to check for</param>
        /// <returns>true/false</returns>
        public virtual bool Exists( object Item ) {
            if ( !IsOfType( Item ) )
                throw new InvalidParameter( "Object not of the correct type" );

            return i_Colector.Contains( Item );
        }
    }
}