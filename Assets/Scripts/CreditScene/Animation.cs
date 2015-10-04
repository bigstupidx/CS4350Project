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
		platform.SetActive (false);
		basement.SetActive (false);
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		durationTime = Time.time - startTime;
		if (durationTime > 4.0f && durationTime < 6.0f) {
			platform.SetActive(true);
		}
		else if (durationTime > 41.0f && durationTime < 43.0f) {
			basement.SetActive (true);
			ground.SetActive (false);
			platform.SetActive (false);
			Debug.Log("reach here");
		}
	}
}
