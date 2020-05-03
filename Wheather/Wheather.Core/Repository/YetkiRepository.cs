using Wheather.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity.Migrations; //AddorUpdate için gerekli
using Wheather.Data.Model;

namespace Wheather.Core.Repository
{
    public class YetkiRepository : IYetkiRepository
    {
        private readonly wheatherdb _context = new wheatherdb();

        public int Count()
        {
            return _context.Yetki.Count();
        }

        public void Delete(int id)
        {
            var Yetki = GetById(id);
            if (Yetki != null)
            {
                _context.Yetki.Remove(Yetki);
            }
        }

        public Yetki Get(Expression<Func<Yetki, bool>> expression)
        {
            return _context.Yetki.FirstOrDefault(expression);
        }

        public IEnumerable<Yetki> GetAll()
        {
            return _context.Yetki.Select(x => x);  // Tüm Yetkiler dönecek
        }

        public Yetki GetById(int id)
        {
            return _context.Yetki.FirstOrDefault(x => x.id == id);
        }

        public IQueryable<Yetki> GetMany(Expression<Func<Yetki, bool>> expression)
        {
            return _context.Yetki.Where(expression);
        }

        public void Insert(Yetki obj)
        {
            _context.Yetki.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Yetki obj)
        {
            _context.Yetki.AddOrUpdate();
        }
    }
}
