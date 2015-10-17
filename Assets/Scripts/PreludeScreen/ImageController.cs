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
	public Sprite[] partOneText;
	private int index = 0;

	private Vector3[] oriPos = new Vector3[3]; // [ 0 - center, 1 - left, 2 - right ]
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

		int index = 0;
		foreach (GameObject img in imageList) {
			oriPos[index] = img.transform.position;
			img.GetComponent<Image>().color = color_Clear;
			img.SetActive(false);
			index++;
		}

		oriPos [0] = preludeCredit.transform.position;

		startTime = Time.time;

		preludeCredit.SetActive (true);
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
					if (currTime > 16.0f) {
						if (!imageList [0].activeSelf) {
							if (!imageList [1].activeSelf && !imageList [2].activeSelf) {
								selectionText.SetActive (true);
								imageList [2].SetActive (true);
								imageList [1].SetActive (true);
							}
						}
					} else if (currTime > 6.0f) {
						if (!imageList [0].activeSelf) {
							imageList [0].SetActive (true);
							imageList[0].GetComponent<ImageBehaviour>().SetPosition(oriPos[2]);
							imageList [0].GetComponent<Image> ().sprite = chosenResult [0];
						}
					} else if (currTime > 3.0f) {
						imageList [0].SetActive (true);
						imageList[0].GetComponent<ImageBehaviour>().SetPosition(oriPos[1]);
					} else {
				
					}
				}
		
			} // end of transit Check

		} else {
			if (currTime > 4.4f) {
				if( index < partOneFrame.Length ){
					if(!preludeCredit.activeSelf){

						if(!rollingText.activeSelf && index < partOneText.Length){
							rollingText.SetActive(true);
							rollingText.GetComponent<Image>().sprite = partOneText[index];
						}

						if( index == partOneFrame.Length-1 )
						{
							preludeCredit.GetComponent<ImageBehaviour>().isMoving = false;
						}


						preludeCredit.SetActive(true);
						preludeCredit.GetComponent<ImageBehaviour>().SetPosition(oriPos[0]);
						preludeCredit.GetComponent<Image>().sprite = partOneFrame[index];
						startTime = Time.time;
						index++;

					}
				}
				else{
					if( preludeCredit.GetComponent<Image>().color.a <= 0.5f ){
						hasPartOneCompleted = true;
						startTime = Time.time;
						GameObject.Find ("BGM").GetComponent<AudioSource>().Stop();
						GetComponent<AudioSource> ().clip = preludeSfx[0];
						GetComponent<AudioSource> ().Play ();
					}
					else{
						GameObject.Find ("BGM").GetComponent<AudioSource>().volume = (GameObject.Find ("BGM").GetComponent<AudioSource>().volume - 0.1f *Time.deltaTime);
					}
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
