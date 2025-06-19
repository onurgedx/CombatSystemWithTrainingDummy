using System;
using UnityEngine;

namespace CS
{

    public class Weapon : IWeapon
    {
        public virtual float Power => 1;
                

        public event Action DamageActivated =  delegate { };
        public event Action DamageDeactivated =  delegate { };

        public event Action<Collider> OnHit = delegate { };

        public void Hit(Collider collider)
        {
            OnHit(collider);
        }


        public void ActivateDamage()
        {
            DamageActivated.Invoke();
        }


        public void DeactivateDamage()
        {
            DamageDeactivated.Invoke();
        }
    }

}