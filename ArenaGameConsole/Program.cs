// See https://aka.ms/new-console-template for more information
using ArenaGameLib.GameObjects;

Console.WriteLine("Welcome to the Arena!");

//ConfigGamespecsReader configSpecsReader = new ConfigGamespecsReader();
//configSpecsReader.StartReadConfigFile("C:\\Users\\magnu\\OneDrive\\Skrivebord\\Datamatiker\\4-Sem\\Advanced-Software-Construction\\ArenaGameLib\\ArenaGameLib\\Configuration");

//int X = configSpecsReader.MaxX;
//int Y = configSpecsReader.MaxY;
//string difficulty = configSpecsReader.Difficulty;

int X = 20;
int Y = 20;
string difficulty = "normal";

Console.WriteLine(X);
Console.WriteLine(Y);
Console.WriteLine(difficulty);


// Set up arena
Arena arena = new Arena(X, Y);

// Set up creatures
Creature orc = new Creature("Bork", 200, 15, 400, 150, 150, 10, 0);
Creature minotaur = new Creature("Keldr", 220, 10, 400, 150, 150, 10, 20);

arena.AddCreature(orc);
arena.AddCreature(minotaur);

// Set up loot items

Weapon sword = new Weapon("Steel Sword", 12, 10, 2, 20, 1);
Weapon battleAxe = new Weapon("Steel Battleaxe", 20, 10, 18, 30, 1);

arena.AddLootableItem(sword);
arena.AddLootableItem(battleAxe);

// set up walls
Wall wall1 = new Wall(2, 1, 5, 5);
Wall wall2 = new Wall(2, 1, 15, 5);

arena.AddWall(wall1);
arena.AddWall(wall2);
