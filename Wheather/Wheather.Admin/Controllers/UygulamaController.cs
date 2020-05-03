using Eblog.Admin.Class;
using Eblog.Admin.CustomFilter;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wheather.Core.Infrastructure;
using Wheather.Data.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Wheather.Admin.Controllers
{
    public class UygulamaController : Controller
    {
        #region Veritabanı
        private readonly IUygulamaRepository _uygulamaRepository;
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly IYetkiRepository _yetkiRepository;

        public UygulamaController(IUygulamaRepository uygulamaRepository, IKullaniciRepository kullaniciRepository, IYetkiRepository yetkiRepository)
        {
            _uygulamaRepository = uygulamaRepository;
            _kullaniciRepository = kullaniciRepository;
            _yetkiRepository = yetkiRepository;
        }
        #endregion

        #region Uygulama Listesi
        [HttpGet]
        [LoginFilter]
        public ActionResult Index(int Sayfa = 1)
        {
            var UygulamaListe = _uygulamaRepository.GetAll();
            return View(UygulamaListe.OrderByDescending(x => x.id).ToPagedList(Sayfa, 10));
        }
        #endregion

        #region Uygulama Ekle
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        [LoginFilter]
        public JsonResult Ekle(Uygulama uygulama)
        {
            var sessionControl = HttpContext.Session["id"];
            if (ModelState.IsValid)
            {
                var kullanici = _kullaniciRepository.GetById(Int32.Parse(sessionControl.ToString()));
                uygulama.aktif = true;
                uygulama.tarih = DateTime.Now.ToLocalTime().ToString();
                uygulama.kullanici_id = kullanici.id;
                _uygulamaRepository.Insert(uygulama);
                try
                {
                    _uygulamaRepository.Save();
                    return Json(new ResultJson { Success = true, Message = "Uygulama Ekleme İşleminiz Başarılı" });
                }
                catch (Exception ex)
                {
                    return Json(new ResultJson { Success = false, Message = "Uygulama Eklerken Hata Oluştu !" });
                }
            }
            return Json(new ResultJson { Success = false, Message = "Uygulama Eklerken Hata Oluştu !" });
        }

        #endregion

        #region Uygulama Düzenle
        [HttpGet]
        [LoginFilter]
        public ActionResult Duzenle(int id)
        {
            Uygulama gelenUygulama = _uygulamaRepository.GetById(id);

            if (gelenUygulama == null)
            {
                //throw new Exception("Kullanıcı Bulunamadı !");
                return Json(new ResultJson { Success = false, Message = "Uygulama Bulunamadı!" });
            }
            else
            {
                return View(gelenUygulama);
            }
        }

        [HttpPost]
        [LoginFilter]
        public JsonResult Duzenle(Uygulama uygulama)
        {
            Uygulama gelenUygulama = _uygulamaRepository.GetById(uygulama.id);


            gelenUygulama.adi = uygulama.adi;
            gelenUygulama.aktif = uygulama.aktif;
            gelenUygulama.url = uygulama.url;
            
            try
            {
                _uygulamaRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Başarılı Bir Şekilde Güncellendi" });
            }
            catch (Exception ex)
            {
                return Json(new ResultJson { Success = false, Message = "Güncelleme İşlemi Başarısız" });
            }

        }
        #endregion

        #region Uygulama Detay
        [LoginFilter]
        public ActionResult Detay(int id)
        {
            Uygulama gelenUygulama = _uygulamaRepository.GetById(id);
            if (gelenUygulama == null)
            {
                //throw new Exception("Kullanıcı Bulunamadı !");
                return Json(new ResultJson { Success = false, Message = "Uygulama Bulunamadı!" });
            }
            else
            {
                return View(gelenUygulama);
            }
        }
        #endregion
    }
}