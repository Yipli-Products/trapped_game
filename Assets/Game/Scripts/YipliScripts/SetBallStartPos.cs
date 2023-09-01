using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetBallStartPos : MonoBehaviour
{
    [SerializeField] GameObject ballOBJ;
    [SerializeField] PlayerStats ps;

    string currentLevel = null;

    private void Awake()
    {
        currentLevel = SceneManager.GetActiveScene().name;
        int playerLife = ps.PlayerLives;

        //Debug.LogError("Player Life : " + playerLife);
        //Debug.LogError("currentlevel : " + currentLevel);
        //Debug.LogError("checkpoint sttus : " + ps.CheckPointPassed);

        if (ps.CheckPointPassed)
        {
            //gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("CHKP_X"), 3, PlayerPrefs.GetFloat("CHKP_Z"));
            //gameObject.transform.position = new Vector3(ps.CheckPointPos.x, 3, ps.CheckPointPos.z);

            //Debug.LogError("ps.CheckPointPos : " + ps.CheckPointPos);

            ballOBJ.transform.localPosition = ps.CheckPointPos;
        }
        else
        {
            //gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("CHKP_X"), PlayerPrefs.GetFloat("CHKP_Y"), PlayerPrefs.GetFloat("CHKP_Z"));
            ballOBJ.transform.position = gameObject.transform.position;
            ps.CheckPointPassed = false;
        }
    }
}
