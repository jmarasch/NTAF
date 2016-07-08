using System;

namespace NTAF.Core
{
	public class StatException : Exception
	{
		private StatsMod.Stats _HappenedOnStat = StatsMod.Stats.None;

		private StatException.StatExceptionType _SizeIssue = StatException.StatExceptionType.none;

		private int _Max = 12;

		private uint _Min = 1;

		public StatsMod.Stats HappendOnStat
		{
			get
			{
				return this._HappenedOnStat;
			}
		}

		public int Max
		{
			get
			{
				return this._Max;
			}
		}

		public uint Minimum
		{
			get
			{
				return this._Min;
			}
		}

		public StatException.StatExceptionType SizeIssue
		{
			get
			{
				return this._SizeIssue;
			}
		}

		public StatException()
		{
		}

		public StatException(string message) : base(message)
		{
		}

		public StatException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public StatException(StatsMod.Stats OnStat, StatException.StatExceptionType Issue, sbyte StatMin, byte StatMax) : base(string.Concat(OnStat.ToString(), (Issue == StatException.StatExceptionType.ToSmall ? " cannot go below " : " cannot go above "), (Issue == StatException.StatExceptionType.ToSmall ? StatMin.ToString() : StatMax.ToString())))
		{
			this._HappenedOnStat = OnStat;
			this._SizeIssue = Issue;
			this._Min = (uint)StatMin;
			this._Max = StatMax;
		}

		public StatException(StatsMod.Stats OnStat, StatException.StatExceptionType Issue, uint StatMin, int StatMax) : base(string.Concat(OnStat.ToString(), (Issue == StatException.StatExceptionType.ToSmall ? " cannot go below " : " cannot go above "), (Issue == StatException.StatExceptionType.ToSmall ? StatMin.ToString() : StatMax.ToString())))
		{
			this._HappenedOnStat = OnStat;
			this._SizeIssue = Issue;
			this._Min = StatMin;
			this._Max = StatMax;
		}

		public enum StatExceptionType
		{
			none,
			ToLarge,
			ToSmall
		}
	}
}