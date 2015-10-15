using UnityEngine;
using System.Collections;

public class FilterBehaviour : MonoBehaviour {

	public GameObject filter;
	// Use this for initialization
	void Start () {
		if (EndingController.instance.isChapter2Activated) {
			filter.SetActive(true);
		}
		else
			filter.SetActive(false);
	}
}
