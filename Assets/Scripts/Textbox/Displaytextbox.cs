using UnityEngine;
using System.Collections;

public class Displaytextbox : MonoBehaviour {
	public GameObject textbox;
	bool isTrigger = false;
	bool isDisplay = false;
	string colliderName = null;

	// Use this for initialization
	void Start () {
		textbox.SetActive (false);
		//StartCoroutine (MyCoroutine (textbox));
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
			if (Input.GetKeyUp (KeyCode.Space)) {
				textbox.SetActive(true);
				isDisplay = true;
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
