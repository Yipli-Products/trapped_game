using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class ChangeToJumpText : MonoBehaviour
{
    // required variables
    [SerializeField] Text speakerT;

    private BallController bc;

    private void Start()
    {
        bc = FindObjectOfType<BallController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bc.allowjump = true;
        bc.allowRun = true;
        bc.Runbackward = false;

        speakerT.text = "Use Run or Stop to Jump";
    }
}
