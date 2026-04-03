using ArenaGameLib.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces.Decorators
{
	public interface IWeaponModify
	{
		Weapon ImproveWeaponDamage(int modifier);
		Weapon DegradeWeaponDamage(int modifier);
	}
}
