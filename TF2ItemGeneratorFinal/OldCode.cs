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
					bool hasValue = Int32.TryParse(i.Replace('+', ' '), out repeat);
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