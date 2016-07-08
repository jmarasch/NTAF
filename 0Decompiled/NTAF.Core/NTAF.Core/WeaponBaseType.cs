using System;

namespace NTAF.Core
{
	[Flags]
	public enum WeaponBaseType
	{
		NoBase = 1,
		HandToHand = 2,
		Blade = 4,
		Bludgen = 8,
		PoleArm = 16,
		Gun = 32,
		Projectile = 64,
		Thrown = 128
	}
}