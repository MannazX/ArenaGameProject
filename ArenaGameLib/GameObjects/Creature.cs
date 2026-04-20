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
		/// <param name="name">Type: string - Creature's name</param>
		/// <param name="health">Type: int - Creature's hitpoints</param>
		/// <param name="unarmedDmg">Type: int - Amount of unarmed damage the creature can deal</param>
		/// <param name="inventoryCapacity">Type: int - Creature's inventory capacity</param>
		/// <param name="weaponCapacity">Type: int - Creature's weapon collection capacity</param>
		/// <param name="armourCapacity">Type: int - Creature's armour collection capacity</param>
		/// <param name="locX">Type: int - Positional X coordinate</param>
		/// <param name="locY">Type: int - Positional Y coordinate</param>
		public Creature(string name, int health, int unarmedDmg, int inventoryCapacity, int weaponCapacity, int armourCapacity, int locX, int locY) : base(name, health, unarmedDmg, inventoryCapacity, weaponCapacity, armourCapacity, locX, locY)
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
		/// <param name="targetDist">Type: int - Attack Range to the target</param>
		/// <param name="attack">Type: IAttackStrategy - Strategy class object for computing damage output</param>
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
		/// <param name="damage">Type: int - Incoming damage from another creature</param>
		/// <param name="reducedDamage">Type: IAbsorbDamageStrategy - Strategy class object for computing damage output</param>
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
		/// Method for the creatures action of looting an item and adding it to Armour Collection or Weapon Collection and the Inventory making it unclaimable. The Method is predicated on the creature being in reaching distance (1 Square) of item and on the item itself being lootable.
		/// </summary>
		/// <param name="item">Type: ArenaObject - The item looted, if Weapon - Add to WeaponCollection, if Armour - Add to ArmourCollection.</param>
		public override void Loot(ArenaObject item)
		{
			if ((LocationX - item.LocationX <= 1 || LocationY - item.LocationY <= 1) && item.Lootable)
			{
				try
				{
					if (Inventory.TotalWeight() + item.Weight < Inventory.Capacity)
					{
						if (item.GetType() == typeof(Weapon))
						{
							Weapon weapon = (Weapon)item;
							WeaponCollection.Weapons.Add(weapon);
							weapon.SetClaim();
							weapon.SetLocation(LocationX, LocationY);
						}
						else if (item.GetType() == typeof(Armour))
						{
							Armour armour = (Armour)item;
							ArmourCollection.ArmourSet.Add((armour));
							armour.SetClaim();
							armour.SetLocation(LocationX, LocationY);
						}
						logger.WriteInfo($"Creature {Name} looted item {item}");
						Inventory.Items.Add(item);
					}
				}
				catch (ArgumentOutOfRangeException ex)
				{
					logger.LogError(ex.Message);
				}
			}
		}

		/// <summary>
		/// Method for creatures action of dropping an item and removing it from Weapon Collection or Armour Collection and the Inventory making it claimable. 
		/// </summary>
		/// <param name="item">Type: ArenaObject - The item in the creature's inventory to drop</param>
		public override void Drop(ArenaObject item)
		{
			try
			{
				if (item.GetType() == typeof(Weapon))
				{
					Weapon weapon = (Weapon)item;
					WeaponCollection.Weapons.Remove((Weapon)item);
					weapon.SetClaim();
					weapon.SetLocation(LocationX, LocationY);
				}
				else if (item.GetType() == typeof(Armour))
				{
					Armour armour = (Armour)item;
					ArmourCollection.ArmourSet.Remove((Armour)item);
					armour.SetClaim();
					armour.SetLocation(LocationX, LocationY);
				}
				logger.WriteInfo($"Creature {Name} dropped item {item}");
				Inventory.Remove(item);
			}
			catch (ArgumentException ex)
			{
				logger.LogError(ex.Message);
			}
		}

		/// <summary>
		/// Method for creature's action to improve damage of one of it's weapons - using instance of IWeaponModify decorator class.
		/// </summary>
		/// <param name="weapon">Type: IWeapon - Weapon that creature improves</param>
		/// <param name="modifier">Type: int - Factor that the creature's weapon is improved by</param>
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
		/// Method for creature to have one of its weapon's damage degraded - using instance of IWeaponModify decorator class.
		/// </summary>
		/// <param name="weapon">Type: IWeapon - Weapon in the creature's inventory to be degraded</param>
		/// <param name="modifier">Type: int - Factor that the creature's weapon is degraded by</param>
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
		/// Method for creature to have one of its armour piece's durability improved - using instance of IArmourModify decorator class.
		/// </summary>
		/// <param name="armour">Type: IArmour - Armour in the creature's inventory to be improved</param>
		/// <param name="modifier">Type: int - Factor that the creature's armour durability is improved by</param>
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
		/// Method for creature to have one of its armour piece's durability degraded by - using instance of IArmourModify decorator class.
		/// </summary>
		/// <param name="armour">Type: IArmour - Armour in the creature's inventory to be degraded</param>
		/// <param name="modifier">Type: int - Factor that the creature's armour durability is degraded by</param>
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
		/// Method for adding a new observer class object to notify this creature.
		/// </summary>
		/// <param name="notifier">Type: ICombatNotifier - Combat notifier observer class object</param>
		public override void AddObserver(ICombatNotifier notifier)
		{
			CombatNotifications.Add(notifier);
			logger.WriteInfo($"New combat notifier added on {notifier.Creature}");
		}

		/// <summary>
		/// Metod for removing an observer class object on this creature.
		/// </summary>
		/// <param name="notifier">Type: ICombatNotifier - Combat notifier observer class object</param>
		public override void RemoveObserver(ICombatNotifier notifier)
		{
			CombatNotifications.Remove(notifier);
			logger.WriteInfo($"Combat notifier removed from {notifier.Creature}");
		}

		/// <summary>
		/// Method for executing Damage Taken notification from all observers.
		/// </summary>
		/// <param name="damage">Type: int - Damage that the notifiers notify</param>
		/// <param name="absorbed">Type: int - Damage absorbed that the notifiers notify</param>
		public override void NotifyAllDamageTaken(int damage, int absorbed)
		{
			foreach (ICombatNotifier notifier in CombatNotifications)
			{
				notifier.NotifyDamageTaken(damage, absorbed);
			}
		}

		/// <summary>
		/// Method for executing Defeat notification from all notifiers.
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
