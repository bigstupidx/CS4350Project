using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FeedTextFromObject : MonoBehaviour {

	public static bool moreThanOneLine = false;
	public int ind = 0;
	public string[] multipleResponds;

	Text text;
	void Awake(){
		text = transform.GetComponent<Text> ();
		text.text = "";

		ResetTextFeed ();
	}

	public void SetText(string _respond)
	{
		if (_respond.Contains("\n")) {
			moreThanOneLine = true; 
			multipleResponds = _respond.Split('\n');
			ind = 0;
		} else {
			text.text = _respond;
		}
	}

	public void Update(){
		// change text set when multiple lines of responds is detected
		if (moreThanOneLine && Input.GetKeyUp (KeyCode.Space)) {
			text.text = multipleResponds [ind++];
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
