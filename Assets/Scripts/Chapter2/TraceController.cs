using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TraceController : MonoBehaviour {
	
	public List<string> storyList;
	private LineRenderer lineRenderer;
	private float counter;
	private float distance;

	public Vector3 origin;
	public Vector3 destination;

	public float speed = 5;

	static public TraceController instance;
	public Dictionary<string, ItemState> allItemDic;
	
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

	void OnEnable()
	{
		Init ();
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
						if(PlayerController.instance.currentLevel == 2 && temp.level != 2)
						{
							destination = GameObject.Find("EscalatorDown").transform.position;
						}
						else if(PlayerController.instance.currentLevel == 1 && temp.level == 2)
						{
							destination = GameObject.Find("EscalatorUp").transform.position;
						}
						else if(PlayerController.instance.currentLevel == 1 && temp.level == 0)
						{
							destination = GameObject.Find("SewageEntrance").transform.position;
						}
						else if(PlayerController.instance.currentLevel == 0 && temp.level != 0) // this part need to change
						{
							destination = GameObject.Find("SewageExit").transform.position;
						}
					}
				}// end of hasItem

			}// end of item not in same level

		}
	}

	public void TriggerItem(string _item)
	{
		Debug.Log("Input: " + _item + ", curr DEST: " + storyList[0] );
		if (storyList [0].CompareTo (_item) == 0) {
			storyList.RemoveAt (0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Application.loadedLevelName.Contains ("GameScene") && GameController.instance.isChapter2Activated) {
			lineRenderer.enabled = true;
		}
		else
			lineRenderer.enabled = false;


		if (GameController.instance.isChapter2Activated) {
			if (GameObject.FindGameObjectWithTag ("Player") != null) {
				origin = GameObject.FindGameObjectWithTag ("Player").transform.position;
			}

			if (storyList.Count > 0) {
				SetDestination (storyList [0]);
			}
			
			if (GameObject.Find ("Floor") != null) {
				float newY = GameObject.Find ("Floor") .transform.position.y;
				origin.y = destination.y = (newY + 0.3f);
			}

			lineRenderer.SetPosition (0, origin);
			lineRenderer.SetPosition (1, destination);
		}
	}
}

public class EndingState {
	public string id;
	public string[] sequence;
	
	public EndingState() {
		sequence = new string[0];
	}
}

