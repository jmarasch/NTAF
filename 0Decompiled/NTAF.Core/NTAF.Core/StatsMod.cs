using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NTAF.Core
{
	[Serializable]
	public class StatsMod : ICloneable
	{
		private StatsMod.Stats _StatToMod;

		private int _Mod;

		[XmlIgnore]
		public string DataRow
		{
			get
			{
				string str;
				string str1 = this.StatToMod.ToString();
				str = (this.Modifier > 0 ? "+" : "");
				int modifier = this.Modifier;
				string str2 = string.Concat(str1, " ", str, modifier.ToString());
				return str2;
			}
		}

		[XmlAttribute]
		public int Modifier
		{
			get
			{
				return this._Mod;
			}
			set
			{
				this._Mod = value;
			}
		}

		[XmlAttribute]
		public StatsMod.Stats StatToMod
		{
			get
			{
				return this._StatToMod;
			}
			set
			{
				this._StatToMod = value;
			}
		}

		public StatsMod()
		{
			this.StatToMod = StatsMod.Stats.None;
			this.Modifier = 0;
		}

		public StatsMod(StatsMod var)
		{
			this.StatToMod = var.StatToMod;
			this.Modifier = var.Modifier;
		}

		public StatsMod(string var)
		{
			string[] temp = var.Split(new char[] { ' ' });
			this.StatToMod = (StatsMod.Stats)Enum.Parse(typeof(StatsMod.Stats), temp[0]);
			if (!temp[1].Contains("-"))
			{
				this.Modifier = int.Parse(temp[1].Substring(1));
			}
			else
			{
				this.Modifier = int.Parse(temp[1]);
			}
		}

		public StatsMod(StatsMod.Stats smStat, int smMod)
		{
			this.StatToMod = smStat;
			this.Modifier = smMod;
		}

		public object Clone()
		{
			return this.MemberwiseClone();
		}

		public static List<StatsMod> LoadModsFromString(string val)
		{
			string[] temp = val.Split(new char[] { ',' });
			List<StatsMod> retVal = new List<StatsMod>();
			string[] strArrays = temp;
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				retVal.Add(new StatsMod(strArrays[i]));
			}
			return retVal;
		}

		public static string myMods(List<StatsMod> val)
		{
			string retVal = "";
			foreach (StatsMod tmp in val)
			{
				retVal = string.Concat(retVal, tmp.DataRow, ",");
			}
			return retVal.TrimEnd(new char[] { ',' });
		}

		public static string myMods(StatsMod[] val)
		{
			string retVal = "";
			StatsMod[] statsModArray = val;
			for (int i = 0; i < (int)statsModArray.Length; i++)
			{
				StatsMod tmp = statsModArray[i];
				retVal = string.Concat(retVal, tmp.DataRow, ",");
			}
			return retVal.TrimEnd(new char[] { ',' });
		}

		public override string ToString()
		{
			string str;
			string str1 = this.StatToMod.ToString();
			str = (this.Modifier > 0 ? "+" : "");
			int modifier = this.Modifier;
			string str2 = string.Concat(str1, " ", str, modifier.ToString());
			return str2;
		}

		public enum Stats
		{
			None = 0,
			Movement = 1,
			HitPoints = 2,
			HandToHand = 4,
			AttackPoints = 8,
			PsyPoints = 16,
			RangedWeapons = 32,
			Might = 64,
			Toughness = 128,
			Agility = 256,
			Willpower = 512,
			HorrorFactor = 1024
		}
	}
}