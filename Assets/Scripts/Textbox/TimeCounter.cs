using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeCounter : MonoBehaviour {

	int hour;
	int minutes;
	int seconds;
	float startTime;

	Text time;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		time = gameObject.GetComponent<Text> ();
		time.text = "00:00:00:00";
	}
	
	// Update is called once per frame
	void Update () {

		float curr = Time.time - startTime;
		hour = (int)curr / 3600;
		minutes = (int)curr / 60;
		seconds = (int)curr % 60;

		time.text = string.Format ("{0:00}:{1:00}:{2:00}",hour, minutes, seconds); 
	}
}
