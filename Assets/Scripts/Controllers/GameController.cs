using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class GameController : MonoBehaviour {

	static public GameController instance;
	private Dictionary<string, Item> items;

	public Dictionary<string, string> allHintDic;
	public Dictionary<string, DateTime> timeStamp;

	private DateTime startGameTime;
	public string lastLoadedScene = "TitleScene";
	private int timeSinceGameStart = 0;
	public bool isAndroidVersion = false;
	public bool isPaused = false;

	private float prevTime;

	private int realTime_hour = 0;
	private int realTime_minute = 0;
	private int realTime_second = 0;

	public void Awake() {
		if (instance == null) {
			AudioListener.pause = false;
			instance = this;
			DontDestroyOnLoad (this);
			allHintDic = new Dictionary<string, string>();
			timeStamp = new Dictionary<string, DateTime>();
		} else {
			DestroyImmediate(gameObject);
		}
	}

	public void Reset()
	{
		SetStartTime ();
	}

	public void SetStartTime()
	{
		isPaused = false;
		startGameTime = System.DateTime.Now;

		realTime_hour = startGameTime.Hour;
		realTime_minute = startGameTime.Minute;
		realTime_second = startGameTime.Second;


		prevTime = Time.time;
		timeSinceGameStart = 0;
	}

	public int GetTick(){
		return timeSinceGameStart;
	}

	public DateTime GetCurrentObjectTime()
	{
		return startGameTime;
	}

	public int GetActualTime(int _option){
		switch (_option) {
		case 0: // seconds
			return realTime_second;
		case 1: // minutes
			return realTime_minute;
		case 2:
			return realTime_hour;
		}
		return 0;

	}

	private void incrementSeconds()
	{
		realTime_second++;

		if (realTime_second == 60) {
			realTime_minute++;
			realTime_second = 0;
		}
		
		if (realTime_minute == 60) {
			realTime_hour++;
			realTime_minute = 0;
		}
		
		if (realTime_hour == 24) {
			realTime_hour = 0;
		}
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

	public void Update()
	{
		if (!isPaused) {
			if( Time.time - prevTime > 1.0f){
				incrementSeconds();
				timeSinceGameStart++;
				prevTime = Time.time;
			}
		}
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

		GameObject camera = Camera.main.gameObject;//GameObject.Find ("Main Camera");
		CameraFollow followCamera = camera.GetComponent<CameraFollow> ();
		//Debug.Log (followCamera);
		followCamera.switchOffset (PlayerController.instance.currentLevel);
		updateItemsVisibility ();
	}

	public void GameOver(EndingType endingType) {

		string[] ending = this.getCorePath ();
		
//		switch (endingType) {
//		case EndingType.Ending1:
//			Debug.Log ("Game over: Death by Suffocation");
//			break;
//		case EndingType.Ending2:
//			Debug.Log ("Game over: Death by Dog Allergy");
//			break;
//		case EndingType.Ending3:
//			Debug.Log ("Game over: Death by Head Injury");
//			break;
//		case EndingType.Ending4:
//			Debug.Log ("Game over: Death by Fallen Ceiling");
//			break;
//		case EndingType.Ending5:
//			Debug.Log ("Game over: Death by Train Accident");
//			break;
//		}

		if (EndingController.instance.isChapter2Activated) {	// this part need to change
			EndingController.instance.isChapter2Completed = true;
		} 
			GameController.instance.SetLastLoadedScene(Application.loadedLevelName);
			LevelHandler.Instance.LoadSpecific ("TransitionScene");
	}

	public void SetLastLoadedScene(string _sceneName)
	{
		lastLoadedScene = _sceneName;
	}

	public void SetChapter2ObjectTime(string itemId)
	{
		if (EndingController.instance.isChapter2Activated) {
			
			DateTime currTimeStamp;
			bool hasItem = timeStamp.TryGetValue (itemId, out currTimeStamp);
			if (hasItem) {
				if (timeStamp.ContainsKey ("StartGame"))
					timeStamp.Remove ("StartGame");
				
				startGameTime = currTimeStamp;
				GameObject.Find ("timer").GetComponent<TimeCounter> ().setTime (currTimeStamp.Hour, currTimeStamp.Minute, currTimeStamp.Second);
			}
		}
	}

	public void TriggerItem(string itemId) {
		Item item = this.GetItem(itemId);
		if (item == null) {
			return;
		}
		if (PlayerController.instance.AbleToTrigger(item)) {
			if (item.type == Item.TRANSITION_TYPE) {
				this.transition(item);
			}
			else{
				if(!EndingController.instance.isChapter2Activated)
					timeStamp.Add(itemId, DateTime.Now);
			}

			PlayerController.instance.ItemTriggered(item);
			EndingController.instance.ItemTriggered(item);

			if(EndingController.instance.isChapter2Activated){
				TraceController.instance.TriggerItem(itemId);
			}

			updateItemsVisibility ();
			foreach(KeyValuePair<string, Item> entry in items) {
				entry.Value.ItemTriggered(item);
			}
		}
	}

	public void transition (Item item) {
		if (item.type == Item.TRANSITION_TYPE) {

			// Disable action during transition
			GameObject.FindGameObjectWithTag("Player").GetComponent<Displaytextbox>().canTextBoxDisplay = false;
			PlayerData.MoveFlag = false;

			if(EndingController.instance.isChapter2Activated)
				TraceController.instance.TurnOffLine();

			if(isAndroidVersion){
				GameObject.Find("InteractionButton").GetComponent<BubbleBehaviour> ().TurnOffButton();
			}

			SetLastLoadedScene(Application.loadedLevelName);

			int nextLevel = item.nextLevel;
			if (nextLevel == 2) {
				LevelHandler.Instance.LoadSpecific ("PlatformGameScene");
			} else if (nextLevel == 1) {
				LevelHandler.Instance.LoadSpecific ("GroundIndoorGameScene");
			} else if (nextLevel == 0) {
				LevelHandler.Instance.LoadSpecific ("BasementGameScene");
			} else if (nextLevel == 3) {
				LevelHandler.Instance.LoadSpecific ("GroundOutdoorGameScene");
			} else if (nextLevel == 4) {
				LevelHandler.Instance.LoadSpecific ("CafeGameScene");
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
			Application.LoadLevel ("CafeGameScene");
		} else if (currentLevel == 5) {
			Application.LoadLevel ("CStoreGameScene");
		} else if (currentLevel == 6) {
			Application.LoadLevel ("ToiletGameScene");
		}
	}

	public void updateItemsVisibility() {
		foreach (KeyValuePair<string, Item> entry in items) {
			Item item = entry.Value;
			if (PlayerController.instance.hideItems.ContainsKey(item.itemId)) {
				item.gameObject.SetActive(false);
				continue;
			}
			if (PlayerController.instance.unhideItems.ContainsKey(item.itemId)) {
				item.gameObject.SetActive(true);
				continue;
			}
		}
	}

	public Item GetItem(string itemId) {
		Item item;
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
			if(respond.Length > 0)
				return respond;
		}
		return "";
	}

	public string[] getCorePath() {
		string[] triggeredItems = PlayerController.instance.triggeredItems.ToArray();
		Dictionary<string, int> triggeredDict = new Dictionary<string, int> ();
		for (int i = 0; i < triggeredItems.Length; i++) {
			triggeredDict.Add(triggeredItems[i], i);
		}
		ItemState[] itemsState = JsonReader.readItemsState();
		Dictionary<string, ItemState> itemDict = new Dictionary<string, ItemState> ();
		Dictionary<string, string> path = new Dictionary<string, string> ();
		foreach (ItemState itemState in itemsState) {
			itemDict.Add(itemState.id, itemState);
		}
		string lastItemId = triggeredItems [triggeredItems.Length - 1];
		dfs (null, lastItemId, itemDict, triggeredDict, path);
		List<string> list = new List<string> ();
		ItemState item = itemDict [lastItemId];
		list.Add (item.id);
		while (item.requiredItems.Length != 0) {
			item = itemDict[path[item.id]];
			list.Add(item.id);
		}
		list.Reverse ();


		// remove compliment of death sequence time stamp
		Dictionary<string, DateTime> coreTimeStamp = new Dictionary<string, DateTime>();
		foreach (string itemId in list) {
			if( timeStamp.ContainsKey(itemId) ){
				DateTime value = DateTime.Now;

				if( timeStamp.TryGetValue( itemId, out value ) )
					coreTimeStamp.Add(itemId, value) ;
			}
		}

		timeStamp.Clear ();
		timeStamp = coreTimeStamp;
		timeStamp.Add ("StartGame", startGameTime);
		
		return list.ToArray();
	}




	public void dfs(string previousItemId, string itemId, Dictionary<string, ItemState> itemDict, Dictionary<string, int> triggeredDict, Dictionary<string, string> path) {
		ItemState item = itemDict [itemId];
		if (previousItemId != null) {
			path [previousItemId] = itemId;
		}
		if (item.requiredItems.Length == 0) {
			return;
		}
		foreach (string prereqItemId in item.requiredItems) {
			if (triggeredDict[prereqItemId] != null && triggeredDict[prereqItemId] < triggeredDict[itemId]) {
				dfs (itemId, prereqItemId, itemDict, triggeredDict, path);
			}
		}
	}
}
