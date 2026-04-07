using ArenaGameLib.GameInterfaces.Observers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects.Observers
{
	public class CombatNotifier : ICombatNotifier
	{
		private string message;

		public Creature Creature { get; set; }

		public CombatNotifier(Creature creature)
		{
			Creature = creature;
		}
		public void NotifyDefeated()
		{
			if (Creature.Health <= 0)
			{
				this.message = "Your creature is defeated";
			}
			Console.WriteLine(this.message);
		}

		public void NotifyHit(int damage, int absorbed)
		{
			if (damage > 0)
			{
				this.message = $"The incoming attack dealt {damage} to your creature. Your creature absorbed {absorbed} damage.";
			}
			else
			{
				this.message = "The incoming attack dealt no damage";
			}
			Console.WriteLine(this.message);
		}
	}
}
