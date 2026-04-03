using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces.Observers
{
	public interface ICombatNotifier
	{
		void NotifyHit(int damage);
		void NotifyDefeated();
	}
}
