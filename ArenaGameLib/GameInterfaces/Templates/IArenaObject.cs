using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces.Templates
{
	public interface IArenaObject
	{
		public string Name { get; set; }
		public bool Lootable { get; set; }
		public bool Removeable { get; set; }
		public int? Weight { get; set; }
		public int? LocationX { get; set; }
		public int? LocationY { get; set; }
	}
}
