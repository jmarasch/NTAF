using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTAF.Core {
    #region Custom Deligates
    public delegate void NTEventHandler();
    public delegate void NTEventHandler<T>( T args );

    #endregion

    public enum ArgAction {
        Add,
        Remove,
        Edit
    }

    public class NameChangeArgs : EventArgs {
        public readonly String
            NewName,
            OldName,
            ObjID;

        public NameChangeArgs( String newName, String oldName, String objID ) {
            NewName = newName;
            OldName = oldName;
            ObjID = objID;
        }
    }

    public class ItemChangedArgs : EventArgs {
        public readonly int
            Index;

        public readonly Object
            Item;

        public readonly ArgAction
            Action;

        public ItemChangedArgs( int index, Object item, ArgAction action ) {
            Index = index;
            Item = item;
            Action = action;
        }

    }

    public class UpdaterEventArgs : EventArgs {
        public readonly int
            NumberOfItems;

        public UpdaterEventArgs( int ItemCount ) {
            NumberOfItems = ItemCount;
        }
    }

    public class UpdateProgressEventArgs : EventArgs {
        public readonly int
            current,
            NumberOfItems;

        public readonly string
            ProcessingMessage,
            verb,
            lastItem;

        public string StackedCount {
            get { return String.Format( "{0}/{1}", new Object[] { current, NumberOfItems } ); }
        }

        public string OfCount {
            get { return String.Format( "{0} of {1}", new Object[] { current, NumberOfItems } ); }
        }

        public string PercentCompleeted {
            get { return String.Format( "{0}%", Percent); }
        }

        public int Percent {
            get {
                Double
                    tmpDouble = ( current / NumberOfItems ) * 100.0D;

                return ( int )tmpDouble;
            }
        }

        public UpdateProgressEventArgs( string Message, string Verb, string LastItem, int Number, int of ) {
            current = Number;
            NumberOfItems = of;

            ProcessingMessage = Message;
            verb = Verb;
            lastItem = LastItem;
        }
    }
}
