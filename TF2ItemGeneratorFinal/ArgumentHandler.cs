namespace TF2ItemGenerator;
static class ArgumentHandler
{
	public static Dictionary<string, dynamic> set = new() { ["Automatic"] = false };
	public static readonly string[][] commands = { 
		new string[] {"-Auto", "=Class:", "=Type: ", "=Prefix: ", }, 
		new string[] {"Automatic", "Preset Class", "Preset Item", "Preset Prefix", } };
	public static void Read(string[] args)
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
	public static Weapon Write(Weapon? weapon = null)
	{
		weapon ??= new Weapon();
		

		return weapon;
	}
	public static Cosmetic Write(Cosmetic? cosmetic = null)
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