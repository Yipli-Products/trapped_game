using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace UnitySampleAssets.CrossPlatformInput.PlatformSpecific
{
    public class JoysticController : MonoBehaviour
    {
        // required variables

        private BallController bc = null;

        private bool buttonHoldDownR = false;
        public float hForce = 500;

        // Start is called before the first frame update
        void Start()
        {
            bc = FindObjectOfType<BallController>();
        }

        private void FixedUpdate()
        {
            if (buttonHoldDownR)
            {
                PlayerMoveRight();
            }
        }

        public void jumpB()
        {
            bc.ballJump = true;
            Invoke("JumpFalse", 1f);
        }

        private void JumpFalse()
        {
            bc.ballJump = false;
        }

        public void PlayerMoveLeft()
        {
            //Invoke("leftOps", 0.3f);

            bc.moveHorzRight = false;
            //bc.touchHorizontalMoveDown(-1);

            //float hForce = -2.5f;

            //bc.GetComponent<Rigidbody2D>().AddForce(new Vector2(hForce--, 0f));
        }

        public void PlayerMoveRight()
        {
            bc.moveHorzLeft = false;
            //bc.touchHorizontalMoveDown(1);

            bc.GetComponent<Rigidbody2D>().AddForce(new Vector2(hForce * Time.deltaTime, 0f));
        }

        public void rightBHoldDown ()
        {
            buttonHoldDownR = true;
        }

        public void rightBHoldUp()
        {
            buttonHoldDownR = false;
        }

        private void leftOps () {
            bc.moveHorzRight = false;
            //bc.touchHorizontalMoveDown(-1);

            float hForce = -15f;

            bc.GetComponent<Rigidbody2D>().AddForce(new Vector2(hForce--, 0f));
        }
    }
}

