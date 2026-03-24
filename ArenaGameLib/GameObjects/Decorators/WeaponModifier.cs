using ArenaGameLib.GameInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects.Decorators
{
	public class WeaponModifier : IWeaponImprove
	{
		private readonly IWeapon _weapon;

		public WeaponModifier(IWeapon weapon)
		{
			_weapon = weapon;
		}

		public Weapon ImproveWeaponDamage(int modifier)
		{
			if (modifier > 0)
			{
				_weapon.Damage *= modifier;
				return (Weapon)_weapon;
			}
			else
			{
				return null;
			}
		}
	}
}
