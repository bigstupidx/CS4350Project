﻿using UnityEngine;
using System.Collections;

public class Audiofeedback : MonoBehaviour {

	private AudioClip audioClip;
	private string colliderName = null;

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource> ().Stop ();
	}

	void OnTriggerEnter(Collider other){
		colliderName = this.gameObject.name;
		Debug.Log (gameObject.name);

		Item curr = GameController.instance.GetItem (colliderName);
		bool status = PlayerController.instance.GetComponent<PlayerController> ().AbleToTrigger (curr);
		
		Debug.Log ("status + " + status);
		
		if (status) {
			GetComponent<AudioSource> ().Play ();
		} else {
			GetComponent<AudioSource> ().Stop ();	
		}

	}
	
}