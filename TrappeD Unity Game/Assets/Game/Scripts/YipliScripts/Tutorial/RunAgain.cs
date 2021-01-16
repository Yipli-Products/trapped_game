using UnityEngine.UI;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class RunAgain : MonoBehaviour
{
    // required variables

    [SerializeField] GameObject obstacleBoxSmall;
    [SerializeField] GameObject leftMoveCol;
    [SerializeField] GameObject runAgainCol;
    [SerializeField] GameObject jumpCol;
    [SerializeField] Text speakerT;

    private BallController bc;
    private TutModelManager tmm;

    // Start is called before the first frame update
    void Start()
    {
        bc = FindObjectOfType<BallController>();
        tmm = FindObjectOfType<TutModelManager>();

        jumpCol.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.activeSelf)
        {
            tmm.ActivateModel();
            tmm.SetRunOverride();

            speakerT.text = "Run and Jump Smartly to move forward";
            AudioControl.Instance.playAudio();
            jumpCol.SetActive(true);
            leftMoveCol.SetActive(false);
            runAgainCol.SetActive(false);

            obstacleBoxSmall.GetComponent<Rigidbody2D>().mass = 1;
        }
    }
}
