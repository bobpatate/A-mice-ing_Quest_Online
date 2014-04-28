using UnityEngine;
using System.Collections;

public class DamageDisplay : MonoBehaviour {

	public float damage = 0;

	public GameObject guiText;
	GameObject damageText;

	// Use this for initialization
	void Start () {
		/*damageText = (GameObject)Instantiate(guiText, transform.position, transform.rotation);
		damageText.guiText.text = damage.ToString();*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
