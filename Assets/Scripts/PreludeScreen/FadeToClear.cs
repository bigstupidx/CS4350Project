using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeToClear : MonoBehaviour {

	public float fadeSpeed = 0.3f;
	public bool transitionToNextScreen = false;
	public string targetScene = "";

	public Image myImage;

	public void SetTargetScene(string _targetScene)
	{
		targetScene = _targetScene;
	}

	public void TransitToNextScene()
	{
		Color temp = myImage.color;
		if (temp.a <= 0.0f) {
			transitionToNextScreen = true;
		}
	}

	void Start()
	{
		if (GameController.instance.lastLoadedScene.Contains ("StoryScene")) {
			targetScene = "CharacterSelectionScreen";
		} else if (GameController.instance.lastLoadedScene.Contains ("CharacterSelectionScreen")) {
			targetScene = "PlatformGameScene";
		} else if (GameController.instance.lastLoadedScene.Contains ("GameScene")) {
			if(EndingController.instance.isChapter2Completed){
				targetScene = "CreditScene"; // this part need to change to ending scene
				Destroy (GameController.instance);
				Destroy (PlayerController.instance);
				EndingController.instance.ResetEndingController (false);
			}
			else{
				targetScene = "PlatformGameScene";
				EndingController.instance.isChapter2Activated = true;
				TraceController.instance.Init();
				EndingController.instance.ResetEndingController(true);
				GameController.instance.Reset();
				PlayerController.instance.Reset();
			}
		}


		myImage = GetComponent<Image> ();
	}

	void FixedUpdate () {

		if (transitionToNextScreen) {
			Color temp = myImage.color;
			if (temp.a >= 1.0f){
				if(targetScene.Length > 0){
					if(targetScene.Contains("GameScene") )
						GameController.instance.SetStartTime();
					Application.LoadLevel(targetScene);
				}
				else
					Debug.Log("ERROR!!! No target scene");
			}
			else {
				temp.a += (fadeSpeed * Time.deltaTime);
				myImage.color = temp;
			}
		} else {
			Color temp = myImage.color;
			if( temp.a > 0.0f && temp.a <= 1.0f ){
				temp.a -= (fadeSpeed * Time.deltaTime);
				myImage.color = temp;
			}
		}
	
	}
}
