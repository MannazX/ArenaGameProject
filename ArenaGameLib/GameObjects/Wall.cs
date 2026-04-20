using ArenaGameLib.GameInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects
{
	/// <summary>
	/// Class that represents a wall as an impassible terrain for the creatures.
	/// </summary>
	public class Wall : IWall
	{
		public int LengthX { get; set; }
		public int LengthY { get; set; }
		public string Name { get; set; }
		public bool Lootable { get; set; }
		public bool Removeable { get; set; }
		public int? Weight { get; set; }
		public int? LocationX { get; set; }
		public int? LocationY { get; set; }

		/// <summary>
		/// Constructor class for instanciating a wall object.
		/// </summary>
		/// <param name="lenX">Type: int - Length on X axis</param>
		/// <param name="lenY">Type: int - Length on Y axis</param>
		/// <param name="locX">Type: int - X coordinate of wall</param>
		/// <param name="locY">Type: int - Y coordinate of wall</param>
		public Wall(int lenX, int lenY, int locX, int locY)
		{
			LengthX = lenX;
			LengthY = lenY;
			LocationX = locX;
			LocationY = locY;
			Name = null;
			Lootable = false;
			Removeable = false;
			Weight = null;
		}
	}
}
