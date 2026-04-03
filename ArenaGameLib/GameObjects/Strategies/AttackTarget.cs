using ArenaGameLib.GameInterfaces.Strategies;
using ArenaGameLib.GameObjects.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects.Strategies
{
	public class AttackTarget : IAttackStrategy
	{
		public int ArmedAttack(int targetDist, WeaponCollection weapons)
		{
			int totalDmg = 0;
			foreach (Weapon wep in weapons.Weapons)
			{
				if (wep.Range >= targetDist)
				{
					totalDmg += wep.Damage;
				}
			}
			return totalDmg;
		}

		public int UnarmedAttack(int targetDist, int unarmedAttack)
		{
			if (targetDist == 1)
			{
				return unarmedAttack;
			}
			else
			{
				return 0;
			}
		}
	}
}
