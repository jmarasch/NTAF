using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Reflection;

namespace NTAF.Core {
    public enum PermissionType {
        Weapon,
        Psy,
        Skill
    }

    [Flags]
    public enum Species {
        Human = 1 << 1,
        Mutant = 1 << 2,
        Undead = 1 << 3,
        Genics_A = 1 << 4,
        Genics_B = 1 << 5,
        Aquatic = 1 << 6,
        Angelic = 1 << 7,
        Demonic = 1 << 8,
        Fun = 1 << 9,
        All = Human | Mutant | Undead | Genics_A | Genics_B | Aquatic | Angelic | Demonic | Fun
    }

    public enum ArchetypeBaseEnu {
        //Description, value, starting exp
        [Description( "New,0,0" )]
        New = 1 << 1,
        [Description( "Leader,30,?D?" )]
        Leader = 1 << 2,
        [Description( "Heavy,15,?D?" )]
        Heavy = 1 << 3,
        [Description( "Vet,15,?D?" )]
        Vet = 1 << 4,
        [Description( "Member,10,?D?" )]
        Member = 1 << 5,
        [Description( "Green,0,?D?" )]
        Green = 1 << 6,
        [Description( "Special,15,?D?" )]
        Special = 1 << 7,
        [Description( "Psychic,20,?D?" )]
        Psychic = 1 << 8,
        [Description( "All,0,0" )]
        All = Leader | Heavy | Vet | Member | Green | Special | Psychic
    }

    //public static class SpeciesFunctions {
    //    public static bool IsSpecies( this Species item, Species query ) {
    //        return ( ( item & query ) == query );
    //    }
    //}

    //public static class PsyGroupFunctions {
    //    public static bool IsSpecies( this Species item, Species query ) {
    //        return ( ( item & query ) == query );
    //    }
    //}

    [Flags]
    public enum PsyGroup {
        Generic = 1 << 0,
        Fire = 1 << 1,
        Lighting = 1 << 2,
        Earth = 1 << 3,
        Water = 1 << 4,
        Cold = 1 << 5,
        Wind = 1 << 6,
        Cloud = 1 << 7,
        Mental = 1 << 8,
        Agility = 1 << 9,
        Medical = 1 << 10

    }

    public enum template {
        None = 0,
        Round = 1,
        Cone = 2,
        Blast = 3
    }

    [Flags]
    public enum WeaponBaseType {
        NoBase = 1 << 0,
        HandToHand = 1 << 1,
        Blade = 1 << 2,
        Bludgen = 1 << 3,
        PoleArm = 1 << 4,
        Gun = 1 << 5,
        Projectile = 1 << 6,
        Thrown = 1 << 7,

    }

    [Flags]
    public enum SkillGroupFlag {
        //[Description("")]
        Generic = 1 << 0,
        Arsonist = 1 << 1,
        Mechanic = 1 << 2,
        TrapSetter = 1 << 3,
        Gunman = 1 << 4,
        ArmsMaster = 1 << 5,
        Medic = 1 << 6,
        Psyonicist = 1 << 7,
        Physical = 1 << 8,
        Stealth = 1 << 9,
        Agility = 1 << 10
    }

    public static class EnumExt {

        //checks to see if an enumerated value contains a type
        public static bool Is<T>( this System.Enum type, T value ) {
            try {
                return ( ( ( int )( object )type & ( int )( object )value ) == ( int )( object )value );
            }
            catch {
                return false;
            }
        }

        public static string GetDescription<T>( this System.Enum type, T EnumValue ) {
            FieldInfo 
                fi = EnumValue.GetType( ).GetField( EnumValue.ToString( ) );

            DescriptionAttribute[] 
                attributes = ( DescriptionAttribute[] )fi.GetCustomAttributes( typeof( DescriptionAttribute ), false );

            return ( attributes.Length > 0 ) ? attributes[0].Description : EnumValue.ToString( );
        }
    }
}
