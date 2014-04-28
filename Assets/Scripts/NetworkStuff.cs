using UnityEngine;
using System.Collections;

public class NetworkStuff : MonoBehaviour {
	public int Port = 25001;
	bool penisToggle =false;
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
	}
	public void CreateServer(string ip)
	{
		if(Network.peerType==NetworkPeerType.Disconnected)
		{
			Network.InitializeServer(5,Port);
		}
	}
	// Update is called once per frame
	void Update () 
	{
		if(Network.peerType==NetworkPeerType.Server)
		{
				
		}
		else if (Network.peerType==NetworkPeerType.Client)
		{
			if(penisToggle)
				networkView.RPC ("monPenis",RPCMode.Server);
		}
	}
	[RPC]
	void monPenis()
	{
		Debug.Log("monPenis");
	}
}
