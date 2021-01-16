using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Canvas parentCanvas;

    // Start is called before the first frame update
    void Start()
    {
        parentCanvas.sortingOrder = 0;
        canvasGroup.alpha = 0;
    }

    public IEnumerator FadeOutScene(string sceneName)
    {
        parentCanvas.sortingOrder = 10;

        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / 1;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator FadeInScene()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / 1;
            yield return null;
        }

        parentCanvas.sortingOrder = 0;
    }
}
