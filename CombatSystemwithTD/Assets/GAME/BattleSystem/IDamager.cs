using UnityEngine;
namespace CS
{
    public interface IDamager
    {

        public void AttemptDamage(Collider damagableCollider, IWeaponUser weaponUser, IWeapon weapon);


    }
}