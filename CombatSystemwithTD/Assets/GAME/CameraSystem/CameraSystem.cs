using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
namespace CS
{
    public class CameraSystem : MonoBehaviour
    {
        [SerializeField] private BattleSystem _battleSystem;
        [SerializeField] private CinemachineBrain _cinemachineBrain;
        [SerializeField] private CinemachineMixingCamera _playerCameraMixing;
        [SerializeField] private CinemachineBasicMultiChannelPerlin _lockOnCameraShaker;


        private int _freelookCameraIndex = 0;
        private int _lockOnCameraIndex = 1;
        private bool _cameraLockOn = false;


        private void Start()
        {
            _battleSystem.OnHit += OnHit;
        }


        private void OnHit(IWeapon weapon, IDamagable damagable, IWeaponUser user)
        {
            if (!_cameraLockOn) return;

            if (user is PlayerUnit)
            {
                ShakeCamera();
            }
        }


        private void ShakeCamera()
        {
            _lockOnCameraShaker.AmplitudeGain =-5;
            StartCoroutine(shakeCamera());
            IEnumerator shakeCamera()
            {
                float timeCounter = 0;
                while (timeCounter<1)
                {
                    timeCounter += Time.deltaTime * 5;                    
                    _lockOnCameraShaker.AmplitudeGain = -5*(1-timeCounter);
                    yield return null;

                }
                _lockOnCameraShaker.AmplitudeGain = 0;
            }
        }



        public void ActivateLockOnState()
        {
            _cameraLockOn = true;
            StopAllCoroutines();
            StartCoroutine(activateLockOnState());

            IEnumerator activateLockOnState()
            {

                float timeCounter = _playerCameraMixing.GetWeight(_lockOnCameraIndex);
                while (timeCounter < 1)
                {
                    timeCounter += Time.deltaTime;
                    _playerCameraMixing.SetWeight(_freelookCameraIndex, 1 - timeCounter);
                    _playerCameraMixing.SetWeight(_lockOnCameraIndex, timeCounter);
                    yield return null;
                }
            }
        }


        public void DeactivateLockOnState()
        {
            _cameraLockOn =false;
            StopAllCoroutines();
            StartCoroutine(deactivateLockOnState());
            IEnumerator deactivateLockOnState()
            {
                float timeCounter = _playerCameraMixing.GetWeight(_freelookCameraIndex);
                while (timeCounter<1 )
                {
                    timeCounter += Time.deltaTime;
                    _playerCameraMixing.SetWeight(_lockOnCameraIndex, 1 - timeCounter);
                    _playerCameraMixing.SetWeight(_freelookCameraIndex, timeCounter);
                    yield return null;
                }
            }
        }

    }
}