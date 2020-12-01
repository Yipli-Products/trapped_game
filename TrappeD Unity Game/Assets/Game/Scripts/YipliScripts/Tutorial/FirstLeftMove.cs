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

    // Start is called before the first frame update
    void Start()
    {
        bc = FindObjectOfType<BallController>();
        tmm = FindObjectOfType<TutModelManager>();

        RunAgainCol.SetActive(false);
        jumpbackCol.SetActive(false);
        jumpbacktwoCol.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tmm.DeActivateModel();

            bc.allowjump = false;
            bc.allowRun = false;
            bc.Runbackward = true;

            speakerBack.SetActive(true);
            speakerT.text = "Opps, we missed that box to cross over these boxes.";
            AudioControl.Instance.playAudio();

            RunAgainCol.SetActive(true);
            jumpbackCol.SetActive(true);
            jumpbacktwoCol.SetActive(false);

            StartCoroutine(TextChange());
        }
    }

    private IEnumerator TextChange()
    {
        yield return new WaitForSecondsRealtime(5f);

        speakerT.text = "Just stand still to go backwards";
        AudioControl.Instance.playAudio();
        yield return new WaitForSecondsRealtime(5f);

        /*
        speakerT.text = "Just stand still to go backwards";
        AudioControl.Instance.playAudio();
        yield return new WaitForSecondsRealtime(5f);
        */
    }
}
