using UnityEngine;
using System.Collections;

public class BlinkingEffect : MonoBehaviour {

	public Light lightSource;
	public float delay = 0.2f;
	private float startTime;

	
	// Update is called once per frame
	void FixedUpdate () {
		if (Time.time - startTime > delay) {

			float rand = Random.value;


			if (rand < 0.3f) 
				lightSource.enabled = true;
			 else
				lightSource.enabled = false;

			startTime = Time.time;
		}
	}
}
