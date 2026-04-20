// See https://aka.ms/new-console-template for more information
using ArenaGameLib;
using ArenaGameLib.GameInterfaces.Templates;
using ArenaGameLib.GameObjects;

Console.WriteLine("Welcome to the Arena!");

int X = 20;
int Y = 20;

Creature orc = new Creature("Bork", 200, 15, 400, 150, 150, 10, 0);
Creature minotaur = new Creature("Keldr", 220, 10, 400, 150, 150, 10, 20);

Arena arena = new Arena(X, Y);
