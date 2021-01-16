using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BalanceManager : MonoBehaviour {

	public Text balanceText;

	[SerializeField] PlayerStats ps;

	// Use this for initialization
	void Start () {
		balanceText.text = ps.GetCoinScore().ToString();
		SetStore();
	}
	
	// Update is called once per frame
	void updateBalance () {
		balanceText.text = ps.GetCoinScore().ToString();
	}

	private void SetStore()
    {
		if (ps.PurchasedBalls == "" || ps.PurchasedBalls == null)
        {
			ps.SetDefaultStore();
        }
		else
        {
			string[] ballIDs = ps.PurchasedBalls.Split(',');
			List<int> pBallNos = new List<int>();

			for (int i = 0; i < ballIDs.Length; i++)
			{
				pBallNos.Add(int.Parse(ballIDs[i]));
			}

			ps.SetListofBalls(pBallNos);
		}
	}

	public void GotoMainMenu()
	{
		MatControlsStatManager.gameStateChanged(GameState.GAME_UI);
		StartCoroutine(FindObjectOfType<Transition>().FadeOutScene("Main Menu"));
	}
}
