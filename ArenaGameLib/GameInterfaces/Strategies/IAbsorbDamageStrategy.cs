using ArenaGameLib.GameObjects.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces.Strategies
{
	public interface IAbsorbDamageStrategy
	{
		int AbsorbedDamage(int damage, ArmourCollection armourSet);
	}
}
