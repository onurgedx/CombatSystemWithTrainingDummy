

using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CS
{
    public class CSPlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private CinemachineMixingCamera _cameraMixing;

        private CSPlayerAnimationHandler _playerAnimationHandler;
        private CSPlayerMovementHandler _playerMovementHandler;
        private CSPlayerInputInterpreter _inputInterpreter;
        private CSPlayerCameraHandler _cameraHandler;
          
        

        [SerializeField] private Transform _lockOnTransform;

        //private CSCamera

        private bool _isLockOn = false;

        void Start()
        {
            
            _playerMovementHandler = new CSPlayerMovementHandler(_characterController, _movementSpeed);
            _playerAnimationHandler = new CSPlayerAnimationHandler(_animator);
            _inputInterpreter = new CSPlayerInputInterpreter();
            _cameraHandler = new CSPlayerCameraHandler(_cameraMixing,this);
            Configurations();  
            
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
            _inputInterpreter.OnMoved += _playerMovementHandler.SetMoveDirection;
            _inputInterpreter.OnMoved += _playerAnimationHandler.SetDestinationMovement;
            _inputInterpreter.AttackRequested += _playerAnimationHandler.Attack;
            _inputInterpreter.OnLockStateChangeRequest += ChangeLockState;
            _inputInterpreter.OnLookRequest += RotatePlayer;
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





    }
}