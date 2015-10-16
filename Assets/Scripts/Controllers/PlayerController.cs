using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	static public PlayerController instance;

	public List<string> triggeredItems;
	public Dictionary<string, string> validItems;
	public Dictionary<string, string> restrictedItems;
	public Dictionary<string, string> hideItems;
	public Dictionary<string, string> unhideItems;
	public Dictionary<string, string> hintDic;
	public double[] position;
	public int currentLevel;

	private int idleTimer;

	public void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
			hintDic = new Dictionary<string, string> ();
		} else {
			DestroyImmediate(gameObject);
		}
	}

	public void Reset()
	{
		Init ();
	}

	public void Init() {
		triggeredItems = new List<string>();
		validItems = new Dictionary<string, string> ();
		restrictedItems = new Dictionary<string, string>();
		hideItems = new Dictionary<string, string> ();
		unhideItems = new Dictionary<string, string> ();
		currentLevel = 2;
		position = new double[3] {0.0f, 0.5f, 0.0f};

		idleTimer = GameController.instance.GetTime ();
	}

	public void UpdateHintDic()
	{
		foreach (string itemId in validItems.Keys) {
			if( !hintDic.ContainsKey(itemId) )
			{
				if(GameController.instance.allHintDic.ContainsKey(itemId) )
				{
					string _respond = GameController.instance.GetHint(itemId);
					hintDic.Add(itemId, _respond);
				}
			}
		}

		foreach (string itemId in restrictedItems.Keys) {
			if( hintDic.ContainsKey(itemId) ){
				hintDic.Remove(itemId);
			}
		}
	}


	public void AddInitialItems(List<string> initialItems) {
		foreach (string itemId in initialItems) {
			if (!restrictedItems.ContainsKey(itemId) && !validItems.ContainsKey(itemId)) {
				validItems.Add(itemId, "true");
			}
		}
	}

	public void AddInitiallyHiddenItems(List<string> initialHiddenItems) {
		foreach (string itemId in initialHiddenItems) {
			if (!unhideItems.ContainsKey(itemId) && !hideItems.ContainsKey(itemId)) {
				hideItems.Add(itemId, "true");
			}
		}
	}

	public void Load() {
		PlayerState saveState = JsonReader.readPlayerState ();
		this.triggeredItems = saveState.triggeredItems;
		validItems = new Dictionary<string, string> ();
		foreach (string itemId in saveState.validItems) {
			validItems.Add(itemId, "true");
		}
		restrictedItems = new Dictionary<string, string> ();
		foreach (string itemId in saveState.restrictedItems) {
			validItems.Add(itemId, "true");
		}
		hideItems = new Dictionary<string, string> ();
		foreach (string itemId in saveState.hideItems) {
			hideItems.Add (itemId, "true");
		}
		unhideItems = new Dictionary<string, string> ();
		foreach (string itemId in saveState.unhideItems) {
			unhideItems.Add (itemId, "true");
		}
		this.position = saveState.position;
		this.currentLevel = saveState.currentLevel;

		UpdateHintDic ();
	}

	public void Save() {
		PlayerState saveState = new PlayerState ();
		saveState.triggeredItems = this.triggeredItems;
		saveState.validItems = new List<string>(this.validItems.Keys);
		saveState.restrictedItems = new List<string>(this.restrictedItems.Keys);
		saveState.hideItems = new List<string> (this.hideItems.Keys);
		saveState.unhideItems = new List<string> (this.unhideItems.Keys);
		saveState.currentLevel = this.currentLevel;
		saveState.position = this.position;
		JsonReader.writePlayerState (saveState);
	}

	public bool AbleToTrigger(Item item) {
		string itemId = item.itemId;
		foreach (string requiredItemId in item.requiredItems) {
			if (!triggeredItems.Contains(requiredItemId)) {
				return false;
			}
		}

		return validItems.ContainsKey(itemId) 
			&& !triggeredItems.Contains(itemId) 
			&& !restrictedItems.ContainsKey(itemId);
	}

	public void updatePlayerPositon() {
		GameObject player = GameObject.Find ("Player2D");
		player.transform.position = new Vector3 ((float) position [0], (float) position [1], (float) position [2]);
	}

	public void ItemTriggered(Item item) {
		string itemId = item.itemId;
		if (item.type.Equals (Item.EVENT_TYPE)) {
			triggeredItems.Add(itemId);
			validItems.Remove(itemId);

			if (!restrictedItems.ContainsKey(itemId)) {
				restrictedItems.Add(itemId, "true");
			}
		}

		if (item.type.Equals (Item.TRANSITION_TYPE)) {
			position = item.offset;
			currentLevel = item.nextLevel;
			this.Save();
		}

		foreach (string leadItemId in item.leadItems) {
			if (!validItems.ContainsKey(leadItemId)) {
				validItems.Add(leadItemId, "true");
			}
		}

		foreach (string restrictedItemId in item.restrictedItems) {
			if (!restrictedItems.ContainsKey(restrictedItemId)) {
				restrictedItems.Add(restrictedItemId, "true");
			}
			validItems.Remove(restrictedItemId);
		}

		foreach (string hideItemId in item.hideItems) {
			if (!hideItems.ContainsKey(hideItemId)) {
				hideItems.Add (hideItemId, "true");
			}
			unhideItems.Remove(hideItemId);
		}

		foreach (string unhideItemId in item.unhideItems) {
			if (!unhideItems.ContainsKey(unhideItemId)) {
				unhideItems.Add (unhideItemId, "true");
			}
			hideItems.Remove(unhideItemId);
		}

		idleTimer = GameController.instance.GetTime ();
		UpdateHintDic ();
	}

	public void Update()
	{
		int currTime = GameController.instance.GetTime () ;
		int timeDiff = ( (currTime - idleTimer) / 60) % 60;

		if ((Application.loadedLevelName).Contains ("GameScene") && !EndingController.instance.isChapter2Activated) {
			if (timeDiff >= 30) {
				if (!GameObject.Find ("TextBox").GetComponent<FadeInFadeOut> ().isActivated) {
					displayHint ();
				}
				idleTimer = currTime;
			}
		}
	}

	public void displayHint()
	{
		List<string> allHints = new List<string> (hintDic.Keys);

		if (allHints.Count > 0) {
			int select = Random.Range (0, (allHints.Count));
			string chosenHint = allHints [select];

			string respond;
			bool hasItem = hintDic.TryGetValue (chosenHint, out respond);
			if (hasItem) {
				GameObject.Find ("ObjectRespond").GetComponent<FeedTextFromObject> ().SetText (respond);
				GameObject.Find ("TextBox").GetComponent<FadeInFadeOut> ().TurnOnTextbox (false);
			}
		}
	}
}

public class PlayerState {
	public List<string> triggeredItems;
	public List<string> validItems;
	public List<string> restrictedItems;
	public List<string> hideItems;
	public List<string> unhideItems;
	public double[] position;
	public int currentLevel;

	public PlayerState() {
		triggeredItems = new List<string> ();
		validItems = new List<string> ();
		restrictedItems = new List<string> ();
		hideItems = new List<string> ();
		unhideItems = new List<string> ();
		position = new double[3] {0.0f, 0.5f, 0.0f};
		currentLevel = 2;
	}
}
