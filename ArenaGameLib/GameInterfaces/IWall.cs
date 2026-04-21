using ArenaGameLib.GameInterfaces.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces
{
	public interface IWall
	{
		int LengthX { get; set; }
		int LengthY { get; set; }
		int LocationX { get; set; }
		int LocationY { get; set; }
	}
}
