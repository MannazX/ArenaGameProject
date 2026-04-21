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
		public List<Wall> Walls { get; set; }

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

		/// <summary>
		/// Method for adding a creature to the arena grid, provided the coordinates are not outside the boundaries of the grid area.
		/// </summary>
		/// <param name="creature">Type: Creature - The creature to be added</param>
		/// <exception cref="ArgumentOutOfRangeException">Argument Out of Range Exception </exception>
		public void AddCreature(Creature creature)
		{
			if ((creature.LocationX >= 0 && creature.LocationX <= MaxX) && (creature.LocationY >= 0 && creature.LocationY <= MaxY))
			{
				Creatures.Add(creature);
			}
			else
			{
				throw new ArgumentOutOfRangeException("Creature coordinates are outside of the Arena Grid");
			}
		}

		public void AddLootableItem(ArenaObject item)
		{
			if ((item.LocationX >= 0 && item.LocationX <= MaxX) && (item.LocationY >= 0 && item.LocationY <= MaxY))
			{
				LootItems.Add(item);
			}
			else
			{
				throw new ArgumentOutOfRangeException("Item coordinates are outside of the Arena Grid");
			}
		}

		public void AddWall(Wall wall)
		{
			if ((wall.LocationX >= 0 && wall.LocationX + wall.LengthX <= MaxX && wall.LocationX <= MaxX) && (wall.LocationY >= 0 && wall.LocationY + wall.LengthY <= MaxY && wall.LocationY <= MaxY))
			{
				Walls.Add(wall);
			}
			else
			{
				throw new ArgumentOutOfRangeException("Wall coordinates are outside or its length extends outside the Arena Grid");
			}
		}
	}
}
