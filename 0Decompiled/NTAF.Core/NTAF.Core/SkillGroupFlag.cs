using System;

namespace NTAF.Core
{
	[Flags]
	public enum SkillGroupFlag
	{
		Generic = 1,
		Arsonist = 2,
		Mechanic = 4,
		TrapSetter = 8,
		Gunman = 16,
		ArmsMaster = 32,
		Medic = 64,
		Psyonicist = 128,
		Physical = 256,
		Stealth = 512,
		Agility = 1024
	}
}