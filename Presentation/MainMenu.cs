/*
	MULTIJOUEUR AVEC UNITY 3D
	1. Utilisation concrète (http://gabrielhebert.com/realisations/interactif/a-mice-ing_quest_arena_online/Build_Online.html)
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


/*

Deux choses pour créer un réseau:
	1. Quel genre de données envoyer par le NetworkView.
	2. Comment les envoyer.


NetworkView params:
	State Synchronization
		Off							- RPC seulement.
		Reliable Delta Compressed	- Envoi seulement les différences. Packets ordonnés (TCP).
		Unreliable					- Envoi l'état complet. Packets désordonnés (UDP). 
	Observed						- Le composant d'objet qu'on veut synchroniser (Transform, Animation, RigidBody, script).
	View ID
	

RPC:
	 1. Seulement besoin d'un composant NetworkView sur le meme GameObject que le script.
	 2. Pas de Observed ou de State Synchronization.
	 3. Declaration avec [RPC].
	 4. On peut appeler les RPC de tous scripts sur le meme GameObject avec networkView.RPC().

Synchronization:
	1. Choisir le mode de State Synchronisation.
	2. Exemple.
	3. OnSerializeNetworkView() est appelée, par défaut, 15 fois par seconde.


Methodes de balancing:
	Client-side Prediction : éviter le delai de réception de réponse serveur.
	Extrapolation : prédiction de mouvements des adversaires du joueur en attendant le packet.
	Interpolation : délai sur le jeu et recalcul de position des adversaires une fois le packet reçu.


Moteurs:
	Unity: 	Facile d'approche, moins optimale.
	UDK:	Complexe, puissant.

*/

void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info) { //15 fois/seconde
	float horizontalInput = 0.0;

	if (stream.isWriting){ // Envoie
		horizontalInput = Input.GetAxis ("Horizontal");
		stream.Serialize (horizontalInput);
	} 
	else { //Reçoit
		stream.Serialize (horizontalInput);
	}
}








