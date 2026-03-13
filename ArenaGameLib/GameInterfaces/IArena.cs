using ArenaGameLib.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces
{
	public interface IArena
	{
		public int MaxX { get; set; }
		public int MaxY { get; set; }
		public List<Creature> Creatures { get; set; }
		public List<ArenaObject> LootItems { get; set; }
	}
}
