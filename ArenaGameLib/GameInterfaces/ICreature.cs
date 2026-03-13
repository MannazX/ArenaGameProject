using ArenaGameLib.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces
{
	public interface ICreature
	{
		public string Name { get; set; }
		public int Health { get; set; }
		public List<IArenaObject> Inventory { get; set; }
		public List<Weapon> Weapons { get; set; }
		public List<Armour> ArmourPieces { get; set; }
		public int LocationX { get; set; }
		public int LocationY { get; set; }
		public int Attack(int targetDist);
		public void TakeDamage(int damage);
		public void Loot(ArenaObject item);
	}
}
