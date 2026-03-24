using ArenaGameLib.GameInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects.Composite
{
	public class ArmourCollection : IArmourCollection
	{
		public List<IArmour> ArmourSet { get; set; }
		public int Capacity { get; set; }

		public ArmourCollection(int capacity)
		{
			ArmourSet = new List<IArmour>();
			Capacity = capacity;
		}

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

		public int TotalWeight()
		{
			return (int)ArmourSet.Sum(x => x.Weight != null ? x.Weight : 0);
		}
	}
}
