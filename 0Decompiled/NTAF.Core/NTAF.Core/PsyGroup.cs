using System;

namespace NTAF.Core
{
	[Flags]
	public enum PsyGroup
	{
		Generic = 1,
		Fire = 2,
		Lighting = 4,
		Earth = 8,
		Water = 16,
		Cold = 32,
		Wind = 64,
		Cloud = 128,
		Mental = 256,
		Agility = 512,
		Medical = 1024
	}
}