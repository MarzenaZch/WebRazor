namespace web1.Models{

public class Product{
    public int ProductID{get; set;}

    public string ProductName{get; set;}

    public int SupplierID{get; set;}

    public int CategoryID{get; set;}

    public double UnitPrice{get; set;}

    public bool Discontinued{get; set;}
}
}
