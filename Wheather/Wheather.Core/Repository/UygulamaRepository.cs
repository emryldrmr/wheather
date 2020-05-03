using Wheather.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using Wheather.Data.Model;
using System.Linq.Expressions;

namespace Wheather.Core.Repository
{
   public class UygulamaRepository : IUygulamaRepository
    {
        private readonly wheatherdb _context = new wheatherdb();

        public int Count()
        {
            return _context.Uygulama.Count();
        }

        public void Delete(int id)
        {
            var Uygulama = GetById(id);
            if (Uygulama != null)
            {
                _context.Uygulama.Remove(Uygulama);
            }
        }

        public Uygulama Get(Expression<Func<Uygulama, bool>> expression)
        {
            return _context.Uygulama.FirstOrDefault(expression);
        }

        public IEnumerable<Uygulama> GetAll()
        {
            return _context.Uygulama.Select(x => x);  // Tüm uygulamalar dönecek
        }

        public Uygulama GetById(int id)
        {
            return _context.Uygulama.FirstOrDefault(x => x.id == id);
        }

        public IQueryable<Uygulama> GetMany(Expression<Func<Uygulama, bool>> expression)
        {
            return _context.Uygulama.Where(expression);
        }

        public void Insert(Uygulama obj)
        {
            _context.Uygulama.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Uygulama obj)
        {
            _context.Uygulama.AddOrUpdate();
        }
    }
}
