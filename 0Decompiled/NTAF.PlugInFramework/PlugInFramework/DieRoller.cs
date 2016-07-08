using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NTAF.PlugInFramework
{
	public class DieRoller
	{
		private List<int> _results = new List<int>();

		public string Description
		{
			get;
			set;
		}

		public string Formula
		{
			get;
			set;
		}

		public DieRoller(string formula, string description)
		{
			this.Formula = formula;
			this.Description = description;
		}

		public virtual int Roll()
		{
			throw new NotImplementedException("Must be implemented in derived class");
		}

		public int[] Roll(int TimesToRollSet)
		{
			List<int> results = new List<int>();
			for (int i = 0; i < TimesToRollSet; i++)
			{
				results.Add(this.Roll());
			}
			return results.ToArray();
		}
	}
}