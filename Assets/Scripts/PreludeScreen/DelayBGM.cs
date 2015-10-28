using UnityEngine;
using System.Collections;

public class DelayBGM : MonoBehaviour {


    public float fadeTimer;
    public float delayTimer;

    private float count;
    private AudioSource audioF;
    // Use this for initialization
    void Start () {

        count = -delayTimer;
        audioF = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	    if(count< fadeTimer)
            count+=Time.deltaTime;

        audioF.volume = Mathf.Max(0.0f, count) / fadeTimer;
    }
}
