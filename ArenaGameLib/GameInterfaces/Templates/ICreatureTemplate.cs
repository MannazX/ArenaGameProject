using ArenaGameLib.GameInterfaces.Strategies;
using ArenaGameLib.GameObjects.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces.Templates
{
	public interface ICreatureTemplate
	{
		public string Name { get; set; }
		public int Health { get; set; }
		public List<IArenaObject> Inventory { get; set; }
		public int LocationX { get; set; }
		public int LocationY { get; set; }
		public int Attack(int targetDist, IAttackStrategy attack);
		public void TakeDamage(int damage, IAbsorbDamageStrategy reducedDamage);
		public void Loot(ArenaObject item);
	}
}
