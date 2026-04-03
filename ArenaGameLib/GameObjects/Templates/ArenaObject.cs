using ArenaGameLib.GameInterfaces.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects.Templates
{
	public abstract class ArenaObject : IArenaObject
	{
		public string Name { get; set; }
		public bool Lootable { get; set; }
		public bool Removeable { get; set; }
		public int? LocationX { get; set; }
		public int? LocationY { get; set; }
		public int? Weight { get; set; }

		protected ArenaObject(string name, int weight, int locX, int locY)
		{
			if (!Lootable)
			{
				Weight = null;
			}
			else
			{
				Weight = weight;
			}
		}
	}
}
