using ArenaGameLib.GameInterfaces.Observers;
using ArenaGameLib.GameInterfaces.Strategies;
using ArenaGameLib.GameInterfaces.Templates;
using ArenaGameLib.GameObjects.AbstractClasses;
using ArenaGameLib.GameObjects.Composite;
using ArenaGameLib.GameObjects.Templates;
using ArenaGameLib.GameLogger;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ArenaGameLib.GameObjects.Composites;
using ArenaGameLib.GameInterfaces;
using ArenaGameLib.GameInterfaces.Decorators;
using ArenaGameLib.GameObjects.Decorators;


namespace ArenaGameLib.GameObjects
{
	/// <summary>
	/// Class defining Creatures combating in the arena, inheriting from CreatureTemplate Class
	/// </summary>
	public class Creature : CreatureTemplate
	{
		private readonly Logger logger;
		public WeaponCollection WeaponCollection { get; set; }
		public ArmourCollection ArmourCollection { get; set; }
		public CreatureInventory Inventory { get; set; }
		public List<ICombatNotifier> CombatNotifications { get; set; }

		/// <summary>
		/// Constructor for instanciating Creature object and logging of creature actions.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="health"></param>
		/// <param name="unarmedDmg"></param>
		/// <param name="inventory"></param>
		/// <param name="inventoryCapacity"></param>
		/// <param name="weaponCapacity"></param>
		/// <param name="armourCapacity"></param>
		/// <param name="locX"></param>
		/// <param name="locY"></param>
		public Creature(string name, int health, int unarmedDmg, CreatureInventory inventory, int inventoryCapacity, int weaponCapacity, int armourCapacity, int locX, int locY) : base(name, health, unarmedDmg, inventoryCapacity, weaponCapacity, armourCapacity, locX, locY)
		{
			XmlDocument configDoc = new XmlDocument();
			logger = Logger.InitLogger();
			string config = Environment.GetEnvironmentVariable("ArenaGameConfig");
			configDoc.Load(config);
			Name = name;
			Health = health;
			UnarmedDamage = unarmedDmg;
			Inventory = new CreatureInventory(inventoryCapacity);
			WeaponCollection = new WeaponCollection(weaponCapacity);
			ArmourCollection = new ArmourCollection(armourCapacity);
			CombatNotifications = new List<ICombatNotifier>();
			LocationX = locX;
			LocationY = locY;
			logger.StartLogger();
		}

		/// <summary>
		/// Method for a creatures attack, that is predicated on the distance that a creature has to its opponent.
		/// </summary>
		/// <param name="targetDist"></param>
		/// <param name="attack"></param>
		/// <returns>Type: int - Damage output of the attack</returns>
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
			logger.WriteInfo($"Creature {Name} dealt {dmgOutput}");
			return dmgOutput;
		}

		/// <summary>
		/// Method for the creature to take damage from an opponent's attack, the armour rating of its ArmourCollection will reduce the amount of health the creature will take, it ensures that if the absorbtion is higher than the damage the damage the creature takes is zero.
		/// </summary>
		/// <param name="damage"></param>
		/// <param name="reducedDamage"></param>
		public override void TakeDamage(int damage, IAbsorbDamageStrategy reducedDamage)
		{
			int absorbed = 0;
			if (ArmourCollection.ArmourSet.Count() > 0)
			{
				absorbed = reducedDamage.AbsorbedDamage(damage, ArmourCollection);
				if (absorbed > damage)
				{
					absorbed = damage;
				}
				Health -= damage - absorbed;
				logger.WriteInfo($"Creature's health reduced to {Health} - absorbing {absorbed} of out {damage} damage points");
			}
			else
			{
				Health -= damage;
				logger.WriteInfo($"Creature's health reduced to {Health} - taking {damage} damage points");
			}
			NotifyAllDamageTaken(damage, absorbed);
			if (Health <= 0)
			{
				logger.WriteInfo($"Creature has been defeated");
				NotifyAllDefeated();
				CombatNotifications.Clear();
				logger.CloseLogger();
			}
		}

