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
	/// Strategy class for methods that compute the total damage absorbed from an incoming attack on creature.
	/// </summary>
	public class AbsorbDamage : IAbsorbDamageStrategy
	{
		/// <summary>
		/// Strategy class for absorbing the damage that a creature takes from an incoming attack, provided the creature is wearing armour.
		/// </summary>
		/// <param name="damage">Type: int - Damage from incoming attack</param>
		/// <param name="armourSet">Type: ArmourCollection - All the armour that the creature possesses</param>
		/// <returns>Type: int - Damage absorbed by creature's armour</returns>
		public int AbsorbedDamage(int damage, ArmourCollection armourSet)
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
			return absorbed;
		}
	}
}
