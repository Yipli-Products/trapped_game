using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSelecVirtual : MonoBehaviour
{
    //required variables
    LevelSelectMenumanagerYipli lsmmy;

    const string LEFT = "left";
    const string RIGHT = "right";
    const string ENTER = "enter";

    // Start is called before the first frame update
    void Start()
    {
        lsmmy = FindObjectOfType<LevelSelectMenumanagerYipli>();
    }

    public void vr()
    {
        lsmmy.ProcessMatInputs(RIGHT);
    }

    public void vl()
    {
        lsmmy.ProcessMatInputs(LEFT);
    }

    public void vj()
    {
        lsmmy.ProcessMatInputs(ENTER);
    }
}
