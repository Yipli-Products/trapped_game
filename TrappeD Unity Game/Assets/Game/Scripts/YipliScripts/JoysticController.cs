using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

namespace UnitySampleAssets.CrossPlatformInput.PlatformSpecific
{
    public class JoysticController : MonoBehaviour
    {
        // required variables
        [SerializeField] Text ScoreText;
        [SerializeField] PlayerStats ps;

        private BallController bc = null;

        private bool buttonHoldDownR = false;
        public float hForce = 650;
        public float hForceNegative = -0.0000005f;

        // Start is called before the first frame update
        void Start()
        {
            bc = FindObjectOfType<BallController>();
        }

        private void Update()
        {
            ScoreText.text = ps.GetCoinScore().ToString();
        }

        private void FixedUpdate()
        {
            print("buttonHoldDownR : " + buttonHoldDownR);

            if (buttonHoldDownR)
            {
                PlayerMoveRight();
            }
            else
            {
                PlayerMoveLeft();
            }
        }

        public void PlayerMoveRight()
        {
            bc.moveHorzLeft = false;

            bc.GetComponent<Rigidbody2D>().AddForce(new Vector2(hForce * Time.deltaTime, 0f));

            bc.leftJump = false;
            bc.calWaitTime = false;
            bc.waitTimeCal = 0f;
        }

        public void PlayerMoveLeft()
        {
            if (bc.waitTimeCal > 5f)
            {
                bc.moveHorzRight = false;
                //bc.GetComponent<Rigidbody2D>().AddForce(new Vector2(hForceNegative--, 0f));
                bc.hInput = -8f;
                bc.leftJump = true;
            }
        }

        public void rightBHoldDown ()
        {
            bc.calWaitTime = false;
            buttonHoldDownR = true;
        }

        public void rightBHoldUp()
        {
            
            bc.calWaitTime = true;
            buttonHoldDownR = false;
        }
        public void jumpB()
        {
            bc.ballJump = true;
            Invoke("JumpFalse", 0.1f);
        }

        private void JumpFalse()
        {
            bc.ballJump = false;
        }
    }
}

