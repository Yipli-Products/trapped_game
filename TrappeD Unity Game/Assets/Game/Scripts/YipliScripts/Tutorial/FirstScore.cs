using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class FirstScore : MonoBehaviour
{
    //required variables
    [SerializeField] GameObject scoreNum;
    [SerializeField] GameObject rightArrow;
    [SerializeField] Text speakerT;

    // Start is called before the first frame update
    void Start()
    {
        scoreNum.SetActive(false);
        rightArrow.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            speakerT.text = "This is your score.";
            AudioControl.Instance.playAudio();

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
            yield return new WaitForSecondsRealtime(1f);

            scoreNum.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
        }

        speakerT.text = "Keep Running. You must finish to unlock Level 1.";
        AudioControl.Instance.playAudio();
        rightArrow.SetActive(false);
        Time.timeScale = 1f;
    }
}