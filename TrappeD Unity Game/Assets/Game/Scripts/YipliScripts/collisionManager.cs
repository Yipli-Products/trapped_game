using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionManager : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject secondCollider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            secondCollider.SetActive(false);
        }
    }
}
