using System;
using System.Runtime.CompilerServices;

namespace NTAF.PlugInFramework
{
	public class Die
	{
		internal Random rnd = new Random(Helpers.Randomizer.Next());

		public int Last
		{
			get;
			private set;
		}

		private int Sides
		{
			get;
			set;
		}

		public Die(int sides)
		{
			this.Sides = sides;
		}

		public int Roll()
		{
			this.Last = this.rnd.Next(1, this.Sides + 1);
			return this.Last;
		}
	}
}