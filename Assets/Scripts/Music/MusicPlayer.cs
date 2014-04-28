using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	public AudioClip enemyHitSound;
	public AudioClip playerHitSound;
	public AudioClip harvestSound;
	public AudioClip buzzerSound;
	public AudioClip playerDeathSound;
	public AudioClip enemyDeathSound;
	public AudioClip playerRespawnSound;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void playEnemyHitSound(){
		audio.PlayOneShot(enemyHitSound);
	}
	public void playPlayerHitSound(){
		audio.PlayOneShot(playerHitSound);
	}
	public void playHarvestSound(){
		audio.PlayOneShot(harvestSound);
	}
	public void playBuzzerSound(){
		audio.PlayOneShot(buzzerSound);
	}
	public void playPlayerDeathSound(){
		audio.PlayOneShot(playerDeathSound);
	}
	public void playEnemyDeathSound(){
		audio.PlayOneShot(enemyDeathSound);
	}
	public void playPlayerRespawnSound(){
		audio.PlayOneShot(playerRespawnSound);
	}

}
