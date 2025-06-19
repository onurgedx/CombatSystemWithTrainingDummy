

using System;
using System.Collections.Generic;
using UnityEngine;

namespace CS
{

    public class CSSoundSystem : MonoBehaviour
    {

        [SerializeField] private BattleSystem _battleSystem;

        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip _deadAudioClip;

        [SerializeField] private AudioClip[] _hitVersionsAudioClip;

        [SerializeField] private AudioClip _stepAudioClip;



        private Dictionary<Type, AudioClip[]> _hitAudioClips = new Dictionary<Type, AudioClip[]>();

        private Dictionary<Type, AudioClip[]> _deadAudioClips = new Dictionary<Type, AudioClip[]>();

        private int _hitCounter=0;


        private void Start()
        {
            _battleSystem.OnDead += DeadSound;
            _battleSystem.OnHit += HitSound;
        }


        private void HitSound(IWeapon weapon,IDamagable damagable)
        {
            if (_hitAudioClips.TryGetValue(weapon.GetType(), out AudioClip[] audioClips))
            {
                _audioSource.PlayOneShot(audioClips[_hitCounter++ % 2]);

            }
            else
            {
                _audioSource.PlayOneShot(_hitVersionsAudioClip[_hitCounter % 2]);
                _hitCounter++;
            }
        }


        private void DeadSound(IDeadable deadable)
        {
            if (_deadAudioClips.TryGetValue(deadable.GetType(), out AudioClip[] audioClips))
            {
                _audioSource.PlayOneShot(audioClips[0]);
            }
            else
            {
                _audioSource.PlayOneShot(_deadAudioClip);
            }
        }
    }
}