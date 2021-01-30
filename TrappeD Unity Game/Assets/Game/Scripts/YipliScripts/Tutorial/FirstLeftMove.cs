using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class FirstLeftMove : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject RunAgainCol;
    [SerializeField] GameObject jumpbackCol;
    [SerializeField] GameObject jumpbacktwoCol;

    [SerializeField] Text speakerT;
    [SerializeField] GameObject speakerBack;

    private BallController bc;
    private TutModelManager tmm;

    private AudioControl ac;

    // Start is called before the first frame update
    void Start()
    {
        bc = FindObjectOfType<BallController>();
        tmm = FindObjectOfType<TutModelManager>();
        ac = FindObjectOfType<AudioControl>();

        RunAgainCol.SetActive(false);
        jumpbackCol.SetActive(false);
        jumpbacktwoCol.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tmm.DeActivateModel();

            speakerBack.SetActive(true);
            speakerT.text = "Just stand still to go backwards";
            ac.PlayStopAudio();

            RunAgainCol.SetActive(true);
            jumpbackCol.SetActive(true);
            jumpbacktwoCol.SetActive(false);
        }
    }
}
