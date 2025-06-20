

using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CS
{
    public class CSPlayerController : MonoBehaviour 
    {
         
        public event Action OnLockState = delegate {}; 
        public event Action OnWeaponActivityOn = delegate { };
        public event Action OnWeaponActivityOff = delegate { };
        public event Action OnFreeLookState = delegate {};
         

        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _movementSpeed; 

        private CSPlayerAnimationHandler _playerAnimationHandler;
        private CSPlayerMovementHandler _playerMovementHandler;

        [SerializeField] private Transform _lockOnTransform;
        [SerializeField] private WeaponBehaviour _weaponBehavior;
        private Weapon _weapon;
        private bool _isLockOn = false;
        private bool _play = false;
                 

        public void Init( Weapon weapon)
        {
            _weapon = weapon;
            _weaponBehavior.Init(_weapon);
            _playerMovementHandler = new CSPlayerMovementHandler(_characterController, _movementSpeed);
            _playerAnimationHandler = new CSPlayerAnimationHandler(_animator);
            _play=true;
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
                 

        public void Movement(Vector2 moveDirection)
        {
            _playerMovementHandler.SetMoveDirection(moveDirection);
            _playerAnimationHandler.SetDestinationMovement(moveDirection);
        }


        public void Attack()
        {
            if (_isLockOn)
            {   
                _playerAnimationHandler.Attack();
            }
        }


        public void RotatePlayer(Vector2 rotateValue)
        {
            _playerMovementHandler.RotatePlayer(rotateValue.x);
        }


        public void ChangeLockState()
        {
            _isLockOn = !_isLockOn;
            if (_isLockOn)
            {
                OnLockState.Invoke(); 
                _playerMovementHandler.SetLockTransform(_lockOnTransform); 
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