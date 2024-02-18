using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Linq;

namespace MusicMarket.Models;

public partial class Track : IDataErrorInfo
{
    public int Id { get; set; }

    public string Musicname { get; set; } = null!;

    public string Groupname { get; set; } = null!;

    public string Authorname { get; set; } = null!;

    public int Quantity { get; set; }

    public string Genre { get; set; } = null!;

    public DateTime Daterelize { get; set; }

    public decimal Costprice { get; set; }

    public decimal Price { get; set; }

    public int Authorid { get; set; }

    public virtual Company Author { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    bool IsDigitsOnly(string str)
    {
        foreach (char c in str)
        {
            if (c < '0' || c > '9')
            {
                return false;
            }
        }
        return true;
    }
    public string this[string columnName]
    {
        get
        {
            string error = String.Empty;
            switch (columnName)
            {
                case "Musicname":
                    if (Musicname != null)
                    {
                        if (Musicname.Length > 50 || IsDigitsOnly(Musicname))
                        {
                            error = "Musicname слишком длинное или в ней содержатся числа";
                        }
                    }
                    break;
                case "Groupname":
                    if (Groupname != null)
                    {
                        if (Groupname.Length > 50 || IsDigitsOnly(Groupname))
                        {
                            error = "Groupname слишком длинная или в ней содержатся числа";
                        }
                    }
                    break;
                case "Genre":
                    if (Genre != null)
                    {
                        if (Genre.Length > 50 || IsDigitsOnly(Genre))
                        {
                            error = "Genre слишком длинное или в ней содержатся числа";
                        }
                    }
                    break;
                case "Quantity":
                    if (Quantity != 0)
                    {
                        if (!IsDigitsOnly(Quantity.ToString()))
                        {
                            error = "Неправильно";
                        }
                    }
                    break;
                case "Costprice":
                    if (Costprice != 0)
                    {
                        if (!IsDigitsOnly(Costprice.ToString()))
                        {
                            error = "Неправильно";
                        }
                    }
                    break;
                case "Price":
                    if (Price != 0)
                    {
                        if (!IsDigitsOnly(Price.ToString()))
                        {
                            error = "Неправильно";
                        }
                    }
                    break;
            }
            return error;
        }
    }
    public string Error
    {
        get { throw new NotImplementedException(); }
    }
}
