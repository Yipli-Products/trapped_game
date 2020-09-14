using UnityEngine.UI;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class JumpAction : MonoBehaviour
{
    //required variables
    [SerializeField] Text speakerT;

    private BallController bc;
    private TutModelManager tmm;

    // Start is called before the first frame update
    void Start()
    {
        bc = FindObjectOfType<BallController>();
        tmm = FindObjectOfType<TutModelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tmm.ActivateModel();
        tmm.SetJumpOverride();

        bc.allowjump = true;
        bc.allowRun = false;
        bc.Runbackward = false;

        speakerT.text = "Stop and Jump to cross the hurdles";
    }
}
