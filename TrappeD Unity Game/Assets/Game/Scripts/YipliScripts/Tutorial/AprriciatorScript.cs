using UnityEngine.UI;
using UnityEngine;

public class AprriciatorScript : MonoBehaviour
{
    // required variables
    [SerializeField] Text speakerT;
    [SerializeField] GameObject speakerBack;

    private string[] sentances = {
        "Good one, Keep Running",
        "Perfect, Keep it up",
        "Bravo, Well Done",
        "Good one, Champ"
    };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "jumpTUT")
        {
            speakerBack.SetActive(true);
            speakerT.text = "Jump";
        }
        else
        {
            speakerT.text = sentances[Random.Range(0, sentances.Length)];
        }
    }
}
