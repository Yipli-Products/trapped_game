using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutModelManager : MonoBehaviour
{
    [SerializeField] GameObject tutModel;

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
        tutModel.SetActive(true);
    }
    
    public void DeActivateModel()
    {
        tutModel.SetActive(false);
    }

    public void SetRunOverride()
    {
        tutModel.GetComponent<Animator>().runtimeAnimatorController = runOR;
    }
    
    public void SetJumpOverride()
    {
        tutModel.GetComponent<Animator>().runtimeAnimatorController = jumpOR;
    }
}
