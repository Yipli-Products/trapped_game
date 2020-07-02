using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class ActionEnabler : MonoBehaviour
{
    // required variables
    [SerializeField] Text AIText;
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
        bc.allowjump = true;
        bc.allowRun = true;
        bc.Runbackward = false;

        AIText.text = "Run or Stop to Jump";

        speakerT.text = "";
        speakerBack.SetActive(false);
        AudioControl.Instance.playAudio();
    }
}
