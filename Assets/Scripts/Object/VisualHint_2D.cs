using UnityEngine;
using System.Collections;

public class VisualHint_2D : MonoBehaviour {


	public Texture2D highlightTex;
	public Texture2D defaultTex;

	private bool myStatus = false;
	private GameObject player;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter(Collider other){
		Item curr = GameController.instance.GetItem (this.gameObject.name);
		myStatus = PlayerController.instance.AbleToTrigger (curr);
	}
	
	void OnTriggerExit(Collider other){
		myStatus = false;
	}

	void Update()
	{
		/*bool isActive = PlayerController.instance.AbleToTrigger (GameController.instance.GetItem (transform.gameObject.name));
		if ((player.transform.position - transform.position).magnitude <= 2.0f && isActive){
			toggle = true;
		}
		else
			toggle = false;
			*/

		if (myStatus) {
			transform.GetComponentInChildren<Renderer> ().material.mainTexture = highlightTex;
		} else {
			transform.GetComponentInChildren<Renderer> ().material.mainTexture = defaultTex;
		}
	}
}
