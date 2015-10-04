using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextScroll_Up : MonoBehaviour {

	private float startTime;
	private float duration;
	public GameObject logo;
	public GameObject header;
	public GameObject text;
	private Color logo_temp;
	private Color header_temp;
	private Color text_temp;



	// Use this for initialization
	void Start () {
		text_temp = text.GetComponent<Image> ().color;
		logo_temp = logo.GetComponent<Image>().color;
		header_temp = header.GetComponent<Image> ().color;
		header_temp.a = 0.0f;
		logo_temp.a = 0.0f;
		header.GetComponent<Image> ().color = header_temp;
		logo.GetComponent<Image> ().color = logo_temp;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		duration = Time.time - startTime;
		if (duration > 3.5f && duration < 6.0f) {
			logo_temp.a += 1.0f * Time.deltaTime;
			logo.GetComponent<Image> ().color = logo_temp;
		} else if (duration > 6.0f && duration < 8.0f) {
			header_temp.a += 1.0f * Time.deltaTime;
			header.GetComponent<Image> ().color = header_temp;
		} else if (duration > 12.0f && duration < 76.0f) {
			transform.Translate (Vector3.up * 30.0f * Time.deltaTime);
		} else if (duration > 78.0f && duration < 80.0f) {
			text_temp.a -= 1.0f * Time.deltaTime;
			text.GetComponent<Image> ().color = text_temp;
		} 
	}
}
