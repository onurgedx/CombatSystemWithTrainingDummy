using System.Collections;
using UnityEngine;
namespace CS
{
    [DefaultExecutionOrder(19)]
    public class DummyBehaviour : MonoBehaviour   
    {

        private Dummy _dummy;


        private static readonly int _hitAnimationHash = Animator.StringToHash("Hit");
        private static readonly int _deadAnimationHash = Animator.StringToHash("Dead");

        public Collider Collider => _collider;

        [SerializeField] private HealthBarFrame _healthBarFrame;
        [SerializeField] private Collider _collider;
        [SerializeField] private Animator _animator;  
        [SerializeField] private float _dummyReviveDuration;
         
        public void Init(Dummy dummy)
        {
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
            StartCoroutine(reviceIEnumerator());
            IEnumerator reviceIEnumerator()
            {
                yield return new WaitForSeconds(_dummy.ReviveDuration);
                _dummy.Revive();
            }
        }


        public void Revive()
        {
            _healthBarFrame.Show(); 
            _animator.SetBool(_deadAnimationHash, false);
            _healthBarFrame.UpdateHealthBar(_dummy.Health);
        }
    }
}