using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeManager : MonoBehaviour
{
    [SerializeField] private GameObject resumeManagerParent = null;

    public void ChangePosAwayFromMat() {
        resumeManagerParent.transform.localPosition = new Vector3(resumeManagerParent.transform.localPosition.x, resumeManagerParent.transform.localPosition.y, 1f);
    }

    public void ChangePosToMat() {
        resumeManagerParent.transform.localPosition = new Vector3(resumeManagerParent.transform.localPosition.x, resumeManagerParent.transform.localPosition.y, 0f);
    }
}
