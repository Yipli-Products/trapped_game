using UnityEngine.UI;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class JumpAction : MonoBehaviour
{
    //required variables
    [SerializeField] GameObject runVideoScreen;
    [SerializeField] GameObject jumpVideoScreen;
    [SerializeField] GameObject stopVideoScreen;
    [SerializeField] Text speakerT;

    private BallController bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = FindObjectOfType<BallController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        runVideoScreen.SetActive(false);
        jumpVideoScreen.SetActive(true);
        stopVideoScreen.SetActive(false);

        bc.allowjump = true;
        bc.allowRun = false;
        bc.Runbackward = false;

        speakerT.text = "Stop and Jump to cross the hurdles";
    }
}
