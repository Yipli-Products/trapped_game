using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSawLR : MonoBehaviour
{
    // required variables
    private Vector2 StartPos;
    private Vector2 EndPos;

    private float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
        EndPos = new Vector3(transform.position.x + 6, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        MoveSaw();
    }

    private void MoveSaw()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(StartPos, EndPos, step);
    }
}
