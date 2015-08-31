using UnityEngine;
using System.Collections;

public class obtainItem : MonoBehaviour {

	// Use this for initialization
	public GameObject feedback;
	public GameObject noObjectFoundFeedback;
	private int countKeyboardHit = 0;
	private bool trigger;

	void Start () {
		feedback.SetActive (false);
		noObjectFoundFeedback.SetActive (false);
	}

	void OnTriggerEnter(Collider other){
		trigger = true;
	}

	void OnTriggerExit(Collider other){
		trigger = false;
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if(countKeyboardHit == 0 && trigger){
				feedback.SetActive(true);
				Destroy (feedback, 5.0f);
				countKeyboardHit++;
			}
			else if(countKeyboardHit !=0 && trigger){
				noObjectFoundFeedback.SetActive(true);
			}
			else{
				noObjectFoundFeedback.SetActive(false);
			}

		}

	}
	
}
