using SQLite4Unity3d;

public class Person  {

	[PrimaryKey, AutoIncrement]
	public int UserId { get; set; }
	public string Name { get; set; }
	public int CalorieThreshold { get; set; }

	public Person(){
	}
 
	public Person(string name, int cal){
		Name = name;
		CalorieThreshold = cal;
	}

	public override string ToString ()
	{
		return string.Format ("[Person: UserId=auto, Name={0}, CalorieThreshold={1}]", Name, CalorieThreshold);
	}
}
