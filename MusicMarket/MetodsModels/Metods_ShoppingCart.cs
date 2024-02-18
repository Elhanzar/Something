using Microsoft.EntityFrameworkCore;
using MusicMarket.ContextModels;
using MusicMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MusicMarket.MetodsModels
{
    class Metods_ShoppingCart
    {
        List<Order> shoppingCart = new List<Order>();
        public void SourseShopCart(ref List<Order> shoppingCart)
        {
            shoppingCart = this.shoppingCart;
        }
        void MinuseQuantity(int TrackId)
        {
            using (MusicsContext context = new MusicsContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Track? tracks = context.Tracks.Where(p => p.Id == TrackId).FirstOrDefault();
                        if (tracks != null)
                        {
                            tracks.Quantity--;
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        transaction.Rollback();
                    }
                }
            }
        }
        void PlusQuantity(int TrackId)
        {
            using (MusicsContext context = new MusicsContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Track? tracks = context.Tracks.Where(p => p.Id == TrackId).FirstOrDefault();
                        if (tracks != null)
                        {
                            tracks.Quantity++;
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        transaction.Rollback();
                    }
                }
            }
        } 
        public void ADD_InShoppingCart(Order shoppingCart)
        {
            try
            {
                using (MusicsContext context = new MusicsContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            MinuseQuantity(shoppingCart.Trackid);
                            context.Orders.Add(shoppingCart);
                            context.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        internal void Delete_FromShoppingCart(Order shoppingCart)
        {
            try
            {
                using (MusicsContext context = new MusicsContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            PlusQuantity(shoppingCart.Trackid);
                            context.Orders.Remove(shoppingCart);
                            context.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        internal void Get_all(int Userid)
        {
            var context = new MusicsContext();
            try
            {
                var tracks = context.Orders.Where(p => p.Userid == Userid).Include(x => x.User).Include(x => x.Track);
                foreach (var items in tracks)
                    shoppingCart.Add(items);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            context.Dispose();
        }
        public void PrintWhere(int Userid, string orders)
        {
            var context = new MusicsContext();
            try
            {
                var tracks = context.Orders.Include(x => x.Track).Where(p => p.Userid == Userid).Where(p => EF.Functions.Like(p.Track.Musicname!, $"%{orders}%"));
                foreach (var items in tracks)
                    shoppingCart.Add(items);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            context.Dispose();
        }
        public void Expensive(int Userid)
        {
            var context = new MusicsContext();
            try
            {
                var tracks = context.Orders.Include(x => x.Track).Where(p => p.Userid == Userid).OrderByDescending(p => p.Track.Price);
                foreach (var items in tracks)
                    shoppingCart.Add(items);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            context.Dispose();
        }
        public void Сheap(int Userid)
        {
            var context = new MusicsContext();
            try
            {
                var tracks = context.Orders.Include(x => x.Track).Where(p => p.Userid == Userid).OrderBy(p => p.Track.Price);
                foreach (var items in tracks)
                    shoppingCart.Add(items);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            context.Dispose();
        }
        public void NewsTracks(int Userid)
        {
            var context = new MusicsContext();
            try
            {
                var tracks = context.Orders.Include(x => x.Track).Where(p => p.Userid == Userid).OrderByDescending(p => p.Track.Daterelize);
                foreach (var items in tracks)
                    shoppingCart.Add(items);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            context.Dispose();
        }
        public void OldsTracks(int Userid)
        {
            var context = new MusicsContext();
            try
            {
                var tracks = context.Orders.Include(x => x.Track).Where(p => p.Userid == Userid).OrderBy(p => p.Track.Daterelize);
                foreach (var items in tracks)
                    shoppingCart.Add(items);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            context.Dispose();
        }
    }
}
