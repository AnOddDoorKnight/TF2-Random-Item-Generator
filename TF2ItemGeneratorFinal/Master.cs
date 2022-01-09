using System;
//Make a class selection and a weapon type selection
//https://www.youtube.com/watch?v=jHJm9VxeO4E
//https://wiki.teamfortress.com/wiki/Weapons

namespace BetterTF2ItemGenerator;
static class Master
{
	static void Main()
	{
		Weapon weapon = new();
		weapon.Write();
	}
}
class Weapon
{
	#region Weapon Values
	public string name;
	public Class @class;
	public Prefix prefix;
	public Prefix_Type? prefix_Type;
	public Grade? grade;
	public BotKiller? botKiller;
	public WeaponType weaponType;
	public List<string>
		upSides,
		downSides,
		Neutral;
	#endregion

	static Random Random = new();
	public Weapon(Class? @class = null, WeaponType? input_weaponType = null, Prefix? prefix = null) 
	{
		//This generates the classes/WeaponType if they are set as Null
		this.@class = @class ?? (Class)Random.Next(Enum.GetNames(typeof(Class)).Length);
		weaponType = input_weaponType ?? (WeaponType)Random.Next(Enum.GetNames(typeof(WeaponType)).Length);
		this.prefix = prefix ?? (Prefix)Random.Next(Enum.GetNames(typeof(Prefix)).Length);


		name = GenerateName(this.prefix switch
		{
			Prefix.Botkiller => Convert.ToString((BotKiller)Random.Next(Enum.GetNames(typeof(BotKiller)).Length)),
			Prefix.Decorated => Convert.ToString((Grade)Random.Next(Enum.GetNames(typeof(Grade)).Length)),
			_ => Convert.ToString((Prefix)Random.Next(Enum.GetNames(typeof(Prefix)).Length)),
		} );
	}
	public void Write()
	{
		ConsoleColor defaultColor = Console.ForegroundColor;
		Console.WriteLine
			(
				$"A {weaponType.ToString().ToLower()} weapon for the {@class}\n"
				+ $"{name}\n"
			);
		Console.ForegroundColor = ConsoleColor.Green;


		Console.ForegroundColor = ConsoleColor.Red;


		Console.ForegroundColor = defaultColor;


	}
	static string GenerateName(string? prefix = null)
	{
		string[][] weaponNames = {
			new string[] { "Force-A-", "Pretty Boy's", "Flying", "Candy",
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
			new string[] { "Scatter", "Blaster", "Pooper", "Popper", "Nature", "Gun", "Pocket Pistol",
			"Punch", "Guillotine", "Cola", "Milk", "Cane", "Basher", "Blade", "Stick", "Assassin", "Launcher", "Box",
			"Mangler", "Bazooka", "Strike", "Shooter", "Boats", "Backup", "Treads", "Jumper", "Action", "Train",
			"Plan", "Thrower", "Napalmer", "Fury", "Blower", "Attack", "Pummeler", "Scratcher", "Degree", "Annihilator",
			"Hand", "Booties", "Cannon", "Bomber", "Resistance", "Targe", "Headtaker", "Nine Iron", "Skullcutter",
			"Curtain", "Beast", "Heater", "Business", "Bar", "Sandvich", "Banana", "Gloves", "of Boxing", "Fists",
			"Steel", "Spirit", "Bite", "Ranger", "Justice", "Hospitality", "Effect", "Crossbow", "Vow", "Rifle",
			"Sleeper", "Compound", "Bargain", "Star", "Heatmaker", "Carbine", "Shield", "Camper", "Shiv", "Dresser",
			"Prick", "Reward", "Kunai", "Watch", "Ringer", "and Dagger", "Pan", "Staff", "Hell", "Maker", "Shank",
			"Guard", "Machete", "Pain-Bringer"
			}
		};
		string output = (prefix ?? "") + weaponNames[0][Random.Next(weaponNames[0].Length)];
		output += output.EndsWith('-') ? "" : " " + weaponNames[1][Random.Next(weaponNames[1].Length)];
		return output;
	}
	public enum StringGetType : byte
	{
		Basic,
		Upsides,
		Downsides,
		Neutral,
	}
	public enum WeaponType : byte
	{ 
		Primary,
		Secondary,
		Melee,
		Tiatary
	}
	public enum Prefix_Type : byte
	{
		Normal,
		Botkiller,
		Decorated
	}
	public enum Prefix : byte
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
	public enum BotKiller : byte
	{
		Silver,
		Gold,
		Rust,
		Blood,
		Carbonado,
		Diamond
	}
}
enum Class : byte
{
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
enum Grade : byte
{
	Civilian,
	Freelance,
	Mercenary,
	Commando,
	Assassin,
	Elite
}
