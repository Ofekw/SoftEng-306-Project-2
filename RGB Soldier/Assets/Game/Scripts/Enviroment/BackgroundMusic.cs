using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Scripts.Enviroment
{
    class BackgroundMusic : MonoBehaviour
    {
        private static BackgroundMusic instance = null;
        public static BackgroundMusic Instance
        {
            get { return instance; }
        }

        void Awake()
        {
            var haha = GameObject.FindGameObjectWithTag("Audio");
            AudioListener.Destroy(haha);

            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(this.gameObject);
        }

    }
}
