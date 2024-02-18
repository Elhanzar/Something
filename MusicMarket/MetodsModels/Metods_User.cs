using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicMarket.ContextModels;
using MusicMarket.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MusicMarket.MetodsModels
{
    class Metods_User
    {
        User userFromOpen = new User();
        public void SourseUser(ref User User)
        {
            User = userFromOpen;
        }
        public bool ADD_User(User user)
        {
            var context = new MusicsContext();
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            context.Dispose();
            return true;
        }
        public bool Opening_User(User user)
        {
            var context = new MusicsContext();
            var users = context.Users.Where(p => p.Name == $"{user.Name}").Where(p => p.Surname == $"{user.Surname}")
                .Where(p => p.Email == $"{user.Email}").Where(p => p.Password == $"{user.Password}");
            foreach (var items in users)
                user = items;
            if (user.Id == 0)
            {
                return false;
            }
            userFromOpen = user;
            return true;
        }
    }
}
