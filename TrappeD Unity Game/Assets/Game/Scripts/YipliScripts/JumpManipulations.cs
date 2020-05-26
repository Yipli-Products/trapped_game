using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class JumpManipulations : MonoBehaviour
{
    // required variables
    BallController bc;

    private void Start()
    {
        bc = FindObjectOfType<BallController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "jumpI")
        {
            if (collision.gameObject.tag == "Player")
            {
                print("from jump I");
                bc.jumpManipulated = false;
            }
        }
        else if (gameObject.tag == "jumpR")
        {
            if (collision.gameObject.tag == "Player")
            {
                print("from jump R");
                bc.jumpManipulated = true;
            }
        }
    }
}
