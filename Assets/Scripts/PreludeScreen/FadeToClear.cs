using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeToClear : MonoBehaviour {

	public bool transitionToNextScreen = false;
	public string targetScene = "";

	private Image myImage;

	public void TransitToNextScene()
	{
		Color temp = myImage.color;
		if (temp.a <= 0.0f) {
			transitionToNextScreen = true;
		}
	}
	void Start()
	{
		myImage = GetComponent<Image> ();
	}
	void FixedUpdate () {

		if (transitionToNextScreen) {
			Color temp = myImage.color;
			if (temp.a >= 1.0f){
				if(targetScene.Length > 0)
					Application.LoadLevel(targetScene);
				else
					Debug.Log("ERROR!!! No target scene");
			}
			else {
				temp.a += (0.3f * Time.deltaTime);
				myImage.color = temp;
			}
		} else {
			Color temp = myImage.color;
			if( temp.a > 0.0f && temp.a <= 1.0f ){
				temp.a -= (0.3f * Time.deltaTime);
				myImage.color = temp;
			}
		}
	
	}
}
