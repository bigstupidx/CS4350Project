using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransitionSceneSettings : MonoBehaviour {

	public bool isPartOneEnded = false;

	void Start()
	{
		transform.GetChild (0).gameObject.GetComponent<Text>().enabled = false;
		transform.GetChild (1).gameObject.GetComponent<Text>().enabled = false;
		
		if (isPartOneEnded) {
			transform.GetChild (1).gameObject.GetComponent<Text>().enabled = true;
		}
		else
		{
			transform.GetChild (0).gameObject.GetComponent<Text>().enabled = true;
		}
	}
}
