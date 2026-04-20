using ArenaGameLib.GameInterfaces.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces
{
	public interface IWall : IArenaObject
	{
		int LengthX { get; set; }
		int LengthY { get; set; }
	}
}
