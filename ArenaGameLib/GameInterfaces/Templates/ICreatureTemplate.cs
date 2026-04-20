using ArenaGameLib.GameInterfaces.Decorators;
using ArenaGameLib.GameInterfaces.Observers;
using ArenaGameLib.GameInterfaces.Strategies;
using ArenaGameLib.GameObjects.AbstractClasses;
using ArenaGameLib.GameObjects.Composites;
using ArenaGameLib.GameObjects.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameInterfaces.Templates
{
	public interface ICreatureTemplate
	{
		string Name { get; set; }
		int Health { get; set; }
		int LocationX { get; set; }
		int LocationY { get; set; }
		int Attack(int targetDist, IAttackStrategy attack);
		void TakeDamage(int damage, IAbsorbDamageStrategy reducedDamage);
		void Loot(ArenaObject item);
		void Drop(ArenaObject item);
		void WeaponImprove(IWeapon weapon, int modifier);
		void WeaponDegrade(IWeapon weapon, int modifier);
		void ArmourImprove(IArmour armour, int modifier);
		void ArmourDegrade(IArmour armour, int modifier);
		void AddObserver(ICombatNotifier notifier);
		void RemoveObserver(ICombatNotifier notifier);
		void NotifyAllDamageTaken(int damage, int absorbed);
		void NotifyAllDefeated();
	}
}
