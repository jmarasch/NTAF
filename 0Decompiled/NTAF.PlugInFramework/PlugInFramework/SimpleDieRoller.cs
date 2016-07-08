using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NTAF.PlugInFramework
{
	public class SimpleDieRoller : DieRoller
	{
		private List<Die> _dice = new List<Die>();

		public int Count
		{
			get
			{
				return this._dice.Count<Die>();
			}
		}

		public int Sides
		{
			get;
			set;
		}

		public SimpleDieRoller(string formula) : base(formula, "")
		{
			int c;
			try
			{
				string[] split = formula.ToLower().Split(new char[] { 'd' });
				int.TryParse(split[0], out c);
				this.Sides = int.Parse(split[1]);
				if (c == 0)
				{
					c++;
				}
				for (int i = 0; i < c; i++)
				{
					this._dice.Add(new Die(this.Sides));
				}
			}
			catch (Exception exception)
			{
				Exception ex = exception;
				Console.WriteLine(ex.Message);
				throw new Exception("Could not create die", ex);
			}
		}

		public string Result()
		{
			string retval = string.Concat(base.Formula, " : ");
			string str = this._dice.Aggregate<Die, string>(retval, (string current, Die die) => string.Concat(current, die.Last, " "));
			return str;
		}

		public override int Roll()
		{
			int num;
			try
			{
				int result = 0;
				foreach (Die die in this._dice)
				{
					result = result + die.Roll();
				}
				Console.WriteLine(string.Concat(this.Result(), " = ", result));
				num = result;
			}
			catch (Exception exception)
			{
				Exception ex = exception;
				Console.WriteLine(ex.Message);
				throw new Exception("Die roll failed", ex);
			}
			return num;
		}

		public override string ToString()
		{
			int num = this._dice.Sum<Die>((Die die) => die.Last);
			return num.ToString();
		}
	}
}