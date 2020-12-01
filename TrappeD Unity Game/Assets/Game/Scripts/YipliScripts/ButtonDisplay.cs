using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerSession.Instance.currentYipliConfig.onlyMatPlayMode)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
