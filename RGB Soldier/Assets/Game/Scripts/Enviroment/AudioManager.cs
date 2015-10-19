using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Scripts.Enviroment
{

    /// <summary>
    /// Singleton class that controls all the background music for the game
    /// </summary>
    class AudioManager : MonoBehaviour
    {
        private AudioSource audioSource;
        private AudioClip lavaSong;
        private AudioClip menuSong;
        private AudioClip iceSong;
        private AudioClip bossSong;
        private AudioClip cutsceneSong;
        private AudioClip tutorialSong;


        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (Application.loadedLevelName == "menu_start_screeen")
            {
               Destroy(gameObject);
            }
        }

        // Use this for initialization
        void Start()
        {
            //loads in the audio tracks
            tutorialSong = Resources.Load("Audio/tutorial_song") as AudioClip;
            menuSong = Resources.Load("Audio/menu_song") as AudioClip;
            lavaSong = Resources.Load("Audio/lava_song") as AudioClip;
            bossSong = Resources.Load("Audio/boss_battle_loop") as AudioClip;
            iceSong = Resources.Load("Audio/ice_song") as AudioClip;
            cutsceneSong = Resources.Load("Audio/cutscene_song") as AudioClip;
            audioSource = GetComponent<AudioSource>();

        }

        /// <summary>
        /// Called once per frame. Sets volume and listens to the scene name, and decides which 
        /// track to play
        /// </summary>
        void Update()
        {
           audioSource.volume = (float)(GameControl.control.backgroundVolume) / 100;
            if (Application.loadedLevelName.Contains("menu"))
            {
                if (audioSource.clip != menuSong)
                {
                    audioSource.clip = menuSong;
                }
  
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else if (Application.loadedLevelName == "stage_tutorial")
            {
                if (audioSource.clip != tutorialSong)
                {
                    audioSource.clip = tutorialSong;
                }
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else if (Application.loadedLevelName == "stage_1")
            {
                if (audioSource.clip != lavaSong)
                {
                    audioSource.clip = lavaSong;
                }
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else if (Application.loadedLevelName == "stage_2")
            {
                if (audioSource.clip != iceSong)
                {
                    audioSource.clip = iceSong;
                }
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else if (Application.loadedLevelName == "stage_3")
            {
                if (audioSource.clip != iceSong)
                {
                    audioSource.clip = iceSong;
                }
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else if (Application.loadedLevelName == "stage_4")
            {
                if (audioSource.clip != iceSong)
                {
                    audioSource.clip = iceSong;
                }
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else if (Application.loadedLevelName == "stage_boss")
            {
                audioSource.clip = bossSong;

                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else if (Application.loadedLevelName.Contains("cutscene"))
            {
                audioSource.clip = cutsceneSong;

                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}