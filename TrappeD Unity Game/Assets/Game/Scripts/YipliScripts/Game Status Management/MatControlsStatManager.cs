using UnityEngine;

public class MatControlsStatManager : MonoBehaviour
{
    [SerializeField] PlayerStats ps;

    public delegate void OnGameStateChange(GameState newState);
    public static OnGameStateChange gameStateChanged;

    private void OnEnable()
    {
        gameStateChanged += GameMatControlsDelegate;
    }

    private void OnDisable()
    {
        gameStateChanged -= GameMatControlsDelegate;
    }

    private void GameMatControlsDelegate(GameState newState)
    {
        ps.GameState = newState;

        switch (newState) 
        {
            case GameState.GAME_UI:
                YipliHelper.SetGameClusterId(0);
                break;

            case GameState.GAME_PLAY:
                YipliHelper.SetGameClusterId(1);
                break;

            case GameState.GAME_NEW_LIFE:
                YipliHelper.SetGameClusterId(0);
                YipliHelper.SetGameClusterId(1);
                break;

            case GameState.GAME_PAUSED:
                YipliHelper.SetGameClusterId(0);
                break;

            case GameState.GAME_OVER:
                YipliHelper.SetGameClusterId(0);
                break;

            default:
                break;
        }
    }
}
