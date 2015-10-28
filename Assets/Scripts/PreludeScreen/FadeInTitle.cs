using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInTitle : MonoBehaviour {

	public bool fadeInState = true;
	private float speed = 0.4f;
	// Update is called once per frame
	void Update () {
		Color newAlpha = GetComponent<Image> ().color;
		if (newAlpha.a >= 1.5f) {
			fadeInState = false;
		} else if (newAlpha.a < 0.0f) {
			GetComponent<Image>().color = new Color(1.0f,1.0f, 1.0f, -1.0f);
			gameObject.SetActive (false);
		}
		
		if (fadeInState) {
			newAlpha.a += (speed * Time.deltaTime);
			GetComponent<Image> ().color = newAlpha;
		} else {
			newAlpha.a -= (speed  * Time.deltaTime);
			GetComponent<Image> ().color = newAlpha;
		}
	}
}
