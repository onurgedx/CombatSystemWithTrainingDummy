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

        [SerializeField] private HealthBarFrame _healthBarFrame;
        [SerializeField] private Collider _collider;
        [SerializeField] private Animator _animator; 
        [SerializeField] private float _dummyHealth;
        [SerializeField] private float _dummyReviveDuration;
        
        private void Start()
        {
            _dummy = new Dummy(_dummyHealth, _dummyReviveDuration,transform.position+Vector3.up);

            _dummy.Damaged += GetDamaged;
            _dummy.Dead += Die;
            _dummy.Revived += Revive;

            _healthBarFrame.Configure(_dummyHealth);
            BattleSystem.RecordDamageable(_collider, _dummy);
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