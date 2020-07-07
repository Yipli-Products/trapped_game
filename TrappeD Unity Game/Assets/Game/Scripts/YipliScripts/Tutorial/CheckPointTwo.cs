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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bc.allowjump = true;
        bc.allowRun = true;
        bc.Runbackward = false;
    }
}
