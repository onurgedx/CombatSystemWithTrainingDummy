
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace CS
{
    public class Dummy : IDummy, IUnit, IDamagable , IDeadable, ILockableTarget
    {
        public float Health => _health;
        private float _health =100;
        private float _maxHealth = 100;
        public float ReviveDuration { get; private set; }  

        public event Action Damaged = delegate { };
        public event Action Dead = delegate { };
        public event Action Revived = delegate { };

        public bool IsDead { get; private set; }

        public Vector3 Position { get; private set; }

        public Dummy(float health ,float reviveDuration,Vector3 position)
        {
            Position = position;
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
            IsDead = true;
            Dead.Invoke();
        }


        public void Revive()
        {   
            IsDead = false;
            _health = _maxHealth;
            Revived.Invoke();
        }
    }
}
