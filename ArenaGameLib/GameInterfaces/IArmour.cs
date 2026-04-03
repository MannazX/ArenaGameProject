using ArenaGameLib.GameInterfaces.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces
{
	public interface IArmour : IArenaObject
	{
		public int ArmourDurability { get; set; }
		public int ReduceDamage { get; set; }
		public bool Claimed { get; set; }
	}
}
