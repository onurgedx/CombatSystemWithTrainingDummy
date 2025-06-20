using System;
using System.Collections;
using UnityEngine;
namespace CS
{
    public class CSPlayerAnimationHandler
    {

        private readonly static int _movementXHash = Animator.StringToHash("MovementX");
        private readonly static int _movementZHash = Animator.StringToHash("MovementZ");
        private readonly static int _lockOn = Animator.StringToHash("LockOn");
        private readonly static int _attack = Animator.StringToHash("Attack");
         
        private Animator _animator;

        private Vector2 _destinationMoveDirection;
        private Vector2 _currentnMoveDirection;

        private Action Updates = delegate { };

        public CSPlayerAnimationHandler(Animator animator)
        {
            _animator = animator;
        }

        public void Update()
        {
            Updates.Invoke();
            UpdateMovementsOverTime();
        }
        

        public void SetDestinationMovement(Vector2 moveDirection)
        {
            _destinationMoveDirection = moveDirection;
        }

        private void UpdateMovementsOverTime()
        {
            _currentnMoveDirection = Vector2.Lerp(_currentnMoveDirection, _destinationMoveDirection, Time.deltaTime*9);
            _animator.SetFloat(_movementXHash, _currentnMoveDirection.x);
            _animator.SetFloat(_movementZHash, _currentnMoveDirection.y);
        }

 
        private void SetLayerWeightOverTime(int layerIndex, float destinationWeight)
        {
           
            float timeCounter = 0;
            float startWeight = 1 - destinationWeight;

            void changeWeightOverTime()
            {
                timeCounter += Time.deltaTime;
                if (timeCounter < 1) {
                    _animator.SetLayerWeight(layerIndex, Mathf.Lerp(startWeight,destinationWeight,timeCounter));
                }
                else
                {
                    Updates -= changeWeightOverTime;
                }
            } 
            Updates += changeWeightOverTime;

        }
      
        public void ActivateLockOnState()
        {
            SetLayerWeightOverTime(1, 1);
            _animator.SetBool(_lockOn, true); 
        }

        public void DeactivateLockOnState()
        {
            SetLayerWeightOverTime(1, 0);
            _animator.SetBool(_lockOn, false); 
        }

        public void StartAttack()
        {
            _animator.SetBool(_attack ,true); 
        }

        public void EndAttack()
        {
            _animator.SetBool(_attack , false);    
        }


    }
}