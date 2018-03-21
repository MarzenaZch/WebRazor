using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using webRazor.Models;

public class ProductsController :Controller
{
    [HttpGet]
    public ViewResult GetAllProducts(string sortBy, string searchP, int page)
    {
      
        SqlConnection connection= new SqlConnection();
        connection.ConnectionString="Server=DESKTOP-0EKNFJB;Database=TSQL2012;Trusted_Connection=True";
        connection.Open();
        SqlCommand command= new SqlCommand();
      
        if(sortBy==null)///argument przyjmuje null GetAllProduct przyjmuje product name zeby nie wywaliło błędu 
        {
            sortBy="ProductName";

        };
        ViewData["currentPage"]=page;
        ViewData["sortBy"]=sortBy;

        

        command.CommandText=$@"Select * from GetAllProduct where productname like  '%{searchP}%'  order by {sortBy} OFFSET {page*5} ROWS FETCH NEXT 5 ROWS ONLY ";
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
        SqlConnection connection= new SqlConnection();
        connection.ConnectionString="Server=DESKTOP-0EKNFJB;Database=TSQL2012;Trusted_Connection=True";
        connection.Open();
        SqlCommand command= new SqlCommand();
        command.CommandType=CommandType.Text;
        command.CommandText=$@"Select * from Production.Suppliers";
        command.Connection=connection;

        SqlDataReader reader = command.ExecuteReader();
      
        List <SelectListItem> item=new List<SelectListItem>();
         while(reader.Read())
        {
            Suppliers temp1=new Suppliers();
            temp1.campanyName=reader["companyname"].ToString();
            temp1.supplierID=int.Parse(reader["supplierid"].ToString());
           
            item.Add(new SelectListItem { Text =  temp1.campanyName,Value="1"});
            
            
        }
        ViewData["supplier"]=item;
        
        
        
        

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