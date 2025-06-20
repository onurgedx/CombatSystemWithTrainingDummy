using System.Collections;
using UnityEngine;
namespace CS
{ 
    public class DummyBehaviour : MonoBehaviour   
    {

        private static readonly int _hitAnimationHash = Animator.StringToHash("Hit");
        private static readonly int _deadAnimationHash = Animator.StringToHash("Dead");

        public Collider Collider => _collider;
        [SerializeField] private HealthBarFrame _healthBarFrame;
        [SerializeField] private Collider _collider;
        [SerializeField] private Animator _animator;  
        [SerializeField] private float _dummyReviveDuration;
         
        private Dummy _dummy;

        public void Init(Dummy dummy)
        {
            if(_dummy != null)
            {
                _dummy.Damaged -= GetDamaged;
                dummy.Dead-= Die;
                _dummy.Revived -= Revive;
            }
            _dummy = dummy;
            _dummy.Damaged += GetDamaged;
            _dummy.Dead += Die;
            _dummy.Revived += Revive;
            _healthBarFrame.Configure(_dummy.Health);
        }


        private void GetDamaged()
        {
            _animator.SetTrigger(_hitAnimationHash);
            _healthBarFrame.UpdateHealthBar(_dummy.Health);
        }         


        public void Die()
        {
            _healthBarFrame.Hide();
            _animator.SetBool(_deadAnimationHash, true); 
        }

        public void Revive()
        {
            _healthBarFrame.Show(); 
            _animator.SetBool(_deadAnimationHash, false);
            _healthBarFrame.UpdateHealthBar(_dummy.Health);
        }
    }
}