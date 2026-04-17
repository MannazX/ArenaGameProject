using ArenaGameLib.GameInterfaces.Strategies;
using ArenaGameLib.GameObjects.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects.Strategies
{
	/// <summary>
	/// Strategy class for methods that compute the total damage output of a creature's attack.
	/// </summary>
	public class AttackTarget : IAttackStrategy
	{
		/// <summary>
		/// Method for computing damage from armed attacks with all the weapons that a creature possesses. 
		/// </summary>
		/// <param name="targetDist">Type: int - Distance to target creature</param>
		/// <param name="weapons">Type: WeaponCollection - All the weapons a creature possesses</param>
		/// <returns>Type: int - Total Damage</returns>
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

		/// <summary>
		/// Method for determining damage for unarmed attacks dependant on the target being within range.
		/// </summary>
		/// <param name="targetDist">Type: int - Distance to target</param>
		/// <param name="unarmedAttack">Type: int - Damage dealt by unarmed attack</param>
		/// <returns>Type: int - Unarmed Damage (0 if out of target range)</returns>
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
