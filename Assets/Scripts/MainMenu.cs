using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GameObject obj;
	public string stringToEdit = "Ip address";
	void OnGUI () {
		GUI.backgroundColor = Color.yellow;

		stringToEdit = GUI.TextField (new Rect (10, 10, 200, 20), stringToEdit, 25);
		if (GUI.Button (new Rect (Screen.width*0.29f, Screen.height*0.58f, Screen.width*0.2f, Screen.height*0.15f), "Client")) {
			NetworkStuff other =(NetworkStuff) obj.GetComponent(typeof(NetworkStuff));
			other.ConnectToServer(stringToEdit);
			Application.LoadLevel("MainGame");

		}

		if (GUI.Button (new Rect (Screen.width*0.49f, Screen.height*0.58f, Screen.width*0.2f, Screen.height*0.15f), "Server")) {
			NetworkStuff other =(NetworkStuff) obj.GetComponent(typeof(NetworkStuff));
			other.CreateServer(stringToEdit);
			Application.LoadLevel("MainGame");
			
		}
	}
}
