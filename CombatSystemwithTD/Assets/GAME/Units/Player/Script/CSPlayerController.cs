

using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CS
{
    public class CSPlayerController : MonoBehaviour
    {
        [SerializeField] private BattleSystem _battleSystem;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private CinemachineMixingCamera _cameraMixing;

        private CSPlayerAnimationHandler _playerAnimationHandler;
        private CSPlayerMovementHandler _playerMovementHandler;
        private CSPlayerInputInterpreter _inputInterpreter;
        private CSPlayerCameraHandler _cameraHandler;

        [SerializeField] private Transform _lockOnTransform;
        private Weapon _weapon;
        [SerializeField] private WeaponBehaviour _weaponBehavior;
        private bool _isLockOn = false;
        private PlayerUnit _playerUnit;

        void Start()
        {
            _playerUnit = new PlayerUnit();
            _weapon = new GreatSword();
            _weaponBehavior.Init(_weapon);
            _weapon.OnHit += Weapon_HitACollider;
            _playerMovementHandler = new CSPlayerMovementHandler(_characterController, _movementSpeed);
            _playerAnimationHandler = new CSPlayerAnimationHandler(_animator);
            _inputInterpreter = new CSPlayerInputInterpreter();
            _cameraHandler = new CSPlayerCameraHandler(_cameraMixing, this);
            Configurations();

        }


        private void Weapon_HitACollider(Collider collider)
        {
            _battleSystem.AttemptDamage(collider, _playerUnit, _weapon );
        }


        private void Update()
        {
            _playerMovementHandler.Move();
            _playerAnimationHandler.Update();
            if (_isLockOn)
            {
                _playerMovementHandler.LockRotation();
            }
        }


        private void Configurations()
        {
            _inputInterpreter.OnMoved += Movement;
            _inputInterpreter.AttackRequested += Attack;
            _inputInterpreter.OnLockStateChangeRequest += ChangeLockState;
            _inputInterpreter.OnLookRequest += RotatePlayer;
        }


        private void Movement(Vector2 moveDirection)
        {
            _playerMovementHandler.SetMoveDirection(moveDirection);
            _playerAnimationHandler.SetDestinationMovement(moveDirection);
        }


        private void Attack()
        {
            if (_isLockOn)
            {
                _playerAnimationHandler.Attack();
            }
        }


        private void RotatePlayer(Vector2 rotateValue)
        {
            _playerMovementHandler.RotatePlayer(rotateValue.x);
        }


        private void ChangeLockState()
        {
            _isLockOn = !_isLockOn;
            if (_isLockOn)
            {
                _playerMovementHandler.SetLockTransform(_lockOnTransform);
                _playerAnimationHandler.ActivateLockOnState();
                _cameraHandler.ActivateLockState();
            }
            else
            {
                _cameraHandler.DeactivateLockState();
                _playerAnimationHandler.DeactivateLockOnState();
            }
        }


        public void ActivateWeaponDamage()
        { 
            _weapon.ActivateDamage();
        }


        public void DeactivateWeaponDamage()
        { 
            _weapon.DeactivateDamage();
        }
    }
}