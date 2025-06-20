

using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CS
{
    public class CSPlayerBehaviour : MonoBehaviour
    {

        public event Action OnLockState = delegate { };
        public event Action OnWeaponActivityOn = delegate { };
        public event Action OnWeaponActivityOff = delegate { };
        public event Action OnFreeLookState = delegate { };

        private CSPlayerAnimationHandler _playerAnimationHandler;
        private CSPlayerMovementHandler _playerMovementHandler;


        [SerializeField] private Transform _cameraOriginTransform;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _movementSpeed;

        [SerializeField] private WeaponBehaviour _weaponBehavior;
        private Weapon _weapon;

        private float _currentCameraOriginRotateAspect = 0.5f;
        private float _destinationCameraOriginRotateAspect = 0.5f;
        public bool IsLockOn => _isLockOn;
        private bool _isLockOn = false;
        private bool _play = false;


        public void Init(Weapon weapon)
        {
            _weapon = weapon;
            _weaponBehavior.Init(_weapon);
            _playerMovementHandler = new CSPlayerMovementHandler(_characterController, _movementSpeed);
            _playerAnimationHandler = new CSPlayerAnimationHandler(_animator);
            _play = true;
        }


        private void Update()
        {
            if (!_play) return;
            _playerMovementHandler.Move();
            _playerAnimationHandler.Update();
            if (_isLockOn)
            {
                _playerMovementHandler.LockRotation();
            }
        }


        private void LateUpdate()
        {
            _currentCameraOriginRotateAspect = Mathf.Lerp(_currentCameraOriginRotateAspect, _destinationCameraOriginRotateAspect, Time.deltaTime * 8);
            _cameraOriginTransform.localRotation = Quaternion.Slerp(Quaternion.Euler(Vector3.right * -20), Quaternion.Euler(Vector3.right * 20), _currentCameraOriginRotateAspect);
        }

        public void Movement(Vector2 moveDirection)
        {
            _playerMovementHandler.SetMoveDirection(moveDirection);
            _playerAnimationHandler.SetDestinationMovement(moveDirection);
        }


        public void Attack()
        {
            if (_isLockOn)
            {
                _playerAnimationHandler.StartAttack();
            }
        }


        public void EndAttack()
        {
            if (_isLockOn)
            {
                _playerAnimationHandler.EndAttack();
            }
        }


        public void Rotate(Vector2 rotateValue)
        {
            if (!_isLockOn)
            {
                _playerMovementHandler.RotatePlayer(rotateValue.x);
            }
            _destinationCameraOriginRotateAspect -= rotateValue.y * Time.deltaTime;
            _destinationCameraOriginRotateAspect = Mathf.Clamp01(_destinationCameraOriginRotateAspect);
        }


        public void UpdateLockableTarget(ILockableTarget lockableTarget)
        {
            _playerMovementHandler.SetLockableTarget(lockableTarget);
        }


        public void ChangeLockState()
        {
            _isLockOn = !_isLockOn;
            if (_isLockOn)
            {
                OnLockState.Invoke();
                _playerAnimationHandler.ActivateLockOnState();
            }
            else
            {
                OnFreeLookState.Invoke();
                _playerAnimationHandler.DeactivateLockOnState();
            }
        }


        public void ActivateWeaponDamage()
        {
            OnWeaponActivityOn.Invoke();
        }


        public void DeactivateWeaponDamage()
        {
            OnWeaponActivityOff.Invoke();
        }
    }
}