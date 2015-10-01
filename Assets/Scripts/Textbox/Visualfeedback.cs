using UnityEngine;
using System.Collections;

public class Visualfeedback : MonoBehaviour {
	
	bool isTrigger = false;
	private string colliderName = null;
	private GameObject model;
	
	// Use this for initialization
	void Start () {
		transform.GetComponentInChildren<Renderer>().material.SetFloat ("_Outline", 0f);
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

		//to test the shader
		if (isTrigger) {
			Item curr = GameController.instance.GetItem (colliderName);
			bool status = PlayerController.instance.AbleToTrigger (curr);
			
			//Debug.Log ("status + " + status);

			if (status) {//if it is interactable
				transform.GetComponentInChildren<Renderer>().material.SetFloat("_Outline", 0.0009f);
			} else {
				transform.GetComponentInChildren<Renderer>().material.SetFloat ("_Outline", 0.00f);
			}
		} else {
				transform.GetComponentInChildren<Renderer>().material.SetFloat ("_Outline", 0.00f);
		}
	}	

}
