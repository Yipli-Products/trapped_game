using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class FirstLife : MonoBehaviour
{
    //required variables
    [SerializeField] GameObject lifeOne;
    [SerializeField] GameObject lifeTwo;
    [SerializeField] GameObject lifeThree;
    [SerializeField] GameObject leftArrow;

    [SerializeField] GameObject runVideoScreen;

    [SerializeField] Text speakerT;

    // Start is called before the first frame update
    void Start()
    {
        lifeOne.SetActive(false);
        lifeTwo.SetActive(false);
        lifeThree.SetActive(false);
        leftArrow.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            runVideoScreen.SetActive(false);

            speakerT.text = "These are your lives. You have 3 chances to finish the level.";
            AudioControl.Instance.playAudio();

            Time.timeScale = 0.1f;
            StartCoroutine(frameAnimation());
        }
    }

    IEnumerator frameAnimation()
    {
        leftArrow.SetActive(true);

        lifeOne.SetActive(true);
        lifeTwo.SetActive(true);
        lifeThree.SetActive(true);

        for (int i = 0; i < 5; i++)
        {
            lifeOne.SetActive(false);
            lifeTwo.SetActive(false);
            lifeThree.SetActive(false);
            yield return new WaitForSecondsRealtime(1f);

            lifeOne.SetActive(true);
            lifeTwo.SetActive(true);
            lifeThree.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
        }

        speakerT.text = "Keep Running";
        AudioControl.Instance.playAudio();
        leftArrow.SetActive(false);
        Time.timeScale = 1f;
        runVideoScreen.SetActive(true);
    }
}
