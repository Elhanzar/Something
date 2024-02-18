using Microsoft.EntityFrameworkCore;
using MusicMarket.ContextModels;
using MusicMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MusicMarket.MetodsModels
{
    class Metods_Track
    {
        List<Track> trackFrom = new List<Track>();
        public void SourseUser(ref List<Track> track)
        {
            track = trackFrom;
        }
        public void Get_all()
        {
            var context = new MusicsContext();
            try
            {
                var tracks = context.Tracks;
                foreach (var items in tracks)
                    trackFrom.Add(items);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            context.Dispose();
        }
        public bool Add_track(Track track)
        {
            var context = new MusicsContext();
            bool addfalse;
            try
            {
                context.Tracks.Add(track);
                context.SaveChanges();
                addfalse = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                addfalse = true;

            }
            if (addfalse) { return false; }
            context.Dispose();
            return true;
        }
        public void PrintWhere(string orders)
        {
            var context = new MusicsContext();
            try
            {
                var tracks = context.Tracks.Where(p => EF.Functions.Like(p.Musicname!, $"%{orders}%"));
                foreach (var items in tracks)
                    trackFrom.Add(items);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            context.Dispose();
        }
        public void DeleteTrack(Track track)
        {
            var context = new MusicsContext();
            try
            {
                context.Tracks.Remove(track);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            context.Dispose();
        }
        public void Expensive()
        {
            var context = new MusicsContext();
            try
            {
                var tracks = context.Tracks.OrderByDescending(p => p.Price);
                foreach (var items in tracks)
                    trackFrom.Add(items);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            context.Dispose();
        } 
        public void Сheap()
        {
            var context = new MusicsContext();
            try
            {
                var tracks = context.Tracks.OrderBy(p => p.Price);
                foreach (var items in tracks)
                    trackFrom.Add(items);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            context.Dispose();
        }    
        public void NewsTracks()
        {
            var context = new MusicsContext();
            try
            {
                var tracks = context.Tracks.OrderByDescending(p => p.Daterelize);
                foreach (var items in tracks)
                    trackFrom.Add(items);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            context.Dispose();
        }
        public void OldsTracks()
        {
            var context = new MusicsContext();
            try
            {
                var tracks = context.Tracks.OrderBy(p => p.Daterelize);
                foreach (var items in tracks)
                    trackFrom.Add(items);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            context.Dispose();
        }
    }
}
