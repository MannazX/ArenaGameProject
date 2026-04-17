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
	/// Decorator class for improving or degrading the creature's armour.
	/// </summary>
	public class ArmourModifier : IArmourModify
	{
		private readonly IArmour _armour;

		/// <summary>
		/// Constructor method for instanciating the armour modifier.
		/// </summary>
		/// <param name="armour">Type: IArmour - The armour that is modified</param>
		public ArmourModifier(IArmour armour)
		{
			_armour = armour;			
		}

		/// <summary>
		/// Method for degrading an armour piece's durability - Debuff from opponent creature.
		/// </summary>
		/// <param name="modifier">Type: int - Modifying factor for the armour's durability degrading</param>
		/// <returns>Type: Armour - The armour with degraded durability</returns>
		public Armour DegradeArmourDurability(int modifier)
		{
			if (modifier > 0)
			{
				_armour.ArmourDurability *= (1 - modifier / 100);
				return (Armour)_armour;
			}
			else if (modifier == 0)
			{
				return (Armour)_armour;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Method for improving an armour piece's durability - Buff from creature itself.
		/// </summary>
		/// <param name="modifier">Type: int - Modifying factor for the armour's durability degrading</param>
		/// <returns></returns>
		public Armour ImproveArmourDurability(int modifier)
		{
			if (modifier > 0)
			{
				_armour.ArmourDurability *= (1 + modifier / 100);
				return (Armour)_armour;
			}
			else if (modifier == 0)
			{
				return (Armour)_armour;
			}
			else
			{
				return null;
			}
		}
	}
}
