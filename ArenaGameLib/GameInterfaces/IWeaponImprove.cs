using ArenaGameLib.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces
{
	public interface IWeaponImprove
	{
		Weapon ImproveWeaponDamage(int modifier);
	}
}
