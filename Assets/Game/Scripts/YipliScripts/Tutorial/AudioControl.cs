using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    // required variables
    [SerializeField] AudioSource audioS;

    [SerializeField] AudioClip RunClip;
    [SerializeField] AudioClip JumpClip;
    [SerializeField] AudioClip StopClip;
    [SerializeField] AudioClip RunAndJumpClip;

    public void PlayRunAudio()
    {
        audioS.clip = RunClip;
        audioS.Play();
    }

    public void PlayJumpAudio()
    {
        audioS.clip = JumpClip;
        audioS.Play();
    }

    public void PlayStopAudio()
    {
        audioS.clip = StopClip;
        audioS.Play();
    }

    public void PlayRunAndJumpAudio()
    {
        audioS.clip = RunAndJumpClip;
        audioS.Play();
    }
}
