using SQLite4Unity3d;

public class Person  {

	[PrimaryKey, AutoIncrement]
	public int UserId { get; set; }
	public string Name { get; set; }
	// public string Surname { get; set; }
	public int FoolID { get; set; }
	public uint EpochTime { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Person: UserId={0}, Name={1},  FoolID={2}, EpochTime={3}]", UserId, Name, FoolID, EpochTime);
	}
}
