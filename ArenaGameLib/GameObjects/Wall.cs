using ArenaGameLib.GameInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects
{
	/// <summary>
	/// Class that represents a wall as an impassible terrain for the creatures. The properties are not allowed to change value once set.
	/// </summary>
	public class Wall : IWall
	{
		private int _lenX;
		private int _lenY;
		private int _locX;
		private int _locY;
		public int LengthX { get { return _lenX; } set { if (value != _lenX) { throw new ArgumentException("You cannot change the value of this property"); } } }
		public int LengthY { get { return _lenY; } set { if (value != _lenY) { throw new ArgumentException("You cannot change the value of this property"); } } }
		public int LocationX { get { return _locX; } set { if (value != _locX) { throw new ArgumentException("You cannot change the value of this property"); } } }
		public int LocationY { get { return _locY; } set { if (value != _locY) { throw new ArgumentException("You cannot change the value of this property"); } } }

		/// <summary>
		/// Constructor class for instanciating a wall object;.
		/// </summary>
		/// <param name="lenX">Type: int - Length on X axis</param>
		/// <param name="lenY">Type: int - Length on Y axis</param>
		/// <param name="locX">Type: int - X coordinate of wall</param>
		/// <param name="locY">Type: int - Y coordinate of wall</param>
		public Wall(int lenX, int lenY, int locX, int locY)
		{
			_lenX = lenX;
			_lenY = lenY;
			_locX = locX;
			_locY = locY;
			LengthX = _lenX;
			LengthY = _lenY;
			LocationX = _locX;
			LocationY = _locY;
		}
	}
}
