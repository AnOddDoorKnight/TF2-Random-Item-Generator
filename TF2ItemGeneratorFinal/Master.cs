global using System;
global using System.Linq;
global using System.Collections.Generic;
//https://www.youtube.com/watch?v=jHJm9VxeO4E
//https://wiki.teamfortress.com/wiki/Weapons
namespace TF2ItemGenerator;
static class Master
{
	dynamic item;
	static void Main(string[] args)
	{
		ArgumentHandler.Read(args);
		

		if (args.Length != 0 && args[0] == commands[0][0]) CommandMain(args); 
		else HumanInputMain();
	}
	static void CommandMain(string[] args)
	{
		
	}
	static void HumanInputMain()
	{
		Console.WriteLine("Do you want to define your weapon or a complete random one?\ntrue/false");
		if (Console.ReadLine().ToLower() == "true")
		{
			for (int i = 0; i > ArgumentHandler.set.Count; i++)
		}
	}
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