using ArenaGameLib.GameInterfaces;
using ArenaGameLib.GameObjects.Templates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects
{
	/// <summary>
	/// Class defining Armour objects, inheriting from ArenaObject template class.
	/// </summary>
	public class Armour : ArenaObject, IArmour
	{
		public int ReduceDamage { get; set; }
		public int ArmourDurability { get; set; }
		public bool Claimed { get; set; }

		/// <summary>
		/// Constructor for instanciating the Armour object.
		/// </summary>
		/// <param name="name">Type: string - Name of the Armour object</param>
		/// <param name="weight">Type: int (nullable) - Weight of the object</param>
		/// <param name="reduceDmg">Type: int - The amount of damage the armour object reduces</param>
		/// <param name="armourDur">Type: int - The amount of hits the armour object can take before it breaks and is unable to reduce damage</param>
		/// <param name="locX">Type: int (nullable) - The X coordinate of the armour object in the Arena</param>
		/// <param name="locY">Type: int (nullable) - The Y coordinate of the armour object in the Arena</param>
		public Armour(string name, int? weight, int reduceDmg, int armourDur, int? locX, int? locY) : base(name, weight, locX, locY)
		{
			Name = name;
			ReduceDamage = reduceDmg;
			ArmourDurability = armourDur;
			Lootable = true;
			Removeable = true;
			LocationX = locX;
			LocationY = locY;
			Claimed = false;
		}

		/// <summary>
		/// Method for setting the Claimed Boolean to true or false, if the armour has been looted by a creature.
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
		/// Sets the location X and Y coordinates based on the armour piece being claimed or not.
		/// </summary>
		/// <param name="locX">Type: int - X location</param>
		/// <param name="locY">Type: int - Y location</param>
		public void SetLocation(int locX, int locY)
		{
			if (Claimed)
			{
				LocationX = null;
				LocationY = null;
			}
			else if (!Claimed)
			{
				LocationX = locX;
				LocationY = locY;
			}
		}
	}
}
