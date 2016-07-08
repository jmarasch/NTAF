using System;
using System.Collections.Generic;

namespace NTAF.Core
{
	public class OperationLogger
	{
		private List<Operation> i_UOperations = new List<Operation>();

		private List<Operation> i_ROperations = new List<Operation>();

		public EventHandler EventAddedUndoOperation;

		public EventHandler EventUndoOperationsChanged;

		public EventHandler EventRedoOperationsChanged;

		public int RedoCount
		{
			get
			{
				return this.i_ROperations.Count;
			}
		}

		public int UndoCount
		{
			get
			{
				return this.i_UOperations.Count;
			}
		}

		public OperationLogger()
		{
		}

		public void AddUndoableOpp(Enum Action, object Data)
		{
			this.AddUndoableOpp(new Operation(Action, Data));
		}

		public void AddUndoableOpp(Operation operation)
		{
			if (this.i_ROperations.Count != 0)
			{
				this.i_ROperations.Clear();
			}
			this.i_UOperations.Insert(0, operation);
			if (this.EventAddedUndoOperation != null)
			{
				this.EventAddedUndoOperation(this, new EventArgs());
			}
		}

		public void ClearLog()
		{
			this.i_ROperations.Clear();
			this.i_UOperations.Clear();
		}

		public Operation RedoAction()
		{
			if (this.i_ROperations.Count <= 0)
			{
				throw new Exception("Nothing to redo");
			}
			Operation retVal = this.i_ROperations[0];
			this.i_UOperations.Insert(0, retVal);
			this.i_ROperations.RemoveAt(0);
			if (this.EventUndoOperationsChanged != null)
			{
				this.EventUndoOperationsChanged(this, new EventArgs());
			}
			if (this.EventRedoOperationsChanged != null)
			{
				this.EventRedoOperationsChanged(this, new EventArgs());
			}
			return retVal;
		}

		public Operation UndoAction()
		{
			if (this.i_UOperations.Count <= 0)
			{
				throw new Exception("Nothing to undo");
			}
			Operation retVal = this.i_UOperations[0];
			this.i_ROperations.Insert(0, retVal);
			this.i_UOperations.RemoveAt(0);
			if (this.EventUndoOperationsChanged != null)
			{
				this.EventUndoOperationsChanged(this, new EventArgs());
			}
			if (this.EventRedoOperationsChanged != null)
			{
				this.EventRedoOperationsChanged(this, new EventArgs());
			}
			return retVal;
		}
	}
}