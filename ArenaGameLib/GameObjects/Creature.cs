using ArenaGameLib.GameInterfaces.Strategies;
using ArenaGameLib.GameInterfaces.Templates;
using ArenaGameLib.GameObjects.AbstractClasses;
using ArenaGameLib.GameObjects.Composite;
using ArenaGameLib.GameObjects.Templates;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects
{
	/// <summary>
	/// Class representing Creatures battling in the arena, inheriting from CreatureTemplate Class
	/// </summary>
	public class Creature : CreatureTemplate
	{
		public WeaponCollection WeaponCollection { get; set; }
		public ArmourCollection ArmourCollection { get; set; }
		
		/// <summary>
		/// Constructor for making initial instance of the creature.
		/// </summary>
		/// <param name="name">Type: string - Name of the creature.</param>
		/// <param name="health">Type: int - Amount of hitpoints the creature can take before they are defeated.</param>
		/// <param name="unarmedDmg">Type: int - Amount of damage done without a weapon.</param>
		/// <param name="inventory">Type: List<IArenaObject> - List representing the creature's inventory.</param>
		/// <param name="inventoryCapacity">Type: int - Capacity of creatures inventory.</param>
		/// <param name="locX">Type: int - X Location of creature.</param>
		/// <param name="locY">Type: int - Y Location of creature.</param>
		public Creature(string name, int health, int unarmedDmg, List<IArenaObject> inventory, int inventoryCapacity, int locX, int locY) : base(name, health, unarmedDmg, inventory, armour, locX, locY
		{
			Name = name;
			Health = health;
			UnarmedDamage = unarmedDmg;
			Inventory = inventory;
			InventoryCapacity = inventoryCapacity;
			WeaponCollection = new WeaponCollection(200);
			ArmourCollection = new ArmourCollection(200);
			LocationX = locX;
			LocationY = locY;
		}

		/// <summary>
		/// Method for a creatures attack, that is predicated on the distance that a creature has to its opponent.
		/// </summary>
		/// <param name="targetDist">Type: int - Distance to creatures opponent.</param>
		/// <returns></returns>
		public override int Attack(int targetDist, IAttackStrategy attack)
		{
			int dmgOutput;
			if (WeaponCollection.Weapons.Count() > 0)
			{
				dmgOutput = attack.ArmedAttack(targetDist, WeaponCollection);
			}
			else
			{
				dmgOutput = attack.UnarmedAttack(targetDist, UnarmedDamage);
			}
			return dmgOutput;
		}

		/// <summary>
		/// Method for the creatures action of looting an item and adding it to Armour Collection, Weapon Collection or Inventory. The Method is predicated on the creature being in reaching distance (1 Square) of item.
		/// </summary>
		/// <param name="item">Type: ArenaObject - The item looted, if Weapon - Added to WeaponCollection, if Armour - Added to ArmourCollection.</param>
		public override void Loot(ArenaObject item)
		{
			if ((LocationX - item.LocationX <= 1 || LocationY - item.LocationY <= 1) && Inventory.Sum(x => x.Weight) + item.Weight < InventoryCapacity)
			{
				if (item.GetType() == typeof(Weapon))
				{
					WeaponCollection.Weapons.Add((Weapon)item);
				}
				else if (item.GetType() == typeof(Armour))
				{
					ArmourCollection.ArmourSet.Add((Armour)item);
				}
				else
				{
					Inventory.Add(item);
				}
			}
		}

		/// <summary>
		/// Method for the creature to take damage from an opponent's attack, the armour rating of its ArmourCollection will reduce the amount of health the creature will take.
		/// </summary>
		/// <param name="damage">Type: int - The damage of the incoming attack from the opponent.</param>
		public override void TakeDamage(int damage, IAbsorbDamageStrategy reducedDamage)
		{
			if (ArmourCollection.ArmourSet.Count() > 0)
			{
				Health = reducedDamage.ReducedDamage(damage, ArmourCollection, Health);
			}
			else
			{
				Health -= damage;
			}
		}
	}
}
