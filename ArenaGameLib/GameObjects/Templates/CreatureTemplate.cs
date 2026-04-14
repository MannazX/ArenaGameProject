using ArenaGameLib.GameInterfaces.Strategies;
using ArenaGameLib.GameInterfaces.Templates;
using ArenaGameLib.GameLogger;
using ArenaGameLib.GameObjects.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ArenaGameLib.GameObjects.AbstractClasses
{
	/// <summary>
	/// Template class for the Creature class, which is an abstract class that the Creature class inherits from and defines constraints and methods and overloads for the creature subclass.
	/// </summary>
	public abstract class CreatureTemplate : ICreatureTemplate
	{
		public string Name { get; set; }
		public int Health { get; set; }
		public int UnarmedDamage { get; set; }
		public List<IArenaObject> Inventory { get; set; }
		public int InventoryCapacity { get; set; }
		public int LocationX { get; set; }
		public int LocationY { get; set; }

		/// <summary>
		/// Constructor method for the Creature template clas that sets the argument constraint for the inventory that the Creature is capable of carrying - regarding weapons and armour.
		/// </summary>
		/// <param name="name">Type: string - Creature name</param>
		/// <param name="health">Type: int - Creature health</param>
		/// <param name="unarmedDmg">Type: int - Unarmed damage dealt by Creature</param>
		/// <param name="inventory">Type: List<ArenaObject> - List of arena objects</param>
		/// <param name="inventoryCapacity"></param>
		/// <param name="weaponCapacity"></param>
		/// <param name="armourCapacity"></param>
		/// <param name="locX"></param>
		/// <param name="locY"></param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		protected CreatureTemplate(string name, int health, int unarmedDmg, List<IArenaObject> inventory, int inventoryCapacity, int weaponCapacity, int armourCapacity, int locX, int locY)
		{
			if (weaponCapacity + armourCapacity > inventoryCapacity)
			{
				throw new ArgumentOutOfRangeException("The weapons and armour capacity exceeds the inventory capacity");
			}
			Health = health;
			UnarmedDamage = unarmedDmg;
		}

		public static CreatureTemplate operator +(CreatureTemplate creature, ArenaObject item)
		{
			/// This method can replace creature.Loot(item) with creature + item 
			creature.Loot(item);
			return creature;
		}

		public abstract int Attack(int targetDist, IAttackStrategy attack);

		public abstract void Loot(ArenaObject item);

		public abstract void TakeDamage(int damage, IAbsorbDamageStrategy reducedDamage);
	}
}
