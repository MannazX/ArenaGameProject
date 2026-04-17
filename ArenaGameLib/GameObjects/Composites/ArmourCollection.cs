using ArenaGameLib.GameInterfaces;
using ArenaGameLib.GameInterfaces.Composites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects.Composite
{
	/// <summary>
	/// Composite class representing a creature's collection of armour. 
	/// </summary>
	public class ArmourCollection : IArmourCollection
	{
		public List<IArmour> ArmourSet { get; set; }
		public int Capacity { get; set; }

		/// <summary>
		/// Constructor method for instanciating the armour collection.
		/// </summary>
		/// <param name="capacity">Type: int - The maximum capacity for the weight of the armour</param>
		public ArmourCollection(int capacity)
		{
			ArmourSet = new List<IArmour>();
			Capacity = capacity;
		}

		/// <summary>
		/// Method for adding an armour piece to the collection.
		/// </summary>
		/// <param name="item">Type: IArmour - Armour piece to be added to the collection</param>
		/// <exception cref="ArgumentOutOfRangeException">Exception thrown if total weight exceeds the capacity</exception>
		public void Add(IArmour item)
		{
			if (item.Weight + TotalWeight() > Capacity)
			{
				ArmourSet.Add(item);
			}
			else
			{
				throw new ArgumentOutOfRangeException("Armour Set Weight Capacity Exceeded!");
			}
		}

		/// <summary>
		/// Method for removing an armour piece from the collection.
		/// </summary>
		/// <param name="item">Type: IArmour - Armour piece to be removed from the collection</param>
		/// <exception cref="ArgumentException">Exception thrown if armour is not in the collection</exception>
		public void Remove(IArmour item)
		{
			if (ArmourSet.Contains(item))
			{
				ArmourSet.Remove(item);
			}
			else
			{
				throw new ArgumentException("Armour Not Contained in Collection!");
			}
		}

		/// <summary>
		/// Method for computing the total weight of the collection.
		/// </summary>
		/// <returns>Type: int - Total weight of armour pieces in the collection</returns>
		public int TotalWeight()
		{
			return (int)ArmourSet.Sum(x => x.Weight != null ? x.Weight : 0);
		}
	}
}
