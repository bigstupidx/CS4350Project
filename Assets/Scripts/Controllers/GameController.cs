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
		Debug.Log ("Start GameController");
		GameController.instance.Init ();
		EndingController.instance.Init ();
		PlayerController.instance.Init (this.getInitialItems());
	}

	public void Init() {
		//this.loadLevel(2);
		//this.InitializeLevel ();
	}

	public void InitializeLevel() {
		this.items = new Dictionary<string, Item> ();
		Debug.Log ("Initialize Items: " + items);
		GameObject itemList = GameObject.Find ("Items");
		foreach (Transform t in itemList.transform) {
			items.Add(t.name, (Item) t.gameObject.GetComponent("Item"));
		}
		ItemState[] itemsState = JsonReader.readItemsState();
		foreach (ItemState itemState in itemsState) {
			if (!items.ContainsKey(itemState.id)) {
				continue;
			}
			Debug.Log ("Read item level: " + itemState.level);
			Item item = items[itemState.id];
			if (itemState.type.Equals(Item.EVENT_TYPE)) {
				item.loadEventItemState(itemState);
			} else {
				item.loadTransitionItemState(itemState);		
			}
		}
		Debug.Log ("done loading level");
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

	private List<string> getInitialItems() {
		List<string> initialItems = new List<string>();
		foreach (KeyValuePair<string, Item> entry in items) {
			Item item = entry.Value;
			if (item.itemId != null && item.itemId.Length > 0 && item.requiredItems.Length == 0) {
				initialItems.Add(entry.Value.itemId);
			}
		}
		return initialItems;
	}
}
