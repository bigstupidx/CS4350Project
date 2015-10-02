using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class GameController : MonoBehaviour {

	static public GameController instance;
	private float timeSinceGameStart;

	private Dictionary<string, Item> items;

	public void Awake() {
		instance = this;
		DontDestroyOnLoad (this);
		timeSinceGameStart = Time.time;
	}

	public float GetTime(){
		return timeSinceGameStart;
	}

	public void Start() {
		GameController.instance.Init ();
		EndingController.instance.Init ();
		PlayerController.instance.Init ();
	}

	public void Init() {
	
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
		updateItemsVisibility ();
	}

	public void GameOver(EndingType endingType) {
		Debug.Log ("Game over");
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
			foreach(KeyValuePair<string, Item> entry in items) {
				entry.Value.ItemTriggered(item);
			}
		}
		updateItemsVisibility ();
	}

	public void updateItemsVisibility() {
		foreach (KeyValuePair<string, Item> entry in items) {
			Item item = entry.Value;
			if (PlayerController.instance.hideItems.ContainsKey(item.itemId)) {
				item.enabled = false;
				continue;
			}
			if (PlayerController.instance.unhideItems.ContainsKey(item.itemId)) {
				item.enabled = true;
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
}
