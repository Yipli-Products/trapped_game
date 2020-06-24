using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeToJumpText : MonoBehaviour
{
    // required variables
    [SerializeField] Text AIText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AIText.text = "Jump";
    }
}
