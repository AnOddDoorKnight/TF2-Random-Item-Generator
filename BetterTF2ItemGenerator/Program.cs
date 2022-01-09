using System;
//Make a class selection and a weapon type selection
//https://www.youtube.com/watch?v=jHJm9VxeO4E
//https://wiki.teamfortress.com/wiki/Weapons

namespace BetterTF2ItemGenerator
{
	class Program
	{
		static void Main()
		{
			Weapon weapon = new();
			weapon.ToConsole();
		}
	}
	class Weapon
	{
		#region Weapon Values
		public Class Class;
		public WeaponType weaponType;
		#endregion

		public static Random Random = new(4);
		public Weapon(Class @class = Class.Null, WeaponType input_weaponType = WeaponType.Null) // This actually does the shit
		{
			//This generates the classes/WeaponType if they are set as Null
			if (@class == Class.Null) Class = (Class)Random.Next(1, Enum.GetNames(typeof(Class)).Length);
			else Class = @class;
			if (input_weaponType == WeaponType.Null) weaponType = (WeaponType)Random.Next(1, Enum.GetNames(typeof(WeaponType)).Length);
			else weaponType = input_weaponType;
		}
		int TypeRandomizer<T>(T type)
		{
			if ((int)T == 0)
			return ; 
		}
		public void ToConsole()
		{
			ConsoleColor @default = Console.ForegroundColor;
			Console.WriteLine($"A {weaponType.ToString().ToLower()} weapon for the {Class}");




			Console.ForegroundColor = @default;
		}


		public enum WeaponType : sbyte
		{
			Null,
			Primary,
			Secondary,
			Melee,
			Tiatary
		}
		public enum Prefix_Type : sbyte 
		{ 
			Normal, 
			Botkiller,
			Decorated 
		}
		public enum Prefix : sbyte
		{
			Botkiller, 
			Decorated, 
			Unique, 
			Vintage, 
			Haunted, 
			Collectors, 
			SelfMade, 
			Valve, 
			Community,
			Strange, 
			Unusual, 
			Genuine, 
			Australium, 
			Festive
		}
		public enum Botkiller : sbyte
		{ 
			Silver, 
			Gold, 
			Rust, 
			Blood, 
			Carbonado, 
			Diamond 
		}
	}
	enum Class : sbyte
	{
		Null,
		Scout,
		Soldier,
		Pyro,
		Demoman,
		Heavy,
		Engineer,
		Medic,
		Sniper,
		Spy
	}
	enum Grade : sbyte
	{ 
		Civilian, 
		Freelance, 
		Mercenary, 
		Commando, 
		Assassin, 
		Elite 
	}
}
