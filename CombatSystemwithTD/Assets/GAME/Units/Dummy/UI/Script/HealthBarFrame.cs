using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace CS
{
    public class HealthBarFrame : MonoBehaviour
    {

        [SerializeField] private Slider _slider;

        private Camera _mainCam;

        private void Start()
        {
            _mainCam = Camera.main;
        }


        private void LateUpdate()
        {
             transform.rotation = Quaternion.LookRotation(transform.position - _mainCam.transform.position);
        }

        public void Configure(float healthMax)
        {
            _slider.maxValue = healthMax;
            _slider.value = healthMax;
            _slider.minValue = 0;
        }


        public void UpdateHealthBar(float health)
        {
            if (!gameObject.activeInHierarchy) return;

            float timeCounter = 0;
            StopAllCoroutines();
            StartCoroutine(updateHealthBarOverTime());
            IEnumerator updateHealthBarOverTime()
            {
                while (timeCounter < 1)
                {
                    timeCounter += Time.deltaTime * 10;
                    _slider.value = Mathf.Lerp(_slider.value, health, timeCounter);
                    yield return null;
                }
            }

        }


        public void Hide()
        {
            transform.gameObject.SetActive(false);
        }


        public void Show()
        {
            transform.gameObject.SetActive(true);
        }
    }
}