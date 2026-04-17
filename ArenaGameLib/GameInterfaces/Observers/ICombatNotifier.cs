using ArenaGameLib.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces.Observers
{
	public interface ICombatNotifier
	{
		Creature Creature { get; set; }
		void NotifyDamageTaken(int damage, int absorbed);
		void NotifyDefeated();
	}
}
