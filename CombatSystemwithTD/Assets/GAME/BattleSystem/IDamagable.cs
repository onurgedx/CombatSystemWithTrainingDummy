using UnityEngine;
namespace CS
{
    public interface IDamagable
    {
        public Vector3 Position { get; }
        public void GetDamaged(float damageAmount);
        public float Health { get; }
    }
}