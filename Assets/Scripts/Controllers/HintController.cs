using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HintController : MonoBehaviour {

	public GameObject textBoxRef;
	public bool hasHint = false;

	public List< List<string> > hintList;

	private float hintTimer;

	// Use this for initialization
	void Start () {
		hintList = new List< List<string> > ();
		for (int i = 0; i < 5; i++) {
			List<string> newList = new List<string>();
			hintList.Add(newList);
		}

		hintTimer = Time.time;
	}

	public void DisplayHint()
	{
		if (textBoxRef == null) {
			PlayerController.instance.SetTextboxObj ();
			textBoxRef = PlayerController.instance.GetTextboxObj ();
		}


		string respond = GetHint();

		textBoxRef.transform.GetChild(0).GetComponent<FeedTextFromObject> ().SetText (respond);
		textBoxRef.GetComponent<FadeInFadeOut> ().TurnOnTextbox (false, true);

		hasHint = false;
		hintTimer = Time.time;
	}

	private string GetHint()
	{
		string chosenHint = "HINT";
//		List<string> allHints = new List<string> (PlayerController.instance.hintDic.Keys);
//		
//		if (allHints.Count > 0) {
//			foreach( string hintID in allHints){
//				Item curr = GameController.instance.GetItem(hintID);
//				int endType = curr.GetEndingType();
//
//				if( !hintList[endType].Contains(curr.idleDialogue[0]) )
//				{
//					hintList[endType].Add(curr.idleDialogue[0]);
//				}
//			}
//
//			int currPeak = EndingController.instance.GetCurrEndingPeak();
//
//			List<string> currSet = hintList[currPeak];
//			int select = Random.Range (0, ( currSet.Count) );
//			chosenHint = currSet [select];
//		}

		return chosenHint;
	}

	
	// Update is called once per frame
	void Update () {
		if ((Time.time - hintTimer) > 15.0f) {
			hasHint = true;
		}

		if (hasHint) {
			this.GetComponent<Image>().color = new Color( 1.0f, 1.0f, 1.0f, 1.0f);
		}
		else
			this.GetComponent<Image>().color = new Color( 1.0f, 1.0f, 1.0f, 0.5f);

	}
}
