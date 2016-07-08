using System;

namespace NTAF.Core
{
	[Flags]
	public enum Species
	{
		Human = 2,
		Mutant = 4,
		Undead = 8,
		Genics_A = 16,
		Genics_B = 32,
		Aquatic = 64,
		Angelic = 128,
		Demonic = 256,
		Fun = 512,
		All = 1022
	}
}