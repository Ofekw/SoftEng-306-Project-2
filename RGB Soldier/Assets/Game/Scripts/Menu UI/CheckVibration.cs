using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Menu_UI
{
    class CheckVibration : MonoBehaviour
    {

        public Toggle vibrationToggle;
        
        // Use this for initialization
        void Start()
        {
            // load persisted data
            bool vibrationOn = GameControl.control.vibrateOn;
            vibrationToggle.isOn = vibrationOn;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void toggleUpdate(bool update)
        {
            // update 
            Debug.Log(update.ToString());
            GameControl.control.vibrateOn = update;

        }
    }
}
