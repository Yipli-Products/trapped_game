using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepBackStart : MonoBehaviour
{
    [SerializeField] Text speakerText;
    [SerializeField] GameObject speakerGO;

    private void Start()
    {
        speakerGO.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("stepBackStart"))
        {
            speakerGO.SetActive(true);
            speakerText.text = "If you are trapped, then Stand Still to GO BACK";
        }
        else if (gameObject.CompareTag("stepBackEnd"))
        {
            speakerGO.SetActive(false);
        }
    }
}
