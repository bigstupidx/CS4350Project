using UnityEngine;
using System.Collections;

public class SetGender : MonoBehaviour {

    public int genderSet;
	// Use this for initialization
	void Start () {
	
	}

    void onMouseDown()
    {
        LookAtPoint.genderSet = genderSet;
        GameObject.Find("Main_Camera").GetComponent<LookAtPoint>().unfreeze();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
