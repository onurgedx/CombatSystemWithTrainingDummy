using UnityEngine;
namespace CS
{
    public interface IWeapon
    {
        public float Power { get; }


        public void ActivateDamage();
        public void DeactivateDamage();


    }
}