using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    //required variables
    [SerializeField] GameObject lifeOne;
    [SerializeField] GameObject lifeTwo;
    [SerializeField] GameObject lifeThree;

    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        lifeOne.SetActive(false);
        lifeTwo.SetActive(false);
        lifeThree.SetActive(false);
        pauseButton.SetActive(false);
        ScoreText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
