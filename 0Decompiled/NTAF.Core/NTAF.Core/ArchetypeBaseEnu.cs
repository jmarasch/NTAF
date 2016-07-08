using System;
using System.ComponentModel;

namespace NTAF.Core
{
	public enum ArchetypeBaseEnu
	{
		[Description("New,0,0")]
		New = 2,
		[Description("Leader,30,?D?")]
		Leader = 4,
		[Description("Heavy,15,?D?")]
		Heavy = 8,
		[Description("Vet,15,?D?")]
		Vet = 16,
		[Description("Member,10,?D?")]
		Member = 32,
		[Description("Green,0,?D?")]
		Green = 64,
		[Description("Special,15,?D?")]
		Special = 128,
		[Description("Psychic,20,?D?")]
		Psychic = 256,
		[Description("All,0,0")]
		All = 508
	}
}