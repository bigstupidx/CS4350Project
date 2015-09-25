using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkingButton : MonoBehaviour {

	private float speed = 0.4f;
	private bool fadeOut = true;
	private Color defaultColor;
	// Use this for initialization
	void Start () {
		defaultColor = new Color (1.0f, 1.0f, 1.0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		float alpha = gameObject.GetComponent<Image> ().color.a;
		if (fadeOut) 
			alpha -= (speed * Time.deltaTime);
		else
			alpha += (speed * Time.deltaTime);

		gameObject.GetComponent<Image> ().color = new Color (defaultColor.r, defaultColor.g, defaultColor.b, alpha);

		if (gameObject.GetComponent<Image> ().color.a <= 0.0f || gameObject.GetComponent<Image> ().color.a >= 0.5f)
			fadeOut = !fadeOut;
	}
}
