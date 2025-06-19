
using System;

namespace CS
{
    public class Dummy : IDummy, IUnit, IDamagable , IDeadable
    {
        public float Health => _health;
        private float _health =100;
        private float _maxHealth = 100;
        public float ReviveDuration { get; private set; }  

        public event Action Damaged = delegate { };
        public event Action Dead = delegate { };
        public event Action Revived = delegate { };

        public Dummy(float health ,float reviveDuration)
        {
            _maxHealth = health;
            _health = health;
            ReviveDuration = reviveDuration;
        }

        public void GetDamaged(float damageAmount )
        {
            _health -= damageAmount;
            Damaged.Invoke();
        }


        public void Die()
        {
            Dead.Invoke();
        }


        public void Revive()
        {   
            _health = _maxHealth;
            Revived.Invoke();
        }
    }
}
