using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class StoreMsgController : MonoBehaviour {

	public Canvas msgCanvas;
	private GameObject balanceManager;

	private GameObject[] allBtn;

	[SerializeField] PlayerStats ps;

	[JsonIgnore]
	public YipliConfig currentYipliConfig;

	// Use this for initialization
	void Start () {
		balanceManager = GameObject.Find ("BalanceManager");
		allBtn = GameObject.FindGameObjectsWithTag ("BALL_PRICE");
	}

	public void confirm(){
		msgCanvas.enabled = false;
	}

	public void buyThisBall(){
		/*PlayerPrefs.SetInt ("Coins", PlayerPrefs.GetInt ("Coins") - PlayerPrefs.GetInt("INTENTED_BALL_PRICE"));
		PlayerPrefs.SetInt ("BOUGHT_BALL_" + PlayerPrefs.GetInt ("INTENTED_BALL_ID").ToString(), 1);*/

		ps.SetCoinScore(ps.GetCoinScore() - ps.Intended_ball_price);
		ps.SetBallBoughtStatus(ps.Intended_ball_id, true);
		
		balanceManager.SendMessage ("updateBalance");
		for(int i= 0; i<allBtn.Length; i++)allBtn[i].SendMessage("updateThePriceAndStateText");
		confirm ();

		UpdatePurchaseToDBAsync();
	}

	private void setThisBallAsActive(){
		//PlayerPrefs.SetInt ("ACTIVE_BALL", PlayerPrefs.GetInt ("INTENTED_BALL_ID"));
		ps.Active_ball = ps.Intended_ball_id;
	}

	private async Task UpdatePurchaseToDBAsync()
    {
		Dictionary<string, System.Object> storeData;
		storeData = new Dictionary<string, System.Object>();

		storeData.Add("userid", currentYipliConfig.userId);
		storeData.Add("playerid", currentYipliConfig.playerInfo.playerId);
		storeData.Add("store-data", ps.GetBallInStoreList());

		await PlayerSession.Instance.UpdateStoreData("trapped", storeData);

		Debug.LogError("Done Updating the data");
	}
}
