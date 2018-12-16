using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class AddRecordScript : MonoBehaviour {

	public Text DebugText;
	public int FOOD_ID;

	// Use this for initialization
	void Start () {
		var ds = new DataService ("MainRecord.db");
		uint timestamp = (uint)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
		ToConsole("timestamp: " + timestamp);
		var rec = ds.AddRecord(1, FOOD_ID, timestamp);
		ToConsole("Add Record: ");
		ToConsole (rec.ToString());
	}

	private void ToConsole(IEnumerable<PersonRecord> record){
		foreach (var rec in record) {
			ToConsole(rec.ToString());
		}
	}

	private void ToConsole(string msg){
		DebugText.text += System.Environment.NewLine + msg;
		Debug.Log (msg);
	}
}
