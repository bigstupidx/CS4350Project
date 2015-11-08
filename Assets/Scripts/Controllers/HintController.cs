using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HintController : MonoBehaviour {

	public GameObject textBoxRef;
	public bool hasHint = false;

	public List< List<string> > hintList;

	// Use this for initialization
	void Start () {
		hintList = new List< List<string> > ();
		for (int i = 0; i < 5; i++) {
			List<string> newList = new List<string>();
			hintList.Add(newList);
		}

	}

	public void SetHintStatus(bool _status)
	{
		hasHint = _status;
	}

	public void DisplayHint()
	{
		if (hasHint) {
			PlayerController.instance.displayHint ();
			hasHint = false;
		}
	}

	private int GetPeak(int[] _input)
	{
		int result = 0;
		int max = _input[0];

		for (int i = 0; i < _input.Length; i++) {
			if( max < _input[i]){
				max = _input[i];
				result = i;
			}
		}

		return result;
	}

	private string GetHint()
	{
		string chosenHint = "HINT";
		List<string> allHints = new List<string> (PlayerController.instance.hintDic.Keys);
		
		if (allHints.Count > 0) {
			foreach( string hintID in allHints){
				Item curr = GameController.instance.GetItem(hintID);

				if(curr !=  null){
					int endType = GetPeak( curr.endingPoints );
					Debug.Log("curr item:" +  hintID + " , " + endType);
					if( !hintList[endType].Contains(curr.idleDialogue[0]) )
					{
						hintList[endType].Add(curr.idleDialogue[0]);
					}
				}
			}

			int currPeak = GetPeak( EndingController.instance.endings );

			List<string> currSet = hintList[currPeak];
			int select = Random.Range (0, ( currSet.Count) );
			chosenHint = currSet [select];
			hintList[currPeak].Remove(chosenHint);
		}

		return chosenHint;
	}

	
	// Update is called once per frame
	void Update () {
		if (!EndingController.instance.isChapter2Activated) {
			if (hasHint) {
				this.GetComponent<Image> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			} else
				this.GetComponent<Image> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
		}
		else
			this.GetComponent<Image> ().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);

	}
}
