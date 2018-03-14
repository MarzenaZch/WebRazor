using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using web1.Models;

public class ProductsController :Controller
{
    [HttpGet]
    public ViewResult GetAllProducts(string sortBy, string searchP)
    {
       /* List <string> lista=new List<string>();
        lista.Add("pierwszy");
        lista.Add("drugi");
        ViewData["lista"]=lista;*/
       /* List <Product> products=new List<Product>();
        products.Add(new Product{
            ProductName="nam1",
            SupplierID=4
        });
        products.Add(new Product{
            ProductName="nam2",
            SupplierID=5

        });

        ViewData["products"]=products;
        return View();*/
        SqlConnection connection= new SqlConnection();
        connection.ConnectionString="Server=DESKTOP-0EKNFJB;Database=TSQL2012;Trusted_Connection=True";
        connection.Open();
        SqlCommand command= new SqlCommand();
      //  command.CommandType=CommandType.Text;
        if(sortBy==null)///argument przyjmuje null GetAllProduct przyjmuje product name zeby nie wywaliło błędu 
        {
            sortBy="ProductName";

        };

        command.CommandText=$@"Select * from GetAllProduct where productname like  '%{searchP}%'  order by {sortBy} ";
        command.Connection=connection;

        SqlDataReader reader = command.ExecuteReader();
      
        List <Product> productNames=new List<Product>();
        while(reader.Read())
        {
            Product temp= new Product();
            temp.ProductID=int.Parse(reader["productid"].ToString());
            temp.ProductName=reader["productname"].ToString();
            temp.SupplierID=int.Parse(reader["supplierid"].ToString());
            temp.CategoryID=int.Parse(reader["categoryid"].ToString());
            temp.UnitPrice=double.Parse(reader["unitprice"].ToString());
            temp.Discontinued=bool.Parse(reader["discontinued"].ToString());
        productNames.Add(temp);
        }

        ViewData["products"]=productNames;///wysłanie view elemntu 
        return View();
        
    }
    [HttpGet]
    public ViewResult AddProductForm()
    {
        return View();
    }
    [HttpPost]//dodawnaie formularza 
    public void AddProduct([FromForm] Product p)
    {
        SqlConnection connection= new SqlConnection();
        connection.ConnectionString="Server=DESKTOP-0EKNFJB;Database=TSQL2012;Trusted_Connection=True";
        connection.Open();
        SqlCommand command= new SqlCommand();
        command.CommandType=CommandType.Text;

        command.CommandText=$"exec [dbo].[addProduct]  {p.ProductName},{p.SupplierID},{p.CategoryID},{p.UnitPrice} ,{p.Discontinued}";
        command.Connection=connection;
        command.ExecuteNonQuery();///dodowanie do sql

    }
}