using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WheelMenuButton : MonoBehaviour
{
    public static float posLeft = -700.0f;
    public static float posCenter = 0.0f;
    public static float posRight = 700.0f;
    static float totalMoveTimeInSec = 0.2f;
    static float halfScreenWidth = 1080;
    static float blackFadeAlphaChg = 0.1f;
    static float colFadeAlphaChgUp = 10.0f;
    static float colFadeAlphaChgDown = 1.0f;

    RectTransform myRect;
    Text myText;
    public string hint_text;

    public RawImage blackScreen;
    public int myID;
    public int choice = -1;
    public GameObject hint;

    public Color32 colHighlighted = new Color32(255, 255, 255, 255);
    public Color32 colNormal = new Color32(119, 167, 210, 255);
    private bool highlighted = false;
    static bool floatIn;
    // Use this for initialization
    void Start()
    {
        myRect = transform.GetComponent<RectTransform>();
        myText = transform.GetComponent<Text>();
        blackScreen.enabled = true;
        floatIn = false;
        myText.color = colNormal;
    }

    public void onClick()
    {
        if (choice != -1)
            return;

        // Set menu choice
        choice = myID;
        floatIn = true;
        blackScreen.enabled = true;
    }

    public void onPointerEnter()
    {
        highlighted = true;
        hint.GetComponent<Text>().text = hint_text;
    }

    public void onPointerExit()
    {
        highlighted = false;
        hint.GetComponent<Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(ModelRotate.flashLight)
        {
            myText.color = colHighlighted;
        }

        if(highlighted|| ModelRotate.flashLight)
        {
            Color tempColor = myText.color;
            tempColor *= new Vector4(255.0f, 255.0f, 255.0f, 255.0f);
            tempColor.r += (colHighlighted.r - tempColor.r) * colFadeAlphaChgUp * Time.deltaTime;
            tempColor.g += (colHighlighted.g - tempColor.g) * colFadeAlphaChgUp * Time.deltaTime;
            tempColor.b += (colHighlighted.b - tempColor.b) * colFadeAlphaChgUp * Time.deltaTime;
            tempColor *= new Vector4(0.00392156862f, 0.00392156862f, 0.00392156862f, 1.0f);
            myText.color = tempColor;
        } else
        {
            Color tempColor = myText.color;
            tempColor *= new Vector4(255.0f, 255.0f, 255.0f, 255.0f);
            tempColor.r += (colNormal.r - tempColor.r) * colFadeAlphaChgDown * Time.deltaTime;
            tempColor.g += (colNormal.g - tempColor.g) * colFadeAlphaChgDown * Time.deltaTime;
            tempColor.b += (colNormal.b - tempColor.b) * colFadeAlphaChgDown * Time.deltaTime;
            tempColor *= new Vector4(0.00392156862f, 0.00392156862f, 0.00392156862f, 1.0f);
            myText.color = tempColor;
        }

        if (floatIn)
        {
            // Fade black in
            Color tempColor = blackScreen.color;
            tempColor.a = Mathf.Min(1.0f, tempColor.a + blackFadeAlphaChg * Time.deltaTime);
            blackScreen.color = tempColor;
            AudioSource a = blackScreen.GetComponent<AudioSource>();
            a.volume = 1.0f - tempColor.a;


            // Change Scene if menu choice selected
            if (blackScreen.color.a >= 1.0f && choice != -1)
            {
                if (choice == 0)
                {
                    GameController.instance.load();
                }
                else if (choice == 1)
                {
                    Application.LoadLevel("PreludeScene");
                }
                else if (choice == 2)
                {
                    Application.LoadLevel("CreditScene");
                }
            }
        }
        else
        {
            // Fade black out
            Color tempColor = blackScreen.color;
            tempColor.a = Mathf.Max(0.0f, tempColor.a - blackFadeAlphaChg * Time.deltaTime);
            if (tempColor.a <= 0.0f)
                blackScreen.enabled = false;
            blackScreen.color = tempColor;
            AudioSource a = blackScreen.GetComponent<AudioSource>();
            a.volume = 1.0f - tempColor.a;
        }
    }
}
