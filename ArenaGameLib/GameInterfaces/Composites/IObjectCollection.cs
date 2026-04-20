using ArenaGameLib.GameInterfaces.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces.Composites
{
	public interface IObjectCollection
	{
		List<IArenaObject> Items { get; set; }
		int Capacity { get; set; }

		void Add(IArenaObject item);
		void Remove(IArenaObject item);
		int TotalWeight();
	}
}
