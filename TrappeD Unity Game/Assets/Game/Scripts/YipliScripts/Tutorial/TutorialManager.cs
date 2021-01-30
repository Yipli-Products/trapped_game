using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject TextTutorial;
    [SerializeField] GameObject PlayerName;
    [SerializeField] GameObject LevelName;

    [SerializeField] PlayerStats ps;

    private bool pauseForTextTut;

    // Start is called before the first frame update
    void Start()
    {
        /*
        PlayerName.SetActive(false);
        LevelName.SetActive(false);

        if (!ps.IsStartTextShown)
        {
            ps.IsStartTextShown = true;
            TextTutorial.SetActive(true);
            pauseForTextTut = true;
        }
        else
        {
            TextTutorial.SetActive(false);
            pauseForTextTut = false;
        }
        */

        TextTutorialNextButton();

        ps.IsPreviousSceneTut = true;
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
