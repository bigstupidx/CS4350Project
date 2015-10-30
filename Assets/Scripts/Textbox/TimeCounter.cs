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

		/*float curr = Time.time - GameController.instance.GetTime ();
		hour = (int)curr / 3600;
		minutes = (int)curr / 60;
		seconds = (int)curr % 60;
		*/

		//int curr = GameController.instance.GetTime ();
		//hour = curr / 21600;
		//minutes = (curr / 3600) %60;
		//seconds = (curr / 60)%60;
		hour = System.DateTime.Now.Hour;
		minutes = System.DateTime.Now.Minute;
		seconds = System.DateTime.Now.Second;

		time.text = string.Format ("{0:00}:{1:00}:{2:00}",hour, minutes, seconds); 
	}
}
