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

		if (Network.peerType==NetworkPeerType.Client){
			startGameMessage();
		}
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

		if ((Input.GetKeyUp(KeyCode.E) || Input.GetButtonUp("joystick 1 button 0")))
		 {
			if(Network.peerType==NetworkPeerType.Server)
			{
				NetworkViewID viewID = Network.AllocateViewID();
				networkView.RPC ("actionKey",RPCMode.All,true, "Player1");
			}
			else if (Network.peerType==NetworkPeerType.Client)
			{

				NetworkViewID viewID = Network.AllocateViewID();
				networkView.RPC ("actionKey",RPCMode.All,true, "Player2");

			}
		}
	}
	[RPC]
	void actionKey(bool isDown, string playerName)
	{
		GameObject obj =GameObject.Find(playerName);
		PlayerInventory other =(PlayerInventory) obj.GetComponent(typeof(PlayerInventory));
		other.playerAction=true;
	}

	[RPC]
	void startGame()
	{
		Application.LoadLevel("MainGame");
	}

}
