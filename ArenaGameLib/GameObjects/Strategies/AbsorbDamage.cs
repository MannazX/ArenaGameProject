using ArenaGameLib.GameInterfaces.Strategies;
using ArenaGameLib.GameObjects.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects.Strategies
{
	public class AbsorbDamage : IAbsorbDamageStrategy
	{
		public int ReducedDamage(int damage, ArmourCollection armourSet, int health)
		{
			int absorbed = 0;
			foreach (Armour armour in armourSet.ArmourSet)
			{
				if (armour.ArmourDurability > 0)
				{
					absorbed += armour.ReduceDamage;
					armour.ArmourDurability -= 1;
				}
			}
			health -= damage - absorbed;
			return health;
		}
	}
}
