using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTAF.Core {
    public class OperationLogger {
        List<Operation> 
            i_UOperations = new List<Operation>(),
            i_ROperations = new List<Operation>();

        public EventHandler EventAddedUndoOperation;
        public EventHandler EventUndoOperationsChanged;
        public EventHandler EventRedoOperationsChanged;

        public int UndoCount { get { return i_UOperations.Count; } }
        public int RedoCount { get { return i_ROperations.Count; } }

        public void AddUndoableOpp( Enum Action, Object Data ) {
            AddUndoableOpp( new Operation( Action, Data ) );
        }

        public void AddUndoableOpp( Operation operation ) {
            if ( i_ROperations.Count != 0 )
                i_ROperations.Clear();
            i_UOperations.Insert( 0, operation );

            if ( EventAddedUndoOperation != null )
                EventAddedUndoOperation(this, new EventArgs());
        }

        public Operation UndoAction() {
            if ( i_UOperations.Count <= 0 )
                throw new Exception( "Nothing to undo" );
            
            Operation retVal = i_UOperations[0];
            i_ROperations.Insert( 0, retVal );
            i_UOperations.RemoveAt( 0 );

            if ( EventUndoOperationsChanged != null )
                EventUndoOperationsChanged( this, new EventArgs() );

            if ( EventRedoOperationsChanged != null )
                EventRedoOperationsChanged( this, new EventArgs() );

            return retVal;
        }

        public Operation RedoAction() {
            if ( i_ROperations.Count <= 0 )
                throw new Exception( "Nothing to redo" );
            
            Operation retVal = i_ROperations[0];
            i_UOperations.Insert( 0, retVal );
            i_ROperations.RemoveAt( 0 );

            if ( EventUndoOperationsChanged != null )
                EventUndoOperationsChanged( this, new EventArgs() );

            if ( EventRedoOperationsChanged != null )
                EventRedoOperationsChanged( this, new EventArgs() );

            return retVal;
        }

        public void ClearLog() {
            i_ROperations.Clear();
            i_UOperations.Clear();
        }
    }

    public class Operation {
        Enum 
            i_Action = null;

        Object
            i_Data = null;

        public Enum Action {
            get { return i_Action; }
        }

        public Object Data {
            get { return i_Data; } 
        }

        public Operation( Enum action, Object data ) {
            i_Data = data;
            i_Action = action;
        }

    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace ActionTracker{
//    public class OperationLogger {
//        List<Operation> 
//            i_UOperations = new List<Operation>(),
//            i_ROperations = new List<Operation>();

//        public EventHandler EventAddedUndoOperation;
//        public EventHandler EventUndoOperationsChanged;
//        public EventHandler EventRedoOperationsChanged;

//        public int UndoCount { get { return i_UOperations.Count; } }
//        public int RedoCount { get { return i_ROperations.Count; } }

//        public void AddUndoableOpp( String Action, Object Data ) {
//            AddUndoableOpp( new Operation( Action, Data ) );
//        }

//        public void AddUndoableOpp( Operation ) {
//            if ( i_ROperations.Count != 0 )
//                i_ROperations.Clear();
//            i_UOperations.Insert( 0, operation );

//            if ( EventAddedUndoOperation != null )
//                EventAddedUndoOperation(this, new EventArgs());
//        }

//        public Operation UndoAction() {
//            if ( i_UOperations.Count <= 0 )
//                throw new Exception( "Nothing to undo" );
            
//            Operation retVal = i_UOperations[0];
//            i_ROperations.Insert( 0, retVal );
//            i_UOperations.RemoveAt( 0 );

//            if ( EventUndoOperationsChanged != null )
//                EventUndoOperationsChanged( this, new EventArgs() );

//            if ( EventRedoOperationsChanged != null )
//                EventRedoOperationsChanged( this, new EventArgs() );

//            return retVal;
//        }

//        public Operation RedoAction() {
//            if ( i_UOperations.Count <= 0 )
//                throw new Exception( "Nothing to redo" );
            
//            Operation retVal = i_ROperations[0];
//            i_UOperations.Insert( 0, retVal );
//            i_ROperations.RemoveAt( 0 );

//            if ( EventUndoOperationsChanged != null )
//                EventUndoOperationsChanged( this, new EventArgs() );

//            if ( EventRedoOperationsChanged != null )
//                EventRedoOperationsChanged( this, new EventArgs() );

//            return retVal;
//        }
//    }

//    public class Operation {
//        String 
//            i_Action = String.Empty;

//        Object
//            i_Data = null;

//        public String Action {
//            get { return i_Action; }
//        }

//        public Object Data {
//            get { return i_Data; } 
//        }

//        public Operation( String action, Object data ) {
//            i_Data = data;
//            i_Action = action;
//        }

//    }
//}