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
        public AudioClip pyramidSong;
        public AudioClip menuSong;
        public AudioClip forestSong;
        public AudioClip windySong;
        public AudioClip iceSong;
        public AudioClip airSong;
        public AudioClip bossSong;
        public AudioClip finalRoadSong;
        public AudioClip shrineSong;
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
            menuSong = Resources.Load("Audio/Visager_-_19_-_Village_Dreaming_Loop") as AudioClip;
            pyramidSong = Resources.Load("Audio/Visager_-_15_-_Pyramid_Level_Loop") as AudioClip;
            forestSong = Resources.Load("Audio/Visager_-_19_-_The_Great_Forest_Loop") as AudioClip; ;
            windySong = Resources.Load("Audio/Visager_-_19_-_Windy_Bluffs_Loop") as AudioClip; ;
            iceSong = Resources.Load("Audio/Visager_-_19_-_Ice_Cave_Loop") as AudioClip; ;
            airSong = Resources.Load("Audio/Visager_-_19_-_Airship_Loop") as AudioClip; ;
            bossSong = Resources.Load("Audio/Visager_-_18_-_Dark_Sanctum_Boss_Fight_Loop") as AudioClip; ;
            finalRoadSong = Resources.Load("Audio/Visager_-_20_-_The_Final_Road_Loop") as AudioClip; ;
            shrineSong = Resources.Load("Audio/Visager_-_03_-_Shrine") as AudioClip; ;
            mySource = GetComponent<AudioSource>();

        }

        // Update is called once per frame
        void Update()
        {

            if (Application.loadedLevelName == "start_screen 1")
            {
                mySource.clip = menuSong;
                if (!mySource.isPlaying)
                {
                    mySource.Play();
                }
            }
            else if (Application.loadedLevelName == "stage_select 1")
            {
                if (!mySource.isPlaying)
                {
                    mySource.Play();
                }
            }
            else if (Application.loadedLevelName == "OpeningCutScene 1")
            {
                mySource.clip = shrineSong;

                if (!mySource.isPlaying)
                {
                    mySource.Play();
                }
            }
            else if (!mySource.isPlaying)
            {
                mySource.Play();
            }
        }
    }
}