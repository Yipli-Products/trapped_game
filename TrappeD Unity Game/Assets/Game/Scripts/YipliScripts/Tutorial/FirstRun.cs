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

    private AudioControl ac;

    private void Awake()
    {
        speakerT.text = "";
        tmm = FindObjectOfType<TutModelManager>();
        ac = FindObjectOfType<AudioControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        speakerBack.SetActive(false);

        bc = FindObjectOfType<BallController>();

        speakerT.text = "";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            speakerBack.SetActive(true);

            tmm.ActivateModel();
            tmm.SetRunOverride();

            speakerT.text = "Run to move Forward";
            ac.PlayRunAudio();

            bc.calWaitTime = false;
            bc.waitTimeCal = 0f;
        }
    }
}
