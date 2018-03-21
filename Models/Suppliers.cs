using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

public class Suppliers
{
    public int supplierID{set;get;}
    public string campanyName{set;get;}

    public static implicit operator Suppliers(List<SelectListItem> v)
    {
        throw new NotImplementedException();
    }

    internal void Add(SelectListItem selectListItem)
    {
        throw new NotImplementedException();
    }
}
