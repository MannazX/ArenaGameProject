using ArenaGameLib.GameObjects.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces.Strategies
{
	public interface IAttackStrategy
	{
		int ArmedAttack(int targetDist, WeaponCollection weapons);
		int UnarmedAttack(int targetDist, int unarmedAttack);
	}
}
