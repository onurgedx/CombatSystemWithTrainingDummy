using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.PlayerLoop;
namespace CS
{

    public class CSPlayerCameraHandler
    {

        private CinemachineMixingCamera _mixingCamera;


        private int _currentCameraIndex=0;


        private MonoBehaviour _monoBehaviour;

        public CSPlayerCameraHandler(CinemachineMixingCamera mixingCamera,MonoBehaviour monoBehaviour)
        {
            _mixingCamera = mixingCamera;
            _monoBehaviour = monoBehaviour;
        }

        private IEnumerator SwitchWeightOverTime(int nextCameraIndex)
        {
            float timeCounter = 0;
            while (timeCounter < 1)
            {
                timeCounter += Time.deltaTime;
                _mixingCamera.SetWeight(_currentCameraIndex, 1 - timeCounter);
                _mixingCamera.SetWeight(nextCameraIndex, timeCounter);
                yield return null;

            }
            _currentCameraIndex = nextCameraIndex;


        }

        public void ActivateLockState()
        {
            _monoBehaviour.StartCoroutine(SwitchWeightOverTime(1));
        }


        public void DeactivateLockState()
        {
            _monoBehaviour.StartCoroutine(SwitchWeightOverTime(0));
        }

    }
}
