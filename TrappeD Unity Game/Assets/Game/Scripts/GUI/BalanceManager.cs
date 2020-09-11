using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BalanceManager : MonoBehaviour {

	public Text balanceText;

	[SerializeField] PlayerStats ps;

	// Use this for initialization
	void Start () {
		balanceText.text = "Balance: $ " + ps.GetCoinScore();
	}
	
	// Update is called once per frame
	void updateBalance () {
		balanceText.text = "Balance: $ " + ps.GetCoinScore();
	}
}
