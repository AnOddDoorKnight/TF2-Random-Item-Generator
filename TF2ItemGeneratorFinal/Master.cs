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
        if (args.Length != 0 && args[0] == commands[0]) CommandMain(args); 
        else HumanInputMain();
    }
    static void HumanInputMain()
    {

    }
    static Dictionary<string, dynamic> set = new();
	static readonly string[] commands = { "-Auto", "=Class:", "=Type: " };
    static void CommandMain(string[] args)
    {
		if (commands[0] == args[0]) ReadArguments(args);
		else return;
    }
    static void ReadArguments(string[] args)
    {
		var argsHashSet = Enumerable.ToHashSet<string>(args);
		foreach (string i in args) argsHashSet.Add(i);
		for (int i = 0; i < commands.Length; i++)
		{
			if (Array.Exists(args, commands[i]))
				switch (i)
				{
					case 0:
						set.New("Automatic", bool.Parse(args[Array.IndexOf<string>(args, commands[0])]));
						goto end;
					case 1:
						set.New("Preset Class", Enum.Parse<Class>(args[Array.IndexOf<string>(args, commands[1])++]));
						goto end;
					case 2:
						set.New("Preset Item", args[Array.IndexOf<string>(args, commands[2])++)] switch { "Weapon" => "Weapon", "Cosmetic" => "Cosmetic", _ => throw new ArgumentException() });
						goto end;
					end:
						continue; 
				}
		}
    }
}
public class Weapon : Item
{
    public WeaponType type;
    public BotKiller? botkiller;
    public Weapon()
    {

    }
    public List<string> upsides, downsides, neutral;
    public override void Write()
	{
		ConsoleColor defaultColor = Console.ForegroundColor;
		Console.WriteLine($"A {type.ToString().ToLower()} weapon for the {@class}\n{name}");
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
    public Cosmetic()
    {
		blockedPrefixes = new Prefix[] { Prefix.Botkiller, Prefix.Australium, Prefix.Festive }.ToList().;
        List<Prefix> allowedPrefixes = new();
		foreach (Prefix i in Enum.GetValues(typeof(Prefix)))
			allowedPrefixes.Add(i);
		while (blockedPrefixes.Count != 0)
            allowedPrefixes.Remove((Prefix)allowedPrefixes.IndexOf(blockedPrefixes.Dequeue()));
    }
	static Queue<Prefix> blockedPrefixes = new Queue<Prefix>()
        {Prefix.Botkiller, Prefix.Australium, Prefix.Festive};
    
}
public abstract class Item
{
    public Item()
    {

    }
    public string name;
    public Class @class;
    public Prefix prefix;
	public Grade? grade;
	public abstract void Write();
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