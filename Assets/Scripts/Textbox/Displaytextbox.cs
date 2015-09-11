using UnityEngine;
using System.Collections;

public class Displaytextbox : MonoBehaviour {
	//public GameObject textbox;
	public GameObject currentObject;

	bool isTrigger = false;
	string colliderName = null;

	// Use this for initialization
	void Start () {
		//textbox.SetActive (false);
		//StartCoroutine (MyCoroutine (textbox));
	}

	void OnTriggerEnter(Collider other){
		isTrigger = true;
		colliderName = other.name;
		currentObject = other.gameObject;
	}
	
	void OnTriggerExit(Collider other){
		isTrigger = false;
		colliderName = other.name;
	}

		
	// Update is called once per frame
	void Update () {
		if (isTrigger) {
			//textbox.SetActive(true);
			if (FeedTextFromObject.moreThanOneLine == false && Input.GetKeyUp (KeyCode.Space)) {
				if(currentObject != null){
					Item curr = GameController.instance.GetItem(colliderName);
					bool status =  PlayerController.instance.GetComponent<PlayerController>().AbleToTrigger(curr);
					
					GameObject.Find("ObjectRespond").GetComponent<FeedTextFromObject>().SetText(curr.GetRespond(status));
					Debug.Log (curr.GetRespond(status));
					if( status )
					{
						PlayerController.instance.GetComponent<PlayerController>().ItemTriggered(curr);
					}
				}else
					GameObject.Find("ObjectRespond").GetComponent<FeedTextFromObject>().SetText("Mum?");
				
			}
		} else {
			//textbox.SetActive (false);
		}
	}

//	IEnumerator MyCoroutine(GameObject textbox){
//		while (isDisplay) {
//			Item curr = GameController.instance.GetItem(colliderName);
//			bool status =  PlayerController.instance.GetComponent<PlayerController>().AbleToTrigger(curr);
//			
//			GameObject.Find("ObjectRespond").GetComponent<FeedTextFromObject>().SetText(curr.GetRespond(status)) ;
//			
//			if(status){
//				PlayerController.instance.GetComponent<PlayerController>().ItemTriggered(curr);
//			}else{
//			}
//			
//		}
//	}
}
