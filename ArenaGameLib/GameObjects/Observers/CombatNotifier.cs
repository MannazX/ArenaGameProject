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
		private Creature creature;
		public Creature Creature
		{
			get { return creature; }
			set { creature = value; }
		}

		public CombatNotifier(Creature creature)
		{
			this.creature = creature;
		}
		public void NotifyDefeated()
		{
			if (this.creature.Health <= 0)
			{
				this.message = "Your creature's is defeated";
			}
			Console.WriteLine(this.message);
		}

		public void NotifyHit(int damage)
		{
			if (damage > 0)
			{
				this.message = $"The incoming attack dealt {damage} to your creature";
			}
			else
			{
				this.message = "The incoming attack dealt no damage";
			}
			Console.WriteLine(this.message);
		}
	}
}
