using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class TileTriggers : MonoBehaviour
{
    private BallController bc = null;

    private void Start()
    {
        bc = FindObjectOfType<BallController>();

        bc.isGrounded = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bc.isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bc.isGrounded = false;
        }
    }
}
