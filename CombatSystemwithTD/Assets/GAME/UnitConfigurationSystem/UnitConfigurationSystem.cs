using UnityEngine;
namespace CS
{
    [DefaultExecutionOrder(123)]
    public class UnitConfigurationSystem : MonoBehaviour
    { 
        [SerializeField] private BattleSystem _battleSystem;
        [SerializeField] private CameraSystem _cameraSystem;
        [SerializeField] private CSSoundSystem _soundSystem;
        [SerializeField] private GameplayUISystem _gameplayUISystem;

        [SerializeField] private DummyBehaviour[] _dummies;
        [SerializeField] private CSPlayerBehaviour _csPlayerBehaviour;

        private CSPlayerInputInterpreter _inputInterpreter;
        private PlayerConfigurationSystem _playerControlSystem;
        private DummyConfigurationSystem _dummyControlSystem;

        
        private void Start()
        { 
            _inputInterpreter = new CSPlayerInputInterpreter();
            _playerControlSystem = new PlayerConfigurationSystem(_battleSystem, _cameraSystem, _soundSystem, _gameplayUISystem);
            _dummyControlSystem = new DummyConfigurationSystem(_dummies, _battleSystem);
            _dummyControlSystem.DummyCreated += (IDummy dummy) =>
            {
                if (dummy is ILockableTarget lockableTarget)
                { 
                    _playerControlSystem.RecordLockableTarget(lockableTarget);
                }
            };
            _playerControlSystem.Configure(_csPlayerBehaviour);
            _playerControlSystem.StartControllingPlayer(_csPlayerBehaviour, _inputInterpreter);
            _dummyControlSystem.Configure();
        }

        

    }
}