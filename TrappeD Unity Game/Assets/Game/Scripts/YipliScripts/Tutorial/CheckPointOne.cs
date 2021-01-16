using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class CheckPointOne : MonoBehaviour
{
    // required variables
    private BallController bc;

    private void Start()
    {
        bc = FindObjectOfType<BallController>();
    }
}
