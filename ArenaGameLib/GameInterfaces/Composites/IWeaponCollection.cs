using ArenaGameLib.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces.Composites
{
	public interface IWeaponCollection
	{
		List<IWeapon> Weapons { get; set; }

		void Add(IWeapon item);
		void Remove(IWeapon item);
		int TotalWeight();
	}
}
