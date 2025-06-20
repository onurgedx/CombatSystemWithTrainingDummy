

using System;
using System.Collections.Generic;
using UnityEngine;

namespace CS
{

    public class CSSoundSystem : MonoBehaviour
    {

        [SerializeField] private BattleSystem _battleSystem;

        [SerializeField] private AudioSource[] _audioSources;

        [SerializeField] private AudioClip _deadAudioClip;
        [SerializeField] private AudioClip _dummyHitAudioClip;

        [SerializeField] private AudioClip[] _hitVersionsAudioClip;

        [SerializeField] private AudioClip _stepAudioClip;

        [SerializeField] private AudioClip[] _battleCriesAudioClip;
        

        private Dictionary<Type, AudioClip[]> _hitAudioClips = new Dictionary<Type, AudioClip[]>();

        private Dictionary<Type, AudioClip[]> _deadAudioClips = new Dictionary<Type, AudioClip[]>();

        private int _hitCounter=0;
        private int _attackCounter = 0;

        private void Start()
        {
            _battleSystem.OnDead += DeadSound;
            _battleSystem.OnHit += HitSound;            
        }


        public void PlayerBattleCry()
        {
            VaccantAudioSound().PlayOneShot(_battleCriesAudioClip[_attackCounter++ % _battleCriesAudioClip.Length]);
        }


        private void HitSound(IWeapon weapon,IDamagable damagable,IWeaponUser user)
        {
            if (_hitAudioClips.TryGetValue(weapon.GetType(), out AudioClip[] audioClips))
            {
                VaccantAudioSound().PlayOneShot(audioClips[_hitCounter++ % 2]);

            }
            else
            {
                VaccantAudioSound().PlayOneShot(_hitVersionsAudioClip[_hitCounter % 2]);
                _hitCounter++;
            }

            VaccantAudioSound().PlayOneShot(_dummyHitAudioClip);
        }


        private void DeadSound(IDeadable deadable)
        {
            if (_deadAudioClips.TryGetValue(deadable.GetType(), out AudioClip[] audioClips))
            {
                VaccantAudioSound().PlayOneShot(audioClips[0]);
            }
            else
            {
                VaccantAudioSound().PlayOneShot(_deadAudioClip);
            }
        }

        private AudioSource VaccantAudioSound()
        {
            foreach (var audioSource in _audioSources)
            {
                if(!audioSource.isPlaying)
                {
                    return audioSource;
                }
            }
            return _audioSources[0];
        }
    }
}