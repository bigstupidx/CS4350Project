using UnityEngine;
using System.Collections;

public class SetDeathScene : MonoBehaviour {

    public Texture[] dsHandler;
    public Texture[] dsStaticHandler;
    public Texture dsBlankHandler;

    public float startTimer;
    public float randomizeTimer;
    public float sceneTimer;

    private float counter;
    public EndingType eT;

    private Renderer rend;
	// Use this for initialization
	void Start () {
        counter = 0.0f;
        rend = GetComponent<Renderer>();
        randomizeTimer = Random.Range(0.0f, randomizeTimer);

        eT = EndingController.instance.deathReason;
        //eT = (EndingType)1;
        Debug.Log(eT);
        updateScreen(-1);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (counter < startTimer + randomizeTimer+sceneTimer)
        {
            counter += 0.1f;
        } else
        {
            updateScreen((int)eT);
        }
        if(counter>= startTimer + randomizeTimer&& counter < startTimer + randomizeTimer+ sceneTimer)
        {
            updateScreen(5);
        }
    }

    void updateScreen(int type)
    {
        switch (type)
        {
            case 0:
                //myText.text = "A Kid found dead at train Station.\nSuspected Cause of Death:\n Suffocation";
                rend.material.mainTexture = dsHandler[0];
                break;
            case 1:
                //myText.text = "A Kid found dead at train Station.\nSuspected Cause of Death:\n Dog Allergy";
                rend.material.mainTexture = dsHandler[1];
                break;
            case 2:
                //myText.text = "A Kid found dead at train Station.\nSuspected Cause of Death:\n Head Injury";
                rend.material.mainTexture = dsHandler[2];
                break;
            case 3:
                //myText.text = "A Kid found dead at train Station.\nSuspected Cause of Death:\n Fallen Ceiling";
                rend.material.mainTexture = dsHandler[3];
                break;
            case 4:
                //myText.text = "A Kid found dead at train Station.\nSuspected Cause of Death:\n Train Accident";
                rend.material.mainTexture = dsHandler[4];
                break;
            case 5:
                rend.material.mainTexture = dsStaticHandler[Random.Range(0,4)];
                break;
            default:
                rend.material.mainTexture = dsBlankHandler;
                break;
        }
    }
}
