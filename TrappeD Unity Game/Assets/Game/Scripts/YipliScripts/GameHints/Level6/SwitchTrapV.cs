using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;
using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class SwitchTrapV : MonoBehaviour
{
    // required variables
    [SerializeField] VideoPlayer trapVideo;

    private BallController bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = FindObjectOfType<BallController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trapVideo.Play();
            StartCoroutine(goBackToLevel());
        }
    }

    private IEnumerator goBackToLevel()
    {
        while (trapVideo.isPlaying)
        {
            bc.gameHint = true;
            bc.calWaitTime = false;
            bc.waitTimeCal = 0f;
            yield return null;
        }

        bc.gameHint = false;
        bc.calWaitTime = true;

        trapVideo.targetCameraAlpha = 0;
    }
}
