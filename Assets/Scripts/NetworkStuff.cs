using UnityEngine;
using System.Collections;

public class NetworkStuff : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
	}


	public void ConnectToServer(string ip)
	{
		Debug.Log("MES PENISS");
	}
	// Update is called once per frame
	void Update () {
	
	}
}
