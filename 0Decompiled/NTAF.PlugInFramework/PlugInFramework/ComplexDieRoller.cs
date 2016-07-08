using IronPython.Hosting;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NTAF.PlugInFramework
{
	public class ComplexDieRoller : DieRoller
	{
		private List<SimpleDieRoller> _sets = new List<SimpleDieRoller>();

		private string _tokenFormula = "";

		private ScriptEngine engine = Python.CreateEngine();

		public ComplexDieRoller(string formula) : base(formula, "")
		{
			char tmp;
			this._tokenFormula = formula;
			int curSet = 0;
			do
			{
				int s = 0;
				int e = 0;
				int d = this._tokenFormula.ToLower().IndexOf('d');
				string eval = "";
				s = d - 1;
				while (s >= 0)
				{
					tmp = this._tokenFormula[s];
					if (!char.IsDigit(tmp))
					{
						s++;
						break;
					}
					else
					{
						eval = string.Concat(tmp, eval);
						s--;
					}
				}
				eval = string.Concat(eval, 'd');
				e = d + 1;
				while (e < this._tokenFormula.Length)
				{
					tmp = this._tokenFormula[e];
					if (!char.IsDigit(tmp))
					{
						break;
					}
					else
					{
						eval = string.Concat(eval, tmp);
						e++;
					}
				}
				if (s < 0)
				{
					s++;
				}
				if (eval == "d")
				{
					throw new Exception("Formula error, please check syntax.");
				}
				this._sets.Add(new SimpleDieRoller(eval));
				this._tokenFormula = this._tokenFormula.Remove(s, eval.Length);
				this._tokenFormula = this._tokenFormula.Insert(s, string.Concat("{", curSet, "}"));
				curSet++;
			}
			while (this._tokenFormula.ToLower().Contains<char>('d'));
			Python.ImportModule(this.engine, "math");
		}

		private void ParseDice(string formula)
		{
			while (formula.ToLower().Contains<char>('d'))
			{
			}
			base.Formula = formula;
		}

		public override int Roll()
		{
			int num;
			try
			{
				foreach (SimpleDieRoller set in this._sets)
				{
					set.Roll();
				}
				dynamic result = this.engine.CreateScriptSourceFromString(string.Format(this._tokenFormula, this._sets.ToArray())).Execute();
				typeof(Console).WriteLine(string.Concat(base.Formula, " = ") + result);
				int retval = -2147483648;
				typeof(int).TryParse(result.ToString(), &retval);
				num = retval;
				return num;
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
			}
			num = -2147483648;
			return num;
		}
	}
}