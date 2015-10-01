using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class textBehaviour : MonoBehaviour {
	private float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float currTime = Time.time - startTime;
		Color temp = gameObject.GetComponent<Text> ().color;
		if (currTime > 1.5f) {

			pos
			temp.a *= 0.5f;
			gameObject.GetComponent<Text> ().color = temp;
			
		} else if (currTime > 3.0f) {	
			temp.a = 1.0f;
			gameObject.GetComponent<Text> ().color = temp;
			startTime = Time.time;
		}
	}
}
