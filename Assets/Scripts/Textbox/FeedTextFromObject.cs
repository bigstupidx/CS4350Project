using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FeedTextFromObject : MonoBehaviour {

	public bool moreThanOneLine = false;
	public bool isActivated = false;

	public bool endOfRespond = false;
	public string[] multipleResponds = new string[1];
	private Color defaultColor = new Color (1.0f, 1.0f, 1.0f, 1.0f);

	public int ind = -1;
	public Item targetItem;
	public bool targetStatus;

	private Text text;
	private float alpha = 1.0f;

	private FadeInFadeOut textBox;

	void Start(){
		textBox = transform.GetComponentInParent<FadeInFadeOut> ();
		text = transform.GetComponent<Text> ();
		ResetTextFeed ();
	}

	public void setAlpha(float _alpha)
	{
		text.color = new Color (1.0f, 1.0f, 1.0f, _alpha); 
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
		if (_respond.Length > 1) {
			if (_respond.Contains ("\\")) {
				moreThanOneLine = true; 
				multipleResponds = _respond.Split ('\\');
			} else {
				moreThanOneLine = false;
				multipleResponds[0] = _respond;
			}
			text.text = PostRespondProcessing( multipleResponds [0] );
		} 

		targetItem = _item;
		targetStatus = _status;

		UpdateText (0);
	}

	public void UpdateText(int _index)
	{
		if (moreThanOneLine) {
			if (_index < multipleResponds.Length) {
				text.text = PostRespondProcessing (multipleResponds [_index]);
				endOfRespond = false;
				isActivated = true;
			} else {
				endOfRespond = true;
				isActivated = true;
			}
		}
	}
	
	public void ResetTextFeed()
	{
		multipleResponds = new string[1];
		endOfRespond = false;
		isActivated = false;
		text.color = defaultColor;
		text.text = "";

		if (targetItem != null && targetStatus) {
			GameController.instance.TriggerItem(targetItem.itemId);
		}
	}
}
