using UnityEngine;
using System.Collections;

public class Displaytextbox : MonoBehaviour {

	bool isTrigger = false;
	public string colliderName = "";

	private FeedTextFromObject feedText;
	private FadeInFadeOut textBox;

	// Use this for initialization
	void Start () {
		feedText = GameObject.Find ("ObjectRespond").GetComponent<FeedTextFromObject> ();
		textBox = GameObject.Find ("TextBox").GetComponent<FadeInFadeOut> ();
	}

	void OnTriggerEnter(Collider other){
		isTrigger = true;
		colliderName = other.name;
		//Debug.Log ("Enter: " + colliderName);
	}
	
	void OnTriggerExit(Collider other){
		isTrigger = false;
		colliderName = "";
		//Debug.Log ("Exit: " + colliderName);
	}

		
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Space) && !colliderName.Contains("ZoomPoint") ) {

			if (feedText.ind == -1 ){	// textbox is not fed yet

				if (colliderName.Length < 1) { // player stand at out of nowhere
					if(PlayerData.ParentGenderId == 1) // Male Parent
						feedText.SetText ("Papa?");
					else if(PlayerData.ParentGenderId == 2) // Female Parent
						feedText.SetText ("Mama?");
					textBox.TurnOnTextbox( false ); // means do not fade out
					Debug.Log("Done Feeding: out of no where");
				}

				else //  player near to interactable object
				{
					Item curr = GameController.instance.GetItem (colliderName);
					bool status = PlayerController.instance.AbleToTrigger (curr);
					string respond = curr.GetRespond (status);

					if(respond.Length > 0){		// not empty respond 
						transform.GetComponent <PlayerMovement>().StopMoving ();//PlayerData.MoveFlag = false;	// disable player move
						feedText.SetText (respond, curr, status);
						textBox.TurnOnTextbox( false ); // means do not fade out
					}
				}
			}
			else{	// textbox fed already
				if( feedText.getIsMoreThanOneLine() ){		// respond more than 1 line
					feedText.ind++;
					Debug.Log("More than one line!! Incremented to " + feedText.ind);
					if(feedText.ind == feedText.multipleResponds.Length)
					{
						textBox.TurnOnTextbox( true ); // fade out
						//PlayerData.MoveFlag = true;	// ENABLE player move
						Debug.Log("Exceed array bound. Fading ON");
					}
					else if(feedText.ind < feedText.multipleResponds.Length){
						transform.GetComponent <PlayerMovement>().StopMoving ();//PlayerData.MoveFlag = false;	// disable player move
						textBox.TurnOnTextbox( false ); // means do not fade out
						Debug.Log("Less than array bound. Fading OFF");
					}
				}
				else {		// respond ONLY have 1 line
					if( !textBox.isFadingOn ){
						textBox.TurnOnTextbox( true );
						PlayerData.MoveFlag = true;	// ENABLE player move
						Debug.Log("Only one line. Fading ON");
					}
				}
			}
		}
	}
}
