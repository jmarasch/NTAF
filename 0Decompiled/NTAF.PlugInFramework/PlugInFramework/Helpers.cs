using System;

namespace NTAF.PlugInFramework
{
	public static class Helpers
	{
		public static Random Randomizer;

		static Helpers()
		{
			Helpers.Randomizer = new Random();
		}
	}
}