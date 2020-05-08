using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJDetection : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject boxObstacle;

    private int detectionTracker;

    private string playerName = "Ball";

    // Start is called before the first frame update
    void Start()
    {
        detectionTracker = 0;
        boxObstacle.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.name == playerName) && (detectionTracker == 0))
        {
            boxObstacle.SetActive(true);
        }

        detectionTracker++;
    }
}
