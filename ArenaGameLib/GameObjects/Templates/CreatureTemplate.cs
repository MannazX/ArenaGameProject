using ArenaGameLib.GameInterfaces;
using ArenaGameLib.GameInterfaces.Decorators;
using ArenaGameLib.GameInterfaces.Observers;
using ArenaGameLib.GameInterfaces.Strategies;
using ArenaGameLib.GameInterfaces.Templates;
using ArenaGameLib.GameLogger;
using ArenaGameLib.GameObjects.Composite;
using ArenaGameLib.GameObjects.Composites;
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
	/// Template class for the Creature class - Abstract class that the Creature class inherits from and defines constraints and methods and overloads for the creature subclass.
	/// </summary>
	public abstract class CreatureTemplate : ICreatureTemplate
	{
		public string Name { get; set; }
		public int Health { get; set; }
		public int UnarmedDamage { get; set; }
		public int LocationX { get; set; }
		public int LocationY { get; set; }

		/// <summary>
		/// Constructor method for the Creature template class that sets the argument constraint for the inventory that the creature is capable of carrying - regarding weapons and armour.
		/// </summary>
		/// <param name="name">Type: string - Creature name</param>
		/// <param name="health">Type: int - Creature health</param>
		/// <param name="unarmedDmg">Type: int - Unarmed damage dealt by Creature</param>
		/// <param name="inventory">Type: List<ArenaObject> - Creatures inventory</param>
		/// <param name="inventoryCapacity">Type: int - Maximum inventory capacity</param>
		/// <param name="weaponCapacity">Type: int - Maximum weapon collection capacity</param>
		/// <param name="armourCapacity">Type: int - Maxumum armour collection capacity</param>
		/// <param name="locX">Type: int - Positional X coordinate</param>
		/// <param name="locY">Type: int - Positional Y coordinate</param>
		/// <exception cref="ArgumentOutOfRangeException">Exception thrown if weapons and armour capacity exceeds inventory capacity</exception>
		protected CreatureTemplate(string name, int health, int unarmedDmg, int inventoryCapacity, int weaponCapacity, int armourCapacity, int locX, int locY)
		{

			if (weaponCapacity + armourCapacity > inventoryCapacity)
			{
				throw new ArgumentOutOfRangeException("The weapons and armour capacity exceeds the inventory capacity");
			}
			Health = health;
			UnarmedDamage = unarmedDmg;
		}

		/// <summary>
		/// Operator overload for + operation to support the creature looting an item through the + operation.
		/// </summary>
		/// <param name="creature">Type: Creature - The creature looting the item</param>
		/// <param name="item">Type: ArenaObject - The item looted by the creature</param>
		/// <returns>Type: Creature - Creature that looted the item</returns>
		public static CreatureTemplate operator +(CreatureTemplate creature, ArenaObject item)
		{
			creature.Loot(item);
			return creature;
		}

		/// <summary>
		/// Abstract template method Attack that the Creature subclass overrides.
		/// </summary>
		/// <param name="targetDist">Type: int - Distance to target</param>
		/// <param name="attack">Type: IAttackStrategy - Strategy class containing methods returning the damage dealt by individual attacks</param>
		/// <returns>Type: int - Total damage output of a creature</returns>
		public abstract int Attack(int targetDist, IAttackStrategy attack);

		/// <summary>
		/// Abstract template method Loot that the Creature subclass overrides.
		/// </summary>
		/// <param name="item">Type: ArenaObject - The item looted by the creature</param>
		public abstract void Loot(ArenaObject item);

		/// <summary>
		/// Abstract template method Drop that the Creature subclass overrides.
		/// </summary>
		/// <param name="item">Type: ArenaObject - The item dropped by the creature</param>
		public abstract void Drop(ArenaObject item);
		
		/// <summary>
		/// Abstract template method TakeDamage that the Creature subclass overrides.
		/// </summary>
		/// <param name="damage">Type: int - Damage from an incoming attack at the creature</param>
		/// <param name="reducedDamage">Type: IAbsorbedDamageStrategy - Strategy class containing methods returning the reduced damage absorbed from individual incoming attacks</param>
		public abstract void TakeDamage(int damage, IAbsorbDamageStrategy reducedDamage);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="weapon"></param>
		/// <param name="modifier"></param>
		public abstract void WeaponImprove(IWeapon weapon, int modifier);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="weapon"></param>
		/// <param name="modifier"></param>
		public abstract void WeaponDegrade(IWeapon weapon, int modifier);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="armour"></param>
		/// <param name="modifier"></param>
		public abstract void ArmourImprove(IArmour armour, int modifier);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="armour"></param>
		/// <param name="modifier"></param>
		public abstract void ArmourDegrade(IArmour armour, int modifier);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="notifier"></param>
		public abstract void AddObserver(ICombatNotifier notifier);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="notifier"></param>
		public abstract void RemoveObserver(ICombatNotifier notifier);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="damage"></param>
		/// <param name="absorbed"></param>
		public abstract void NotifyAllDamageTaken(int damage, int absorbed);

		/// <summary>
		/// 
		/// </summary>
		public abstract void NotifyAllDefeated();
	}
}
