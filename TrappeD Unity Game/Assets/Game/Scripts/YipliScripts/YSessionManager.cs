using UnityEngine;
using UnityEngine.SceneManagement;

public class YSessionManager : MonoBehaviour
{
     // Start is called before the first frame update
    void Start()
    {
        PlayerSession.Instance.StartSPSession("trapped");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSession.Instance.UpdateDuration();
    }

    public void StoreSession() {
        PlayerSession.Instance.StoreSPSession(PlayerPrefs.GetInt("Coins"));
    }
}
