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
		public List<IArenaObject> Items { get; set; }
		public int Capacity { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="capacity"></param>
		public CreatureInventory(int capacity)
		{
			Items = new List<IArenaObject>();
			Capacity = capacity;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
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
		/// 
		/// </summary>
		/// <param name="item"></param>
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
		/// 
		/// </summary>
		/// <returns></returns>
		public int TotalWeight()
		{
			return (int)Items.Sum(x => x.Weight != null ? x.Weight : 0);
		}
	}
}
