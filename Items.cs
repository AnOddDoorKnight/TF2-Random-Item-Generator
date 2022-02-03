using System.Collections.Generic;
namespace TF2ItemGenerator;
public class Weapon : Item
{
	public WeaponType type;
	public BotKiller? botkiller;
	public Weapon() : base(null)
	{
		ushort amountEach = (ushort)Random.Next(6);
		for (; amountEach != 0; amountEach--)
			switch (Random.Next(3))
			{
				case 0: upsides.Add(new Modifier()); break;
				case 1: downsides.Add(new Modifier()); break;
				case 2: neutral.Add(new Modifier()); break;
			}
	}
	public List<Modifier> upsides = new(), downsides = new(), neutral = new();
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
			foreach (Modifier j in i switch { 0 => upsides, 1 => downsides, 2 => neutral, _ => throw new IndexOutOfRangeException() })
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
	public static Weapon GetWeapon()
	{

	}
	public static Cosmetic GetCosmetic()
	{

	}
}
public class Modifier
{
	readonly char? @char;
	string value;
	public Modifier()
	{
		string index = Random.Next(4) switch {0 => "Pros", 1 => "Cons", 2 => "Neutral", 3 => "Customizable", _ => throw new InvalidOperationException()};
		value = StaticModData[index][Random.Next(StaticModData[index].Length)];
		if (value.StartsWith("{0}")) @char = index == "Customizable" ? Random.Next(2) switch {0 => '+', _ => '-'} 
			: index switch {"Pros" => '+', "Cons" => '-', _ => ' '};
		value = @char.ToString() ?? "" + ApplyValues(value);
	}
	public override string ToString() => ApplyValues(value);
	static Random Random = new();
	static string ApplyValues(string input)
	{
		foreach (string i in new string[] { "{0}", "{1}", "{2}", "{3}" })
			if (input.Contains(i))
				input = input.Replace(i, i switch
				{   "{0}" => Random.Next(5, 101).ToString(),
					"{1}" => Random.Next(1, 6).ToString(),
					"{2]" => Random.Next(4) switch
					{   0 => "Bullet",
						1 => "Explosive",
						2 => "Fire",
						_ => "Melee"
					},
					"{3}" => Random.Next(2) switch
					{   0 => "resistance",
						_ => "vulnerability"
					},
					_ => throw new InvalidOperationException()
				}); ;
		return input;
	}
	public static readonly Dictionary<string, string[]> StaticModData = new()
	{   // Some of these are different, which requires dif values
		["Pros"] = new string[] {
			"Regenerate {1} health points per second",
			"{0} damage bonus",
			"{0}% faster reload time",
			"On Hit: Builds Boost \nRun speed increased with Boost",
			"{0}% more accurate",
			"Mini-crits targets when fired at their back from close range",
			"{0}% greater jump height when active",
			"On Hit: Gain up to {1} health",
			"Alt-Fire: Launches a ball that slows opponents",
			"On Kill: A small health pack is dropped",
			"On Hit: Bleed for {1} second(s)",
			"Crits whenever it would normally mini-crit",
			"On Hit: One target at a time is Marked-For-Death, causing all damage taken to be mini-crits",
			"Grants another Jump while deployed",
			"Attacks mini-crit while airborne.",
			"Alt-Fire: Launches a festive ornament that shatters causing bleed",
			"Mini-crits targets launched airborne by explosions, grapple hooks or rocket packs.",
		},
		["Cons"] = new string[] {
			"No random crits",
			"{0} damage penalty",
			"Increase in push force taken from damage and airblast",
			"{0}% slower reload time",
			"{0}% less accurate",
			"On Miss: Hit yourself. Idiot.",
		},
		["Neutral"] = new string[] {
			"Knockback on the target and shooter",
			"Alt-Fire to reach and shove someone!",

		},
		["Customizable"] = new string[] {
			"{0}% self-inflicted damage {3} while deployed",
			"{0} max health",
			"{0}% damage to buildings on wearer",
			"{0}% {2} damage {3} while deployed", // 2 = explosive, phys, etc. 3 = resistance and vulnerability
			"{0}% movement speed while deployed", 
			"{0}% faster firing speed",
			"{0}% bullets per shot",
			"{0]% clip size",
			"{0}% critical hit vs burning players",
		}
	};
}