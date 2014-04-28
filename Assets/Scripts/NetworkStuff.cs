using UnityEngine;
using System.Collections;

public class NetworkStuff : MonoBehaviour {
	public int Port = 25001;
	public bool penisToggle = false;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
	}


	public void ConnectToServer(string ip)
	{
		if(Network.peerType==NetworkPeerType.Disconnected)
		{
			Network.Connect(ip,Port);
			Debug.Log("MES PENISS");
		}

		if (Network.peerType==NetworkPeerType.Client)
			startGameMessage();
	}
	public void CreateServer(string ip)
	{
		if(Network.peerType==NetworkPeerType.Disconnected)
		{
			Network.InitializeServer(5,Port);
		}
	}

	public void startGameMessage()
	{
		NetworkViewID viewID = Network.AllocateViewID();
		networkView.RPC ("startGame",RPCMode.All);
	}
	// Update is called once per frame
	void Update () 
	{
		if(Network.peerType==NetworkPeerType.Server)
		{
				
		}
		else if (Network.peerType==NetworkPeerType.Client)
		{
			if(penisToggle){
				NetworkViewID viewID = Network.AllocateViewID();
				networkView.RPC ("monPenis",RPCMode.Server);
				penisToggle = false;
			}
		}
	}
	[RPC]
	void monPenis()
	{
		Debug.Log("monPenis");
	}
	[RPC]
	void startGame()
	{
		Application.LoadLevel("MainGame");
	}

}
