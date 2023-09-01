using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class RawVideoRenderer : MonoBehaviour
{
    // required variables
    [SerializeField] RawImage videoScreen;
    [SerializeField] VideoPlayer vPlayer;
    [SerializeField] AudioSource audioS;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StreamVideo());
    }

    IEnumerator StreamVideo()
    {
        vPlayer.Prepare();
        WaitForSeconds wfs = new WaitForSeconds(1f);

        while (!vPlayer.isPrepared)
        {
            yield return wfs;
            break;
        }

        videoScreen.texture = vPlayer.texture;
        vPlayer.Play();
        audioS.Play();
    }
}
