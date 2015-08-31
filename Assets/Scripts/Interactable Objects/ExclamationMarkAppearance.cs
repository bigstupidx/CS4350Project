using UnityEngine;
using System.Collections;

public class ExclamationMarkAppearance : MonoBehaviour {

	public GameObject exclamationMark;

	// Use this for initialization
	void Start () {
		exclamationMark.SetActive (false);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "trashCan") {
			exclamationMark.SetActive (true);
		} else {
			exclamationMark.SetActive (false);
		}
	}

	void OnTriggerExit(Collider other){
		exclamationMark.SetActive (false);
	}
}
