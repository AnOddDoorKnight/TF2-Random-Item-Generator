global using System;
global using System.Linq;
global using System.Collections.Generic;
//https://www.youtube.com/watch?v=jHJm9VxeO4E
//https://wiki.teamfortress.com/wiki/Weapons
namespace TF2ItemGenerator;
static class Master
{
	public static Dictionary<string, dynamic?> Set = new() { ["Automatic"] = false };
	public static readonly string[][] commands = {
		new string[] {"-Auto", "=Class:", "=Type: ", "=Prefix: ", "=Name: " },
		new string[] {"Automatic", "Preset Class", "Preset Item", "Preset Prefix", "Preset Name"} };
	static void Main(string[] args)
	{
		ReadArguments(args);
		dynamic item = Set.ContainsKey("Preset Item") ? Set["Preset Item"]! : new Random().Next(2) == 0 ? Item.GetWeapon() : Item.GetCosmetic();
	}
	static void ReadArguments(string[] args)
	{


		if (Set["Automatic"] && Set.Count != commands[0].Length) AllowHumanSetArgs();
	}
	static void AllowHumanSetArgs()
	{
		Console.WriteLine("Do you want to set anything?\n[Y]es/[N]o");
		if ((Console.ReadLine() ?? "").ToLower()[0].Equals('y')) { Set["Automatic"] = true; return; }
		if (!Set.ContainsKey(commands[1][2]))
		{	// Preset Item
			Console.WriteLine("Do you want to define your weapon or a complete random one?\n[T]rue/[F]alse");
			if ((Console.ReadLine() ?? "").ToLower()[0].Equals('t'))
			{
				Console.WriteLine("[W]eapon or [C]osmetic?");
				Set.Add(commands[1][2], Enum.Parse<ItemType>((Console.ReadLine() ?? "W").ToUpper().Split()[0]));
			}
		}
		if (!Set.ContainsKey(commands[1][1]))
		{	// Preset Class
			Console.WriteLine("Choose your Class, or type [R]andom for Random");
			foreach (Class i in Enum.GetValues<Class>())
				Console.WriteLine($"{i.GetHashCode()}. {i}");
			string foo = Console.ReadLine() ?? "";
			Class? bar = Enum.TryParse<Class>(foo, out Class ass) ? ass : null;
			Set.Add(commands[1][1], bar); 
		}
		for (int i = 0, j = i == 0 ? 3 : 1; i < 2; i++)
		{
			if (Set.ContainsKey(commands[1][j])) continue; // 3 = Prefix, 1 = Class
			bool isPrefix = j == 3;
			Console.WriteLine($"Choose your {(isPrefix ? "Prefix" : "Class")}, or type [R]andom for Random");
			Type enumType = isPrefix ? typeof(Class) : typeof(Prefix);
			for (int ii = 0; ii < Enum.GetNames(enumType).Length; i++) Console.WriteLine($"{ii}. {Enum.GetName(enumType, ii)}");
			// Code here about allowing people to input on what they want.
			
		}
		if (!Set.ContainsKey(commands[1][3]))
		{	// Preset Prefix
			// Should also include grades n stuff
			//case 3: set.Add(commands[1][i], Enum.Parse<Prefix>(args[Array.IndexOf(args, commands[0][i]) + 1])); continue;
			Console.WriteLine("Choose your Prefix, or type [R]andom for Random");


		}
	}
}
public enum ItemType : byte
{
	Weapon,
	Cosmetic,
}
public enum CosmeticType : byte
{
	Hat,
}
public enum WeaponType : byte
{ 
	Primary,
	Secondary,
	Melee,
	Tiatary
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
public enum Class : byte
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
public enum Grade : byte
{
	Civilian,
	Freelance,
	Mercenary,
	Commando,
	Assassin,
	Elite
}