		/// <summary>
		/// Method for the creatures action of looting an item and adding it to Armour Collection, Weapon Collection or Inventory. The Method is predicated on the creature being in reaching distance (1 Square) of item.
		/// </summary>
		/// <param name="item">Type: ArenaObject - The item looted, if Weapon - Added to WeaponCollection, if Armour - Added to ArmourCollection.</param>
		public override void Loot(ArenaObject item)
		{
			if ((LocationX - item.LocationX <= 1 || LocationY - item.LocationY <= 1) && Inventory.Items.Sum(x => x.Weight) + item.Weight < Inventory.Capacity)
			{
				try
				{
					if (item.GetType() == typeof(Weapon))
					{
						WeaponCollection.Weapons.Add((Weapon)item);
					}
					else if (item.GetType() == typeof(Armour))
					{
						ArmourCollection.ArmourSet.Add((Armour)item);
					}
					logger.WriteInfo($"Creature {Name} looted item {item}");
					Inventory.Items.Add(item);
				}
				catch (ArgumentOutOfRangeException ex)
				{
					logger.LogError(ex.Message);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		public override void Drop(ArenaObject item)
		{
			try
			{
				if (item.GetType() == typeof(Weapon))
				{
					WeaponCollection.Weapons.Remove((Weapon)item);
				}
				else if (item.GetType() == typeof(Armour))
				{
					ArmourCollection.ArmourSet.Remove((Armour)item);
				}
				logger.WriteInfo($"Creature {Name} dropped item {item}");
				Inventory.Add(item);
			}
			catch (ArgumentException ex)
			{
				logger.LogError(ex.Message);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="weapon"></param>
		/// <param name="modifier"></param>
		public override void WeaponImprove(IWeapon weapon, int modifier)
		{
			if (WeaponCollection.Weapons.Contains(weapon))
			{
				IWeaponModify wepDecor = new WeaponModifier(weapon);
				wepDecor.ImproveWeaponDamage(modifier);
				logger.WriteInfo($"Weapon - {weapon.Name}'s damage was improved by a factor of {modifier}");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="weapon"></param>
		/// <param name="modifier"></param>
		public override void WeaponDegrade(IWeapon weapon, int modifier)
		{
			if (WeaponCollection.Weapons.Contains(weapon))
			{
				IWeaponModify wepDecor = new WeaponModifier(weapon);
				wepDecor.DegradeWeaponDamage(modifier);
				logger.WriteInfo($"Weapon - {weapon.Name}'s damage was degraded by a factor of {modifier}");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="armour"></param>
		/// <param name="modifier"></param>
		public override void ArmourImprove(IArmour armour, int modifier)
		{
			if (ArmourCollection.ArmourSet.Contains(armour))
			{
				IArmourModify armDecor = new ArmourModifier(armour);
				armDecor.ImproveArmourDurability(modifier);
				logger.WriteInfo($"Armour - {armour.Name}'s armour durability was improved by a factor of {modifier}");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="armour"></param>
		/// <param name="modifier"></param>
		public override void ArmourDegrade(IArmour armour, int modifier)
		{
			if (ArmourCollection.ArmourSet.Contains(armour))
			{
				IArmourModify armDecor = new ArmourModifier(armour);
				armDecor.DegradeArmourDurability(modifier);
				logger.WriteInfo($"Armour - {armour.Name}'s armour durability was improved by a factor of {modifier}");
			}
		}

		/// <summary>
		/// Method for adding a new observer to notify this Creature object.
		/// </summary>
		/// <param name="notifier"></param>
		public override void AddObserver(ICombatNotifier notifier)
		{
			CombatNotifications.Add(notifier);
			logger.WriteInfo($"New combat notifier added on {notifier.Creature}");
		}

		/// <summary>
		/// Metod for removing an observer in on this Creature object.
		/// </summary>
		/// <param name="notifier"></param>
		public override void RemoveObserver(ICombatNotifier notifier)
		{
			CombatNotifications.Remove(notifier);
			logger.WriteInfo($"Combat notifier removed from {notifier.Creature}");
		}

		/// <summary>
		/// Method for executing Damage Taken notification from all observers.
		/// </summary>
		/// <param name="damage"></param>
		/// <param name="absorbed"></param>
		public override void NotifyAllDamageTaken(int damage, int absorbed)
		{
			foreach (ICombatNotifier notifier in CombatNotifications)
			{
				notifier.NotifyDamageTaken(damage, absorbed);
			}
		}

		/// <summary>
		/// Method for executing Defeat notification from all observers.
		/// </summary>
		public override void NotifyAllDefeated()
		{
			foreach (ICombatNotifier notifier in CombatNotifications)
			{
				notifier.NotifyDefeated();
			}
		}
	}
}
