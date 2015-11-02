using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransitionSceneSettings : MonoBehaviour {

	public bool isPartOneEnded = false;

	private GameObject[] children = new GameObject[2];

	void Start()
	{
		isPartOneEnded = EndingController.instance.isChapter2Activated;

		children [0] = transform.GetChild (0).gameObject;
		children [1] = transform.GetChild (1).gameObject;

		foreach (GameObject child in children) {
			child.SetActive(false);
		}
		
		if (isPartOneEnded || EndingController.instance.isChapter2Completed) {
			transform.GetChild (1).gameObject.SetActive(true);
		}
		else{
			transform.GetChild (0).gameObject.SetActive(true);
		}
	}
}
