using ArenaGameLib.GameInterfaces;
using ArenaGameLib.GameObjects.AbstractClasses;
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
		public int Damage { get; set; }
		public int Range { get; set; }
		public bool Claimed { get; set; }

		public Weapon(string name, int weight, int locX, int locY, int damage, int range) : base(name, weight, locX, locY)
		{
			Name = name;
			Weight = weight;
			Lootable = true;
			Removeable = true;
			Damage = damage;
			Range = range;
			Claimed = false;
		}

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
