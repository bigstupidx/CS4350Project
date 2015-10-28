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
	private GameObject filter;
	
	public void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
			allItemDic = new Dictionary<string, ItemState>();
			lineRenderer = transform.GetComponent<LineRenderer>();
			Init();
		} else {
			DestroyImmediate(gameObject);
		}
	}

	void ChangeTexture()
	{
		if (filter == null )
			filter = GameObject.Find ("Chapter2Filter");

		if (EndingController.instance.isChapter2Activated && Application.loadedLevelName.Contains ("GameScene")) {
			modelTexture.mainTexture = textureArray [1];
			filter.GetComponent<Image>().color = new Color( (112.0f/255.0f), (66.0f/255.0f), (20.0f/255.0f), 0.3f );
		} else {
			modelTexture.mainTexture = textureArray [0];
			filter.GetComponent<Image>().color = new Color( (112.0f/255.0f), (66.0f/255.0f), (20.0f/255.0f), 0.0f );
		}
	}

	void OnEnable()
	{
		Init ();
	}

	public void TurnOffLine()
	{
		lineRenderer.enabled = false;
	}

	public void Init()
	{
		EndingState[] endingList = JsonReader.readEndingState();
		ItemState[] itemList = JsonReader.readItemsState ();

		foreach (ItemState currItem in itemList) {
			if( !allItemDic.ContainsKey(currItem.id) )
				allItemDic.Add(currItem.id, currItem);
		}

		foreach (EndingState currEnding in endingList) {
			if(currEnding.id.CompareTo( (EndingController.instance.deathReason).ToString() ) == 0 )
			{
				storyList = new List<string>(currEnding.sequence);
			}
		}

		lineRenderer.SetPosition (0, origin);
		lineRenderer.SetWidth (0.15f, 0.15f);
		lineRenderer.SetColors (new Color (1.0f, 1.0f, 1.0f, 1.0f), new Color (1.0f, 1.0f, 1.0f, 0.2f));
		
		SetDestination (storyList [0]);
		
		distance = Vector3.Distance (origin, destination);

		if (player == null)
			player = GameObject.FindGameObjectWithTag ("Player");

		if (filter == null && Application.loadedLevelName.Contains("GameScene") )
			filter = GameObject.Find ("Chapter2Filter");
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
								destination = GameObject.Find("Transition_StationEntrance").transform.position;
							}else if(temp.level == 6){
								destination = GameObject.Find("Transition_ToiletDoorOutside").transform.position;
							}else if(temp.level == 5){
								destination = GameObject.Find("Transition_ShopDoorOutside").transform.position;
							}else if(temp.level == 4){
								destination = GameObject.Find("Transition_CafeDoorOutside").transform.position;
							}
							break;
							
						case 2:
							if(temp.level != 2){
								destination = GameObject.Find("Transition_EscalatorDown").transform.position;
							}
							break;
							
						case 1:
							if(temp.level >= 3){
								if(player.transform.position.x < 1.0f ){
									destination = GameObject.Find("Transition_GantryInside").transform.position;
								}
								else
									destination = GameObject.Find("Transition_StationExit").transform.position;
							}
							else {
								if( player.transform.position.x > GameObject.Find("Transition_GantryInside").transform.position.x )
									destination = GameObject.Find("Transition_GantryOutside").transform.position;
								else {
									if(temp.level == 2){
										destination = GameObject.Find("Transition_EscalatorUp").transform.position;
									}else{
										destination = GameObject.Find("Transition_SewageEntrance").transform.position;
									}
								}
							}
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

		/*
		if (Application.loadedLevelName.Contains ("GameScene") && EndingController.instance.isChapter2Activated) {
			lineRenderer.enabled = true;
		}
		else
			lineRenderer.enabled = false;
			*/
		ChangeTexture();

		if (EndingController.instance.isChapter2Activated && Application.loadedLevelName.Contains ("GameScene")) {
			if (player == null)
				player = GameObject.FindGameObjectWithTag ("Player");
			else {
				origin = player.transform.position;
			}

			if (storyList.Count > 0) {
				SetDestination (storyList [0]);
			}
			
			if (GameObject.Find ("Floor") != null) {
				float newY = GameObject.Find ("Floor") .transform.position.y;
				origin.y = destination.y = (newY + 0.3f);
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

