using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FirstLeftMove : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject leftMoveSpeak;
    [SerializeField] GameObject RunAgainSpeak;
    [SerializeField] GameObject RunAgainCol;

    [SerializeField] GameObject runVideoScreen;
    [SerializeField] GameObject stopVideoScreen;
    [SerializeField] Text AIText;

    // Start is called before the first frame update
    void Start()
    {
        stopVideoScreen.SetActive(false);
        RunAgainSpeak.SetActive(false);
        RunAgainCol.SetActive(false);
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            runVideoScreen.SetActive(false);
            stopVideoScreen.SetActive(true);
            AIText.fontSize = 50;
            AIText.text = "Stop Running";

            RunAgainSpeak.SetActive(true);
            RunAgainCol.SetActive(true);

            Invoke("leftSpeakDisable", 2f);
            
            //countDownText.text = "Stop Running. if you dont do anything on mat, after a while, you will start moving backwords automatically.";
        }
    }

    private void leftSpeakDisable()
    {
        leftMoveSpeak.SetActive(false);
    }
}
