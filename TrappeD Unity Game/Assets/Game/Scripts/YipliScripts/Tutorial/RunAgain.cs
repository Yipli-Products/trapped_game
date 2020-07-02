﻿using UnityEngine.UI;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class RunAgain : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject obstacleBoxSmall;
    [SerializeField] GameObject leftMoveCol;
    [SerializeField] Text speakerT;

    private BallController bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = FindObjectOfType<BallController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.activeSelf)
        {
            bc.allowjump = true;
            bc.allowRun = true;
            bc.Runbackward = false;

            speakerT.text = "Keep Running or Stop to Jump to finish the level";
            AudioControl.Instance.playAudio();
            leftMoveCol.SetActive(false);

            obstacleBoxSmall.GetComponent<Rigidbody2D>().mass = 1;
        }
    }
}
