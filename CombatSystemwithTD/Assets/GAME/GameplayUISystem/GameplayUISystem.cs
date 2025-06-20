using UnityEngine;
namespace CS
{
    [DefaultExecutionOrder(110)]
    public class GameplayUISystem : MonoBehaviour
    {

        [SerializeField] private Canvas _canvas;
        [SerializeField] private CanvasGroup _canvasGroupForLockOnState;
        [SerializeField] private BattleSystem _battleSystem;
        [SerializeField] private DamageDealtDisplayBehavior _damageDealtDisplayBehaviorPrefab;

        private LockOnStateUIHandler _lockOnStateUIHandler;
        private DamageDealtDisplayFrame _damageDisplayFrame;

         
        void Start()
        {
            _lockOnStateUIHandler = new LockOnStateUIHandler(this, _canvasGroupForLockOnState);
            _damageDisplayFrame = new DamageDealtDisplayFrame(_damageDealtDisplayBehaviorPrefab, _canvas.transform);
            _battleSystem.OnHit += _damageDisplayFrame.Display;
        }


        public void ActivateLockOn()
        {
            _lockOnStateUIHandler.Enter();
        }


        public void DeactivateLockOn()
        {
            _lockOnStateUIHandler.Exit();
        }

    }
}