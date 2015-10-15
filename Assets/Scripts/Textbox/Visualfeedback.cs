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
			
			if(EndingController.instance.isChapter2Activated && TraceController.instance.storyList.Count > 0)
			{
				status = TraceController.instance.storyList[0].Contains(colliderName);
			}

			if (status) {//if it is interactable
				transform.GetComponentInChildren<Renderer>().material.SetFloat("_Outline", 0.001f);
			} else {
				transform.GetComponentInChildren<Renderer>().material.SetFloat ("_Outline", 0.00f);
			}
		} else {
				transform.GetComponentInChildren<Renderer>().material.SetFloat ("_Outline", 0.00f);
		}
	}	

}
