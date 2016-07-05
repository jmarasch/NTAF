using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using NTAF.Core;
using System.Xml;
using System.Xml.Serialization;

namespace NTAF.Core {
    public class StatException : System.Exception {
        private StatsMod.Stats _HappenedOnStat = StatsMod.Stats.None;
        private StatExceptionType _SizeIssue = StatExceptionType.none;
        private int _Max = 12;
        private  uint _Min = 1;

        public enum StatExceptionType {
            none,
            ToLarge,
            ToSmall
        }

        public StatsMod.Stats HappendOnStat { get { return _HappenedOnStat; } }

        public StatExceptionType SizeIssue { get { return _SizeIssue; } }

        public Int32 Max { get { return _Max; } }

        public UInt32 Minimum { get { return _Min; } }

        public StatException() { }
        public StatException( string message ) : base( message ) { }
        public StatException( string message, Exception innerException ) : base( message, innerException ) { }
        public StatException( StatsMod.Stats OnStat, StatExceptionType Issue, SByte StatMin, Byte StatMax )
            : base( OnStat.ToString() + ( Issue == StatExceptionType.ToSmall ? " cannot go below " : " cannot go above " ) +
            ( Issue == StatExceptionType.ToSmall ? StatMin.ToString() : StatMax.ToString() ) ) {
            _HappenedOnStat = OnStat;
            _SizeIssue = Issue;
            _Min = ( uint )StatMin;
            _Max = ( int )StatMax;
        }
        public StatException( StatsMod.Stats OnStat, StatExceptionType Issue, uint StatMin, int StatMax )
            : base( OnStat.ToString() + ( Issue == StatExceptionType.ToSmall ? " cannot go below " : " cannot go above " ) +
            ( Issue == StatExceptionType.ToSmall ? StatMin.ToString() : StatMax.ToString() ) ) {
            _HappenedOnStat = OnStat;
            _SizeIssue = Issue;
            _Min = StatMin;
            _Max = StatMax;
        }
    }

    public static class GeneralOperations {
        /* code in this section was borrowed from
         * http://blog.spontaneouspublicity.com/post/2008/01/17/Associating-Strings-with-enums-in-C.aspx
         * January 17, 2008 14:50 by Luke
         * Accessed 6-10-2009*/

        public static string GetDescription<T>( T EnumValue ) {
            FieldInfo 
                fi = EnumValue.GetType().GetField( EnumValue.ToString() );

            DescriptionAttribute[] 
                attributes = ( DescriptionAttribute[] )fi.GetCustomAttributes( typeof( DescriptionAttribute ), false );

            return ( attributes.Length > 0 ) ? attributes[0].Description : EnumValue.ToString();
        }

        public static T GetEnumValueFromDescription<T>( String Description ) {
            Object 
                retEnumValue = null;

            Type 
                enumType = typeof( T );

            Array 
                enumValueArray = Enum.GetValues( enumType );

            if ( enumType.BaseType != typeof( Enum ) )
                throw new ArgumentException( "T must be of type System.Enum" );

            foreach ( T val in enumValueArray ) {
                if ( GetDescription<T>( val ).ToLower() == Description.ToLower() )
                    retEnumValue = val;
            }

            return ( T )retEnumValue;
        }

        public static List<T> EnumToList<T>() {
            Type enumType = typeof( T );

            if ( enumType.BaseType != typeof( Enum ) )
                throw new ArgumentException( "T must be of type System.Enum" );

            Array enumValArray = Enum.GetValues( enumType );
            List<T> enumValList = new List<T>( enumValArray.Length );

            foreach ( T val in enumValArray ) {
                enumValList.Add( ( T )Enum.Parse( enumType, val.ToString() ) );
            }
            return enumValList;
        }

