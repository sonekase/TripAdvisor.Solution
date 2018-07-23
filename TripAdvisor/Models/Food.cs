using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TripAdvisor;

namespace TripAdvisor.Models
{
  public class Food
  {
    private int _id;
    private string _name;
    private string _description;

    public Food(string name, string description, int id = 0)
    {
      _name = name;
      _id = id;
      _description = description;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetDescription()
    {
      return _description;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO food (name, description) VALUES (@name, @description);";
      cmd.Parameters.Add(new MySqlParameter("@name", _name));
      cmd.Parameters.Add(new MySqlParameter("@description", _description));

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Food> GetAll()
  {
    List<Food> allFood = new List<Food> {};
    MySqlConnection conn = DB.Connection();
    conn.Open();
    MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"SELECT * FROM food;";
    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
    while(rdr.Read())
    {
      int foodId = rdr.GetInt32(0);
      string foodName = rdr.GetString(1);
      string foodDescription = rdr.GetString(1);
      Food newFood = new Food(foodName, foodDescription, foodId);
      allFood.Add(newFood);
    }
    conn.Close();
    if (conn != null)
    {
      conn.Dispose();
    }
    return allFood;
  }
////
  public static Food Find(int id)
 {
   MySqlConnection conn = DB.Connection();
   conn.Open();
   var cmd = conn.CreateCommand() as MySqlCommand;
   cmd.CommandText = @"SELECT * FROM food WHERE id = @thisId;";
   MySqlParameter thisId = new MySqlParameter();
   thisId.ParameterName = "@thisId";
   thisId.Value = id;
   cmd.Parameters.Add(thisId);

   var rdr = cmd.ExecuteReader() as MySqlDataReader;
   int foodId = 0;
   string foodName = "";
   string foodDescription = "";

   while (rdr.Read())
   {
     foodId = rdr.GetInt32(0);
     foodName= rdr.GetString(1);
     foodDescription = rdr.GetString(2);

   }
   Food foundFood = new Food(foodName, foodDescription, foodId);

   conn.Close();
   if (conn != null)
   {
     conn.Dispose();
   }
   return foundFood;
 }


 public static void DeleteAll()
{
  MySqlConnection conn = DB.Connection();
  conn.Open();
  var cmd = conn.CreateCommand() as MySqlCommand;
  cmd.CommandText = @"DELETE FROM food;";
  cmd.ExecuteNonQuery();
  conn.Close();
  if (conn != null)
  {
    conn.Dispose();
  }
}

//Edit



}
}