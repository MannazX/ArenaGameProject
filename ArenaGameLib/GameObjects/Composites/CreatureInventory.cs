using ArenaGameLib.GameInterfaces.Composites;
using ArenaGameLib.GameInterfaces.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects.Composites
{
	public class CreatureInventory : IObjectCollection
	{
		public List<IArenaObject> Inventory { get; set; }
		public int Capacity { get; set; }
		public CreatureInventory(int capacity)
		{
			Inventory = new List<IArenaObject>();
			Capacity = capacity;
		}

		public void Add(IArenaObject item)
		{
			if (item.Weight + TotalWeight() > Capacity)
			{
				throw new ArgumentOutOfRangeException("Inventory Capacity Exceeded");
			}
			else
			{
				Inventory.Add(item);
			}
		}

		public void Remove(IArenaObject item)
		{
			if (Inventory.Contains(item))
			{
				Inventory.Remove(item);
			}
			else
			{
				Console.WriteLine("Object not contained in Inventory");
			}
		}

		public int TotalWeight()
		{
			return (int)Inventory.Sum(x => x.Weight != null ? x.Weight : 0);
		}
	}
}
