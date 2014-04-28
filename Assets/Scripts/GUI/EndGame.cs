using UnityEngine;
using System.Collections;
using System.Linq;

public class EndGame : MonoBehaviour {
	string[] teamsToCheck = {"Red", "Blue"};
	public string winner = "";
	public GUIStyle myStyle;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] merchants = GameObject.FindGameObjectsWithTag ("Merchant");

		winner = "";
		/*
		foreach (string team in teamsToCheck) {
			if(merchants.Count(m => m.GetComponent<Enemy>().team == team) == 0) {
				winner = team == "Red" ? "Blue" : "Red";
			}
		}
		*/
		if (winner != "") {
			Time.timeScale = 0f;
		}
	}

	void OnGUI() {

		myStyle.alignment = TextAnchor.MiddleCenter;
		myStyle.fontSize = 40;
		myStyle.fontStyle = FontStyle.Bold;
		myStyle.normal.textColor = Color.white;

		if (winner != "") {
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), winner + " WIN!!!", myStyle);
		}
	}
}
