using ArenaGameLib.GameInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects
{
	public class Armour : ArenaObject, IArmour
	{
		public int ReduceDamage { get; set; }
		public int ArmourDurability { get; set; }
		public bool Claimed { get; set; }

		public Armour(string name, int weight, int reduceDmg, int armourDur, int locX, int locY) : base(name, weight, locX, locY)
		{
			Name = name;
			ReduceDamage = reduceDmg;
			ArmourDurability = armourDur;
			Claimed = false;
			if (Claimed)
			{
				LocationX = null;
				LocationY = null;
			}
			else
			{
				LocationX = locX;
				LocationY = locY;
			}
		}
	}
}
