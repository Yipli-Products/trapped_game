using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLife : MonoBehaviour
{
    //required variables
    [SerializeField] GameObject lifeOne;
    [SerializeField] GameObject lifeTwo;
    [SerializeField] GameObject lifeThree;
    [SerializeField] GameObject lifeFrame;

    [SerializeField] GameObject runVideoScreen;

    // Start is called before the first frame update
    void Start()
    {
        lifeOne.SetActive(false);
        lifeTwo.SetActive(false);
        lifeThree.SetActive(false);
        lifeFrame.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            runVideoScreen.SetActive(false);

            Time.timeScale = 0.1f;
            StartCoroutine(frameAnimation());
        }
    }

    IEnumerator frameAnimation()
    {
        lifeOne.SetActive(true);
        lifeTwo.SetActive(true);
        lifeThree.SetActive(true);

        for (int i = 0; i < 5; i++)
        {
            lifeFrame.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);

            lifeFrame.SetActive(false);
            yield return new WaitForSecondsRealtime(1f);
        }

        Time.timeScale = 1f;
        runVideoScreen.SetActive(true);
    }
}
