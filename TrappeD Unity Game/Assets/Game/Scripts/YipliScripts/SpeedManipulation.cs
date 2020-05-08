using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitySampleAssets.CrossPlatformInput.PlatformSpecific
{
    public class SpeedManipulation : MonoBehaviour
    {
        // required variables
        private BallController bc = null;
        private JoysticController jc = null;

        // Start is called before the first frame update
        void Start()
        {
            bc = FindObjectOfType<BallController>();
            jc = FindObjectOfType<JoysticController>();
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (gameObject.tag == "SpeedI") {
                if (col.gameObject.tag == "Player") {
                    bc.forwardForce = 2000f;
                    jc.hForce = 2000f;

                    Debug.Log("Player Triggered SpeedI");
                }
            } else if (gameObject.tag == "SpeedR") {
                if (col.gameObject.tag == "Player") {
                    bc.forwardForce = 500f;
                    jc.hForce = 500f;

                    Debug.Log("Player Triggered SpeedR");
                }
            }
        }
    }
}
