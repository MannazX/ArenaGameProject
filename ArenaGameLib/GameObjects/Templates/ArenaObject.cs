using ArenaGameLib.GameInterfaces.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects.Templates
{
	/// <summary>
	/// Template class for Arena Object classes - Abstract class that the individual objects in the arena inherits from and sets the constraints for the objects that are lootable or not
	/// </summary>
	public abstract class ArenaObject : IArenaObject
	{
		public string Name { get; set; }
		public bool Lootable { get; set; }
		public bool Removeable { get; set; }
		public int? LocationX { get; set; }
		public int? LocationY { get; set; }
		public int? Weight { get; set; }

		/// <summary>
		/// Constructor method for the Arena Object specifying the lootable constraint for the items - If lootable items are assigned a weight, if not the weight is set to null. 
		/// </summary>
		/// <param name="name">Type: string - Name of object</param>
		/// <param name="weight">Type: int (nullable) - Weight of object</param>
		/// <param name="locX">Type: int (nullable) - Positional X coordinate of item</param>
		/// <param name="locY">Type: int (nullable) - Postitonal Y coordinate of item</param>
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
