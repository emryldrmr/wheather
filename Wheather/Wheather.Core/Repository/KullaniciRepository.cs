﻿using Wheather.Core.Infrastructure;
using Wheather.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace Wheather.Core.Repository
{
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly wheatherdb _context = new wheatherdb();

        public IEnumerable<Data.Model.Kullanici> GetAll()
        {
            return _context.Kullanici.Select(x => x); // Tüm kullanıcılar dönecek
        }

        public Data.Model.Kullanici GetById(int id)
        {
            return _context.Kullanici.FirstOrDefault(x => x.id == id);
        }

        public Data.Model.Kullanici Get(System.Linq.Expressions.Expression<Func<Data.Model.Kullanici, bool>> expression)
        {
            return _context.Kullanici.FirstOrDefault(expression);
        }

        public IQueryable<Data.Model.Kullanici> GetMany(System.Linq.Expressions.Expression<Func<Data.Model.Kullanici, bool>> expression)
        {
            return _context.Kullanici.Where(expression);
        }

        public void Insert(Data.Model.Kullanici obj)
        {
            _context.Kullanici.Add(obj);
        }

        public void Update(Data.Model.Kullanici obj)
        {
            _context.Kullanici.AddOrUpdate();
        }

        public void Delete(int id)
        {
            var Kullanici = GetById(id);
            if (Kullanici != null)
            {
                _context.Kullanici.Remove(Kullanici);
            }
        }

        public int Count()
        {
            return _context.Kullanici.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Data.Model.Kullanici KullaniciBul(string Email)
        {
            return _context.Kullanici.FirstOrDefault(x => x.email == Email);
        }
    }
}
