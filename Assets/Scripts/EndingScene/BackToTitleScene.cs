using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackToTitleScene : MonoBehaviour {

	RectTransform myRect;
	Text myText;

	// Use this for initialization
	void Start () {
		myRect = transform.GetComponent<RectTransform> ();
		myText = transform.GetComponent<Text> ();
	}
	
	public void onClick(){
		if(transform.name.Equals ("BackToTitleScene")){
			Application.LoadLevel ("TitleScene");
		}
	}

	
}
