using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TimeCounter : MonoBehaviour {

	int hour;
	int minutes;
	int seconds;

	Text time;
	// Use this for initialization
	void Awake () {
		time = gameObject.GetComponent<Text> ();
		time.text = "00:00:00";
	}
	
	// Update is called once per frame
	void Update () {

		hour = GameController.instance.GetActualTime (2);
		minutes = GameController.instance.GetActualTime (1);
		seconds = GameController.instance.GetActualTime (0);

		time.text = string.Format ("{0:00}:{1:00}:{2:00}",hour, minutes, seconds); 
	}
}
