using UnityEngine;
using System.Collections;

public class RessourceStats : MonoBehaviour {
	public float collectingTime;
	public int tier;

	public bool harvested;
	float respawnDelay = 25.0f;
	float harvestedAtTime;
	float respawnTime;

	private Shader shaderHarvested;
	private Shader shader;

	// Use this for initialization
	void Start () {
		shaderHarvested = Shader.Find("Shader Forge/shader_disabled");
		if(name == "Textile")
			shader = gameObject.transform.FindChild("SM_clothes").renderer.material.shader;
		else if(name == "Metal")
			shader = gameObject.transform.FindChild("SM_metal").renderer.material.shader;
		else if(name == "Electronique")
			shader = gameObject.transform.FindChild("SM_tv").renderer.material.shader;
		//shader = Shader.Find("Shader Forge/shader_corrosif");
	}
	
	// Update is called once per frame
	void Update () {
		if(harvested){
			if(Time.time >= respawnTime){
				harvested = false;

				if(name == "Textile")
					gameObject.transform.FindChild("SM_clothes").renderer.material.shader = shader;
				else if(name == "Metal")
					gameObject.transform.FindChild("SM_metal").renderer.material.shader = shader;
				else if(name == "Electronique")
					gameObject.transform.FindChild("SM_tv").renderer.material.shader = shader;
			}
		}
	}

	public void Harvested(){
		harvested = true;
		harvestedAtTime = Time.time;
		respawnTime = harvestedAtTime + respawnDelay;

		if(name == "Textile")
			gameObject.transform.FindChild("SM_clothes").renderer.material.shader = shaderHarvested;
		else if(name == "Metal")
			gameObject.transform.FindChild("SM_metal").renderer.material.shader = shaderHarvested;
		else if(name == "Electronique")
			gameObject.transform.FindChild("SM_tv").renderer.material.shader = shaderHarvested;
	}
}
