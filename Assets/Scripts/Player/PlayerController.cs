using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public string modelChild = "player_mouse";

	private float _horizontal, _vertical;
	
	public float _maxVelocityChange = 5.0f;
	public float _speed = 5.0f;
	public Vector3 initialPos;
	public string horizontal, vertical;

	// Use this for initialization
	void Start () {
		initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		_horizontal = Input.GetAxis(horizontal);
		_vertical = Input.GetAxis(vertical);
		
		Vector3 targetVelocity = new Vector3(_horizontal, 0, _vertical);
		targetVelocity = transform.rotation*targetVelocity;
		targetVelocity *= _speed;
		
		Vector3 v = rigidbody.velocity;
		Vector3 velocityChange = (targetVelocity-v);
		
		velocityChange.x = Mathf.Clamp(velocityChange.x, -_maxVelocityChange, _maxVelocityChange);
		velocityChange.z = Mathf.Clamp(velocityChange.z, -_maxVelocityChange, _maxVelocityChange);
		
		velocityChange.y = 0;
		
		rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

		if(_horizontal != 0 || _vertical != 0)
			transform.FindChild(modelChild).LookAt(transform.position + targetVelocity);
		
		Animator animator = GetComponentInChildren<Animator>();
		animator.SetBool("walking", _horizontal != 0 || _vertical != 0);
	}

	public void LookAt(GameObject objet){
		transform.FindChild(modelChild).LookAt(objet.transform);
	}
}
