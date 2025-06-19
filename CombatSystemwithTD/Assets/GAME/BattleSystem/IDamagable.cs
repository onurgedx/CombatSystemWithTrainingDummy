using UnityEngine;
namespace CS
{
    public interface IDamagable
    { 
        public void GetDamaged(float damageAmount);
        public float Health { get; }
    }
}