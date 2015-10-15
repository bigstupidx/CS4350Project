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
		} else {
			DestroyImmediate(gameObject);
		}
	}
		
	// Use this for initialization
	void Start () {
		lineRenderer = transform.GetComponent<LineRenderer>();
		lineRenderer.SetPosition (0, origin);
		lineRenderer.SetWidth (0.2f, 0.2f);

		storyList = new List<string> (PlayerController.instance.validItems.Keys);

		if (storyList.Count > 0) {
			SetDestination (storyList [0]);
			storyList.RemoveAt(0);
		} else
			destination = origin;

		distance = Vector3.Distance (origin, destination);

	}


	public void SetDestination(string _itemName)
	{
		//Vector3 dest = new Vector3 (0, 0, 0);
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
					if(PlayerController.instance.currentLevel == 2 && temp.level == 1)
					{
						destination = GameObject.Find("EscalatorDown").transform.position;
					}
				}
			}
		}

		//return dest;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			origin = GameObject.FindGameObjectWithTag ("Player").transform.position;
			origin.y = 0.15f;
		}
		destination.y = 0.15f;
		lineRenderer.SetPosition (0, origin);

		if (Input.GetKeyUp (KeyCode.P)) {
			if(storyList.Count > 0){
				SetDestination (storyList [0]);
				storyList.RemoveAt(0);
			}
			else
				Debug.Log("Out of bound");
		}

		lineRenderer.SetPosition (1, destination);

		//if (counter < distance) {

//			counter += 0.1f / speed;

//			float x = Mathf.Lerp(0, distance, counter);


			//Vector3 pointAlongLine = x * Vector3.Normalize(destination - origin) + origin;

			//lineRenderer.SetPosition(1, pointAlongLine);
		//}

	}
}
