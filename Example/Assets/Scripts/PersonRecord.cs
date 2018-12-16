using SQLite4Unity3d;
// using System.Globalization;
using System;

public class PersonRecord  {

	[PrimaryKey, AutoIncrement]
	public int recID { get; set; }
	public int UserId { get; set; }
	public int FoolID { get; set; }
	public uint EpochTime { get; set; }

	public PersonRecord(){
	}
 
	public PersonRecord(int uid, int foodid){
		UserId = uid;
		FoolID = foodid;
		EpochTime = getCurrentEpochTime();
	}

	private uint getCurrentEpochTime() {
		return (uint)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
	}

	public override string ToString ()
	{
		return string.Format ("[Person: UserId={0}, FoolID={1}, EpochTime={2}]", UserId, FoolID, EpochTime);
	}
}
