using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces.Templates
{
	public interface IArenaObject
	{
		string Name { get; set; }
		bool Lootable { get; set; }
		bool Removeable { get; set; }
		int? Weight { get; set; }
		int? LocationX { get; set; }
		int? LocationY { get; set; }
	}
}
