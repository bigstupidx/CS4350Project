using UnityEngine;

using System.Collections;

public class test : MonoBehaviour {
	private float startTime;
	public AudioSource a;
	private float durationTime;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	//	cameraObject.transform.position = startPt;
	//	cameraObject.transform.rotation = Quaternion.Euler(rotate_y);
	// Update is called once per frame
	void Update () {
		durationTime = Time.time - startTime;
		if (durationTime > 0.0f && durationTime < 4.0f) {
			a.volume -=1.0f*Time.deltaTime;
			this.GetComponent<AudioSource>().volume = a;
		}
	
	}
}
