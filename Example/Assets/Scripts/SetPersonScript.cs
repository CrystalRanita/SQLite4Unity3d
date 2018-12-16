using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SetPersonScript : MonoBehaviour {

	public Text DebugText;
	public string USERNAME;

	// Use this for initialization
	void Start () {
		var ds = new DataService ("MainRecord.db");
		var p = ds.setPerson(USERNAME);
		ToConsole("Add person: ");
		ToConsole (p.ToString());
	}

	private void ToConsole(IEnumerable<Person> people){
		foreach (var person in people) {
			ToConsole(person.ToString());
		}
	}

	private void ToConsole(string msg){
		DebugText.text += System.Environment.NewLine + msg;
		Debug.Log (msg);
	}
}
