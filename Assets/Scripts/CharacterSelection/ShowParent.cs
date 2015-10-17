using UnityEngine;
using System.Collections;

public class ShowParent : MonoBehaviour {

	// Use this for initialization
	void Awake () {
	   if(PlayerData.ParentGenderId == 1)
        {
            GameObject.Find("spr_mom").SetActive(false);
            GameObject.Find("spr_dad").SetActive(true);
        }
        else
        {
            GameObject.Find("spr_dad").SetActive(false);
            GameObject.Find("spr_mom").SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
