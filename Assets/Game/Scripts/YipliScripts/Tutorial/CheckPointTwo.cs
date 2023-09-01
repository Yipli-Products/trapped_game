using UnityEngine;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class CheckPointTwo : MonoBehaviour
{
    // required variables
    private BallController bc;

    private void Start()
    {
        bc = FindObjectOfType<BallController>();
    }
}
