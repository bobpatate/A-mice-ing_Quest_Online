/*
	MULTIJOUEUR AVEC UNITY 3D
	1. Utilisation concrète (exemple)
	2. Utilisation concrète (explications)
	2. Remote Procedure Calls vs State Synchronization
	3. Unity vs UDK
*/


using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GameObject obj;
	public string stringToEdit = "127.0.0.1";
	void OnGUI () {
		GUI.backgroundColor = Color.yellow;

		stringToEdit = GUI.TextField (new Rect (10, 10, 200, 20), stringToEdit, 25);

		//1. Client
		if (GUI.Button (new Rect (Screen.width*0.29f, Screen.height*0.58f, Screen.width*0.2f, Screen.height*0.15f), "Client")) {

			NetworkStuff other =(NetworkStuff) obj.GetComponent(typeof(NetworkStuff));
			other.ConnectToServer(stringToEdit);
		
		}

		//1. Server
		if (GUI.Button (new Rect (Screen.width*0.49f, Screen.height*0.58f, Screen.width*0.2f, Screen.height*0.15f), "Server")) {

			NetworkStuff other =(NetworkStuff) obj.GetComponent(typeof(NetworkStuff));
			other.CreateServer();

		}
	}
}
