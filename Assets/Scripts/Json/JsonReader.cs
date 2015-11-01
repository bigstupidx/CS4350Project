using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LitJson;

public class JsonReader : MonoBehaviour {

	static public ItemState[] readItemsState() {
		TextAsset itemAsset = Resources.Load ("data") as TextAsset;
		string jsonString = itemAsset.text;
		return JsonMapper.ToObject<ItemState[]>(jsonString);
	}

	static public EndingState[] readEndingState() {
		TextAsset endingAsset = Resources.Load ("EndingSequence") as TextAsset;
		string jsonString = endingAsset.text;
		return JsonMapper.ToObject<EndingState[]>(jsonString);
	}

	static public PlayerState readPlayerState() {
		string jsonString = File.ReadAllText (JsonReader.getDocumentDir() + "/player.json");
		return JsonMapper.ToObject<PlayerState> (jsonString);
	}
  
	static public void writePlayerState(PlayerState state) {
		StringBuilder builder = new StringBuilder();
		JsonWriter writer = new JsonWriter(builder);
		writer.PrettyPrint = true;
		writer.IndentValue = 2;
		JsonMapper.ToJson(state, writer);
		File.WriteAllText (JsonReader.getDocumentDir() + "/player.json", builder.ToString ());
	}

	static public string getDocumentDir() {
		return Application.persistentDataPath;
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
