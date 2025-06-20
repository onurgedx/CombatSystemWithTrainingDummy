using UnityEngine;
namespace CS
{
    [DefaultExecutionOrder(123)]
    public class UnitControlSystem : MonoBehaviour
    { 
        [SerializeField] private BattleSystem _battleSystem;
        [SerializeField] private CameraSystem _cameraSystem;
        [SerializeField] private CSSoundSystem _soundSystem;
        [SerializeField] private GameplayUISystem _gameplayUISystem;

        [SerializeField] private DummyBehaviour[] _dummies;

        [SerializeField] private CSPlayerBehaviour _csPlayerBehaviour;
        private CSPlayerInputInterpreter _inputInterpreter;

        private PlayerControlSystem _playerControlSystem;
        
         
        private void Start()
        { 
            _inputInterpreter = new CSPlayerInputInterpreter();
            _playerControlSystem = new PlayerControlSystem(_battleSystem, _cameraSystem, _soundSystem, _gameplayUISystem);
            _playerControlSystem.Configure(_csPlayerBehaviour);
            _playerControlSystem.StartControllingPlayer(_csPlayerBehaviour, _inputInterpreter); 
            ConfigureDummies();
        }

      

        private void ConfigureDummies()
        {
            foreach (DummyBehaviour dummyBehaviour in _dummies)
            {
                float health = 200;
                float reviveDuration = 5;
                Dummy dummy = new Dummy(health, reviveDuration,dummyBehaviour.transform.position + Vector3.up*1.6f);
                dummyBehaviour.Init(dummy);
                _battleSystem.RecordDamageable(dummyBehaviour.Collider, dummy);

                if(dummy is ILockableTarget lockableTarget)
                {
                    _playerControlSystem.RecordLockableTarget(lockableTarget);
                }
            }
        }
    }
}
