using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.LogError("Scene " + SceneManager.GetActiveScene().buildIndex + " is restarting.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
