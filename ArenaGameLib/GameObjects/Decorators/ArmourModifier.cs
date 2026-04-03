using ArenaGameLib.GameInterfaces;
using ArenaGameLib.GameInterfaces.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects.Decorators
{
	public class ArmourModifier : IArmourModify
	{
		private readonly IArmour _armour;

		public ArmourModifier(IArmour armour)
		{
			_armour = armour;			
		}

		public Armour DegradeArmourDurability(int modifier)
		{
			if (modifier > 0)
			{
				_armour.ArmourDurability -= modifier / 100;
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

		public Armour ImproveArmourDurability(int modifier)
		{
			if (modifier > 0)
			{
				_armour.ArmourDurability *= modifier;
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
