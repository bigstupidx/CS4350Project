using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Misc : MonoBehaviour {

	private int backCounter = 0;
	public GameObject hint;
	private float timer;

	public void QuitGame()
	{
		Application.Quit ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Escape)) {
			backCounter++;
			timer = Time.time;
		}

		if (backCounter == 1) {
			hint.GetComponent<Text>().text = "Press again to Quit Game";
		}
		else if (backCounter >= 2)
			QuitGame ();


		if ( backCounter > 0 && Time.time - timer > 1.0f) {
			hint.GetComponent<Text>().text = "";
			backCounter = 0;
		}

	}
}
