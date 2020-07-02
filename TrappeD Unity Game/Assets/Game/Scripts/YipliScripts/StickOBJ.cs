using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickOBJ : MonoBehaviour
{
    [SerializeField] GameObject TempColl;

    private void Start()
    {
        TempColl.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "StickeyObj") {
            TempColl.SetActive(true);
            col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
