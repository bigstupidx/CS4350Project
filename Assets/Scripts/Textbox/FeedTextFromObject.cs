using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FeedTextFromObject : MonoBehaviour {

	public static bool moreThanOneLine = false;
	private int ind = 0;
	private string[] multipleResponds;
	private Color defaultColor;

	private Text text;

	void Start(){
		text = transform.GetComponent<Text> ();
		text.text = "";
		defaultColor = text.color;
		ResetTextFeed ();
	}

	public void SetText(string _respond)
	{
		if (_respond.Contains("\\")) {
			moreThanOneLine = true; 
			multipleResponds = _respond.Split('\\');
			ind = 0;
		} else {
			text.text = _respond;
			moreThanOneLine = false;
		}
	}

	public void Update(){
		// change text set when multiple lines of responds is detected
		if (Input.GetKeyUp (KeyCode.Space)) {
			text.color = defaultColor;

			if (moreThanOneLine) {
				text.text = multipleResponds [ind++];
			}
		}

		if (FadeInFadeOut.isActivated && !moreThanOneLine && text.color.a  > 0.0f) {
			text.color = new Color( 255.0f, 255.0f, 255.0f, FadeInFadeOut.alpha); 
			Debug.Log (text.color);
		}

		// guard to reset textoverflow using ind count
		if (ind >= multipleResponds.Length) {
			moreThanOneLine = false;
		}

		// guard to reset multipleResponds
		if (!moreThanOneLine)
			ResetTextFeed ();
	}

	public void ResetTextFeed()
	{
		ind = 0;
		multipleResponds = new string[0];
	}
}
