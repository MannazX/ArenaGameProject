using ArenaGameLib.GameInterfaces.Observers;
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
		public List<ICombatNotifier> CombatNotifications { get; set; }

		/// <summary>
		/// Constructor for making initial instance of the creature.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="health"></param>
		/// <param name="unarmedDmg"></param>
		/// <param name="inventory"></param>
		/// <param name="inventoryCapacity"></param>
		/// <param name="locX"></param>
		/// <param name="locY"></param>
		public Creature(string name, int health, int unarmedDmg, List<IArenaObject> inventory, int inventoryCapacity, int locX, int locY) : base(name, health, unarmedDmg, inventory, armour, locX, locY
		{
			Name = name;
			Health = health;
			UnarmedDamage = unarmedDmg;
			Inventory = inventory;
			InventoryCapacity = inventoryCapacity;
			WeaponCollection = new WeaponCollection(200);
			ArmourCollection = new ArmourCollection(200);
			CombatNotifications = new List<ICombatNotifier>();
			LocationX = locX;
			LocationY = locY;
		}

		/// <summary>
		/// Method for a creatures attack, that is predicated on the distance that a creature has to its opponent.
		/// </summary>
		/// <param name="targetDist"></param>
		/// <param name="attack"></param>
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
		/// <param name="damage"></param>
		/// <param name="reducedDamage"></param>
		public override void TakeDamage(int damage, IAbsorbDamageStrategy reducedDamage)
		{
			int absorbed = 0;
			if (ArmourCollection.ArmourSet.Count() > 0)
			{
				absorbed = reducedDamage.AbsorbedDamage(damage, ArmourCollection);
				Health -= damage - absorbed;
			}
			else
			{
				Health -= damage;
			}
			NotifyAllHits(damage, absorbed);
			if (Health <= 0)
			{
				NotifyAllDefeated();
				CombatNotifications.Clear();
			}
		}

		/// <summary>
		/// Method for adding a new observer to notify this Creature object.
		/// </summary>
		/// <param name="notifier"></param>
		public void AddObserver(ICombatNotifier notifier)
		{
			CombatNotifications.Add(notifier);
		}

		/// <summary>
		/// Metod for removing an observer in on this Creature object.
		/// </summary>
		/// <param name="notifier"></param>
		public void RemoveObserver(ICombatNotifier notifier)
		{
			CombatNotifications.Remove(notifier);
		}

		/// <summary>
		/// Method for executing Hit notification from all observers.
		/// </summary>
		/// <param name="damage"></param>
		/// <param name="absorbed"></param>
		public void NotifyAllHits(int damage, int absorbed)
		{
			foreach (ICombatNotifier notifier in CombatNotifications)
			{
				notifier.NotifyHit(damage, absorbed);
			}
		}

		/// <summary>
		/// Method for executing Defeat notification from all observers.
		/// </summary>
		public void NotifyAllDefeated()
		{
			foreach (ICombatNotifier notifier in CombatNotifications)
			{
				notifier.NotifyDefeated();
			}
		}
	}
}
