using ArenaGameLib.GameInterfaces;

namespace ArenaGameLib.GameObjects
{
	public class Arena : IArena
	{
		public int MaxX { get; set; }
		public int MaxY { get; set; }
		public List<Creature> Creatures { get; set; }
		public List<ArenaObject> LootItems { get; set; }

		public Arena(int maxX, int maxY)
		{
			MaxX = maxX;
			MaxY = maxY;
		}
	}
}
