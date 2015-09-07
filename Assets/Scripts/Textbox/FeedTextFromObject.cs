using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FeedTextFromObject : MonoBehaviour {


	Text text;
	void Awake(){
		text = transform.GetComponent<Text> ();
		text.text = "";
	}

	public void SetText(string _respond)
	{
		text.text = _respond;
	}	
}
