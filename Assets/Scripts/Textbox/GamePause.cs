using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePause: MonoBehaviour {
    public GameObject pauseButton;
    public GameObject pausePanel;

    Displaytextbox textboxScript;
    PlayerMovement movementScript;
    // Use this for initialization
    void Awake()
    {
        textboxScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Displaytextbox>();
        movementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PausePressed() {
        pauseButton.SetActive(false);
        pausePanel.SetActive(true);
        textboxScript.enabled= false;
        movementScript.StopMoving();
        movementScript.MouseLeftButton();
        PlayerData.MoveFlag = false;
        PlayerController.instance.isShowingHint = false;
    }

    public void ResumePressed() {
        PlayerController.instance.isShowingHint = true;
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
        textboxScript.enabled = true;
        PlayerData.MoveFlag = true;

    }

    public void ExitPressed() {

        textboxScript.enabled = true;
        Destroy(GameController.instance);
        Destroy(PlayerController.instance);
        Destroy(EndingController.instance);
        LevelHandler.Instance.LoadSpecific("TitleScene");
        
        PlayerData.MoveFlag = true;
    }

}
