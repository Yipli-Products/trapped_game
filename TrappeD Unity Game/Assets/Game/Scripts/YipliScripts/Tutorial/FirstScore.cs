using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScore : MonoBehaviour
{
    //required variables
    [SerializeField] GameObject scoreNum;
    [SerializeField] GameObject scoreFrame;

    // Start is called before the first frame update
    void Start()
    {
        scoreNum.SetActive(false);
        scoreFrame.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0.1f;
            StartCoroutine(frameAnimation());
        }
    }

    IEnumerator frameAnimation()
    {
        scoreNum.SetActive(true);

        for (int i = 0; i < 5; i++)
        {
            scoreFrame.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);

            scoreFrame.SetActive(false);
            yield return new WaitForSecondsRealtime(1f);
        }

        Time.timeScale = 1f;
    }
}