using UnityEngine;
using System.Collections;

public class triggerTextBox : MonoBehaviour {

	public GameObject gameObject;
	public GameObject gameObjectNoItem;
	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
		gameObjectNoItem.SetActive (false);
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "trashCan") {
			gameObject.SetActive (true);
		} 
		else if(other.gameObject.tag == "nonCollectable"){
			gameObject.SetActive (false);
			gameObjectNoItem.SetActive (true);
		}
	}

	void OnTriggerExit(Collider other){
		gameObject.SetActive (false);
		gameObjectNoItem.SetActive (false);
	}
}
