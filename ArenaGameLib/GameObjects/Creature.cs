using ArenaGameLib.GameInterfaces;
using ArenaGameLib.GameObjects.AbstractClasses;
using ArenaGameLib.GameObjects.Composite;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects
{
	public class Creature : CreatureTemplate, ICreatureTemplate
	{
		
		public WeaponCollection WeaponCollection { get; set; }
		public ArmourCollection ArmourCollection { get; set; }
		

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

		public override int Attack(int targetDist)
		{
			int totalDmg = 0;
			if (WeaponCollection.Weapons.Count() > 0)
			{
				foreach (Weapon wep in WeaponCollection.Weapons)
				{
					if (wep.Range >= targetDist)
					{
						totalDmg += wep.Damage;
					}
				}
			}
			else
			{
				totalDmg = base.Attack(targetDist);
			}
			return totalDmg;
		}

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

		public override void TakeDamage(int damage)
		{
			if (ArmourCollection.ArmourSet.Count() > 0)
			{
				int absorbed = 0;
				foreach (Armour armour in ArmourCollection.ArmourSet)
				{
					if (armour.ArmourDurability > 0)
					{
						absorbed += armour.ReduceDamage;
						armour.ArmourDurability -= 1;
					}
				}
				Health -= damage - absorbed;
			}
			else
			{
				base.TakeDamage(damage);
			}
		}
	}
}
