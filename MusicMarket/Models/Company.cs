using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MusicMarket.Models;

public partial class Company : IDataErrorInfo
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
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
            string error = string.Empty;
            switch (columnName)
            {
                case "Name":
                    if (Name != null)
                    {
                        if (Name.Length > 50 || IsDigitsOnly(Name))
                        {
                            error = "Имя слишком длинное или в ней содержатся числа";
                        }
                    }
                    break;
                case "Number":
                    if (Number != null)
                    {
                        if (Number.Length > 12 || !IsDigitsOnly(Number))
                        {
                            error = "Номер слишком длинный или в нем буквы";
                        }
                    }
                    break;
                case "Email":
                    if (Email != null)
                    {
                        if (IsDigitsOnly(Email))
                        {
                            error = "В ней содержатся только числа";
                        }
                    }
                    break;
                case "Password":
                    if (Password != null)
                    {
                        if (IsDigitsOnly(Password))
                        {
                            error = "В ней содержатся только числа";
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
