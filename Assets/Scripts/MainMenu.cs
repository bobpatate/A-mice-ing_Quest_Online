using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GameObject obj;
	void OnGUI () {
		GUI.backgroundColor = Color.yellow;
		if (GUI.Button (new Rect (Screen.width*0.29f, Screen.height*0.58f, Screen.width*0.2f, Screen.height*0.15f), "PLAY")) {
			NetworkStuff other =(NetworkStuff) obj.GetComponent(typeof(NetworkStuff));
			other.ConnectToServer();
			Application.LoadLevel("MainGame");

		}
	}
}
