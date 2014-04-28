using UnityEngine;
using System.Collections;

public class DamageTextController : MonoBehaviour {

	private const float speed = 0.4f/2;

	private float timer = 0f;
	private float velocity = -0.4f/2;
	private TextMesh mesh;

	public int Amount { get; set;}

	// Use this for initialization
	void Start () {
		mesh = this.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer > 1.5f) {
			Destroy (gameObject);
		} else {
			Color color = new Color(1.0f, 1.0f, 1.0f, 1.0f - (timer - 1.0f) / 0.5f);

			velocity += speed * Time.deltaTime;
			transform.position += Vector3.down * velocity;
			mesh.text = Amount.ToString();
			mesh.color = color;
		}
	}
}
