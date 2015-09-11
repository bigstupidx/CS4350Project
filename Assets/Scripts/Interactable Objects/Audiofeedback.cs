using UnityEngine;
using System.Collections;

public class Audiofeedback : MonoBehaviour {

	private AudioClip audioClip;
	bool isTrigger = false;
	private string colliderName = null;

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource> ().Stop ();
	}

	void OnTriggerEnter(Collider other){
		isTrigger = true;
		colliderName = this.gameObject.name;
	}
	
	void OnTriggerExit(Collider other){
		isTrigger = false;
	}

	// Update is called once per frame
	void Update () {
		if (isTrigger) {
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
}
