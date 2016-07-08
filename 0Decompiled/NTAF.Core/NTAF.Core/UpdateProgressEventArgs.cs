using System;

namespace NTAF.Core
{
	public class UpdateProgressEventArgs : EventArgs
	{
		public readonly int current;

		public readonly int NumberOfItems;

		public readonly string ProcessingMessage;

		public readonly string verb;

		public readonly string lastItem;

		public string OfCount
		{
			get
			{
				object[] numberOfItems = new object[] { this.current, this.NumberOfItems };
				return string.Format("{0} of {1}", numberOfItems);
			}
		}

		public int Percent
		{
			get
			{
				double tmpDouble = (double)(this.current / this.NumberOfItems) * 100;
				return (int)tmpDouble;
			}
		}

		public string PercentCompleeted
		{
			get
			{
				return string.Format("{0}%", this.Percent);
			}
		}

		public string StackedCount
		{
			get
			{
				object[] numberOfItems = new object[] { this.current, this.NumberOfItems };
				return string.Format("{0}/{1}", numberOfItems);
			}
		}

		public UpdateProgressEventArgs(string Message, string Verb, string LastItem, int Number, int of)
		{
			this.current = Number;
			this.NumberOfItems = of;
			this.ProcessingMessage = Message;
			this.verb = Verb;
			this.lastItem = LastItem;
		}
	}
}