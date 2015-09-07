using UnityEngine;
using System.Collections;

public class triggerTextBox : MonoBehaviour {

	public GameObject gameObject;
	public GameObject gameObjectNoItem;

	public GameObject currentObject;
	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
		gameObjectNoItem.SetActive (false);
	}

	void OnCollisionEnter(Collision other){
		Debug.Log (other.collider.gameObject.name);
		if(other.collider.gameObject.name.CompareTo("Floor") != 0 )
		currentObject = other.collider.gameObject;
	}

	void Update(){
		if (Input.GetKeyUp (KeyCode.Space)) {
			if(currentObject != null){
				Item curr = GameController.instance.GetItem(currentObject.name);
				bool status =  PlayerController.instance.GetComponent<PlayerController>().AbleToTrigger(curr);

				GameObject.Find("ObjectRespond").GetComponent<FeedTextFromObject>().SetText(curr.GetRespond(status) ) ;
				if( status )
				{
					PlayerController.instance.GetComponent<PlayerController>().ItemTriggered(curr);
				}
			}else
				GameObject.Find("ObjectRespond").GetComponent<FeedTextFromObject>().SetText("Mum?");

		}

		if (currentObject != null) {
			if ((currentObject.transform.position - transform.position).magnitude > 2.0f)
				currentObject = null;
		}

	}

	void OnTriggerExit(Collider other){
		gameObject.SetActive (false);
		gameObjectNoItem.SetActive (false);
	}
}
