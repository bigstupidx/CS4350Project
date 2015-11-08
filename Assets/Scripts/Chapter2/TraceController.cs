using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TraceController : MonoBehaviour {
	
	public List<string> storyList;
	public Material modelTexture;
	public Texture2D[] textureArray;
	private LineRenderer lineRenderer;
	private float counter;
	private float distance;

	public Vector3 origin;
	public Vector3 destination;

	public float speed = 5;

	static public TraceController instance;
	public Dictionary<string, ItemState> allItemDic;

	private GameObject player;
	
	public void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
			allItemDic = new Dictionary<string, ItemState>();
			lineRenderer = transform.GetComponent<LineRenderer>();
			//Init();
		} else {
			DestroyImmediate(gameObject);
		}
	}

	void Start()
	{
		if(Application.loadedLevelName.Contains ("GameScene") && EndingController.instance.isChapter2Activated)
			Init ();
	}

	public void TurnOffLine()
	{
		lineRenderer.enabled = false;
		//SetDestination ("Player2D");
	}

	public void TurnOnLine()
	{
		lineRenderer.enabled = true;
		//SetDestination (storyList [0]);
	}
	
	public void Init()
	{
		if (EndingController.instance.isChapter2Activated) {
			EndingState[] endingList = JsonReader.readEndingState ();
			ItemState[] itemList = JsonReader.readItemsState ();

			foreach (ItemState currItem in itemList) {
				if (!allItemDic.ContainsKey (currItem.id))
					allItemDic.Add (currItem.id, currItem);
			}

			foreach (EndingState currEnding in endingList) {
				if (currEnding.id.CompareTo ((EndingController.instance.deathReason).ToString ()) == 0) {
					storyList = new List<string> (currEnding.sequence);
					break;
				}
			}

			List<string> savedTriggered = PlayerController.instance.triggeredItems;
			foreach (string item in savedTriggered) {
				storyList.Remove (item);
			}


			lineRenderer.SetPosition (0, origin);
			lineRenderer.SetWidth (0.10f, 0.10f);
			lineRenderer.SetColors (new Color (1.0f, 1.0f, 1.0f, 0.7f), new Color (1.0f, 1.0f, 1.0f, 0.2f));

			if (storyList.Count > 0) {
				SetDestination (storyList [0]);
		
				distance = Vector3.Distance (origin, destination);
			} else
				TurnOffLine ();

			if (player == null)
				player = GameObject.FindGameObjectWithTag ("Player");
		}

	}


	public void SetDestination(string _itemName)
	{
		if(Application.loadedLevelName.Contains("GameScene") ){

			GameObject target = GameObject.Find (_itemName);

			if (target != null) {
				destination = target.transform.position;
			} else { // target possible not on the same level

				ItemState temp;
				bool hasItem = allItemDic.TryGetValue(_itemName, out temp);

				if(hasItem)
				{
					if(PlayerController.instance.currentLevel != temp.level)
					{
						GameObject _target = null;
						// if player at platform, target at ground
						switch(PlayerController.instance.currentLevel){
						case 6:
							if(temp.level != 6){
								destination = GameObject.Find("Transition_ToiletDoorInside").transform.position;
							}
							break;
						
						case 5:
							if(temp.level != 5){
								destination = GameObject.Find("Transition_ShopDoorInside").transform.position;
							}
							break;

						case 4:
							if(temp.level != 4){
								destination = GameObject.Find("Transition_CafeDoorInside").transform.position;
							}
							break;

						case 3:
							if(temp.level <= 2){
								_target = GameObject.Find("Transition_StationEntrance");
							}else if(temp.level == 6){
								_target = GameObject.Find("Transition_ToiletDoorOutside");
							}else if(temp.level == 5){
								_target = GameObject.Find("Transition_ShopDoorOutside");
							}else if(temp.level == 4){
								_target = GameObject.Find("Transition_CafeDoorOutside");
							}

							if(_target != null)
								destination = _target.transform.position;
							break;
							
						case 2:
							if(temp.level != 2){
								destination = GameObject.Find("Transition_EscalatorDown").transform.position;
							}
							break;
							
						case 1:
							if(temp.level >= 3){
								if(player.transform.position.x < 1.0f ){
									_target = GameObject.Find("Transition_GantryInside");
								}
								else
									_target = GameObject.Find("Transition_StationExit");
							}
							else { // Target's level is 0 or 2

								// inside Gantry : x < 3	Outside Gantry: x > 4
								GameObject gantryObject = null;
								gantryObject = GameObject.Find("Transition_GantryInside");

								// if player is at left right of gantry and target is 0 or 2
								if( player.transform.position.x >= 4 ) // right of gantry
									_target = GameObject.Find("Transition_GantryOutside");
								else if(player.transform.position.x <= 2){
									if(temp.level == 2){
										_target = GameObject.Find("Transition_EscalatorUp");
									}else{
										_target = GameObject.Find("Transition_SewageEntrance");
									}
								}
							}
							if(_target != null)
								destination = _target.transform.position;
							break;

						case 0:
							if(temp.level != 0){
								destination = GameObject.Find("Transition_SewageExit").transform.position;
							}
							break;
						}

					} //  end of not same level
				}// end of hasItem

			}// end of item not in same level
		}
	}

	public void TriggerItem(string _item)
	{
		if (storyList [0].CompareTo (_item) == 0) {
			storyList.RemoveAt (0);
		}
	}
	
	// Update is called once per frame
	void Update () {


		if (Application.loadedLevelName.Contains ("GameScene") && EndingController.instance.isChapter2Activated) {
			if(storyList.Count == 0)
				this.Init();
			modelTexture.mainTexture = textureArray [1];
		} else
			modelTexture.mainTexture = textureArray [0];



		if (EndingController.instance.isChapter2Activated && Application.loadedLevelName.Contains ("GameScene") ){
			if(lineRenderer.enabled == false)
				TurnOnLine();

			if (player == null)
				player = GameObject.FindGameObjectWithTag ("Player");
			else {
				origin = player.transform.GetChild(3).position;
			}

			if (storyList.Count > 0) {
				SetDestination (storyList [0]);
			}

			if (Vector3.Distance (origin, destination) < 0.5f)
				lineRenderer.enabled = false;
			else {
				lineRenderer.enabled = true;
				lineRenderer.SetPosition (0, origin);
				lineRenderer.SetPosition (1, destination);
			}
		} else
			TurnOffLine ();
	}
}

public class EndingState {
	public string id;
	public string[] sequence;
	
	public EndingState() {
		sequence = new string[0];
	}
}

