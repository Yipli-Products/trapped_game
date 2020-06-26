using UnityEngine.UI;
using UnityEngine;

public class AprriciatorScript : MonoBehaviour
{
    // required variables
    [SerializeField] Text speakerT;

    private string[] sentances = {
        "Good one, Keep Running",
        "Perfect, Keep it up",
        "Bravo, Done exactly as expected",
        "Good one, Champ"
    };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        speakerT.text = sentances[Random.Range(0, sentances.Length)];
    }
}
