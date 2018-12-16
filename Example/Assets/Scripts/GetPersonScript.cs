using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GetPersonScript : MonoBehaviour {

	public Text DebugText;

	// Use this for initialization
	void Start() {
		var ds = new DataService ("MainRecord.db");
		// ds.CreateDB ();
		var person = ds.GetPerson(0);
		ToConsole ("Create done!");
		ToConsole (person);

		// people = ds.GetPersonsNamedRoberto ();
		// ToConsole("Searching for Roberto ...");
		// ToConsole (people);

		// ds.CreatePerson ();
		// ToConsole("New person has been created");
		// var p = ds.GetJohnny ();
		// ToConsole(p.ToString());
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
