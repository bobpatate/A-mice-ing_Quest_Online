using UnityEngine;
using System.Collections;

public class NetworkStuff : MonoBehaviour {
	public string stringToEdit = "Ip address";
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
	}
	void OnGUI () {
		GUI.backgroundColor = Color.yellow;
		stringToEdit = GUI.TextField (new Rect (10, 10, 200, 20), stringToEdit, 25);

	}

	public void ConnectToServer()
	{
		Debug.Log("MES PENISS");
	}
	// Update is called once per frame
	void Update () {
	
	}
}
