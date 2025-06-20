using UnityEngine;
namespace CS
{
    public class UnitControlSystem : MonoBehaviour
    {

        [SerializeField] private BattleSystem _battleSystem;
        [SerializeField] private CameraSystem _cameraSystem;
        [SerializeField] private CSSoundSystem _soundSystem;
        [SerializeField] private GameplayUISystem _gameplayUISystem;

        [SerializeField] private CSPlayerController _csPlayerController;
        private CSPlayerInputInterpreter _inputInterpreter;


        private void Start()
        {
            _inputInterpreter = new CSPlayerInputInterpreter();
            ConfigurePlayer(_csPlayerController);
            StartControllingPlayer(_csPlayerController);
        }

        
        private void ConfigurePlayer(CSPlayerController csPlayerController)
        {
            PlayerUnit playerUnit = new PlayerUnit();
            Weapon weapon = new GreatSword();
            csPlayerController.Init(weapon);
            weapon.OnHit += (Collider collider) => { _battleSystem.AttemptDamage(collider, playerUnit, weapon); }; 
            csPlayerController.OnLockState += _cameraSystem.ActivateLockOnState;
            csPlayerController.OnLockState += _gameplayUISystem.ActivateLockOn;
            csPlayerController.OnFreeLookState += _gameplayUISystem.DeactivateLockOn;
            csPlayerController.OnFreeLookState += _cameraSystem.DeactivateLockOnState;
            csPlayerController.OnWeaponActivityOn += _soundSystem.PlayerBattleCry;
            csPlayerController.OnWeaponActivityOn += weapon.ActivateDamage;
            csPlayerController.OnWeaponActivityOff += weapon.DeactivateDamage;            
        }


        private void StartControllingPlayer(CSPlayerController csPlayerController)
        {
            _inputInterpreter.OnMoved += csPlayerController.Movement;
            _inputInterpreter.AttackRequested += csPlayerController.Attack;
            _inputInterpreter.OnLockStateChangeRequest += csPlayerController.ChangeLockState;
            _inputInterpreter.OnLookRequest += csPlayerController.RotatePlayer;
        }
    }
}
