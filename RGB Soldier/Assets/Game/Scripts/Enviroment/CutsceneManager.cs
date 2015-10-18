using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Scripts.Enviroment
{
    public class CutsceneManager : MonoBehaviour
    {
        public GameObject player;
        public SkinnedMeshRenderer skin;
        public SkinnedMeshRenderer pauseSkin;
        public Material[] materials;
        public Material[] pauseMaterials;
        public Texture[] textures;


        public void Start()
        {
            var i = GameControl.control != null ? GameControl.control.playerSprite : 1;
            player = GameObject.Find("Player");
            skin = player.transform.FindChild("p_sotai").GetComponent<SkinnedMeshRenderer>();
            materials = skin.materials;

            if (!Application.loadedLevelName.Contains("cutscene"))
            {
                pauseMaterials = pauseSkin.materials;

            }

            if (i == 1)
            {
                for (i = 0; i < 4; i++)
                {
                    materials[i].mainTexture = Resources.Load("ch034", typeof(Texture2D)) as Texture2D;
                }
            }
            else if (i == 2)
            {
                for (i = 0; i < 4; i++)
                {
                    materials[i].mainTexture = Resources.Load("ch037", typeof(Texture2D)) as Texture2D;
                }
            }
            else if (i == 3)
            {
                for (i = 0; i < 4; i++)
                {
                    materials[i].mainTexture = Resources.Load("ch029", typeof(Texture2D)) as Texture2D;
                }
            }
        }
    }
}
