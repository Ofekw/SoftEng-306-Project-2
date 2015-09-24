using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;


namespace Assets.Game.Scripts.Enviroment
{
    public class CameraControl : MonoBehaviour
    {
        // Transform of the camera to shake. Grabs the gameObject's transform
        // if null.
        public Transform camTransform;

        // How long the object should shake for.
        public float shake = 0f;

        // Amplitude of the shake. A larger value shakes the camera harder.
        public float shakeAmount = 0.7f;
        public float decreaseFactor = 1.0f;

        Vector3 originalPos;

        //var currentpos = mainCam.transform.position;
        //Vector3 vext = new Vector3(10, 10, -10);
        //Camera.main.transform.position = vext;
        //Vector3 decrementVector = new Vector3(-1, -1, 0);
        //while (vext.x > 0)
        //{
        //    Debug.Log("Camera Moving");

        //    vext = vext + decrementVector;
        //    Camera.main.transform.position = vext; 
        //}

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
                camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

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
