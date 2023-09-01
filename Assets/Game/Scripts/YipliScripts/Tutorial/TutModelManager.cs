using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutModelManager : MonoBehaviour
{
    [SerializeField] GameObject tutModelRun;
    [SerializeField] GameObject tutModelJump;

    [SerializeField] AnimatorOverrideController runOR;
    [SerializeField] AnimatorOverrideController jumpOR;

    Vector3 startPos;
    Vector3 modelPos;

    void Start()
    {
        DeActivateModel();
        startPos = transform.position;
        modelPos = Camera.main.transform.position;
    }

    void Update()
    {
        /*transform.position = Camera.main.ViewportToWorldPoint(startPos);
        transform.rotation = Camera.main.transform.rotation;*/

        modelPos = new Vector3(Camera.main.transform.position.x - 8, Camera.main.transform.position.y - 1, Camera.main.transform.position.z + 5);
        transform.position = modelPos;
    }

    public void ActivateModel()
    {
        tutModelRun.SetActive(true);
    }
    
    public void DeActivateModel()
    {
        tutModelRun.SetActive(false);
        tutModelJump.SetActive(false);
    }

    public void SetRunOverride()
    {
        DeActivateModel();
        tutModelRun.SetActive(true);
        tutModelRun.GetComponent<Animator>().runtimeAnimatorController = runOR;
    }
    
    public void SetJumpOverride()
    {
        //tutModelRun.GetComponent<Animator>().runtimeAnimatorController = jumpOR;
        tutModelRun.SetActive(false);
        tutModelJump.SetActive(true);
    }
}
