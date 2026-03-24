using ArenaGameLib.GameObjects.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces
{
	public interface ICreatureTemplate
	{
		public string Name { get; set; }
		public int Health { get; set; }
		public List<IArenaObject> Inventory { get; set; }
		public int LocationX { get; set; }
		public int LocationY { get; set; }
		public int Attack(int targetDist);
		public void TakeDamage(int damage);
		public void Loot(ArenaObject item);
	}
}
