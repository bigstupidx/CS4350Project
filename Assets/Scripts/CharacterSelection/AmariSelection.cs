using UnityEngine;
using System.Collections;

public class AmariSelection : MonoBehaviour {
	public int gender = 1;
	public int hair = 1;

	public static bool selectionEnabled = false;
	public static bool selectionDone = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown ()
	{
		if (selectionEnabled) {
			Debug.Log (transform.name);

			PlayerData.GenderId = gender;
			PlayerData.HairId = hair;

			selectionDone = true;

		}
	}
}