        //todo update with printer version
        public static string WrapLength( string str, int len ) {
            List<string>
                stringBuilder = new List<string>();

            String 
                buffer = "",
                retVal = "";

            int curPos = 0;
            for ( int i = 0; i <= str.Length; i++ ) {
                if ( i + 1 < str.Length ) {
                    if ( ( curPos >= len ) && ( str.Substring( i, 1 ) == " " ) ) {
                        stringBuilder.Add( buffer );
                        buffer = "";
                        curPos = 0;
                    }
                    else
                        buffer += str.Substring( i, 1 );
                    curPos++;
                }
            }
            if ( str != String.Empty )
                stringBuilder.Add( str );

            foreach ( String tmpString in stringBuilder )
                retVal += tmpString + "\n";

            return retVal;
            //string retVal = "";

            //int curPos = 0;
            //for ( int i = 0; i <= str.Length; i++ ) {
            //    if ( i + 1 < str.Length ) {
            //        if ( ( curPos >= len ) && ( str.Substring( i, 1 ) == " " ) ) {
            //            retVal += "\n";
            //            curPos = 0;
            //        }
            //        else
            //            retVal += str.Substring( i, 1 );
            //        curPos++;
            //    }
            //}
            //if ( str != String.Empty )
            //    retVal += str.Substring( str.Length - 1 );
            //return retVal;

        }
        public static string[] WrapLength( int width, string str ) {
            List<string> retVal = new List<string>();

            string buffer = "";

            int curPos = 0;
            for ( int i = 0; i <= str.Length; i++ ) {
                if ( i + 1 < str.Length ) {
                    if ( ( curPos >= width ) && ( str.Substring( i, 1 ) == " " ) ) {
                        retVal.Add( buffer );
                        buffer = "";
                        curPos = 0;
                    }
                    else
                        buffer += str.Substring( i, 1 );
                    curPos++;
                }
            }
            if ( str != String.Empty )
                retVal.Add( str );

            return retVal.ToArray();

        }
    }


    //class serializable
    [Serializable()]
    public class StatsMod : ICloneable {

        #region Enums
        public enum Stats {
            None = 0,
            Movement = 1 << 0,
            HitPoints = 1 << 1,
            HandToHand = 1 << 2,
            AttackPoints = 1 << 3,
            PsyPoints = 1 << 4,
            RangedWeapons = 1 << 5,
            Might = 1 << 6,
            Toughness = 1 << 7,
            Agility = 1 << 8,
            Willpower = 1 << 9,
            HorrorFactor = 1 << 10
        }
        #endregion

        #region Fields
        private Stats _StatToMod;
        private int _Mod;
        #endregion

        #region Properties
        [XmlAttribute()]
        public Stats StatToMod {
            get { return _StatToMod; }
            set { _StatToMod = value; }
        }

        [XmlAttribute()]
        public int Modifier {
            get { return _Mod; }
            set { _Mod = value; }
        }

        [XmlIgnore()]
        public string DataRow {
            get {
                return
                    StatToMod.ToString() + " " + ( Modifier > 0 ? "+" : "" ) + Modifier.ToString();
            }
        }
        #endregion

        #region Methods
        public override string ToString() {
            return StatToMod.ToString() + " " + ( Modifier > 0 ? "+" : "" ) + Modifier.ToString();
        }
        public static List<StatsMod> LoadModsFromString( string val ) {
            string[] temp = val.Split( ',' );
            List<StatsMod> retVal = new List<StatsMod>();
            foreach ( string thing in temp )
                retVal.Add( new StatsMod( thing ) );

            return retVal;
        }

        public static string myMods( List<StatsMod> val ) {
            string retVal = "";
            foreach ( StatsMod tmp in val )
                retVal += tmp.DataRow + ",";
            return retVal.TrimEnd( ',' );
        }

        public static string myMods( StatsMod[] val ) {
            string retVal = "";
            foreach ( StatsMod tmp in val )
                retVal += tmp.DataRow + ",";
            return retVal.TrimEnd( ',' );
        }
        #endregion

        #region IClone Members
        public Object Clone() {
            return this.MemberwiseClone();
        }
        #endregion

        #region Constructors
        public StatsMod() {
            StatToMod = Stats.None;
            Modifier = 0;
        }

        //constructor to accept current class object
        public StatsMod( StatsMod var ) {
            StatToMod = var.StatToMod;
            Modifier = var.Modifier;
        }

        //constructor to load data from single passed formatted string
        public StatsMod( string var ) {
            string[] temp = var.Split( ' ' );
            StatToMod = ( Stats )Enum.Parse( typeof( Stats ), temp[0] );
            if ( temp[1].Contains( "-" ) )
                Modifier = ( int )int.Parse( temp[1] );
            else
                Modifier = ( int )int.Parse( temp[1].Substring( 1 ) );

        }

        //constructor that takes all vars
        public StatsMod( Stats smStat, int smMod ) {
            StatToMod = smStat;
            Modifier = smMod;
        }
        #endregion
    }
}