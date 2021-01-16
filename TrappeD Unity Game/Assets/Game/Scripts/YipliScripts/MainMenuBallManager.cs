using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBallManager : MonoBehaviour
{
    [SerializeField] PlayerStats ps;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] Sprite[] ballSprites;

    private void Update()
    {
        spriteRenderer.sprite = ballSprites[ps.Active_ball];
    }
}
