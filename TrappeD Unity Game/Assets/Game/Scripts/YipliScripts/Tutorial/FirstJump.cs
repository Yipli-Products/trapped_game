using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class FirstJump : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject stopVideoScreen;
    [SerializeField] GameObject jumpVideoScreen;
    [SerializeField] Text AIText;
    [SerializeField] Text speakerT;

    [SerializeField] GameObject windowFrame;

    private BallController bc;

    private PauseGame pg;

    // Start is called before the first frame update
    void Start()
    {
        jumpVideoScreen.SetActive(false);
        windowFrame.SetActive(false);

        bc = FindObjectOfType<BallController>();
        pg = FindObjectOfType<PauseGame>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            stopVideoScreen.SetActive(false);
            jumpVideoScreen.SetActive(true);

            AIText.text = "Game Hints";

            speakerT.text = "You will be provided Text instructions through out the Game.";

            StartCoroutine(frameAnimation());

            Time.timeScale = 0.01f;
        }
    }

    IEnumerator frameAnimation()
    {
        for (int i = 0; i < 3; i++)
        {
            windowFrame.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);

            windowFrame.SetActive(false);
            yield return new WaitForSecondsRealtime(1f);
        }

        AIText.text = "JUMP";
        Time.timeScale = 1f;
    }
}
