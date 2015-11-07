using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextScroll_Up : MonoBehaviour {
	public GameObject logo;
	public GameObject header;
	public GameObject text;
	
	private Color logo_temp;
	private Color header_temp;
	private Color text_temp;

	private float stateTimer;
	private int currentState;

	// Use this for initialization
	void Start () {
		text_temp = text.GetComponent<Image> ().color;
		logo_temp = logo.GetComponent<Image>().color;
		header_temp = header.GetComponent<Image> ().color;

		header_temp.a = 0.0f;
		logo_temp.a = 0.0f;
		text_temp.a = 0.0f;

		text.GetComponent<Image> ().color = text_temp;
		header.GetComponent<Image> ().color = header_temp;
		logo.GetComponent<Image> ().color = logo_temp;

		currentState = 0;
		stateTimer = 20.0f;
	}

	void run()
	{
		switch (currentState) {
		case 0: logo_temp.a += 1.0f * Time.deltaTime; logo.GetComponent<Image>().color = logo_temp; break;
		case 1: header_temp.a += 1.0f * Time.deltaTime; header.GetComponent<Image> ().color = header_temp; break;
		case 2: text_temp.a += 1.0f * Time.deltaTime; text.GetComponent<Image> ().color = text_temp; break;
		case 3: up(); break;
		case 4: text_temp.a -= 1.0f * Time.deltaTime; text.GetComponent<Image> ().color = text_temp; break;
		case 5: Application.LoadLevel("TitleScene"); break;
		}
		if (stateTimer > 0.0f) {
				stateTimer -= 10 * Time.deltaTime;
		} else {
			currentState++;
			switch (currentState) {
			case 0: stateTimer = 20.0f; break;
			case 1: stateTimer = 20.0f; break;
			case 2: stateTimer = 20.0f; break;
			case 3: stateTimer = 590.0f; break;
			case 4: stateTimer = 60.0f; break;
			case 5: stateTimer = 10.0f; break;
			default: stateTimer = 100000.0f; break;
			}
		}
		Debug.Log (currentState + " " + stateTimer);
	}

	void up() {
		text.transform.Translate (Vector3.up * 30.0f * Time.deltaTime);
		logo.transform.Translate (Vector3.up * 30.0f * Time.deltaTime);
		header.transform.Translate (Vector3.up * 30.0f * Time.deltaTime);
	}

	// Update is called once per frame
	void Update () {
		run ();
	}
}
