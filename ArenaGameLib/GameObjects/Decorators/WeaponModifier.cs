using ArenaGameLib.GameInterfaces;
using ArenaGameLib.GameInterfaces.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects.Decorators
{
	/// <summary>
	/// Decorator class for improving or degrading the creature's weapons.
	/// </summary>
	public class WeaponModifier : IWeaponModify
	{
		private readonly IWeapon _weapon;

		/// <summary>
		/// Constructor method for instanciating the weapon modifier.
		/// </summary>
		/// <param name="weapon">Type: IWeapon - Weapon that is modified</param>
		public WeaponModifier(IWeapon weapon)
		{
			_weapon = weapon;
		}

		/// <summary>
		/// Method for degrading a creatures weapon - Debuff from opponent creature.
		/// </summary>
		/// <param name="modifier">Type: int - Modifying factor for the weapon degrading</param>
		/// <returns>Type: Weapon - Returns the degraded weapon</returns>
		public Weapon DegradeWeaponDamage(int modifier)
		{
			if (modifier > 0)
			{
				_weapon.Damage *= (1 - modifier / 100);
				return (Weapon)_weapon;
			}
			else if (modifier == 0)
			{
				return (Weapon)_weapon;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Method for improving a creatures weapon - Buff from creature itself.
		/// </summary>
		/// <param name="modifier">Type: int - Modifying factor for the weapon improvement</param>
		/// <returns>Type: Weapon - Returning the improved weapon</returns>
		public Weapon ImproveWeaponDamage(int modifier)
		{
			if (modifier > 0)
			{
				_weapon.Damage *= (1 + modifier / 100);
				return (Weapon)_weapon;
			}
			else if (modifier == 0)
			{
				return (Weapon)_weapon;
			}
			else
			{
				return null;
			}
		}
	}
}
