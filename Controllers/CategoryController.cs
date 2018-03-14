using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;


public class CategoryController:Controller
{
    public List<Category> GetAllCategory(string c)
    {
        SqlConnection connection= new SqlConnection();
        connection.ConnectionString="Server=DESKTOP-0EKNFJB;Database=TSQL2012;Trusted_Connection=True";
        connection.Open();
        SqlCommand command= new SqlCommand();
        command.CommandType=CommandType.Text;
        command.CommandText=$@"Select categoryname from Production.Categories";
        command.Connection=connection;

        SqlDataReader reader = command.ExecuteReader();
      
        List <Category> categoryNames=new List<Category>();
        while(reader.Read())
        {
            Category temp1=new Category();
            temp1.categoryID=int.Parse(reader["categoryid"].ToString());
            temp1.categoryName=reader["categoryname"].ToString();
            temp1.description=reader["description"].ToString();
            categoryNames.Add(temp1);

        }
        return categoryNames;
    }
}