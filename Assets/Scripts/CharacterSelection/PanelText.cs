using UnityEngine;
using System.Collections;

public class PanelText : MonoBehaviour {

    public GameObject fatherText;
    public GameObject motherText;


    // Use this for initialization
    void Awake () {
        
        if(PlayerData.ParentGenderId == 1)
        {
            fatherText.SetActive(true);
            motherText.SetActive(false);
        }
        else
        {
            fatherText.SetActive(false);
            motherText.SetActive(true);
        }
    }

	
	// Update is called once per frame
	void Update () {
	    
	}
}
