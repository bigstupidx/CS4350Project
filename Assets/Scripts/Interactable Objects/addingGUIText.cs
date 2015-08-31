using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// not working
public class addingGUIText : MonoBehaviour {
	public GameObject gui;
	private Text text;
	private string textDisplay = null;  //passing from Database (Check again T__T)

	void Awake(){
		text = gui.GetComponent<Text> ();
	}
	
	void OnTriggerEnter(Collider other) {
		textDisplay = "Hello World";
		//text.text = textDisplay.ToString;
	}
}
