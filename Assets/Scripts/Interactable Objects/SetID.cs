using UnityEngine;
using System.Collections;

public class SetID : MonoBehaviour {
	public int id = -1; 
	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider other){
		PlayerData.nearestObjectId = id;
		
	}
	
	void OnTriggerExit(Collider other){
		PlayerData.nearestObjectId = -1;
		
	}


	// Update is called once per frame
	void Update () {
	
	}
}
