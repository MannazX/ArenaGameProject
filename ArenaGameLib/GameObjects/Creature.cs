using ArenaGameLib.GameInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameObjects
{
	public class Creature : ICreature
	{
		public string Name { get; set; }
		public int Health { get; set; }
		public int UnarmedDamage { get; set; }
		public List<IArenaObject> Inventory { get; set; }
		public int InventoryCapacity { get; set; }
		public List<Weapon> Weapons { get; set; }
		public List<Armour> ArmourPieces { get; set; }
		public int LocationX { get; set; }
		public int LocationY { get; set; }

		public Creature(string name, int health, int UnarmedDmg, List<IArenaObject> inventory, int inventoryCapacity, List<Weapon> weapons, List<Armour> armour, int locX, int locY)
		{
			Name = name;
			Health = health;
			UnarmedDamage = UnarmedDmg;
			Inventory = inventory;
			InventoryCapacity = inventoryCapacity;
			Weapons = weapons;
			ArmourPieces = armour;
			LocationX = locX;
			LocationY = locY;
		}

		public int Attack(int targetDist)
		{
			int totalDmg = 0;
			if (Weapons.Count() > 0)
			{
				foreach (Weapon wep in Weapons)
				{
					if (wep.Range >= targetDist)
					{
						totalDmg += wep.Damage;
					}
				}
			}
			else
			{
				totalDmg = UnarmedDamage;
			}
			return totalDmg;
		}

		public void Loot(ArenaObject item)
		{
			if ((LocationX - item.LocationX <= 1 || LocationY - item.LocationY <= 1) && Inventory.Sum(x => x.Weight) + item.Weight < InventoryCapacity)
			{
				if (item.GetType() == typeof(Weapon))
				{
					Weapons.Add((Weapon)item);
				}
				else if (item.GetType() == typeof(Armour))
				{
					ArmourPieces.Add((Armour)item);
				}
				else
				{
					Inventory.Add(item);
				}
			}
		}

		public void TakeDamage(int damage)
		{
			int absorbed = 0;
			if (ArmourPieces.Count() > 0)
			{
				foreach (Armour armour in ArmourPieces)
				{
					if (armour.ArmourDurability > 0)
					{
						absorbed += armour.ReduceDamage;
						armour.ArmourDurability -= 1;
					}
				}
			}
			Health -= damage - absorbed;
		}
	}
}
