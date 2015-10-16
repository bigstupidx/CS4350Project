using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageController : MonoBehaviour {

	public GameObject reference;
	public GameObject selectionText;
	public GameObject preludeCredit;
	public GameObject rollingText;

	private bool hasPartOneCompleted = false;
	public Sprite[] partOneFrame;
	private int index = 0;

	public GameObject[] imageList; // [ 0 - center, 1 - left, 2 - right ]
	public Sprite[] chosenResult;  // [ 1 - chosen papa, 2 - chosen mama ]
	public bool hasSelectionMade = false;
	public bool transitToNextScene = false;

	public AudioClip[] preludeSfx;

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

		if (hasPartOneCompleted) {

			if (!transitToNextScene) {

				if (hasSelectionMade) {
					if (!imageList [1].activeSelf && !imageList [2].activeSelf) {
						if (PlayerData.ParentGenderId == 1)  // chosen papa
							imageList [0].GetComponent<Image> ().sprite = chosenResult [2];
						else  // chosen mama
							imageList [0].GetComponent<Image> ().sprite = chosenResult [1];

						transitToNextScene = true;
						if (!imageList [0].activeSelf) {
							selectionText.SetActive (false);
							imageList [0].SetActive (true);
							GetComponent<AudioSource> ().clip = preludeSfx[1];
							GetComponent<AudioSource> ().Play ();
						}
					}

				} else {
					if (currTime > 15.0f) {
						if (!imageList [0].activeSelf) {
							if (!imageList [1].activeSelf && !imageList [2].activeSelf) {
								selectionText.SetActive (true);
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
					} else {
				
					}
				}
		
			} // end of transit Check

		} else {
			if (currTime > 4.4f) {
				if( index < partOneFrame.Length ){
					if(!preludeCredit.activeSelf){
						preludeCredit.SetActive(true);
						preludeCredit.GetComponent<Image>().sprite = partOneFrame[index++];
						startTime = Time.time;
					}
				}
				else{
					hasPartOneCompleted = true;
					startTime = Time.time;
					GameObject.Find ("BGM").GetComponent<AudioSource>().Stop();
					GetComponent<AudioSource> ().clip = preludeSfx[0];
					GetComponent<AudioSource> ().Play ();
					rollingText.SetActive(true);
				}
			}
		
		}
	}

	public void LoadNextLevel()
	{
			reference.SetActive(true);
			reference.GetComponent<BlackScreenFading> ().TransferToNextScene ();
	}
}
