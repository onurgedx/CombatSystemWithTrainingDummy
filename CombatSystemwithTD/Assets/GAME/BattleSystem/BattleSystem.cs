using System.Collections.Generic;
using UnityEngine;
namespace CS
{
    public class BattleSystem : MonoBehaviour , IDamager
    {
        private static  Dictionary<Collider, IDamagable> _damageblesDictionary;
        private DamageCalculator _damageCalculator;


        private void Start()
        {
            _damageCalculator = new DamageCalculator();
            _damageblesDictionary = new Dictionary<Collider, IDamagable>();
        }


        public void AttemptDamage(Collider damagableCollider,IWeaponUser weaponUser, IWeapon weapon )
        {            
            if(_damageblesDictionary.TryGetValue(damagableCollider, out var damageble))
            {
                float damage = _damageCalculator.CalculateDamage(weapon,weaponUser);
                damageble.GetDamaged(damage);
                if(damageble.Health<=0&& damageble is IDeadable deadable)
                {
                    deadable.Die();                    
                }                       
            }
        }
        

        public static void RecordDamageable(Collider collider, IDamagable damageable)
        {
            _damageblesDictionary.Add(collider, damageable);
        }
    }
}