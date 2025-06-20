using UnityEngine;
namespace CS
{
    public class GameplayUISystem : MonoBehaviour
    {

        [SerializeField] private CanvasGroup _canvasGroupForLockOnState;

        private LockOnStateUIHandler _lockOnStateUIHandler;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _lockOnStateUIHandler = new LockOnStateUIHandler(this, _canvasGroupForLockOnState);
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