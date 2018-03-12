using Microsoft.AspNetCore.Mvc;

public class ProductsController :Controller
{
    [HttpGet]
    public ViewResult GetAllProducts()
    {
        ViewData["przywitanie"]="hello word";
        return View();
    }
}