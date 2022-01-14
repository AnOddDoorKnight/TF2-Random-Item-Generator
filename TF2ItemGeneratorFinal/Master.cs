using System;
using System.Linq;
using System.Collections.Generic;
//https://www.youtube.com/watch?v=jHJm9VxeO4E
//https://wiki.teamfortress.com/wiki/Weapons
namespace TF2ItemGenerator;
static class Master
{ 
	static Master()
	{

	}
	static void Main(string[] args)
	{
		ReadArguments(args);
		

		if (args.Length != 0 && args[0] == commands[0][0]) CommandMain(args); 
		else HumanInputMain();
	}
	static void HumanInputMain()
	{

	}
	static Dictionary<string, dynamic> set = new() { ["Automatic"] = false };
	static readonly string[][] commands = { 
		new string[] {"-Auto", "=Class:", "=Type: ", "=Prefix: ", }, 
		new string[] {"Automatic", "Preset Class", "Preset Item", "Preset Prefix", } };
	static void CommandMain(string[] args)
	{
		if (commands[0][0] == args[0]) ReadArguments(args);
		else return;

	}
	static void ReadArguments(string[] args)
	{
		HashSet<string> argsHashSet = Enumerable.ToHashSet(args);
		foreach (string i in args) argsHashSet.Add(i);
		for (int i = 0; i < commands[0].Length; i++)
		{
			if (argsHashSet.Contains(commands[0][i]))
				switch (i)
				{
					case 0: set[commands[1][i]] = true; continue;
					case 1: set.Add(commands[1][i], Enum.Parse<Class>(args[Array.IndexOf(args, commands[0][i]) + 1])); continue;
					case 2: set.Add(commands[1][i], args[Array.IndexOf(args, commands[0][i]) + 1].ToLower() switch { "weapon" => "Weapon", "cosmetic" => "Cosmetic", _ => throw new ArgumentException("Invalid Item, did you misspell it?")}); continue;
					case 3: set.Add(commands[1][i], Enum.Parse<Prefix>(args[Array.IndexOf(args, commands[0][i]) + 1])); continue;
				}
		}
	}
	static Weapon WriteArguments(Weapon? weapon = null)
	{
		weapon ??= new Weapon();
		

		return weapon;
	}
	static Cosmetic WriteArguments(Cosmetic? cosmetic = null)
	{
		cosmetic ??= new Cosmetic();
		cosmetic = (Cosmetic)Foo(cosmetic);

		return cosmetic;
	}
	private static Item Foo(Item item)
	{


		return item;
	}
	/*
	static T WriteArguments<T>() where T : Item // Assumes that auto is set to true
	{
		for (int i = 1; i < set.Count; i++)
		{
			if (set.ContainsKey(commands[1][i]))
			{
				switch (set[commands[1][i]].GetType())
				{
					case Class: item.@class = set[commands[1][i]]; break;
					case string: item. break;
					case Prefix: break;
					default: throw new ArgumentOutOfRangeException();
				}
			}
		}
	}
	*/
}
public class Weapon : Item
{
	public WeaponType type;
	public BotKiller? botkiller;
	public Weapon() : base(null)
	{
		
	}
	public List<string> upsides, downsides, neutral;
	public override void Write()
	{
		ConsoleColor defaultColor = Console.ForegroundColor;
		Console.WriteLine($"A {type.ToString().ToLower()} weapon for {$"the {@class}" ?? "all classes"}\n{name}");
		for (int i = 0; i < 3; i++)
		{
			Console.ForegroundColor = i switch {
				0 => ConsoleColor.Green,
				1 => ConsoleColor.Red,
				2 => ConsoleColor.White,
				_ => Console.ForegroundColor
			};
			foreach (string j in i switch { 0 => upsides, 1 => downsides, 2 => neutral, _ => throw new IndexOutOfRangeException() })
				Console.WriteLine(j);
		}
		Console.ForegroundColor = defaultColor;
	}
}
public class Cosmetic : Item
{
	public CosmeticType type;
	public Cosmetic() : base(new Prefix[] {Prefix.Botkiller, Prefix.Australium, Prefix.Festive})
	{

	}
	public override void Write() => Console.WriteLine($"A {type.ToString().ToLower()} cosmetic for {$"the {@class}" ?? "all classes"}\n{name}");
}
public abstract class Item
{
	public Item(Prefix[]? disallowedPrefixes)
	{
		if (disallowedPrefixes != null)
			prefix = (Prefix)Random.Next(GetAllowedPrefixes(disallowedPrefixes).Count - 1);
	}
	static List<T> GetAllowedPrefixes<T>(T[] blockedEnumerations) where T : Enum
	{
		Queue<T> blockedQueue = new();
		foreach (T i in blockedEnumerations)
			blockedQueue.Enqueue(i);
		List<T> allowedEnumerations = new();
		foreach (T i in Enum.GetValues(typeof(T)))
			allowedEnumerations.Add(i);
		allowedEnumerations.Remove(blockedQueue.Dequeue());
		return allowedEnumerations;
	}
	public string name;
	public Class? @class;
	public Prefix prefix;
	public Grade? grade;
	public static readonly Random Random = new();
	public abstract void Write();
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
//	<ItemGroup>
//		<Compile Remove="OldCode.cs" />
//	</ItemGroup>
//
//	<ItemGroup>
//		<None Include="OldCode.cs" />
//	</ItemGroup>