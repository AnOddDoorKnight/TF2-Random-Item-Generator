using System;
using System.Collections.Generic;


namespace CoolTF2ItemGenerator
{
	static class Program
	{
		static readonly Random Random = new();
		static void Main(string[] args)
		{
			int repeat = 1;
			foreach (string i in args) 
				if (i.StartsWith('+'))
				{
					bool hasValue = int.TryParse(i.Replace('+', ' '), out repeat);
					if (hasValue) break;
				}
			for(int i = 0; i < repeat; i++)
			switch (Random.Next(2))
			{
				case 0:
					Weapon weapon = new(args);
					weapon.ToConsole();
					break;
				case 1:
					//Cosmetic cosmetic = new();
					goto case 0;
			}
		}
	}
	class Weapon 
	{
		#region Name Information
		public string name = "";
		public Prefix prefix;
		public Prefix_Type type;
		#endregion
		public List<string> Upsides = new();
		public List<string> Downsides = new();
		#region Metadata
		public enum Prefix_Type { Normal, Botkiller, Decorated }
		public enum Prefix { Botkiller, Decorated, Unique, Vintage, Haunted, Collectors, SelfMade, Valve, Community, 
			Strange, Unusual, Genuine, Australium, Festive }
		public enum Botkiller { Silver, Gold, Rust, Blood, Carbonado, Diamond }
		#endregion
		static readonly Random Random = new();
		readonly bool nameOnly = false;
		public Weapon(string[] args)
		{
			name = GenerateName();
			prefix = GeneratePrefix(out type);
			if (prefix != Prefix.Unique)
			switch (type)
			{
				case Prefix_Type.Normal:
					name = $"{prefix} {name}";
					break;
				case Prefix_Type.Botkiller:
					name = $"{GenerateBotkiller()} Botkiller {name} Mk.I";
					break;
				case Prefix_Type.Decorated:
					name = $"{GenerateGrade()} {name}";
					break;
			}
			foreach (string i in args) if (i == "-NameOnly") { nameOnly = true; break; }
			if (nameOnly) return;
			GenerateStats();
		}
		static string GenerateName()
		{
			string[] prefix_Weapon = new string[] { "Force-A-", "Pretty Boy's", "Flying", "Candy",
			"Boston", "Battalion's Backup", "Pain-Bringing", "Holy", "Beggar's", "Black", "Liberty",
			"Cow", "Soldier's", "Killing", "Baby Face's", "Soda", "Back", "Panic", "Buff", "Panic", "Pain",
			"Disciplinary", "Rubber", "Market", "Iron", "Escape", "Lumbricus", "Flame", "Dragon's", "Reserve",
			"Flare", "Scorch", "Thermal", "Gas", "Postal", "Sharpened", "Third", "Neon", "Hot", "Grenade",
			"Ali Baba's", "Loose", "B.A.S.E", "Scottish", "Splendid", "Tide", "Scotsman's", "Ullapool",
			"Claidheamh", "Persian", "Brass", "Huo-Long", "Family", "Robo-", "Dalokohs", "Buffalo Steak",
			"Second", "Apoco-", "Killing", "Gloves", "Bread", "Boxing", "Fists of", "Eviction", "Frontier",
			"Pomson", "Rescue", "Giger", "Short", "Golden", "Southern", "Eureka", "Organ", "Crusader's",
			"Quick-", "Medi", "Vita-", "Solemn", "Sniper", "AWPer", "Fortified", "Sydney", "Bazaar", "Shooting",
			"Hitman's", "Self-Aware", "Darwin's", "Cozy", "Cleaner's", "Tribalman's", "Big", "Black", "Eternal",
			"Wanga", "Conniver's", "Big", "Spy-", "Enthusiast's", "Cloak and", "Dead", "Invis", "Snack", "Red-Tape",
			"Frying", "Freedom", "Bat Outta", "Memory", "Ham", "Golden Frying", "Necro", "Crossing", "Prinny",
			"Mad"
			},
			name_Weapon = new string[] { "Scatter", "Blaster", "Pooper", "Popper", "Nature", "Gun", "Pocket Pistol",
			"Punch", "Guillotine", "Cola", "Milk", "Cane", "Basher", "Blade", "Stick", "Assassin", "Launcher", "Box",
			"Mangler", "Bazooka", "Strike", "Shooter", "Boats", "Backup", "Treads", "Jumper", "Action", "Train",
			"Plan", "Thrower", "Napalmer", "Fury", "Blower", "Attack", "Pummeler", "Scratcher", "Degree", "Annihilator",
			"Hand", "Booties", "Cannon", "Bomber", "Resistance", "Targe", "Headtaker", "Nine Iron", "Skullcutter",
			"Curtain", "Beast", "Heater", "Business", "Bar", "Sandvich", "Banana", "Gloves", "of Boxing", "Fists",
			"Steel", "Spirit", "Bite", "Ranger", "Justice", "Hospitality", "Effect", "Crossbow", "Vow", "Rifle",
			"Sleeper", "Compound", "Bargain", "Star", "Heatmaker", "Carbine", "Shield", "Camper", "Shiv", "Dresser",
			"Prick", "Reward", "Kunai", "Watch", "Ringer", "and Dagger", "Pan", "Staff", "Hell", "Maker", "Shank",
			"Guard", "Machete", "Pain-Bringer"
			};
			string output = "";
			output += prefix_Weapon[Random.Next(prefix_Weapon.Length)];
			if (!output.EndsWith('-')) output += $" {name_Weapon[Random.Next(name_Weapon.Length)]}";
			else output += $"{name_Weapon[Random.Next(name_Weapon.Length)]}";
			return output;
		}
		static Prefix GeneratePrefix(out Prefix_Type internal_prefixType)
		{
			Prefix internal_prefix = (Prefix)Random.Next(Enum.GetNames(typeof(Prefix)).Length);
			switch (internal_prefix)
			{
				default:
					internal_prefixType = Prefix_Type.Normal;
					goto end;
				case Prefix.Decorated:
					internal_prefixType = Prefix_Type.Decorated;
					goto end;
				case Prefix.Botkiller:
					internal_prefixType = Prefix_Type.Botkiller;
				end:
					return internal_prefix;
			}
		}
		static Botkiller GenerateBotkiller() => (Botkiller)Random.Next(Enum.GetNames(typeof(Botkiller)).Length);
		static Grade GenerateGrade() => (Grade)Random.Next(Enum.GetNames(typeof(Grade)).Length);
		void GenerateStats()
		{
			string[] Pros = new string[] {"+{0}% self-inflicted damage resistance while deployed",
			"+{0} max health", "Regenerate {1} health points per second", /*<== Make sure to replace this with a value*/
			"+{0}% explosive damage resistance while deployed",
			}, //Apply on Hit
			Cons = new string[] { "-{0} max health", "-{0}% damage to buildings on wearer", "-{0}% movement speed while deployed",
			"No random crits", "+{0}% bullet damage vulnderability on wearer"
			},
			Stats2 = new string[] { "damage to buildings", "bullet damage vulnerability", "self-inflicted damage resistance while deployed" };
			int weaponStatisticsCount = Random.Next(1, 4);
			for (int i = 0; i <= weaponStatisticsCount; i++)
			{
				string temp; bool isPositive;
				switch (Random.Next(2))
				{
					case 0: //Upside
						temp = Pros[Random.Next(Pros.Length)];
						isPositive = true;
						goto release;
					case 1: //Downside
						temp = Cons[Random.Next(Cons.Length)];
						isPositive = false;
					release:
						if (temp.Contains("{0}")) temp = temp.Replace("{0}", $"{Random.Next(5, 76)}");
						if (temp.Contains("{1}")) temp = temp.Replace("{1}", $"{Random.Next(1, 6)}");
						if (isPositive) { Upsides.Add(temp); break; }
						Downsides.Add(temp);
						break;
				}
			}
		}
		public void ToConsole()
		{
			string temp = "";
			Console.WriteLine($"{name}");
			if (nameOnly) return;
			Console.ForegroundColor = ConsoleColor.Green;
			foreach (string i in Upsides) temp += $"+  {i}\n";
			Console.WriteLine(temp);
			temp = "";
			Console.ForegroundColor = ConsoleColor.Red;
			foreach (string i in Downsides) temp += $"-  {i}\n";
			Console.WriteLine(temp);
			Console.ForegroundColor = ConsoleColor.White;
		}
	}
	class Cosmetic
	{

	}

	enum Grade { Civilian, Freelance, Mercenary, Commando, Assassin, Elite }
}
