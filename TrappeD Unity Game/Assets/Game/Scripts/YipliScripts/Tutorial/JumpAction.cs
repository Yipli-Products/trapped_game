using UnityEngine.UI;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class JumpAction : MonoBehaviour
{
    //required variables
    [SerializeField] Text speakerT;

    private BallController bc;
    private TutModelManager tmm;

    private AudioControl ac;

    // Start is called before the first frame update
    void Start()
    {
        bc = FindObjectOfType<BallController>();
        tmm = FindObjectOfType<TutModelManager>();
        ac = FindObjectOfType<AudioControl>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tmm.ActivateModel();
        tmm.SetJumpOverride();

        speakerT.text = "Jump to cross the hurdles";
        ac.PlayJumpAudio();
    }
}
