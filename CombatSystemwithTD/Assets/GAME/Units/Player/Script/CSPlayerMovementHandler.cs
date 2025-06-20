using CS;
using UnityEngine;

public class CSPlayerMovementHandler  
{
    private CharacterController _chaController;
    private float _movementSpeed;

    private Vector3 _currentMoveDirection;
    private Vector3 _destinationMoveDirection;
    private float _rotateSpeed;

    private ILockableTarget _lockableTarget;

    public CSPlayerMovementHandler(CharacterController chaController, float movementSpeed)
    {
        _chaController = chaController;
        _movementSpeed = movementSpeed;
        _rotateSpeed = 6;
    }
        
    public void SetMoveDirection(Vector2 moveDirection)
    {
        _destinationMoveDirection = new Vector3( moveDirection.x,0,moveDirection.y); 
    }

    public void Move( )
    {
        _currentMoveDirection = Vector3.Slerp(_currentMoveDirection, _destinationMoveDirection, Time.deltaTime * 9);
        _chaController.SimpleMove(_movementSpeed * _chaController.transform.TransformDirection(_currentMoveDirection));
    }

    public void RotatePlayer(float a_rotateAmount)
    { 
        _chaController.transform.localRotation = Quaternion.Euler(_chaController.transform.rotation.eulerAngles + Vector3.up * a_rotateAmount * Time.deltaTime * _rotateSpeed);
    }


    public void SetLockableTarget(ILockableTarget lockableTarget)
    {
        _lockableTarget = lockableTarget;
    }

    public void LockRotation()
    {
        Vector3 direction = (_lockableTarget.Position - _chaController.transform.position);
        direction.y = 0;
        _chaController.transform.rotation = Quaternion.Slerp( _chaController.transform.rotation , Quaternion.LookRotation(direction) ,Time.deltaTime*10);
    }

}
