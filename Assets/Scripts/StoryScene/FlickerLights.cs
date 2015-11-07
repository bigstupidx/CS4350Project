using UnityEngine;
using System.Collections;

public class FlickerLights : MonoBehaviour {
    private Light l;

    private float count;
    private float altIn;

    // Use this for initialization
    void Start () {
        count = 0.0f;
        l = GetComponent<Light>();
        altIn = 0.0f;
        l.intensity = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (count < 15.0f) {
            count += Time.deltaTime;
          } else {
            if (altIn < 1.32f)
                altIn += Time.deltaTime * 0.5f;
            if (Random.Range(0.0f, 1.0f) > 0.7f)
                l.intensity = 1.32f;
            else
                l.intensity = altIn;
        }


    }
}
