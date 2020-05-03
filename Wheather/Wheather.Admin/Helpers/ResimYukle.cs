using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wheather.Data.Model;

namespace Eblog.Admin.Helpers
{
    public class ResimYukle
    {
        public static string kullaniciResim(HttpPostedFileBase Resim, Kullanici kullanici)
        {
            string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
            string[] uzanti = Resim.ContentType.Split('/');
            string TamYolYeri = "/img/profilfoto/" + DosyaAdi + "." + uzanti[1];

            Resim.SaveAs(System.Web.HttpContext.Current.Server.MapPath(TamYolYeri));
            kullanici.fotograf = TamYolYeri;
            return kullanici.fotograf;
        }
    }
}