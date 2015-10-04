using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class panel : MonoBehaviour {

	private float startTime;
	private float duration;
	private float threshold;
	private Color bg_temp;
	public GameObject background;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		bg_temp = background.GetComponent<Image>().color;
		threshold = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		duration = Time.time - startTime;
		if (duration > 2.0f && duration < 4.0f) {
			fadeIn ();
		} else if (duration > 19.0f && duration < 21.0f) {
			fadeOut ();
		} else if (duration > 21.0f && duration < 23.0f) {
			fadeIn ();
		} else if (duration > 39.0f && duration < 41.0f) {
			fadeOut (); 
		} else if (duration > 41.0f && duration < 43.0f) {
			fadeIn ();
		} else if (duration > 63.0f && duration < 65.0f) {
			fadeOut ();
		}
	}

	void fadeIn() {
		if (bg_temp.a < 0.50f) {
			threshold = 0.0f;
		}
		bg_temp.a -= threshold * Time.deltaTime;
		threshold = 1.0f;
		background.GetComponent<Image> ().color = bg_temp;
	}
	void fadeOut() {
		bg_temp.a += 1.0f * Time.deltaTime;
		background.GetComponent<Image> ().color = bg_temp;
	}
}
