using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    // required variables
    [SerializeField] AudioSource audioS;

    public static AudioControl Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void playAudio()
    {
        audioS.Play();
    }
}
