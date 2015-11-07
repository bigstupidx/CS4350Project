using UnityEngine;
using System.Collections;

public class SetGender : MonoBehaviour {

    public int genderSet;
	// Use this for initialization
	void Start () {
	
	}

    void onMouseDown()
    {
		SetGenderId (genderSet);
    }

	public void SetGenderId(int _index)
	{
		LookAtPoint.genderSet = genderSet;
		Camera.main.GetComponent<LookAtPoint> ().unfreeze ();
		//GameObject.Find("Main_Camera").GetComponent<LookAtPoint>().unfreeze();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
