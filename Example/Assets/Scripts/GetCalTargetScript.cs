using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class GetCalTargetScript : MonoBehaviour {

	public Text DebugText;

	// Use this for initialization
	void Start() {
		var ds = new DataService ("MainRecord.db");
		int targetCal = ds.GetCalTarget(1);
		ToConsole ("Cal target: " + targetCal);
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
