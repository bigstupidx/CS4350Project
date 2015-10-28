using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageController : MonoBehaviour {

	public GameObject reference;
	public GameObject gameTitle;
	public GameObject preludeCredit;
	public GameObject rollingText;
	
	public Texture2D[] partOneFrame;
	public Sprite[] partOneText;
	private int index = 0;

	public bool transitToNextScene = false;
	public bool fastForwardSelected = false;

	private float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.time;

		preludeCredit.SetActive (true);
	}

	
	public void FastForward()
	{
		rollingText.SetActive (false);
		preludeCredit.SetActive (false);
		index = 100;
		reference.GetComponent<FadeToClear>().TransitToNextScene();

		GameObject.Find ("FastForward").GetComponent<Text> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		float currTime = Time.time - startTime;

		if (currTime > 4.4f) {
			if(!rollingText.activeSelf){
				if(index < partOneText.Length - 1 ){
					index++;
					rollingText.SetActive(true);
					rollingText.GetComponent<Image>().sprite = partOneText[index];

					preludeCredit.GetComponent<MeshRenderer>().enabled = true;
					preludeCredit.GetComponent<MeshRenderer>().material.mainTexture = partOneFrame[index];
					startTime = Time.time;
				}
				else
				{
					gameTitle.SetActive(true);
					reference.GetComponent<FadeToClear>().TransitToNextScene();
				}
			}else
			{
				if (currTime > 6.5f) {
					preludeCredit.GetComponent<MeshRenderer>().enabled = false;
				}
			}
		}
	}
}
