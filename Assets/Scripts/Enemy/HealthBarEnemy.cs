using UnityEngine;
using System.Collections;

public class HealthBarEnemy : MonoBehaviour {

	public float hp;
	public float maxhp;
	public int healthbarWidth = 35;
	public int healthbarMaxWidth = 35;
	public GameObject myHealthbar;
	GameObject myhb;

	// Use this for initialization
	void Start () {
		myhb = (GameObject)Instantiate(myHealthbar, transform.position, transform.rotation);

		//Visible for who
		if(gameObject.GetComponent<Enemy>().getTeam() == "Blue"){
			myhb.layer = LayerMask.NameToLayer("Player2Only");
		}
		else{
			myhb.layer = LayerMask.NameToLayer("Player1Only");
		}
	}
	
	// Update is called once per frame
	void Update (){
		Vector3 pos = Vector3.zero;
		if(gameObject.GetComponent<Enemy>().getTeam() == "Blue"){
			pos = GameObject.Find("Camera 2").camera.WorldToViewportPoint(transform.position);
		}
		else{
			pos = GameObject.Find("Camera 1").camera.WorldToViewportPoint(transform.position);
		}

		myhb.transform.position = new Vector3(pos.x-.05f, pos.y, pos.z+1.0f);
		myhb.transform.localScale = Vector3.zero;
		float healthpercent = hp/maxhp;

		if(healthpercent<0)
			healthpercent=0;
		if(healthpercent>100)
			healthpercent=100;

		healthbarWidth = Mathf.FloorToInt(healthpercent*healthbarMaxWidth);
		myhb.guiTexture.pixelInset = new Rect(10, 10, healthbarWidth, 5);
	}

	public void destroyToi(){
		Destroy(myhb);
	}
}
