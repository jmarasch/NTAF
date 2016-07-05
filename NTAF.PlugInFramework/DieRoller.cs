using System;
using System.Collections.Generic;
using System.Linq;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace NTAF.PlugInFramework {
    public class Die {
        internal Random rnd = new Random(Helpers.Randomizer.Next());

        private int Sides { get; set; }

        public int Last { get; private set; }

        public Die(int sides) {
            Sides = sides;
        }

        public int Roll() {
            Last = rnd.Next(1, Sides + 1);
            return Last;
        }
    }

    public class DieRoller {
        //private string _formula,
        //               _description;


        private List<int> _results = new List<int>();

        public string Formula { get; set; }
        public string Description { get; set; }

        public DieRoller(string formula, string description) {
            Formula = formula;
            Description = description;
        }

        public virtual int Roll() {
            throw new NotImplementedException("Must be implemented in derived class");
        }

        public int[] Roll(int TimesToRollSet) {
            List<int> results = new List<int>();
            for (int i = 0; i < TimesToRollSet; i++) {
                results.Add(Roll());
            }
            return results.ToArray();
        }
    }

    public class SimpleDieRoller : DieRoller {
        private List<Die> _dice = new List<Die>();

        public int Count {
            get { return _dice.Count(); }
        }

        public int Sides { get; set; }

        public SimpleDieRoller(string formula)
            : base(formula, "") {
            try {
                string[] split = formula.ToLower().Split('d');
                int c;
                int.TryParse(split[0], out c);

                Sides = int.Parse(split[1]);

                if (c == 0) c++;

                for (int i = 0; i < c; i++) {
                    _dice.Add(new Die(Sides));
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                throw new Exception("Could not create die", ex);
            }
        }

        public override int Roll() {
            try {
                int result = 0;
                foreach (Die die in _dice) {
                    result += die.Roll();
                }

                Console.WriteLine(Result() + " = " + result);

                return result;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                throw new Exception("Die roll failed", ex);
            }
        }

        public string Result() {
            string retval = Formula + " : ";
            retval = _dice.Aggregate(retval, (current, die) => current + (die.Last + " "));
            return retval;
        }

        public override string ToString() {
            return _dice.Sum(die => die.Last).ToString();
        }
    }

    public class ComplexDieRoller : DieRoller {
        private List<SimpleDieRoller> _sets = new List<SimpleDieRoller>();
        private string _tokenFormula = "";
        private ScriptEngine engine = Python.CreateEngine();

        public ComplexDieRoller(string formula)
            : base(formula, "") {
            //find all dice
            _tokenFormula = formula;
            int curSet = 0;

            do {
                int s = 0,
                    e = 0,
                    d = _tokenFormula.ToLower().IndexOf('d');
                string eval = "";
                char tmp;

                for (s = d - 1; s >= 0; s--) {
                    tmp = _tokenFormula[s];
                    if (char.IsDigit(tmp))
                        eval = tmp + eval;
                    else {
                        s++;
                        break;
                    }
                }

                eval += 'd';

                for (e = d + 1; e < _tokenFormula.Length; e++) {
                    tmp = _tokenFormula[e];
                    if (char.IsDigit(tmp))
                        eval += tmp;
                    else
                        break;
                }
                if (s < 0) s++;
                if (eval == "d") throw new Exception("Formula error, please check syntax.");
                _sets.Add(new SimpleDieRoller(eval));
                _tokenFormula = _tokenFormula.Remove(s, eval.Length);
                _tokenFormula = _tokenFormula.Insert(s, "{" + curSet + "}");
                //_tokenFormula = _tokenFormula. Replace(eval, "{" + curSet + "}");
                curSet++;
            } while (_tokenFormula.ToLower().Contains('d'));

            engine.ImportModule("math");

            //scope = engine.Runtime.CreateScope();

        }

        public override int Roll() {
            try {

                foreach (SimpleDieRoller set in _sets) {
                    set.Roll();
                }

                var result =
                    engine.CreateScriptSourceFromString(string.Format(_tokenFormula, _sets.ToArray())).Execute();

                Console.WriteLine(Formula + " = " + result);

                int retval = -2147483648;

                int.TryParse(result.ToString(), out retval);

                return retval;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return -2147483648;
        }

        private void ParseDice(string formula) {
            int curDice = 0;

            do {} while (formula.ToLower().Contains('d'));
            Formula = formula;
        }
    }
}