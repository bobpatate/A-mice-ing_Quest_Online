using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour {

	//Stats
	public float strength = 5.0f;
	public float endurance = 1.0f;
	public float speed = 1.0f;
	public float maxhp = 15.0f;
	public float hp = 15.0f;

	public string team;

	bool _isInBattle = false;
	bool isInBattle {
				get{ return _isInBattle;}
				set {	_isInBattle = value; 
						Animator animator = GetComponentInChildren<Animator> ();
						animator.SetBool ("attacking", value);
					}
				}
	bool enemyPlayed = true;
	float nextAttack;
	Enemy enemy;

	float tempsRespawn = -1;

	PlayerController playerControllerScript;

	public GameObject audioSource;
	public GameObject damageText;

	// Use this for initialization
	void Start () {
		nextAttack = Time.time + 1.5f;
		audioSource = GameObject.Find ("MusicPlayer");
		playerControllerScript = GetComponent<PlayerController>();
	}


	// Update is called once per frame
	void Update () {
		if (tempsRespawn > 0f) {
			tempsRespawn -= Time.deltaTime;

			if(tempsRespawn <= 0f) {
				enemy.PlayerIsDead();
				Respawn();
			}
		}
		//Battle update
		else if(isInBattle && enemyPlayed && Time.time > nextAttack){
			nextAttack = Time.time + 1.5f;
			enemyPlayed = false;
			Attacks();
		}
	}


	//Actions
	void Attacks(){
		PlayerEquipment playerEquipment = GetComponent<PlayerEquipment> ();
		playerEquipment.UseWeapon(1);
		audioSource.GetComponent<MusicPlayer>().playPlayerHitSound();

		if(enemy.ReceivesDamage(strength)){
			playerControllerScript.enabled = true;
			isInBattle = false;
		}
	}

	public void ReceivesDamage( float damage ){
		enemyPlayed = true;
		hp -= damage-endurance;
		print ("Player hp left "  + hp);
		GameObject text = (GameObject)Instantiate (damageText);
		text.transform.position = transform.position;
		text.GetComponent<DamageTextController> ().Amount = (int)(damage - endurance);

		PlayerEquipment playerEquipment = GetComponent<PlayerEquipment> ();
		playerEquipment.UseArmor (1);

		if(hp <= 0){
			hp = 0;
			audioSource.GetComponent<MusicPlayer>().playPlayerDeathSound();
			print ("You dead");
			tempsRespawn = 5f;
			Animator animator = GetComponentInChildren<Animator>();
			animator.SetBool("dying", true);
		}
	}


	//States
	public bool IsInBattle(){
		return isInBattle;
	}

	void Respawn(){
		GetComponent<PlayerInventory>().InitializeInventory();
		isInBattle = false;
		playerControllerScript.enabled = true;
		hp = maxhp;
		audioSource.GetComponent<MusicPlayer>().playPlayerRespawnSound();
		gameObject.transform.position=GetComponent<PlayerController>().initialPos;
		Animator animator = GetComponentInChildren<Animator>();
		animator.SetBool("dying", false);
	}


	//Collisions
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Fight_Trigger" && !isInBattle){
			if(other.gameObject.transform.parent.GetComponent<Enemy>().getTeam() != team){
				playerControllerScript.LookAt(other.gameObject);
				rigidbody.velocity = new Vector3(0,0,0);
				Animator animator = GetComponentInChildren<Animator>();
				animator.SetBool("walking", false);
				playerControllerScript.enabled = false;
				isInBattle = true;
				print("Fightooo!!!");
				enemy = other.gameObject.transform.parent.GetComponent<Enemy>();
				other.gameObject.transform.parent.GetComponent<Enemy>().IsAttacked(this);
			}
		}

		else if(other.gameObject.name == "Detection_Trigger"){
			other.transform.parent.gameObject.GetComponent<Enemy>().PlayerSeen(gameObject);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.name == "Fight_Trigger" && isInBattle){
			isInBattle = false;
			print("Soyonara!!!");
			enemy = null;
			other.gameObject.transform.parent.GetComponent<Enemy>().CombatEnded();
		}

		else if(other.gameObject.name == "Detection_Trigger"){
			other.transform.parent.gameObject.GetComponent<Enemy>().PlayerGone();
		}
	}
}
