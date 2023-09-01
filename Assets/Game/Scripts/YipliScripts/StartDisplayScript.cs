using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput.PlatformSpecific;

public class StartDisplayScript : MonoBehaviour
{
    // required variables
    [SerializeField] Image iOne;
    [SerializeField] Image iTwo;
    [SerializeField] Image iThree;

    [SerializeField] Sprite starFilledSmall;
    [SerializeField] Sprite starFilledBig;
    [SerializeField] Sprite starUnFilled;

    int collectedPoints;
    int thisLevelPoints;

    // Start is called before the first frame update
    void Start()
    {
        thisLevelPoints = PlayerPrefs.GetInt("currentStars");
        collectedPoints = PlayerPrefs.GetInt("thisLevelPoints") / 10;

        displayImages();
    }

    private void displayImages ()
    {
        int percentage = (collectedPoints * 100) / thisLevelPoints;

        if (percentage < 34)
        {
            iOne.sprite = starFilledSmall;
            iTwo.sprite = starUnFilled;
            iThree.sprite = starUnFilled;
        }
        else if (percentage >= 34 && percentage < 67)
        {
            iOne.sprite = starFilledSmall;
            iTwo.sprite = starFilledBig;
            iThree.sprite = starUnFilled;
        }
        else if (percentage >= 67)
        {
            iOne.sprite = starFilledSmall;
            iTwo.sprite = starFilledBig;
            iThree.sprite = starFilledSmall;
        }
        else
        {
            iOne.sprite = starUnFilled;
            iTwo.sprite = starUnFilled;
            iThree.sprite = starUnFilled;
        }
    }
}
