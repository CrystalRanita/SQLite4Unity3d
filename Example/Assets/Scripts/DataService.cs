using SQLite4Unity3d;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;
using System;
public class DataService  {

	private SQLiteConnection _connection;

	public DataService(string DatabaseName){

#if UNITY_EDITOR
        var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
            var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
			// then save to Application.persistentDataPath
			File.Copy(loadDb, filepath);
#elif UNITY_WP8
			var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
			// then save to Application.persistentDataPath
			File.Copy(loadDb, filepath);

#elif UNITY_WINRT
			var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
			// then save to Application.persistentDataPath
			File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
			var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
			// then save to Application.persistentDataPath
			File.Copy(loadDb, filepath);
#else
			var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
			// then save to Application.persistentDataPath
			File.Copy(loadDb, filepath);
#endif
            Debug.Log("Database written");
        }
        var dbPath = filepath;
#endif
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);
		if (!chkTableExists("Person", _connection)) {
			_connection.CreateTable<Person> ();
			Debug.Log("init Person Table");
		}

		if (!chkTableExists("PersonRecord", _connection)) {
			_connection.CreateTable<PersonRecord> ();
			Debug.Log("init PersonRecord Table");
		}

		if (!chkTableExists("Food", _connection)) {
			initFoodTable();
			Debug.Log("init Food Table");
		}
	}

	private bool chkTableExists(string tableName, SQLiteConnection connection )
    {
        bool exist = false;
        try
        {
			string query = string.Format("SELECT name FROM sqlite_master WHERE type='table' AND name='{0}';", tableName);
			SQLiteCommand cmd = connection.CreateCommand(query);
			var item = connection.Query<object>(query);
			if (item.Count > 0)
				exist = true;
			return exist;
        }
        catch (SQLiteException ex)
        {
            Debug.Log("chkTableExists exception: " + ex.Message);
            return false;
        }
    }

	private void initFoodTable(){
		_connection.CreateTable<Food> ();
		// Insert Foods
		_connection.InsertAll (new[]{
			new Food{
				FoodName = "義美小泡芙",
				Calorie = 337
			},
			new Food{
				FoodName = "義美可可消化餅",
				Calorie = 127
			},
			new Food{
				FoodName = "阿華田麥芽牛奶",
				Calorie = 203
			},
			new Food{
				FoodName = "光泉無糖豆漿",
				Calorie = 137
			},
			new Food{
				FoodName = "麥香紅茶",
				Calorie = 120
			},
			new Food{
				FoodName = "桂格黑穀珍寶麥片",
				Calorie = 133
			},
			new Food{
				FoodName = "干貝柴魚海鮮粥",
				Calorie = 140
			}
		});
	}

	public Person setPerson(string name){
		var p = new Person{
				Name = name,
				CalorieThreshold = 1400 // default
		};
		_connection.Insert (p);
		return p;
	}
	
	public IEnumerable<Person> GetPerson(int uid){
		return _connection.Table<Person>().Where(x => x.UserId == uid);
	}

	public IEnumerable<Person> UpdateCalTarget(int uid, int target){
		var query = _connection.Table<Person>().Where(c => c.UserId == uid).FirstOrDefault();
		query.CalorieThreshold = target;
		_connection.Update(query);
		return _connection.Table<Person>().Where(x => x.UserId == uid);
	}

	public int GetCalTarget(int uid){
		var query = _connection.Table<Person>().Where(c => c.UserId == uid).FirstOrDefault();
		return query.CalorieThreshold;
	}

	public PersonRecord AddRecord(int uid, int foodid, uint timestamp) {
		var rec = new PersonRecord{
			UserId = uid,
			FoolID = foodid,
			EpochTime = timestamp
		};
		_connection.Insert (rec);
		return rec;
	}

	public int GetCalToday(int uid) {
		int totalCalToday = 0;
		uint startDateTime = dateTimeToTimestamp(DateTime.Today); //Today at 00:00:00
		uint endDateTime = dateTimeToTimestamp(DateTime.Today.AddDays(1).AddTicks(-1)); //Today at 23:59:59
		var queryList = _connection.Table<PersonRecord>().Where(c => c.UserId == uid && c.EpochTime >= startDateTime && c.EpochTime <= endDateTime);
		foreach (var rec in queryList) {
			int tmpCal = _connection.Table<Food>().Where(c => c.FoodID == rec.FoolID).FirstOrDefault().Calorie;
			totalCalToday += tmpCal;
		}

		return totalCalToday;
	}

	private uint dateTimeToTimestamp(DateTime value)
	{
		//create Timespan by subtracting the value provided from
		//the Unix Epoch
		TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
	
		//return the total seconds (which is a UNIX timestamp)
		return (uint)span.TotalSeconds;
	}

	public IEnumerable<Person> GetPersonsNamedRoberto(){
		return _connection.Table<Person>().Where(x => x.Name == "Roberto");
	}

	public Person GetJohnny(){
		return _connection.Table<Person>().Where(x => x.Name == "Johnny").FirstOrDefault();
	}
}
