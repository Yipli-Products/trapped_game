using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickOBJ : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "StickeyObj") {
            col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
