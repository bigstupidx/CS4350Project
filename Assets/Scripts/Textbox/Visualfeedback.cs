using UnityEngine;
using System.Collections;

public class Visualfeedback : MonoBehaviour {
	
	Component halo;
	bool isTrigger = false;
	private string colliderName = null;
	private GameObject player;
	
	// Use this for initialization
	void Start () {
		halo = GetComponent ("Halo");
		halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
		player = GameObject.FindGameObjectWithTag ("Player");
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
			//Debug.Log (colliderName);
			Item curr = GameController.instance.GetItem (colliderName);
			bool status = PlayerController.instance.AbleToTrigger (curr);
			
			//Debug.Log ("status + " + status);

			//player.GetComponent<Displaytextbox> ().currentObject = null;
			if (status) {
				halo.GetType ().GetProperty ("enabled").SetValue (halo, true, null);
				//player.GetComponent<Displaytextbox> ().currentObject = this.gameObject;
			} else {
				halo.GetType ().GetProperty ("enabled").SetValue (halo, false, null);
			}
		} else {
				halo.GetType ().GetProperty ("enabled").SetValue (halo, false, null);
		}
	}	

}
