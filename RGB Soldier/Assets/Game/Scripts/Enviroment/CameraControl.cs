using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;


namespace Assets.Game.Scripts.Enviroment
{
    /// <summary>
    /// Contains methods to handle camera actions, such as following the player and shaking when the special attack is done
    /// </summary>
    public class CameraControl : MonoBehaviour
    {
        public float xThreshold = 1f;		
        public float yThreshold = 1f;		
        public float xSmooth = 5f;		
        public float ySmooth = 5f;		
        public Vector2 maxXAndY;		
        public Vector2 minXAndY;		

        private Transform player;		


        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void FixedUpdate()
        {
            FollowPlayer();
        }

        void FollowPlayer()
        {
            //get the cameras current position
            float currentX = transform.position.x;
            float currentY = transform.position.y;

            if (CheckXPosition())
            {
                currentX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);
            }
            currentX = Mathf.Clamp(currentX, minXAndY.x, maxXAndY.x);

            if (CheckYPosition())
            {
                currentY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);
            }

            currentY = Mathf.Clamp(currentY, minXAndY.y, maxXAndY.y);

            // Set the camera's position (z pos it the same)
            transform.position = new Vector3(currentX, currentY, transform.position.z);
        }

        //used to see if the player distance is greater than than the x threshold
        bool CheckXPosition()
        {
            return Mathf.Abs(transform.position.x - player.position.x) > xThreshold;
        }

        //used to see if the player distance is greater than than the y threshold 

        bool CheckYPosition()
        {
            return Mathf.Abs(transform.position.y - player.position.y) > yThreshold;
        }

    }
}