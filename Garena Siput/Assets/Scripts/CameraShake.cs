using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

        public float shakeDuration = 0.5f;
        public float shakeMagnitude = 0.8f;

        private Vector3 originalPosition;

        void Start()
        {
            originalPosition = transform.localPosition;
        }

        public void Shake(float changeMagnitude)
        {
            shakeMagnitude = changeMagnitude;
            StartCoroutine(ShakeCoroutine());
        }

        IEnumerator ShakeCoroutine()
        {
            float elapsed = 0.0f;

            while (elapsed < shakeDuration)
            {
                float x = originalPosition.x + Random.Range(-1f, 1f) * shakeMagnitude;
                float y = originalPosition.y + Random.Range(-1f, 1f) * shakeMagnitude;

                transform.localPosition = new Vector3(x, y, originalPosition.z);

                elapsed += Time.deltaTime;

                yield return null;
            }

            transform.localPosition = originalPosition;
        }

    }
