using System;
using System.Linq;
using System.Collections.Generic;
//https://www.youtube.com/watch?v=jHJm9VxeO4E
//https://wiki.teamfortress.com/wiki/Weapons
namespace TF2ItemGenerator;
static class Master
{
    static void Main(string[] args)
    {
        if (args.Length == 0) HumanInputMain();
        else CommandMain(args);
    }
    static void HumanInputMain()
    {

    }
    static Dictionary<string, bool> set;
    static void CommandMain(string[] args)
    {
        ReadArguments(args);
    }
    static void ReadArguments(string[] args)
    {
        set = new Dictionary<string, bool>();
        set.New("", Array.Exists(args, "--"));
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