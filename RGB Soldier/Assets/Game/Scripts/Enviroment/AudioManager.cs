using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Scripts.Enviroment
{
    class AudioManager : MonoBehaviour
    {

        private AudioSource mySource;
        public AudioClip levelOneSong;
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (Application.loadedLevelName == "start_screen 1")
            {
                Destroy(gameObject);
            }
        }

        // Use this for initialization
        void Start()
        {
            mySource = GetComponent<AudioSource>();
   
        }

        // Update is called once per frame
        void Update()
        {

            if (Application.loadedLevelName == "start_screen 1")
            {
                
                mySource.clip = levelOneSong;
                if (!mySource.isPlaying)
                {
                    mySource.Play();
                }
            }
            else if (!mySource.isPlaying)
            {
                Debug.Log("playplay");

                mySource.Play();
            }
        }
    }
}