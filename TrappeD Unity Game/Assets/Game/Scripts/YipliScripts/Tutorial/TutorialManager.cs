using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject TextTutorial;
    private bool pauseForTextTut;

    // Start is called before the first frame update
    void Start()
    {
        TextTutorial.SetActive(true);
        pauseForTextTut = true;
    }

    private void Update()
    {
        if (pauseForTextTut)
        {
            Time.timeScale = 0f;
        }
    }

    public void TextTutorialNextButton ()
    {
        TextTutorial.SetActive(false);
        pauseForTextTut = false;
        Time.timeScale = 1f;
    }
}
