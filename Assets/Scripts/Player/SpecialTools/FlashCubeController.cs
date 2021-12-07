using ET.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.SpecialTools
{
    public class FlashCubeController : MonoBehaviour
    {
        [SerializeField] private Light _pointLight;
        [SerializeField] private Light _lightPulse;

        private float _fullWorkingTime = 20f;
        private float _amountTick = 1e-4f;
        private float amountIntensity = 0f;

        protected void Update()
        {
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            float timer = _fullWorkingTime;

            while(timer > 1e-3f)
            {
                timer--;
                _pointLight.intensity -= _amountTick;

                Pulsate();

                yield return new WaitForSeconds(1f);
            }

            SelfDestruct();

            yield return null;
        }

        private void Pulsate()
        {
            if(amountIntensity == 0)
            {
                _lightPulse.intensity = 2f;
                amountIntensity = 2f;
            }
            else
            {
                _lightPulse.intensity = 0;
                amountIntensity = 0f;
            }
        }

        private void SelfDestruct()
        {
            Destroy(gameObject, 1f);
        }
    }
}
