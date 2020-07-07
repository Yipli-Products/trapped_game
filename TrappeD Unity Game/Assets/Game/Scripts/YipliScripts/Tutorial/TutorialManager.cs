using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject TextTutorial;
    [SerializeField] GameObject PlayerName;
    [SerializeField] GameObject LevelName;
    private bool pauseForTextTut;

    // Start is called before the first frame update
    void Start()
    {
        PlayerName.SetActive(false);
        LevelName.SetActive(false);

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

        PlayerName.SetActive(true);
        LevelName.SetActive(true);

        pauseForTextTut = false;
        Time.timeScale = 1f;
    }
}
