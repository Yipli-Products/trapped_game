using UnityEngine.UI;
using UnityEngine;

public class RunAgain : MonoBehaviour
{
    // required variables
    [SerializeField] GameObject obstacleBoxSmall;
    [SerializeField] GameObject leftMoveCol;
    [SerializeField] Text speakerT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.activeSelf)
        {
            speakerT.text = "Run again to move further";
            AudioControl.Instance.playAudio();
            leftMoveCol.SetActive(false);

            obstacleBoxSmall.GetComponent<Rigidbody2D>().mass = 1;
        }
    }
}
