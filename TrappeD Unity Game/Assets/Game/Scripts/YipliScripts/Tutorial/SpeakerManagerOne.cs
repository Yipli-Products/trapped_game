using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerManagerOne : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject secondarySpeaker;

    public void disableSpeaker()
    {
        secondarySpeaker.SetActive(false);
    }
}
