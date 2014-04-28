using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	HealthBarEnemy healthBar;

	//stats
	public enum EnemyLevel{ Weak, Intermediate, Strong, SuperStrong, Boss};
	public float maxPatrolDistance=20;
	public EnemyLevel level;
	public string team;
	public float respawnDelay=20;
	float strength;
	float endurance;
	float speed;
	float hp;
	float maxhp;

	bool isDead = false;
	bool isInBattle = false;
	PlayerCombat player;
	bool playerPlayed = true;
	float nextAttack;
	bool atInitPos=true;
	
	bool playerSeen = false;
	GameObject playerGO;

	private float respawnTime;
	private Transform playerPos;
	private Vector3 targetDirection;
	private Transform myTransform;
	private int force = 5;
	private Vector3 posInit;

	public GameObject damageText;
	public GameObject audioSource;

	// Use this for initialization
	void Start () {
		healthBar = gameObject.GetComponent<HealthBarEnemy>();
		audioSource = GameObject.Find ("MusicPlayer");
		myTransform = transform;
		posInit = transform.position;

		//levelstats
		if(level == EnemyLevel.Weak){
			strength = 3.0f;
			endurance = 0.5f;
			speed = 2.5f;
			hp = 20.0f;
			maxhp = 20.0f;
		}
		else if(level == EnemyLevel.Intermediate){
			strength = 5.0f;
			endurance = 2.0f;
			speed = 3.9f;
			hp = 35.0f;
			maxhp = 35.0f;
		}
		else if(level == EnemyLevel.Strong){
			strength = 8.0f;
			endurance = 3.0f;
			speed = 1.8f+1.5f;
			hp = 50.0f;
			maxhp = 50.0f;
		}
		else if(level == EnemyLevel.SuperStrong){
			strength = 13.0f;
			endurance = 6.0f;
			speed = 3.5f;
			hp = 80.0f;
			maxhp = 80.0f;
		}
		else if(level == EnemyLevel.Boss){
			strength = 19.0f;
			endurance = 11.0f;
			speed = 0f;
			hp = 130.0f;
			maxhp = 130.0f;
		}

		healthBar.maxhp = maxhp;
		healthBar.hp = hp;

		/*if(team == "Red")
			change asset color
		else
			change asset color
		 */
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead) {
			RespawnTimer();
				}

		else if(isInBattle && playerPlayed && Time.time > nextAttack){
			playerPlayed = false;
			Attacks ();
		}
		
		else if(!isInBattle )
		{
			PlayerCombat combat = playerGO != null ? playerGO.GetComponent<PlayerCombat>() : null;
			if(playerSeen && combat.team != team && !combat.IsInBattle() && combat.hp > 0 && Vector3.Distance(playerGO.transform.position,posInit) < maxPatrolDistance)
			{
				atInitPos=false;
				rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionX;
				rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;		

				playerPos = playerGO.transform;
				targetDirection = (playerPos.position - myTransform.position);


				targetDirection = new Vector3(targetDirection.x, 0, targetDirection.z);
				targetDirection.Normalize();
				gameObject.transform.position+=targetDirection*speed*Time.deltaTime;
				//gameObject.rigidbody.AddForce(targetDirection * force * Time.deltaTime);
				transform.LookAt(targetDirection);
			}
			else{
				moveToStartingPos();
			}
		}
	}

	void moveToStartingPos()
	{
		if(gameObject.name!="Merchant"){
			if (Vector3.Distance(myTransform.position,posInit)<0.5) {
				rigidbody.velocity= new Vector3(0,0,0);
				atInitPos=true;
					}
			else
			{
				targetDirection = (posInit - myTransform.position);
				transform.LookAt(rigidbody.velocity);
				
				targetDirection = new Vector3(targetDirection.x, 0, targetDirection.z);
				targetDirection.Normalize();
				//gameObject.rigidbody.AddForce(targetDirection * force * Time.deltaTime);
				gameObject.transform.position+=targetDirection*speed*Time.deltaTime;
			}
		}
	}
	//Actions
	void Attacks(){
		audioSource.GetComponent<MusicPlayer>().playEnemyHitSound();
		player.ReceivesDamage(strength);
	}

	public bool ReceivesDamage(float damage){
		hp -= damage-endurance;
		playerPlayed = true;
		healthBar.hp = hp;
		GameObject text = (GameObject)Instantiate (damageText);
		text.transform.position = transform.position;
		text.GetComponent<DamageTextController>().Amount = (int)(damage - endurance);


		print ("Enemy hp left "  + hp);
		nextAttack = Time.time + 1.5f;

		if(hp <= 0){
			audioSource.GetComponent<MusicPlayer>().playEnemyDeathSound();
			Die();
			return true;
		}

		return false;
	}


	//States
	public void IsAttacked(PlayerCombat playerCollided){
		isInBattle = true;
		playerSeen = false;
		player = playerCollided;
		rigidbody.velocity = new Vector3(0,0,0);
	}

	public void CombatEnded(){
		isInBattle = false;
	}

	void Die(){
		//healthBar.destroyToi();
		//Destroy (this.gameObject);
		isDead = true;
		CombatEnded ();
		hp = maxhp;
		healthBar.hp = maxhp;
		transform.position = new Vector3 (0, 0, 300);
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;

	}
	void RespawnTimer()
	{
				respawnTime += Time.deltaTime;
				if (respawnTime >= respawnDelay) {
			isDead=false;
			respawnTime=0;
						transform.position = posInit;
			rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionX;
			rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;	
			atInitPos=true;
				}
		}

	public void PlayerIsDead(){
		hp = maxhp;
		healthBar.hp = maxhp;
	}

	public void PlayerSeen(GameObject player){
		playerGO = player;
		playerSeen = true;
	}
	
	public void PlayerGone(){
		playerSeen = false;
		rigidbody.velocity = new Vector3(0,0,0);
	//	rigidbody.constraints = RigidbodyConstraints.FreezeAll;
	}

	public string getTeam(){
		return team;
	}
}
