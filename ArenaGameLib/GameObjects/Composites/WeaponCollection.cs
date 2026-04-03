using ArenaGameLib.GameInterfaces;
using ArenaGameLib.GameInterfaces.Composites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects.Composite
{
	public class WeaponCollection : IWeaponCollection
	{
		public List<IWeapon> Weapons { get; set; }
		public int Capacity { get; set; }

		public WeaponCollection(int capacity)
		{
			Weapons = new List<IWeapon>();
			Capacity = capacity;
		}

		public void Add(IWeapon item)
		{
			if (item.Weight + TotalWeight() > Capacity)
			{
				Weapons.Add(item);
			}
			else
			{
				throw new ArgumentOutOfRangeException("Weapon Weight Capacity Exceeded!");
			}
		}

		public void Remove(IWeapon item)
		{
			if (Weapons.Contains(item))
			{
				Weapons.Remove(item);
			}
			else
			{
				throw new ArgumentException("Weapon Not Contained in Collection!");
			}
		}

		public int TotalWeight()
		{
			return (int)Weapons.Sum(x => x.Weight != null ? x.Weight : 0);
		}
	}
}
