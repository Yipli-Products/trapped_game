using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public YipliConfig currentYipliConfig;
    private void Awake()
    {
        currentYipliConfig.gameId = "trapped";
        //currentYipliConfig.currentGameType = YipliUtils.GameType.ADVENTURE_GAMING;
    }
}
