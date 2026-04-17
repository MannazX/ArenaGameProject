using ArenaGameLib.GameInterfaces.Observers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects.Observers
{
	/// <summary>
	/// Observer class for notifying when a creature takes damage or is defeated.
	/// </summary>
	public class CombatNotifier : ICombatNotifier
	{
		private string message;

		public Creature Creature { get; set; }

		/// <summary>
		/// Constructor class for instanciating the combat notifier.
		/// </summary>
		/// <param name="creature">Type: Creature - The creature being observed (notified on)</param>
		public CombatNotifier(Creature creature)
		{
			Creature = creature;
		}

		/// <summary>
		/// Method for notifying when a creature is defeated.
		/// </summary>
		public void NotifyDefeated()
		{
			if (Creature.Health <= 0)
			{
				this.message = "Your creature is defeated";
			}
			Console.WriteLine(this.message);
		}

		/// <summary>
		/// Method for notifying when a creature has taken damage.
		/// </summary>
		/// <param name="damage">Type: int - Damage from incoming attack</param>
		/// <param name="absorbed">Type: int - Damage absorbed by armour</param>
		public void NotifyDamageTaken(int damage, int absorbed)
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
