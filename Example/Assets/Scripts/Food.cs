using SQLite4Unity3d;

public class Food  {

	[PrimaryKey, AutoIncrement]
	public int FoodID { get; set; }
	public string FoodName { get; set; }
	// public string Surname { get; set; }
	public int Calorie { get; set; }

	public Food(){
	}
 
	public Food(string name, int cal){
		FoodName = name;
		Calorie = cal;
	}
	public override string ToString ()
	{
		return string.Format ("[Person: FoodID={0}, FoodName={1}, Calorie={2}]", FoodID, FoodName, Calorie);
	}
}
