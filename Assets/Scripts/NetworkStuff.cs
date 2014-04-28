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

		if(Application.loadedLevel == 1){
			if (Network.peerType==NetworkPeerType.Disconnected ||(Network.peerType==NetworkPeerType.Server&&Network.connections.Length==0))
			{
				Application.LoadLevel("MainMenu");
				Destroy(this.gameObject);
			}
			else if(Network.peerType==NetworkPeerType.Server)
			{
				NetworkViewID viewID = Network.AllocateViewID();
				if ((Input.GetKeyUp(KeyCode.E) || Input.GetButtonUp("joystick 1 button 0")))
				{
					networkView.RPC ("actionKey",RPCMode.All,true, "Player1");
				}
				networkView.RPC ("moveKey",RPCMode.All, "Player1",Input.GetAxis("Horizontal1"),Input.GetAxis("Vertical1"));
			}
			else if (Network.peerType==NetworkPeerType.Client)
			{
				NetworkViewID viewID = Network.AllocateViewID();
				if ((Input.GetKeyUp(KeyCode.E) || Input.GetButtonUp("joystick 1 button 0")))
				{

					networkView.RPC ("actionKey",RPCMode.All,true, "Player2");
				}
					networkView.RPC ("moveKey",RPCMode.All, "Player2",Input.GetAxis("Horizontal1"),Input.GetAxis("Vertical1"));
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
	void moveKey(string playerName, float hor,float vert)
	{
		GameObject obj =GameObject.Find(playerName);
		PlayerController other =(PlayerController) obj.GetComponent(typeof(PlayerController));
		other._horizontal = hor;
		other._vertical = vert;
	}

	[RPC]
	void startGame()
	{
		Application.LoadLevel("MainGame");
	}



}
