using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class GameTuner : MonoBehaviour
{
    // required variables
    [SerializeField] InputField angDrag;
    [SerializeField] InputField matBSpeed;

    private BallController bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = FindObjectOfType<BallController>();
    }

    public void SetValuesButton()
    {
        bc.matBallForce = float.Parse(matBSpeed.text);
        bc.GetComponent<Rigidbody2D>().angularDrag = float.Parse(angDrag.text);
    }
}
