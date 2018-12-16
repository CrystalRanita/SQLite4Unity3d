using UnityEngine;
using SQLite4Unity3d;
using System;

public class PersonRecord  {

	[PrimaryKey, AutoIncrement]
	public int recID { get; set; }
	public int UserId { get; set; }
	public int FoolID { get; set; }
	public uint EpochTime { get; set; }

	public PersonRecord(){
	}
 
	public PersonRecord(int uid, int foodid, uint timestamp){
		UserId = uid;
		FoolID = foodid;
		EpochTime = timestamp; // 1544977587 (len=10)
	}

	public override string ToString ()
	{
		return string.Format ("[Person: UserId={0}, FoolID={1}, EpochTime={2}]", UserId, FoolID, EpochTime);
	}
}
