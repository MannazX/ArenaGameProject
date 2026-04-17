using ArenaGameLib.GameInterfaces;
using ArenaGameLib.GameObjects.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects
{
	public class Weapon : ArenaObject, IWeapon
	{
		/// <summary>
		/// Class defining Weapon objects, inheriting from ArenaObject template class.
		/// </summary>
		public int Damage { get; set; }
		public int Range { get; set; }
		public bool Claimed { get; set; }

		/// <summary>
		/// Constructor for instanciating Weapon object.
		/// </summary>
		/// <param name="name">Type: string - Name of the weapon object</param>
		/// <param name="weight">Type: int (nullable) - Weight of the weapon object</param>
		/// <param name="locX">Type: int (nullable) - The X coordinate of the weapon object in the Arena</param>
		/// <param name="locY">Type: int (nullable) - The Y coordinate of the weapon object in the Arena</param>
		/// <param name="damage">Type: int - Damage that the weapon object deals</param>
		/// <param name="range">Type: int - Range in which the weapon is capable of dealing damage</param>
		public Weapon(string name, int? weight, int? locX, int? locY, int damage, int range) : base(name, weight, locX, locY)
		{
			Name = name;
			Weight = weight;
			Lootable = true;
			Removeable = true;
			Damage = damage;
			Range = range;
			Claimed = false;
		}

		/// <summary>
		/// Method for setting the Claimed boolean to true or false, if the weapon has been looted by a creature.
		/// </summary>
		public void SetClaim()
		{
			if (!Claimed)
			{
				Claimed = true;
			}
			else
			{
				Claimed = false;
			}
		}

		/// <summary>
		/// Method for setting the X and Y locations to null, if the weapon has been looted by a creature.
		/// </summary>
		public void SetLocation()
		{
			if (Claimed)
			{
				LocationX = null;
				LocationY = null;
			}
		}
	}
}
