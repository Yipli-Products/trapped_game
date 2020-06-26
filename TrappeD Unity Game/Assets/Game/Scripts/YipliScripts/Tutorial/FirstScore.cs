﻿using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class FirstScore : MonoBehaviour
{
    //required variables
    [SerializeField] GameObject scoreNum;
    [SerializeField] GameObject rightArrow;
    [SerializeField] Text speakerT;

    private BallController bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = FindObjectOfType<BallController>();

        scoreNum.SetActive(false);
        rightArrow.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            speakerT.text = "Your score";
            AudioControl.Instance.playAudio();

            bc.allowjump = false;
            bc.allowRun = false;
            bc.allowStop = false;

            Time.timeScale = 0.1f;
            StartCoroutine(frameAnimation());
        }
    }

    IEnumerator frameAnimation()
    {
        rightArrow.SetActive(true);
        scoreNum.SetActive(true);

        for (int i = 0; i < 5; i++)
        {
            scoreNum.SetActive(false);
            yield return new WaitForSecondsRealtime(0.25f);

            scoreNum.SetActive(true);
            yield return new WaitForSecondsRealtime(0.25f);
        }

        bc.allowjump = true;
        bc.allowRun = true;
        bc.allowStop = true;

        speakerT.text = "Keep Running. You must finish Tutorial to unlock Level 1.";
        AudioControl.Instance.playAudio();
        rightArrow.SetActive(false);
        Time.timeScale = 1f;
    }
}