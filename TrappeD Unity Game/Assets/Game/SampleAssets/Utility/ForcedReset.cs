using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySampleAssets.CrossPlatformInput;


[RequireComponent(typeof(UnityEngine.UI.Image))]
public class ForcedReset : MonoBehaviour {

    void Update () {
        
        // if we have forced a reset ...
        if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
        {

            //... reload the scene
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }

}
