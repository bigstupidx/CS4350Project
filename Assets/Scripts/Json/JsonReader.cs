using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class JsonReader : MonoBehaviour {

	private string jsonString;
	private JsonData itemData;
	
	
	void Start () {
		jsonString = File.ReadAllText (Application.dataPath + "/Resources/item.json");
		itemData = JsonMapper.ToObject (jsonString);

		//Debug.Log (jsonString);
		Debug.Log (itemData ["item"][3]["name"]);
		Debug.Log (itemData ["item"][3]["responds_active"][0]);
	}

}
