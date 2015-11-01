using UnityEngine;
using System.Collections;

public class Visualfeedback : MonoBehaviour {
	
	bool isTrigger = false;
	public float outlineThickness = 0.001f;
	private GameObject model;
	
	// Use this for initialization
	void Start () {
		transform.GetComponentInChildren<Renderer>().material.SetFloat ("_Outline", 0f);
	}

	
	void OnTriggerEnter(Collider other){
		isTrigger = true;
	}

	void OnTriggerExit(Collider other){
		isTrigger = false;
	}

	// Update is called once per frame
	void Update () {

		//to test the shader
		if (isTrigger) {
			if (GameObject.FindGameObjectWithTag ("Player").transform.GetComponent<Displaytextbox>().colliderName.Length < 1) {
				GameObject.FindGameObjectWithTag ("Player").transform.GetComponent<Displaytextbox>().colliderName = this.gameObject.name;
			}

			Item curr = GameController.instance.GetItem (this.gameObject.name);
			bool status = PlayerController.instance.AbleToTrigger (curr);
			
			if(EndingController.instance.isChapter2Activated && TraceController.instance.storyList.Count > 0)
			{
				status = TraceController.instance.storyList[0].Contains(this.gameObject.name);
			}

			if (status) {//if it is interactable
				for( int i = 0; i < transform.GetComponentInChildren<Renderer>().materials.Length; i++)
					transform.GetComponentInChildren<Renderer>().materials[i].SetFloat("_Outline", outlineThickness);
			} else {
				for( int i = 0; i < transform.GetComponentInChildren<Renderer>().materials.Length; i++)
						transform.GetComponentInChildren<Renderer>().materials[i].SetFloat("_Outline", 0.0f);
			}
		} else {
			for( int i = 0; i < transform.GetComponentInChildren<Renderer>().materials.Length; i++)
				transform.GetComponentInChildren<Renderer>().materials[i].SetFloat("_Outline", 0.0f);
		}
	}	

}
