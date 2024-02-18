using MusicMarket.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MusicMarket.ContextModels;

public partial class User : IDataErrorInfo
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

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
                case "Name":
                    if (Name != null)
                    {
                        if (Name.Length > 50 || IsDigitsOnly(Name))
                        {
                            error = "Имя слишком длинное или в ней содержатся числа";
                        }
                    }
                    break;
                case "Surname":
                    if (Surname != null)
                    {
                        if (Surname.Length > 50 || IsDigitsOnly(Surname))
                        {
                            error = "Фамилия слишком длинная или в ней содержатся числа";
                        }
                    }
                    break;
                case "Patronymic":
                    if (Patronymic != null)
                    {
                        if (Patronymic.Length > 50 || IsDigitsOnly(Patronymic))
                        {
                            error = "Отчество слишком длинное или в ней содержатся числа";
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
