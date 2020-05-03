using Eblog.Admin.CustomFilter;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Wheather.Core.Infrastructure;
using Wheather.Data.Model;

namespace Wheather.Admin.Controllers
{
    public class HomeController : Controller
    {
        #region Veritabanı
        private readonly IUygulamaRepository _uygulamaRepository;
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly IYetkiRepository _yetkiRepository;

        public HomeController(IUygulamaRepository uygulamaRepository, IKullaniciRepository kullaniciRepository, IYetkiRepository yetkiRepository)
        {
            _uygulamaRepository = uygulamaRepository;
            _kullaniciRepository = kullaniciRepository;
            _yetkiRepository = yetkiRepository;
        }
        #endregion

        [LoginFilter]
        public ActionResult Index(int Sayfa = 1)
        {
            var UygulamaListe = _uygulamaRepository.GetMany(x => x.aktif == false);
            return View(UygulamaListe.OrderByDescending(x => x.id).ToPagedList(Sayfa, 10));
            
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Kullanici kullanici)
        {
            var KullaniciVarmi = _kullaniciRepository.GetMany(x => x.email == kullanici.email && x.sifre == kullanici.sifre).SingleOrDefault();
            if (KullaniciVarmi != null)
            {
                if (KullaniciVarmi.Yetki.yetki_adi == "Admin")
                {
                    Session["yetki_id"] = KullaniciVarmi.yetki_id;
                    Session["ID"] = KullaniciVarmi.id;
                    Session["KullaniciEmail"] = KullaniciVarmi.email;
                    Session["KullaniciAdi"] = KullaniciVarmi.kullanici_adi;
                    Session["AdSoyad"] = KullaniciVarmi.adsoyad;
                    Session["Foto"] = KullaniciVarmi.fotograf;
                    Session["YetkiAdi"] = KullaniciVarmi.Yetki.yetki_adi;
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Mesaj = "Yetkisiz Kullanıcı";
                return View();
            }
            ViewBag.Mesaj = "Kullanıcı Bulunamadı.";
            return View();
        }
    }
}