using ArenaGameLib.GameInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects.Decorators
{
	public class WeaponModifier : IWeaponModify
	{
		private readonly IWeapon _weapon;

		public WeaponModifier(IWeapon weapon)
		{
			_weapon = weapon;
		}

		public Weapon DegradeWeaponDamage(int modifier)
		{
			throw new NotImplementedException();
		}

		public Weapon ImproveWeaponDamage(int modifier)
		{
			if (modifier > 0)
			{
				_weapon.Damage *= modifier;
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
