using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class JsonReader : MonoBehaviour {
	
	static public JsonData[] readItems() {
		string jsonString = File.ReadAllText (Application.dataPath + "/Resources/items.json");
		return JsonMapper.ToObject<JsonData[]>(jsonString);
	}

	static public string[] toStrArray(JsonData jsonData) {
		if (!jsonData.IsArray ) {
			return new List<string>().ToArray();
		}
		List<string> res = new List<string> ();
		for(int i = 0; i< jsonData.Count; i++) {
			res.Add((string) jsonData[i]);
		}
		return res.ToArray ();
	}

	static public int[] toIntArray(JsonData jsonData) {
		if (!jsonData.IsArray ) {
			return new List<int>().ToArray();
		}
		List<int> res = new List<int> ();
		for(int i = 0; i< jsonData.Count; i++) {
			res.Add((int) jsonData[i]);
		}
		return res.ToArray ();
	}
}
