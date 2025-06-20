using System;
using System.Collections.Generic;
using UnityEngine;
namespace CS
{
    public class BattleSystem : MonoBehaviour 
    {
        private Dictionary<Collider, IDamagable> _damageblesDictionary;
        private DamageCalculator _damageCalculator;

        public Action<IDeadable> OnDead = delegate { };
        public Action<IWeapon,IDamagable, IWeaponUser> OnHit = delegate { };


        private void Start()
        {
            _damageCalculator = new DamageCalculator();
            _damageblesDictionary = new Dictionary<Collider, IDamagable>();
        }


        public void AttemptDamage(Collider damagableCollider,IWeaponUser weaponUser, IWeapon weapon)
        {
            if(_damageblesDictionary.TryGetValue(damagableCollider, out var damageble))
            {
                float damage = _damageCalculator.CalculateDamage(weapon,weaponUser);
                damageble.GetDamaged(damage);
                OnHit.Invoke(weapon,damageble, weaponUser);
                if(damageble.Health<=0&& damageble is IDeadable deadable)
                {
                    OnDead.Invoke(deadable);
                    deadable.Die();
                }
            }
        }
        

        public void RecordDamageable(Collider collider, IDamagable damageable)
        {
            _damageblesDictionary.Add(collider, damageable);
        }
    }
}