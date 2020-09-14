using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class FirstLeftMove : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject RunAgainCol;

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
            speakerT.text = "If you want to go backwards, Just stand still.";
            AudioControl.Instance.playAudio();

            RunAgainCol.SetActive(true);
        }
    }

    private IEnumerator TextChange()
    {
        speakerT.text = "If you want to go backwards, Just stand still.";
        AudioControl.Instance.playAudio();
        yield return new WaitForSecondsRealtime(5f);

        speakerT.text = "Stand still to move backwards.";
        AudioControl.Instance.playAudio();
        yield return new WaitForSecondsRealtime(5f);
    }
}
