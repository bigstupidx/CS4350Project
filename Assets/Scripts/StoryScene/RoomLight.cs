using UnityEngine;
using System.Collections;

public class RoomLight : MonoBehaviour {

    private float rotation1;
    private float rotation2;
    // Use this for initialization
    void Start ()
    {
        rotation1 = 90.0f;
        rotation2 = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        rotation1 += 5.0f;
        rotation2 += 6.0f;

        rotation1 %= 360.0f;
        rotation2 %= 360.0f;

        transform.LookAt(new Vector3(0.0f+Mathf.Cos(rotation1*Mathf.Deg2Rad)*0.1f, 0.0f,0.0f + Mathf.Cos(rotation2 * Mathf.Deg2Rad))*0.05f);
    }
}
