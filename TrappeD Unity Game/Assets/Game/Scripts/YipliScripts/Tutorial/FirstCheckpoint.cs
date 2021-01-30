using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class FirstCheckpoint : MonoBehaviour
{
    //required variables
    [SerializeField] GameObject checkpointFrame;
 
    [SerializeField] Text speakerT;

    private BallController bc;
    private TutModelManager tmm;

    bool calledAnim = false;

    // Start is called before the first frame update
    void Start()
    {
        bc = FindObjectOfType<BallController>();
        tmm = FindObjectOfType<TutModelManager>();

        checkpointFrame.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!calledAnim)
            {
                tmm.DeActivateModel();

                speakerT.text = "If you die, Game will resume\nfrom last checkpoint";

                calledAnim = true;

                Time.timeScale = 1f;
                //StartCoroutine(frameAnimation());
            }
            else
            {
                print("Checkpoint tutorial is done already.");
            }
        }
    }

    IEnumerator frameAnimation()
    {
        checkpointFrame.SetActive(true);

        MatControlsStatManager.gameStateChanged(GameState.GAME_UI);
        yield return new WaitForSecondsRealtime(5f);
        MatControlsStatManager.gameStateChanged(GameState.GAME_PLAY);

        checkpointFrame.SetActive(false);

        speakerT.text = "Keep Running";

        tmm.ActivateModel();
        tmm.SetRunOverride();

        Time.timeScale = 1f;
    }
}
