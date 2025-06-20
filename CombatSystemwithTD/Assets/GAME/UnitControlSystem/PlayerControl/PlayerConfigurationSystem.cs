using System.Collections.Generic;
using UnityEngine;
namespace CS
{
    public class PlayerConfigurationSystem
    {
        private ClosestLockableTargetCalculator _closestLockableTargetCalculator;
        private BattleSystem _battleSystem;
        private CameraSystem _cameraSystem;
        private CSSoundSystem _soundSystem;
        private GameplayUISystem _gameplayUISystem;


        private List<ILockableTarget> _lockableTargetList ;

        public PlayerConfigurationSystem(BattleSystem battleSystem,
                                   CameraSystem cameraSystem,
                                   CSSoundSystem soundSystem,
                                   GameplayUISystem gameplayUISystem)
        {

            _lockableTargetList = new List<ILockableTarget>();
            _closestLockableTargetCalculator =new ClosestLockableTargetCalculator(_lockableTargetList);

            _battleSystem = battleSystem;
            _cameraSystem = cameraSystem;
            _soundSystem = soundSystem;
            _gameplayUISystem = gameplayUISystem;
        }


        public void Configure(CSPlayerBehaviour csPlayerController)
        {
            PlayerUnit playerUnit = new PlayerUnit();
            Weapon weapon = new GreatSword();
            csPlayerController.Init(weapon);
            weapon.OnHit += (Collider collider) => { _battleSystem.AttemptDamage(collider, playerUnit, weapon); };
            csPlayerController.OnLockState += () => { csPlayerController.UpdateLockableTarget(_closestLockableTargetCalculator.Calculate(csPlayerController.transform.position)); };
            csPlayerController.OnLockState += _cameraSystem.ActivateLockOnState;
            csPlayerController.OnLockState += _gameplayUISystem.ActivateLockOn;
            csPlayerController.OnFreeLookState += _gameplayUISystem.DeactivateLockOn;
            csPlayerController.OnFreeLookState += _cameraSystem.DeactivateLockOnState;
            csPlayerController.OnWeaponActivityOn += _soundSystem.PlayerBattleCry;
            csPlayerController.OnWeaponActivityOn += weapon.ActivateDamage;
            csPlayerController.OnWeaponActivityOff += weapon.DeactivateDamage;
        }


        public void StartControllingPlayer(CSPlayerBehaviour csPlayerController, CSPlayerInputInterpreter inputInterpreter)
        {
            inputInterpreter.OnMoved += csPlayerController.Movement;
            inputInterpreter.AttackRequested += csPlayerController.Attack;
            inputInterpreter.OnLockStateChangeRequest += csPlayerController.ChangeLockState;
            inputInterpreter.OnLookRequest += csPlayerController.RotatePlayer;
        }
         

        public void RecordLockableTarget(ILockableTarget lockableTarget)
        {
            _lockableTargetList.Add(lockableTarget);
        }


        public void RemoveLockableTarget(ILockableTarget lockableTarget)
        {
            _lockableTargetList.Remove(lockableTarget);
        }
    }
}