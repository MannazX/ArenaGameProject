using ArenaGameLib.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces
{
	public interface IArmourCollection
	{
		List<IArmour> ArmourSet { get; set; }

		void Add(IArmour item);
		void Remove(IArmour item);
		int TotalWeight();
	}
}
