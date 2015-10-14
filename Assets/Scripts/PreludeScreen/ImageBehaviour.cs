using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageBehaviour : MonoBehaviour {

	public bool fadeInState = true;
	public bool isSelectionFrame = false;
	 
	private float speed = 0.3f;
	private ImageController reference;

	public bool selectionEnabled = false;

	void Start()
	{
		reference = GameObject.Find ("ImageController").GetComponent<ImageController> ();
	}

	void OnEnable () {
		fadeInState = true;
		GetComponent<Image> ().color = new Color(1.0f,1.0f,1.0f, 0.01f);
	}

	public void SetParentID ()
	{
		if (selectionEnabled) {
			if(gameObject.name.Contains("Left") )
			   PlayerData.ParentGenderId = 1;
			else
			   PlayerData.ParentGenderId = 2;

			selectionEnabled = false;
			reference.hasSelectionMade = true;
		}
	}

	public void SelectionHover ()
	{
		if (selectionEnabled) {
			GetComponent<Image>().color = Color.white;
			if(gameObject.name.Contains("Left") ){
				GameObject.Find("CutsceneImage_Right").GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
			}
			else
				GameObject.Find("CutsceneImage_Left").GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
		}
	}

	public void Fading()
	{
	
	}
	
	// Update is called once per frame
	void Update () {
		Color newAlpha = GetComponent<Image> ().color;

		if (isSelectionFrame) {
			// fade in till max
			if (newAlpha.a < 1.0f && !selectionEnabled) {
				newAlpha.a += (speed * Time.deltaTime);
				GetComponent<Image> ().color = newAlpha;
			} else {  // hit max already
				selectionEnabled = true;

				if(reference.hasSelectionMade){
					GetComponent<Image> ().color = new Color(1.0f,1.0f,1.0f, 0.5f);
					isSelectionFrame = false;
					fadeInState = false;
				}
			}

		} else {
			if (newAlpha.a >= 1.0f) {
				fadeInState = false;
			} else if (newAlpha.a < 0.0f) {
				if(reference.hasSelectionMade && reference.transitToNextScene){
					reference.LoadNextLevel();
				}
				GetComponent<Image>().color = new Color(1.0f,1.0f, 1.0f, -1.0f);
				gameObject.SetActive (false);
			}

			if (fadeInState) {
				newAlpha.a += (speed * Time.deltaTime);
				GetComponent<Image> ().color = newAlpha;
			} else {
				newAlpha.a -= (speed * Time.deltaTime);
				GetComponent<Image> ().color = newAlpha;
			}
		} //  end of NOT selection frame
	}
}
