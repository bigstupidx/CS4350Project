using UnityEngine;
using System.Collections;

public class AnimateShader : MonoBehaviour {
	
	//float curr = 0.0f;
	bool toggle = false;
	public Material temp1;
	public Material temp2;
	private GameObject player;
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		//curr += Time.deltaTime;
		
		//Debug.Log (curr);
		//if (curr >= 1.0f) {

		bool isActive = PlayerController.instance.AbleToTrigger (GameController.instance.GetItem (transform.gameObject.name));
		if ((player.transform.position - transform.position).magnitude <= 2.0f && isActive){
			toggle = true;//!toggle;
			//curr = 0.0f;
		}
		else
			toggle = false;
		
		if( toggle )
			gameObject.GetComponent<Renderer> ().material = temp2;
		else
			gameObject.GetComponent<Renderer> ().material = temp1;
		
	}
}
