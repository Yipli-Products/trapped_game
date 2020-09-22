using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    [SerializeField] GameObject endNotes;
    
    void Start()
    {
        endNotes.SetActive(false);
    }

    public void ActiveEndNote()
    {
        endNotes.SetActive(true);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
