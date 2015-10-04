using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class promptText : MonoBehaviour {
	private float startTime;
	private float durationTime;
	private Color temp;
	public GameObject prompt; 

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		temp = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		prompt.GetComponent<Text> ().color = temp;
	}
	
	// Update is called once per frame
	void Update () {
		durationTime = Time.time - startTime;
		if (durationTime > 81.0f && durationTime < 83.0f) {
			temp.a += 1.0f * Time.deltaTime;
			prompt.GetComponent<Text> ().color = temp;
		} else if (durationTime > 83.0f && Input.GetKeyDown (KeyCode.Space)) {
			Application.LoadLevel("TitleScene");
		}
	}
}
