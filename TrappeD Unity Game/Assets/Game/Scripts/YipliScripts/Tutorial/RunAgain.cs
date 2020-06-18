using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAgain : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject obstacleBoxBig;
    [SerializeField] GameObject obstacleBoxSmall;

    // Start is called before the first frame update
    void Start()
    {
        obstacleBoxBig.SetActive(true);
        obstacleBoxSmall.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.activeSelf)
        {
            obstacleBoxBig.SetActive(false);
            obstacleBoxSmall.SetActive(true);
        }
    }
}
