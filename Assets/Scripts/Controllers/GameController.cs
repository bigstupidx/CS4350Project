using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class GameController : MonoBehaviour {

	static public GameController instance;
	private Dictionary<string, Item> items;

	public Dictionary<string, string> allHintDic;
	public bool isChapter2Activated = false;

	private int timeSinceGameStart = 0;
	public bool isPaused = false;

	public void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
			allHintDic = new Dictionary<string, string>();
		} else {
			DestroyImmediate(gameObject);
		}
	}

	public void SetStartTime()
	{
		isPaused = false;
		timeSinceGameStart = 0;
	}

	public int GetTime(){
		return timeSinceGameStart;
	}

	public void ToggleTimer()
	{
		isPaused = !isPaused;
	}

	public void Start() {
		GameController.instance.Init ();
		EndingController.instance.Init ();
		PlayerController.instance.Init ();
	}

	public void Init() {
	
	}

	public void FixedUpdate()
	{
		if (!isPaused)
			timeSinceGameStart++;
	}

	public void InitializeLevel() {
		this.items = new Dictionary<string, Item> ();
		GameObject itemList = GameObject.Find ("Items");
		foreach (Transform t in itemList.transform) {
			items.Add(t.name, (Item) t.gameObject.GetComponent("Item"));
		}
		ItemState[] itemsState = JsonReader.readItemsState();
		List<string> noReqItems = new List<string> ();
		List<string> initiallyHiddenItems = new List<string> ();

		foreach (ItemState itemState in itemsState) {

			if (itemState.type.Equals(Item.EVENT_TYPE)) {
				if(itemState.idleDialogue.Length > 0 && !allHintDic.ContainsKey(itemState.id) ){
					allHintDic.Add(itemState.id, itemState.idleDialogue[0]);
				}
			}

			if (!items.ContainsKey(itemState.id)) {
				continue;
			}
			Item item = items[itemState.id];
			if (itemState.type.Equals(Item.EVENT_TYPE)) {
				item.loadEventItemState(itemState);
			} else {
				item.loadTransitionItemState(itemState);		
			}
			if (item.requiredItems.Length == 0) {
				noReqItems.Add(item.itemId);
			}
			if (item.isInitiallyHidden) {
				initiallyHiddenItems.Add (item.itemId);
			}

		}
		PlayerController.instance.AddInitialItems (noReqItems);
		PlayerController.instance.AddInitiallyHiddenItems (initiallyHiddenItems);
		PlayerController.instance.updatePlayerPositon ();
		PlayerController.instance.UpdateHintDic ();

		GameObject camera = GameObject.Find ("Main Camera");
		CameraFollow followCamera = camera.GetComponent<CameraFollow> ();
		Debug.Log (followCamera);
		followCamera.switchOffset (PlayerController.instance.currentLevel);
		updateItemsVisibility ();
	}

	public void GameOver(EndingType endingType) {
		switch (endingType) {
		case EndingType.Ending1:
			Debug.Log ("Game over: Death by Suffocation");
			break;
		case EndingType.Ending2:
			Debug.Log ("Game over: Death by Dog Allergy");
			break;
		case EndingType.Ending3:
			Debug.Log ("Game over: Death by Head Injury");
			break;
		case EndingType.Ending4:
			Debug.Log ("Game over: Death by Fallen Ceiling");
			break;
		case EndingType.Ending5:
			Debug.Log ("Game over: Death by Train Accident");
			break;
		}
		LevelHandler.Instance.LoadSpecific ("EndingScene");
		//Application.LoadLevel ("EndingScene");
	}

	public void TriggerItem(string itemId) {
		Item item = this.GetItem(itemId);
		if (item == null) {
			return;
		}
		if (PlayerController.instance.AbleToTrigger(item)) {
			PlayerController.instance.ItemTriggered(item);
			EndingController.instance.ItemTriggered(item);

			if(isChapter2Activated)
				TraceController.instance.TriggerItem(itemId);

			updateItemsVisibility ();
			foreach(KeyValuePair<string, Item> entry in items) {
				entry.Value.ItemTriggered(item);
			}
			if (item.type == Item.TRANSITION_TYPE) {
				this.transition(item);
			}
		}
	}

	public void transition (Item item) {
		if (item.type == Item.TRANSITION_TYPE) {
			int nextLevel = item.nextLevel;
			if (nextLevel == 2) {
				LevelHandler.Instance.LoadSpecific ("PlatformGameScene");
			} else if (nextLevel == 1) {
				//change this to the indoor scene
				LevelHandler.Instance.LoadSpecific ("GroundIndoorGameScene");
			} else if (nextLevel == 0) {
				LevelHandler.Instance.LoadSpecific ("BasementGameScene");
			} else if (nextLevel == 3) {
				LevelHandler.Instance.LoadSpecific ("GroundOutdoorGameScene");
			} else if (nextLevel == 4) {
				LevelHandler.Instance.LoadSpecific ("BakeryGameScene");
			} else if (nextLevel == 5) {
				LevelHandler.Instance.LoadSpecific ("CStoreGameScene");
			} else if (nextLevel == 6) {
				LevelHandler.Instance.LoadSpecific ("ToiletGameScene");
			}
		}
	}

	public void load() {
		PlayerController.instance.Load ();
		int currentLevel = PlayerController.instance.currentLevel;
		if (currentLevel == 2) {
			Application.LoadLevel ("PlatformGameScene");
		} else if (currentLevel == 1) {
			Application.LoadLevel ("GroundIndoorGameScene");
		} else if (currentLevel == 0) {
			Application.LoadLevel ("BasementGameScene");
		} else if (currentLevel == 3) {
			Application.LoadLevel ("GroundOutdoorGameScene");
		} else if (currentLevel == 4) {
			Application.LoadLevel ("BakeryGameScene");
		} else if (currentLevel == 5) {
			Application.LoadLevel ("CStoreGameScene");
		} else if (currentLevel == 5) {
			Application.LoadLevel ("ToiletGameScene");
		}
	}

	public void updateItemsVisibility() {
		foreach (KeyValuePair<string, Item> entry in items) {
			Item item = entry.Value;
			if (PlayerController.instance.hideItems.ContainsKey(item.itemId)) {
//				Debug.Log("Hide: " + item.itemId);
				item.gameObject.SetActive(false);
				continue;
			}
			if (PlayerController.instance.unhideItems.ContainsKey(item.itemId)) {
//				Debug.Log("Unhide: " + item.itemId);
				item.gameObject.SetActive(true);
				continue;
			}
		}
	}

	public Item GetItem(string itemId) {
		Item item;
//		Debug.Log (this.items);
		bool hasItem = this.items.TryGetValue(itemId, out item);
		if (hasItem) {
			return item;
		}

		return null;
	}

	public string GetHint(string itemId) {
		string respond;
		bool hasItem = this.allHintDic.TryGetValue(itemId, out respond);
		if (hasItem) {
			return respond;
		}
		return "";
	}
}
