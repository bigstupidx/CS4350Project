using UnityEngine;
using System.Collections;

public class obtainItem : MonoBehaviour {

	// Use this for initialization
	public GameObject feedback;
	public GameObject noObjectFoundFeedback;
	//private int countKeyboardHit = 0;

	static  bool showingBox = false;

	void Start () {
		feedback.SetActive (false);
		noObjectFoundFeedback.SetActive (false);
	}

	public void ButtonClick(){

		if(showingBox){
			feedback.SetActive (false);
			noObjectFoundFeedback.SetActive (false);
			PlayerData.MoveFlag = true;
			showingBox = false;
		}
		else if (PlayerData.nearestObjectId >= 0) {
			
			if(!TempDatabase.CollectedFlags[PlayerData.nearestObjectId]){
				feedback.SetActive (true);
				PlayerData.MoveFlag = false;
				showingBox = true;
				TempDatabase.CollectedFlags[PlayerData.nearestObjectId] = true;
			}
			else{
				noObjectFoundFeedback.SetActive (true);
				PlayerData.MoveFlag = false;
				showingBox = true;
			}
			
		}
		else{
			feedback.SetActive (false);
			noObjectFoundFeedback.SetActive (false);
		}
	}


	void Update() {


		if (Input.GetKeyDown (KeyCode.Space)) {

			if(showingBox){
				feedback.SetActive (false);
				noObjectFoundFeedback.SetActive (false);
				PlayerData.MoveFlag = true;
				showingBox = false;
			}
			else if (PlayerData.nearestObjectId >= 0) {

				if(!TempDatabase.CollectedFlags[PlayerData.nearestObjectId]){
					feedback.SetActive (true);
					PlayerData.MoveFlag = false;
					showingBox = true;
					TempDatabase.CollectedFlags[PlayerData.nearestObjectId] = true;
				}
				else{
					noObjectFoundFeedback.SetActive (true);
					PlayerData.MoveFlag = false;
					showingBox = true;
				}

			}
			else{
				feedback.SetActive (false);
				noObjectFoundFeedback.SetActive (false);
			}
		}

	}
	
}
