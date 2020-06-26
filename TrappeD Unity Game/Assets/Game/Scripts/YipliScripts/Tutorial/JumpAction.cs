using UnityEngine.UI;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class JumpAction : MonoBehaviour
{
    //required variables
    [SerializeField] Text AIText;
    [SerializeField] Text speakerT;

    private BallController bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = FindObjectOfType<BallController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0.1f;

        bc.allowjump = true;
        bc.allowRun = true;
        bc.allowStop = false;

        AIText.text = "Stop and Jump";

        speakerT.text = "Stop and Jump on MAT to cross the hurdles";
    }
}
