using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class FirstTrapV : MonoBehaviour
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
            bc.calWaitTime = false;
            bc.gameHint = true;
            bc.waitTimeCal = 0f;
            yield return null;
        }

        bc.gameHint = false;
        bc.calWaitTime = true;

        trapVideo.targetCameraAlpha = 0;
    }
}
