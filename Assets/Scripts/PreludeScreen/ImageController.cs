using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageController : MonoBehaviour {

	public GameObject reference;
	public GameObject selectionText;

	public GameObject[] imageList; // [ 0 - center, 1 - left, 2 - right ]
	public Sprite[] chosenResult;  // [ 1 - chosen papa, 2 - chosen mama ]
	public bool hasSelectionMade = false;
	public bool transitToNextScene = false;

	private Color color_Clear;
	private float startTime;

	// Use this for initialization
	void Start () {
		color_Clear = new Color (1.0f, 1.0f, 1.0f, 0.0f);

		foreach (GameObject img in imageList) {
			img.GetComponent<Image>().color = color_Clear;
		}

		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		float currTime = Time.time - startTime;
		//Debug.Log ("Curr:" + currTime);

		if (!transitToNextScene) {

			if (hasSelectionMade) {
				if (!imageList [1].activeSelf && !imageList [2].activeSelf) {
					if (PlayerData.ParentGenderId == 1)  // chosen papa
						imageList [0].GetComponent<Image> ().sprite = chosenResult [1];
					else  // chosen mama
						imageList [0].GetComponent<Image> ().sprite = chosenResult [2];

					transitToNextScene = true;
					if (!imageList [0].activeSelf) {
						selectionText.SetActive(false);
						imageList [0].SetActive (true);
					}
				}

			} else {
				if (currTime > 15.0f) {
					if (!imageList [0].activeSelf) {
						if (!imageList [1].activeSelf && !imageList [2].activeSelf) {
							selectionText.SetActive(true);
							imageList [2].SetActive (true);
							imageList [1].SetActive (true);
						}
					}
				} else if (currTime > 5.0f) {
					if (!imageList [0].activeSelf) {
						imageList [0].SetActive (true);
						imageList [0].GetComponent<Image> ().sprite = chosenResult [0];
					}
				} else if (currTime > 2.0f) {
					imageList [0].SetActive (true);
					GetComponent<AudioSource>().Play();
				} else {
				
				}
			}
		
		} // end of transit Check
	}

	public void LoadNextLevel()
	{
			reference.SetActive(true);
			reference.GetComponent<BlackScreenFading> ().TransferToNextScene ();
	}
}
