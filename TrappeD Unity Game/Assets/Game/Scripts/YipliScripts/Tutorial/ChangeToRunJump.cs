using UnityEngine.UI;
using UnityEngine;

public class ChangeToRunJump : MonoBehaviour
{
    // required variables
    [SerializeField] Text AIText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AIText.text = "Run, Stop or Jump";
    }
}
