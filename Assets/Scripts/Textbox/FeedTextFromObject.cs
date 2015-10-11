using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FeedTextFromObject : MonoBehaviour {

	public bool moreThanOneLine = false;
	public bool isActivated = false;

	public bool endOfRespond = false;
	public int ind = -1;
	public string[] multipleResponds;
	private Color defaultColor;

	public Item targetItem;
	public bool targetStatus;

	private Text text;
	private float alpha = 1.0f;

	private FadeInFadeOut textBox;

	void Start(){
		textBox = GameObject.Find ("TextBox").GetComponent<FadeInFadeOut> ();
		text = transform.GetComponent<Text> ();
		defaultColor = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		ResetTextFeed ();
	}

	public void setAlpha(float _alpha)
	{
		alpha = _alpha;
	}

	public bool getIsMoreThanOneLine()
	{
		return moreThanOneLine;
	}
	public void setMoreThanOneLineStatus( bool _newValue )
	{
		moreThanOneLine = _newValue;
	}

	private string PostRespondProcessing(string _input)
	{
		string returnRespond = _input;
		if( returnRespond.Contains("[Parent]") )
		{
			string temp = "";
			if(PlayerData.ParentGenderId == 1) // Male Parent
				temp = "Papa";
			else if(PlayerData.ParentGenderId == 2) // Female Parent
				temp = "Mama";

			returnRespond = returnRespond.Replace("[Parent]", temp);
		}

		if( returnRespond.Contains("#") )
		{
			int length = returnRespond.LastIndexOf("#") - returnRespond.IndexOf("#");
			string temp = returnRespond.Substring(returnRespond.IndexOf("#")+1, length-1);
			string[] selection = temp.Split('/');

			string target = "#";
			target += temp;
			target += "#";

			if( PlayerData.GenderId == 1)
			{
				returnRespond = returnRespond.Replace(target, selection[0]);
			}
			else
				returnRespond = returnRespond.Replace(target, selection[1]);
		}

		if( returnRespond.Contains("%") )
		{
			int length = returnRespond.LastIndexOf("%") - returnRespond.IndexOf("%");
			string temp = returnRespond.Substring(returnRespond.IndexOf("%")+1, length-1);
			string[] selection = temp.Split('/');
			
			string target = "%";
			target += temp;
			target += "%";
			
			if( PlayerData.GenderId == 1)
			{
				returnRespond = returnRespond.Replace(target, selection[0]);
			}
			else
				returnRespond = returnRespond.Replace(target, selection[1]);
		}

		return returnRespond;
	}

	public void SetText(string _respond, Item _item = null, bool _status = false)
	{
		isActivated = true;
		if (_respond.Length > 1) {
			if (_respond.Contains ("\\")) {
				moreThanOneLine = true; 
				multipleResponds = _respond.Split ('\\');
                ind = 0;
                text.text = PostRespondProcessing(multipleResponds[ind]);
            } else {
				text.text = PostRespondProcessing(_respond);
				moreThanOneLine = false;
				ind = 0;
			}
		} else
			ind = -1;

		targetItem = _item;
		targetStatus = _status;


	}

	public void Update(){
		// change text set when multiple lines of responds is detected
		if (Input.GetKeyUp (KeyCode.Space) && !textBox.isFadingOn) {
			endOfRespond = false;
			
			if (moreThanOneLine) {
				if( ind < multipleResponds.Length)
					text.text = PostRespondProcessing( multipleResponds [ind] );
			}
		}

		if (isActivated) {
			if (textBox.getStatus () && textBox.isFadingOn ) {
				if(alpha < 0.0f)
					alpha = 1.0f;
				text.color = new Color (1.0f, 1.0f, 1.0f, alpha); 

				if(alpha <= 0.1f)
					endOfRespond = true;
			}
			else if (textBox.getStatus () && !textBox.isFadingOn ) {
				text.color = text.color = defaultColor;
			}
		}
	}

	void LateUpdate()
	{
		if (textBox.isFadingOn) {
				if(targetItem != null &&  targetStatus )
				{
					Debug.Log("Enter");
					GameController.instance.TriggerItem(targetItem.itemId);
					Debug.Log("Over");
					targetStatus = false;	
					targetItem = null;
					
				}
		}
		
		// guard to reset multipleResponds
		if ( (endOfRespond && isActivated) )
			ResetTextFeed ();

		if ( (!isActivated) )
			ResetTextFeed ();

	}
	
	public void ResetTextFeed()
	{
		ind = -1;
		multipleResponds = new string[0];
		endOfRespond = false;
		isActivated = false;
		text.color = defaultColor;
		text.text = "";
	}
}
