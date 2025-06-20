using System.Collections;
using UnityEngine;
namespace CS
{
    public class LockOnStateUIHandler
    {
        private CanvasGroup _canvasGroupForAlpha;
        private MonoBehaviour _mono;
        
        private float _currentAlpha =0f;
        private float _exitalphaDestination=0;
        private float _enteralphaDestination =1f;
        private float _alphaChangeSpeed = 4;
        public LockOnStateUIHandler(MonoBehaviour mono, CanvasGroup canvasGroupForAlpha)
        {
            _mono = mono;
            _canvasGroupForAlpha = canvasGroupForAlpha;
        }

        public void Enter()
        {
            _mono.StartCoroutine(enter());
            IEnumerator enter()
            { 
                while (_currentAlpha < _enteralphaDestination)
                { 
                    _currentAlpha  += Time.deltaTime* _alphaChangeSpeed;
                    _canvasGroupForAlpha.alpha = _currentAlpha;
                    yield return null;
                }
            }
        }


        public void Exit()
        {
            _mono.StartCoroutine(exit());
            IEnumerator exit()
            {
                while (_currentAlpha > _exitalphaDestination)
                {
                    _currentAlpha -= Time.deltaTime * _alphaChangeSpeed;
                    _canvasGroupForAlpha.alpha = _currentAlpha;
                    yield return null;
                }
            }
        }
    }
}