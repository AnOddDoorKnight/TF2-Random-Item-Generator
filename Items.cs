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
				case 0: upsides.Add(new Modifier());
				case 1: downsides.Add(new Modifier());
				case 2: neutral.Add(new Modifier());
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
}
public class Modifier
{
	readonly char? @char;
	string value;
	public Modifier()
	{
		string index = Random.Next(4) switch {0 => "Pros", 1 => "Cons", 2 => "Neutral", 3 => "Customizable", _ => throw new InvalidOperationException()};
		value = staticModifierData[index][Random.Next(staticModifierData[index].Length)];
		if (value.StartsWith("{0}")) @char = index == "Customizable" ? Random.Next(2) switch {0 => '+', _ => '-'} 
			: index switch {"Pros" => '+', "Cons" => '-', _ => ' '};
		value = @char.ToString() ?? "" + ApplyValues(value);
	}
	public Modifier(bool? isPositive)
	{
		switch (isPositive)
		{
			case true:
				
				break;
			case false:
			case null:
		}
	}
	public override string ToString() => value;
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
				} as string); ;
		return input;
	}
	public static Dictionary<string, string[]> staticModifierData = new()
	{   // Some of these are different, which requires dif values
		["Pros"] = new string[] {
			"Regenerate {1} health points per second",
		},
		["Cons"] = new string[] {
			"No random crits",
		},
		["Neutral"] = new string[] {

		},
		["Customizable"] = new string[] {
			"{0}% self-inflicted damage {3} while deployed",
			"{0} max health",
			"{0}% damage to buildings on wearer",
			"{0}% {2} damage {3} while deployed", // 2 = explosive, phys, etc. 3 = resistance and vulnerability
			"{0}% movement speed while deployed", 
		}
	};
}