using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class FirstRun : MonoBehaviour
{
    // required variables
    [SerializeField] Text speakerT;
    [SerializeField] GameObject speakerBack;

    private BallController bc;
    private TutModelManager tmm;

    private void Awake()
    {
        speakerT.text = "";
        tmm = FindObjectOfType<TutModelManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        speakerBack.SetActive(false);

        bc = FindObjectOfType<BallController>();

        speakerT.text = "";
        AudioControl.Instance.playAudio();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            speakerBack.SetActive(true);

            tmm.ActivateModel();
            tmm.SetRunOverride();

            speakerT.text = "Run to move Forward";
            AudioControl.Instance.playAudio();

            bc.calWaitTime = false;
            bc.waitTimeCal = 0f;

            bc.allowjump = false;
            bc.allowRun = true;
            bc.Runbackward = false;
        }
    }
}
