using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class ActionEnabler : MonoBehaviour
{
    // required variables
    [SerializeField] Text speakerT;
    [SerializeField] GameObject speakerBack;

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
        tmm.SetRunOverride();

        speakerT.text = "";
        speakerBack.SetActive(false);
        AudioControl.Instance.playAudio();
    }
}
