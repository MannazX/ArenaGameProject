using ArenaGameLib.GameInterfaces;
using ArenaGameLib.GameObjects.Templates;

namespace ArenaGameLib.GameObjects
{
	/// <summary>
	/// Class defining the Arena for the creatures to combat in.
	/// </summary>
	public class Arena : IArena
	{
		public int MaxX { get; set; }
		public int MaxY { get; set; }
		public List<Creature> Creatures { get; set; }
		public List<ArenaObject> LootItems { get; set; }

		/// <summary>
		/// Constructor method for instanciating the Arena object.
		/// </summary>
		/// <param name="maxX">Type: int - The maximum X coordinate for the Arena grid</param>
		/// <param name="maxY">Type: int - The maximum Y coordinate for the Arena grid</param>
		public Arena(int maxX, int maxY)
		{
			MaxX = maxX;
			MaxY = maxY;
		}
	}
}
