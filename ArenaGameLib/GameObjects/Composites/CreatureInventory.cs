using ArenaGameLib.GameInterfaces.Composites;
using ArenaGameLib.GameInterfaces.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects.Composites
{
	/// <summary>
	/// Composite class representing a creature's inventory - Also containing Weapon and Armour Collections.
	/// </summary>
	public class CreatureInventory : IObjectCollection
	{
		public List<IArenaObject> Items { get; set; }
		public int Capacity { get; set; }

		/// <summary>
		/// Constructor method for instanciating the inventory list (Items).
		/// </summary>
		/// <param name="capacity">Type: int - The capacity of the inventory</param>
		public CreatureInventory(int capacity)
		{
			Items = new List<IArenaObject>();
			Capacity = capacity;
		}

		/// <summary>
		/// Method for adding an item to the inventory list.
		/// </summary>
		/// <param name="item">Type: IArenaObject - Item to be added to the inventory list</param>
		/// <exception cref="ArgumentOutOfRangeException">Argument Out of Range Exception thrown if the added item's weight and the total inventory weight exceeds the inventory capacity</exception>
		public void Add(IArenaObject item)
		{
			if (item.Weight + TotalWeight() > Capacity)
			{
				throw new ArgumentOutOfRangeException("Inventory Capacity Exceeded");
			}
			else
			{
				Items.Add(item);
			}
		}

		/// <summary>
		/// Method for removing an item from the inventory list.
		/// </summary>
		/// <param name="item">Type: IArenaObject - Item to be removed from the inventory list</param>
		public void Remove(IArenaObject item)
		{
			if (Items.Contains(item))
			{
				Items.Remove(item);
			}
			else
			{
				Console.WriteLine("Object not contained in Inventory");
			}
		}

		/// <summary>
		/// Method for computing the total weight of the inventory list.
		/// </summary>
		/// <returns>Type: int - Total weight of the inventory list</returns>
		public int TotalWeight()
		{
			return (int)Items.Sum(x => x.Weight != null ? x.Weight : 0);
		}
	}
}
