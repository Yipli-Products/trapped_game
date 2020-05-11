using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStats/playerData")]
public class PlayerStats : ScriptableObject
{
    private string playerName;
    private int completed_levels;
    private int coinScore;

    public void SetPlayerName(string pname)
    {
        playerName = pname;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetCompletedLevels(int CompletedLevels)
    {
        completed_levels = CompletedLevels;
    }

    public int GetCompletedLevels()
    {
        return completed_levels;
    }

    public void SetCoinScore(int cs)
    {
        coinScore = cs;
    }

    public int GetCoinScore()
    {
        return coinScore;
    }
}
