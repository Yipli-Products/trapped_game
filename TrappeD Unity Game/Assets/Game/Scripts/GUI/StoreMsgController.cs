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

		if (ps.PurchasedBalls == "")
        {
			ps.PurchasedBalls = "0," + ps.Intended_ball_id;
		}
		else
        {
			ps.PurchasedBalls = ps.PurchasedBalls + "," + ps.Intended_ball_id;
		}

		balanceManager.SendMessage ("updateBalance");
		for(int i= 0; i<allBtn.Length; i++)allBtn[i].SendMessage("updateThePriceAndStateText");
		confirm ();

		UpdatePurchaseToDBAsync();
	}

	private void setThisBallAsActive(){
		//PlayerPrefs.SetInt ("ACTIVE_BALL", PlayerPrefs.GetInt ("INTENTED_BALL_ID"));
		ps.Active_ball = ps.Intended_ball_id;
	}

	private void UpdatePurchaseToDBAsync()
    {
		Dictionary<string, object> storeData;
		storeData = new Dictionary<string, object>();

		storeData.Add("coins-collected", ps.GetCoinScore().ToString());
		storeData.Add("active-ball", ps.Active_ball);
		storeData.Add("balls-purchased", ps.PurchasedBalls);
		storeData.Add("completed-levels", ps.GetCompletedLevels().ToString());

		//Debug.LogError("Display store data : " + JsonConvert.SerializeObject(storeData));

		PlayerSession.Instance.UpdateStoreData(storeData);
	}
}
