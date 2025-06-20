using System;
using UnityEngine;
using UnityEngine.InputSystem;
namespace CS
{
    public class CSPlayerInputInterpreter : CSPlayerInputActions.IPlayerActions
    {
        public Action<Vector2> OnMoved = delegate { };
        public Action<Vector2> OnLookRequest = delegate { };
        public Action OnLockStateChangeRequest = delegate { };
        public Action AttackRequested = delegate { }; 

        private CSPlayerInputActions _inputActions;
        private CSPlayerInputActions.PlayerActions _playerActions;


        public CSPlayerInputInterpreter()
        {
            _inputActions = new CSPlayerInputActions();
            _playerActions = _inputActions.Player;
            _playerActions.AddCallbacks(this);
            _playerActions.Enable();
        }


        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                AttackRequested.Invoke();
            }
        }


        public void OnLockOnOffTarget(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                OnLockStateChangeRequest.Invoke();
            }
        }


        public void OnLook(InputAction.CallbackContext context)
        {
            Vector2 lookInput = context.ReadValue<Vector2>();
            OnLookRequest.Invoke(lookInput);
        }


        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            OnMoved.Invoke(moveInput);

        }
    }
}