using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InitDBScript : MonoBehaviour {

	public Text DebugText;

	// Use this for initialization
	void Start () {
		StartSync();
	}

    private void StartSync()
    {
        var ds = new DataService("MainRecord.db");
        ToConsole("Initial database...");
    }
	
	private void ToConsole(string msg){
		DebugText.text += System.Environment.NewLine + msg;
		Debug.Log (msg);
	}
}
