using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Scripts.Enviroment
{
    class CameraShake : MonoBehaviour
    {
        // Transform of the camera to shake. Grabs the gameObject's transform
        // if null.
        public Transform camTransform;

        // How long the object should shake for.
        public float shake = 5f;

        // Amplitude of the shake. A larger value shakes the camera harder.
        public float shakeAmount = 0.7f;
        public float decreaseFactor = 1.0f;

        Vector3 originalPos;

        void Awake()
        {
            if (camTransform == null)
            {
                camTransform = GetComponent(typeof(Transform)) as Transform;
            }
        }

        void OnEnable()
        {
            originalPos = camTransform.localPosition;
        }

        void Update()
        {
            if (shake > 0)
            {
                camTransform.localPosition = originalPos + UnityEngine.Random.insideUnitSphere * shakeAmount;

                shake -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shake = 0f;
                camTransform.localPosition = originalPos;
            }
        }
    }
}
