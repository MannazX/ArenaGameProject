using ArenaGameLib.GameInterfaces.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces
{
	public interface IWeapon : IArenaObject
	{
		public int Damage { get; set; }
		public int Range { get; set; }
		public bool Claimed { get; set; }
	}
}
