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
		timeSinceGameStart = Time.deltaTime;
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
		foreach (ItemState itemState in itemsState) {
			if (!items.ContainsKey(itemState.id)) {
				continue;
			}
			if (itemState.requiredItems.Length == 0) {
				noReqItems.Add(itemState.id);
			}
			Item item = items[itemState.id];
			if (itemState.type.Equals(Item.EVENT_TYPE)) {
				item.loadEventItemState(itemState);
			} else {
				item.loadTransitionItemState(itemState);		
			}
		}
		PlayerController.instance.AddInitialItems (noReqItems);
	}

	public void GameOver(EndingType endingType) {
		Debug.Log ("Game Over");
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
	}

	public Item GetItem(string itemId) {
		Item item;
		Debug.Log (this.items);
		bool hasItem = this.items.TryGetValue(itemId, out item);
		if (hasItem) {
			return item;
		}

		return null;
	}
}
