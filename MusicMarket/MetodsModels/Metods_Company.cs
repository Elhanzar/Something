using MusicMarket.ContextModels;
using MusicMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MusicMarket.MetodsModels
{
    class Metods_Company
    {
        Company companyFromOpen = new Company();
        public void SourseCompany(ref Company сompany)
        {
            сompany = companyFromOpen;
        }
        public bool ADD_Company(Company company)
        {
            var context = new MusicsContext();
            try
            {
                context.Companies.Add(company);
                context.SaveChanges();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            context.Dispose();
            return true;
        }
        public bool Opening_Company(Company company)
        {
            var context = new MusicsContext();
            var companys = context.Companies.Where(p => p.Name == $"{company.Name}").Where(p => p.Number == $"{company.Number}")
                .Where(p => p.Email == $"{company.Email}").Where(p => p.Password == $"{company.Password}");
            foreach (var items in companys)
                company = items;
            if (company.Id == 0)
            {
                return false;
            }
            companyFromOpen = company;
            return true;
        }
    }
}
