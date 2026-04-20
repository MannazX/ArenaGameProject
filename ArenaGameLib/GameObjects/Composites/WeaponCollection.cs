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
	/// Composite class representing a creature's collection of weapons. 
	/// </summary>
	public class WeaponCollection : IWeaponCollection
	{
		public List<IWeapon> Weapons { get; set; }
		public int Capacity { get; set; }

		/// <summary>
		/// Constructor method for instanciating the weapon collection.
		/// </summary>
		/// <param name="capacity">Type: int - Capacity of the weapon collection</param>
		public WeaponCollection(int capacity)
		{
			Weapons = new List<IWeapon>();
			Capacity = capacity;
		}

		/// <summary>
		/// Method for adding an weapon piece to the collection.
		/// </summary>
		/// <param name="item">Type: IWeapon - Weapon to be added to the collection</param>
		/// <exception cref="ArgumentOutOfRangeException">Exception thrown if total weight exceeds the capacity</exception>
		public void Add(IWeapon item)
		{
			if (item.Weight + TotalWeight() > Capacity)
			{
				throw new ArgumentOutOfRangeException("Weapon Weight Capacity Exceeded!");
			}
			else
			{
				Weapons.Add(item);
			}
		}

		/// <summary>
		/// Method for removing a weapon from the collection.
		/// </summary>
		/// <param name="item">Type: IWeapon - Weapon to be removed from the collection</param>
		/// <exception cref="ArgumentException">Exception thrown if the weapon is not in the collection</exception>
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

		/// <summary>
		/// Method for computing the total weight of the collection.
		/// </summary>
		/// <returns>Type: int - Total weight of weapons in the collection</returns>
		public int TotalWeight()
		{
			return (int)Weapons.Sum(x => x.Weight != null ? x.Weight : 0);
		}
	}
}
