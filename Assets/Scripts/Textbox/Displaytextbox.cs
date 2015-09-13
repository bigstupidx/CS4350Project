using UnityEngine;
using System.Collections;

public class Displaytextbox : MonoBehaviour {

	private FeedTextFromObject textbox;
	public GameObject currentObject;

	bool isTrigger = false;
	string colliderName = null;

	// Use this for initialization
	void Start () {
		textbox = GameObject.Find ("ObjectRespond").GetComponent<FeedTextFromObject> ();
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
			if(currentObject != null){
				if(FeedTextFromObject.moreThanOneLine == false && Input.GetKeyUp (KeyCode.Space)){
					Item curr = GameController.instance.GetItem(colliderName);
					bool status =  PlayerController.instance.AbleToTrigger(curr);
					
					textbox.SetText(curr.GetRespond(status));
					//Debug.Log (curr.GetRespond(status));
					if( status )
					{
						GameController.instance.TriggerItem(curr.itemId);
					}
				}
			}
		} else {
			//textbox.SetActive (false);
			if (currentObject == null && Input.GetKeyUp (KeyCode.Space)) {
				textbox.SetText("Mum?");
			}
		}

		if (currentObject != null) {
			if ((currentObject.transform.position - transform.position).magnitude > 1.5f){
				currentObject = null;
				FeedTextFromObject.moreThanOneLine = false;
			}
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
