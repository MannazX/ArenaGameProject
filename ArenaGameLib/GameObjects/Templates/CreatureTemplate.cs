using ArenaGameLib.GameInterfaces.Strategies;
using ArenaGameLib.GameInterfaces.Templates;
using ArenaGameLib.GameObjects.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects.AbstractClasses
{
	public abstract class CreatureTemplate : ICreatureTemplate
	{
		public string Name { get; set; }
		public int Health { get; set; }
		public int UnarmedDamage { get; set; }
		public List<IArenaObject> Inventory { get; set; }
		public int InventoryCapacity { get; set; }
		public int LocationX { get; set; }
		public int LocationY { get; set; }

		protected CreatureTemplate(string name, int health, int unarmedDmg, List<IArenaObject> inventory, int inventoryCapacity, int locX, int locY)
		{
			Health = health;
			UnarmedDamage = unarmedDmg;
		}

		public abstract int Attack(int targetDist, IAttackStrategy attack);

		public abstract void Loot(ArenaObject item);

		public abstract void TakeDamage(int damage, IAbsorbDamageStrategy reducedDamage);
	}
}
