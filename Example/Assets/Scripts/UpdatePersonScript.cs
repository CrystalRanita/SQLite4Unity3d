using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class UpdatePersonScript : MonoBehaviour {

	public Text DebugText;
	public int TARGET_CAL;

	// Use this for initialization
	void Start() {
		var ds = new DataService ("MainRecord.db");
		var person = ds.UpdatePersonTarget(1, TARGET_CAL);
		ToConsole (person);
		ToConsole ("update person done!");
	}
	
	private void ToConsole(IEnumerable<Person> people){
		var count = 0;
		foreach (var person in people) {
			count++;
			ToConsole(person.ToString());
		}
		ToConsole ("Person count: " + count);
	}

	private void ToConsole(string msg){
		DebugText.text += System.Environment.NewLine + msg;
		Debug.Log (msg);
	}

}
