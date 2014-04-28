using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour {
	
	public Ressource[] inventaire = new Ressource[10];
	private bool isInCollectingRange = false;
	private bool showCollectTimeBar = false;
	private GameObject collectible;

	private bool _isCollecting = false;
	private bool isCollecting {
				get{ return _isCollecting; }
				set {	_isCollecting = value; 
						Animator animator = GetComponentInChildren<Animator> ();
						animator.SetBool ("gathering", value);
					}
				}
	public float collectingSpeed = 1.0f;
	private float collectingStartTime;
	private float collectingTime;

	private float collectTimeBarLenght = 100;
	private GUIStyle currentStyle = null;
	private PlayerController playerControllerScript;

	private bool isInCraftingRange = false;
	private GameObject merchant;
	private string collectibleName;

	public int playerNumber;

	private int nbRessources = 0;
	public GameObject audioSource;
	// Use this for initialization
	void Start () {
		InitializeInventory();
		audioSource = GameObject.Find ("MusicPlayer");
		playerControllerScript = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isInCollectingRange && collectible.GetComponent<RessourceStats>().harvested == false)
		{
			if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonUp("joystick 1 button 0")) && !isCollecting && playerNumber==1)
			{
				isCollecting = true;
				showCollectTimeBar = true;
				rigidbody.velocity = new Vector3(0,0,0);
				playerControllerScript.enabled = false;
				rigidbody.constraints = RigidbodyConstraints.FreezeAll;


				collectingStartTime = Time.time;
				collectingTime = Time.time + (collectible.GetComponent<RessourceStats>().collectingTime*collectible.GetComponent<RessourceStats>().tier*collectible.GetComponent<RessourceStats>().tier) / collectingSpeed;
				collectibleName = collectible.name;

				audioSource.GetComponent<MusicPlayer>().playHarvestSound();
			}
			else if ((Input.GetKeyDown(KeyCode.RightControl)|| Input.GetButtonUp("joystick 2 button 0")) && !isCollecting && playerNumber==2)
			{
				isCollecting = true;
				showCollectTimeBar = true;
				rigidbody.velocity = new Vector3(0,0,0);
				playerControllerScript.enabled = false;
				rigidbody.constraints = RigidbodyConstraints.FreezeAll;

				
				collectingStartTime = Time.time;
				collectingTime = Time.time + (collectible.GetComponent<RessourceStats>().collectingTime*collectible.GetComponent<RessourceStats>().tier*collectible.GetComponent<RessourceStats>().tier) / collectingSpeed;
				collectibleName = collectible.name;

				audioSource.GetComponent<MusicPlayer>().playHarvestSound();
			}

			if(isCollecting && Time.time > collectingTime)
				doneCollecting();
		}
		else if(isInCollectingRange && collectible.GetComponent<RessourceStats>().harvested == true)
		{
			if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonUp("joystick 1 button 0")) && !isCollecting && playerNumber==1)
			{
				
				audioSource.GetComponent<MusicPlayer>().playBuzzerSound();
			}
			else if ((Input.GetKeyDown(KeyCode.RightControl)|| Input.GetButtonUp("joystick 2 button 0")) && !isCollecting && playerNumber==2)
			{
				audioSource.GetComponent<MusicPlayer>().playBuzzerSound();
			}
			
		}

		if (isInCraftingRange)
		{
			if ((Input.GetKeyDown(KeyCode.E)|| Input.GetButtonUp("joystick 1 button 0")) && playerNumber==1)
			{
				RestoreHealthPoints();
				if ( nbRessources >= 4 )
					giveInventoryToMerchant(merchant);

			}
			else if ((Input.GetKeyDown(KeyCode.RightControl) || Input.GetButtonUp("joystick 2 button 0")) && playerNumber==2)
			{
				RestoreHealthPoints();
				if ( nbRessources >= 4 )
					giveInventoryToMerchant(merchant);
				
			}
		}
	}

	void doneCollecting(){
		isCollecting = false;
		playerControllerScript.enabled = true;
		collectingTime = 0.0f;
		showCollectTimeBar = false;
		isInCollectingRange = false;
		rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionX;
		rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
		if ( CheckRessourceExisting(collectibleName))
		{
			AddToInventory(collectible);
			collectible.GetComponent<RessourceStats>().Harvested();
			collectible = null;

			PlayerEquipment playerEquipment = GetComponent<PlayerEquipment> ();
			playerEquipment.UseTool (1);
		}
	}


	//Collisions
	void OnTriggerEnter(Collider collider) {
		switch(collider.gameObject.name)
		{
		case "Collectible_Trigger" :
			isInCollectingRange = true;
			collectible = collider.transform.parent.gameObject;
			break;
		case "Merchant_Trigger":
			isInCraftingRange = true;
			merchant = collider.transform.parent.gameObject;
			break;
		}
	}

	void OnTriggerExit(Collider collider) {
		switch(collider.gameObject.name)
		{
		case "Collectible_Trigger" :
			isInCollectingRange = false;
			collectible = null;	
			break;
		case "Merchant_Trigger":
			isInCraftingRange = false;
			merchant = null;
			break;
		}
	}


	//GUI
	void OnGUI()
	{
		InitStyles();
		if ( showCollectTimeBar )
		{
			if ( GetComponent<PlayerCombat>().team == "Blue" )
			{
				GUI.BeginGroup(new Rect(Screen.width/6 , Screen.height*0.8f, collectTimeBarLenght + 10, 25));
				GUI.Box(new Rect(0,0, collectTimeBarLenght + 10, 25),"Harvesting");
				
				GUI.BeginGroup(new Rect(0,0, collectTimeBarLenght * -((collectingStartTime-Time.time)/(collectingTime - collectingStartTime)), 20));
				GUI.Box(new Rect(4,4, collectTimeBarLenght, 20),"", currentStyle);
				GUI.EndGroup();
				GUI.EndGroup();
			}
			else
			{
				GUI.BeginGroup(new Rect(Screen.width - (Screen.width / 3) , Screen.height*0.8f, collectTimeBarLenght + 10, 25));
				GUI.Box(new Rect(0,0, collectTimeBarLenght + 10, 25),"Harvesting");
				
				GUI.BeginGroup(new Rect(0,0, collectTimeBarLenght * -((collectingStartTime-Time.time)/(collectingTime - collectingStartTime)), 20));
				GUI.Box(new Rect(4,4, collectTimeBarLenght, 20),"", currentStyle);
				GUI.EndGroup();
				GUI.EndGroup();
			}
		}
	}

	private void InitStyles()	
	{
		if( currentStyle == null )	
		{
			currentStyle = new GUIStyle( GUI.skin.box );
			currentStyle.normal.background = MakeTex( 2, 2, new Color( 0f, 0.5f, 1f, 0.5f ) );
		}
		
	}

	private Texture2D MakeTex( int width, int height, Color col )	
	{
		
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )	
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}


	//Inventory
	private void AddToInventory(GameObject collectible){
		if (!CheckInventoryFull())
		{
			for (int i = 0; i < 10; i++)
			{
				if ( inventaire[i].nom == "" )
				{
					inventaire[i].nom = collectible.name;
					inventaire[i].tier = collectible.GetComponent<RessourceStats>().tier;
					nbRessources++;
					print ("Successfully added to inventory : " + inventaire[i].nom + " " + inventaire[i].tier);
					return;
				}
			}
		}
		else
			print ("Inventory is full");
	}

	public void InitializeInventory(){
		for ( int i = 0; i < 10; i++ )
		{
			inventaire[i].nom = "";
			inventaire[i].tier = -1;
		}
		nbRessources = 0;
	}

	//Fonction qui retourne vrai si l'inventaire est plein
	private bool CheckInventoryFull(){

		for (int i = 0; i < 10; i++)
		{
			if ( inventaire[i].nom == "" )
			{
				return false;
			}
		}
		return true;
	}

	private void giveInventoryToMerchant(GameObject merchant){
		ItemType itemType = ItemBuilder.Pick(inventaire);
		if (itemType != null) {
			GetComponent<PlayerEquipment>().GiveItem(itemType);
			InitializeInventory();
		}
	}

	private void RestoreHealthPoints(){
		GetComponent<PlayerCombat>().hp = GetComponent<PlayerCombat>().maxhp;
	}
	private bool CheckRessourceExisting(string collectibleName){
		if(GameObject.Find (collectibleName) != null)
			return true;
		else
			return false;
	}
}
