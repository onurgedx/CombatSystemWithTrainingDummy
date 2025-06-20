
using System.Collections;
using TMPro;
using UnityEngine;
namespace CS
{
    public class DamageDealtDisplayBehavior : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _damageDealtText;

        private float _timeCounterSpeed = 4;

        public void Play(int damageDealt)
        {

            _damageDealtText.text = damageDealt.ToString();
            StartCoroutine(fade());
            IEnumerator fade()
            {
                float timeCounter = 0;
                while (timeCounter < 1)
                {
                    timeCounter += Time.deltaTime* _timeCounterSpeed;
                    transform.position += Vector3.up * Time.deltaTime * _timeCounterSpeed;
                    transform.localScale = Vector3.one + Vector3.one * timeCounter / 4;
                    yield return null;
                }
                yield return new WaitForSeconds(0.2f);
                Destroy(gameObject);
            }

        }

    }
}