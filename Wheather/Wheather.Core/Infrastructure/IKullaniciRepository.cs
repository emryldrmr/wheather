using Wheather.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheather.Core.Infrastructure
{
    public interface IKullaniciRepository : IRepository<Kullanici>
    {
        Kullanici KullaniciBul(string email);
    }
}
