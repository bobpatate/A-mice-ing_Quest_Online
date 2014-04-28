using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	void OnGUI () {
		GUI.backgroundColor = Color.yellow;
		if (GUI.Button (new Rect (Screen.width*0.29f, Screen.height*0.58f, Screen.width*0.2f, Screen.height*0.15f), "PLAY")) {
			Application.LoadLevel("MainGame");
		}
	}
}
