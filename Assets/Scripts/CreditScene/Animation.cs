using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;

public class Animation : MonoBehaviour {
	public GameObject ground;
	public GameObject platform;
	public GameObject basement;

	private float startTime;
	private float durationTime;


	// Use this for initialization
	void Start () {
		//ground.SetActive (false);
		//platform.SetActive (false);
		basement.SetActive (false);
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		durationTime = Time.time - startTime;
		if (durationTime > 41.0f && durationTime < 42.0f) {
			ground.SetActive (false);
			platform.SetActive (false);
			basement.SetActive (true);
		}
	}
}
