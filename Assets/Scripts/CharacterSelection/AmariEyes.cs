using UnityEngine;
using System.Collections;

public class AmariEyes : MonoBehaviour {

    public static float blinkDuration = 0.15f;
    public static float maxTimeBeforeBlink = 7.0f;
    public static float minTimeBeforeBlink = 3.0f;

    float timer = 0;
    bool isEyeOpen = true;
    GameObject lookTargetObj = null;

    Sprite[] eyeOpenSprites = new Sprite[4];
    Sprite[] eyeCloseSprites = new Sprite[4];

    public string eyeOpenFile = "face1";
    public string eyeCloseFile = "face2";

    public AmariSpritePiece targetSprite;
    public GameObject eyeDot;
 

    // Use this for initialization
    void Start()
    {
        eyeOpenSprites[0] = Resources.Load<Sprite>(PlayerData.FormSpritePath(eyeOpenFile, 0));
        eyeOpenSprites[1] = Resources.Load<Sprite>(PlayerData.FormSpritePath(eyeOpenFile, 1));
        eyeOpenSprites[2] = Resources.Load<Sprite>(PlayerData.FormSpritePath(eyeOpenFile, 2));
        eyeOpenSprites[3] = null;

        eyeCloseSprites[0] = Resources.Load<Sprite>(PlayerData.FormSpritePath(eyeCloseFile, 0));
        eyeCloseSprites[1] = Resources.Load<Sprite>(PlayerData.FormSpritePath(eyeCloseFile, 1));
        eyeCloseSprites[2] = Resources.Load<Sprite>(PlayerData.FormSpritePath(eyeCloseFile, 2));
        eyeCloseSprites[3] = null;

        RandomizeTimeBeforeBlink();
        eyeDot.SetActive(true);
    }

    void RandomizeTimeBeforeBlink()
    {
        timer = Random.Range(minTimeBeforeBlink, maxTimeBeforeBlink);
    }

    public void LookAtObject(GameObject obj)
    {
        lookTargetObj = obj;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (isEyeOpen)
            {
                targetSprite.SwitchSprites(eyeCloseSprites);
                isEyeOpen = false;
                timer = blinkDuration;
                eyeDot.SetActive(false);


            }
            else
            {
                targetSprite.SwitchSprites(eyeOpenSprites);
                isEyeOpen = true;
                RandomizeTimeBeforeBlink();
                eyeDot.SetActive(true);

            }
        }

        if (lookTargetObj != null)
        {
            Vector3 targetPos = lookTargetObj.transform.position;
                        
            targetPos.y = 0;

            Vector3 diffVec = targetPos - transform.position;

            float angle = Mathf.Atan2(diffVec.z, diffVec.x) * Mathf.Rad2Deg;

            Vector3 pos = new Vector3();
            float eyeStrength = Mathf.Min(1.0f, Mathf.Max(0.0f, diffVec.magnitude / 6.0f));
            pos.x = Mathf.Cos(angle * Mathf.Deg2Rad) * 0.01f * eyeStrength;
            pos.y = Mathf.Sin(angle * Mathf.Deg2Rad) * 0.01f * eyeStrength;
            pos.z = 0.0f;

            eyeDot.transform.localPosition = pos;
        }
        else
        {
            eyeDot.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}
