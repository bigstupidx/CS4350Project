using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathText : MonoBehaviour {

	private Text myText;
	// Use this for initialization
	void Start () {
		myText = GetComponent<Text> ();

		switch (EndingController.instance.deathReason) {
		case EndingType.Ending1:
			myText.text = "A Kid found dead at train Station.\nSuspected Cause of Death:\n Suffocation";
			break;
		case EndingType.Ending2:
			myText.text = "A Kid found dead at train Station.\nSuspected Cause of Death:\n Dog Allergy";
			break;
		case EndingType.Ending3:
			myText.text = "A Kid found dead at train Station.\nSuspected Cause of Death:\n Head Injury";
			break;
		case EndingType.Ending4:
			myText.text = "A Kid found dead at train Station.\nSuspected Cause of Death:\n Fallen Ceiling";
			break;
		case EndingType.Ending5:
			myText.text = "A Kid found dead at train Station.\nSuspected Cause of Death:\n Train Accident";
			break;
		}
	}
	
//	// Update is called once per frame
//	void Update () {
//	
//	}
}
