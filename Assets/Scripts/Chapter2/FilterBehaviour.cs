using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FilterBehaviour : MonoBehaviour {

	//public GameObject filter;
	// Use this for initialization
	void Start () {
		if (EndingController.instance.isChapter2Activated) {
			GetComponent<Image>().color = new Color( (112.0f/255.0f), (66.0f/255.0f), (20.0f/255.0f), 0.3f );
		}
		else
			GetComponent<Image>().color = new Color( 1.0f, 1.0f, 1.0f, 0.0f );
	}
}
