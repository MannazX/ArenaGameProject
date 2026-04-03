using ArenaGameLib.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces.Decorators
{
	public interface IArmourModify
	{
		Armour ImproveArmourDurability(int modifier);
		Armour DegradeArmourDurability(int modifier);
	}
}
