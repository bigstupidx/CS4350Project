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
			bg_temp.a -= threshold * Time.deltaTime;
			if(bg_temp.a < 0.5f) {
				threshold = 0.0f;
			}
			background.GetComponent<Image> ().color = bg_temp;
		} else if (duration > 63.0f && duration < 70.0f){
			bg_temp.a += 0.3f * Time.deltaTime;
			background.GetComponent<Image> ().color = bg_temp;
		}
	}
}
