using UnityEngine;
using System.Collections;

public class NetworkStuff : MonoBehaviour {
	public int Port = 25001;

	// Constant object
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
	}


	//2. Server creation
	public void CreateServer()
	{
		if( Network.peerType == NetworkPeerType.Disconnected )
		{
			Network.InitializeServer(5, Port); // @nb connexions max, @no. port
		}
	}

	//3. Client connection
	public void ConnectToServer(string ip)
	{
		if( Network.peerType == NetworkPeerType.Disconnected )
		{
			Network.Connect(ip, Port); // @ ip, @no. port
		}

		if ( Network.peerType == NetworkPeerType.Client ){
			startGameMessage(); // démarrage de partie
		}
	}
	
	//4. Démarrage de partie
	public void startGameMessage()
	{
		NetworkViewID viewID = Network.AllocateViewID();
		networkView.RPC ("startGame", RPCMode.All); //Tous chargent le niveau 1
	}


	//6. Update is called once per frame
	void Update () 
	{
		if(Application.loadedLevel == 1){ //si la partie est commencée
			//7. Deconnection de joueur
			if ( Network.peerType == NetworkPeerType.Disconnected || (Network.peerType == NetworkPeerType.Server && Network.connections.Length == 0) )
			{
				Application.LoadLevel( "MainMenu" );
				Destroy( this.gameObject );
			}

			//8. Update server
			else if (Network.peerType == NetworkPeerType.Server )
			{
				//9. Action si besoin
				if ( (Input.GetKeyUp(KeyCode.E) || Input.GetButtonUp("joystick 1 button 0")) )
				{
					networkView.RPC ( "actionKey", RPCMode.All, true, "Player1" ); //Player1 a fait une action
				}

				//11. Mouvement à chaque update
				networkView.RPC ( "moveKey",RPCMode.All, "Player1", Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1") ); //Player1 à bougé
			}

			//13. Update client
			else if (Network.peerType == NetworkPeerType.Client)
			{
				if ( (Input.GetKeyUp(KeyCode.E) || Input.GetButtonUp("joystick 1 button 0")) )
				{
					networkView.RPC ( "actionKey", RPCMode.All, true, "Player2" );
				}

				networkView.RPC ( "moveKey", RPCMode.All, "Player2", Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1") );
			}
		}
	}


//RPCs
	[RPC] //10. Player action
	void actionKey( bool isDown, string playerName )
	{
		GameObject obj = GameObject.Find(playerName); //Joueur 1 ou 2
		PlayerInventory other = (PlayerInventory) obj.GetComponent(typeof(PlayerInventory)); //Script approprié

		other.playerAction = true; //Objet joueur sais qu'il a fait une action.
	}

	[RPC] //12. Player movement
	void moveKey( string playerName, float hor, float vert )
	{
		GameObject obj = GameObject.Find(playerName); //Joueur 1 ou 2
		PlayerController other = (PlayerController) obj.GetComponent(typeof(PlayerController)); // Script approprié

		other._horizontal = hor; //Objet joueur sais qu'il s'est déplacé.
		other._vertical = vert;
	}

	[RPC] //5. Game start
	void startGame()
	{
		Application.LoadLevel("MainGame");
	}

}
