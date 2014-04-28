using UnityEngine;
using System.Collections;

public class NetworkStuff : MonoBehaviour {
	public int Port = 25001;
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
				if(Network.connections.Length>0)
				Debug.Log("client connectés: "+ Network.connections.Length);
			}
	}
}
