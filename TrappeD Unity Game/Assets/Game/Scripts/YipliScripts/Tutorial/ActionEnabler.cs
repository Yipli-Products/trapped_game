using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class ActionEnabler : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject runVideoScreen;
    [SerializeField] GameObject jumpVideoScreen;
    [SerializeField] GameObject stopVideoScreen;

    [SerializeField] Text speakerT;
    [SerializeField] GameObject speakerBack;

    private BallController bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = FindObjectOfType<BallController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        runVideoScreen.SetActive(true);
        jumpVideoScreen.SetActive(false);
        stopVideoScreen.SetActive(false);

        bc.allowjump = true;
        bc.allowRun = true;
        bc.Runbackward = false;

        speakerT.text = "";
        speakerBack.SetActive(false);
        AudioControl.Instance.playAudio();
    }
}
