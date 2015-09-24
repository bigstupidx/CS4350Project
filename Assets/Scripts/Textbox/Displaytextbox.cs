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
		if (Input.GetKeyUp (KeyCode.Space)) {
			if (feedText.ind == -1 ){	// textbox is not fed yet

				if (colliderName.Length < 1) { // player stand at out of nowhere
					feedText.SetText ("Mum?");
					textBox.TurnOnTextbox( false ); // means do not fade out
				}

				else //  player near to interactable object
				{
					Item curr = GameController.instance.GetItem (colliderName);
					bool status = PlayerController.instance.AbleToTrigger (curr);
					string respond = curr.GetRespond (status);

					feedText.SetText (respond, curr, status);
					textBox.TurnOnTextbox( false ); // means do not fade out
				}
			}
			else{	// textbox fed already
				if( feedText.getIsMoreThanOneLine() ){		// respond more than 1 line
					feedText.ind++;
					Debug.Log(feedText.ind);
					if(feedText.ind > feedText.multipleResponds.Length-1)
					{
						textBox.TurnOnTextbox( true ); // fade out
					}
					else
						textBox.TurnOnTextbox( false ); // means do not fade out
				}
				else {		// respond ONLY have 1 line
					textBox.TurnOnTextbox( true );
				}
			}
		}
	}
}
