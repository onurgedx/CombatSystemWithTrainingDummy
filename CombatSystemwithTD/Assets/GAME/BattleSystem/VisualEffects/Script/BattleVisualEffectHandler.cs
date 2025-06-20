using System;
using UnityEngine;
namespace CS
{
    public class BattleVisualEffectHandler : MonoBehaviour
    {

        [SerializeField] private BattleSystem _battleSystem;

        [SerializeField] private ParticleSystem _hitBloodSplashVfx;



        private void Start()
        {
            _battleSystem.OnHit += OnHit;
             
        }

        private void OnHit(IWeapon weapon, IDamagable damagable,IWeaponUser user)
        {
            Instantiate(_hitBloodSplashVfx.gameObject, damagable.Position,Quaternion.identity);
        }

    }
}