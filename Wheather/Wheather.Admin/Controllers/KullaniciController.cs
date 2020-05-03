using Eblog.Admin.Class;
using Eblog.Admin.CustomFilter;
using Eblog.Admin.Helpers;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wheather.Core.Infrastructure;
using Wheather.Data.Model;

namespace Wheather.Admin.Controllers
{
    public class KullaniciController : Controller
    {
        #region Veritabanı
        private readonly IUygulamaRepository _uygulamaRepository;
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly IYetkiRepository _yetkiRepository;

        public KullaniciController(IUygulamaRepository uygulamaRepository, IKullaniciRepository kullaniciRepository, IYetkiRepository yetkiRepository)
        {
            _uygulamaRepository = uygulamaRepository;
            _kullaniciRepository = kullaniciRepository;
            _yetkiRepository = yetkiRepository;
        }
        #endregion


        #region Kullanici Listesi
        [HttpGet]
        [LoginFilter]
        public ActionResult Index(int Sayfa = 1)
        {
            var KullaniciListe = _kullaniciRepository.GetAll();
            return View(KullaniciListe.OrderByDescending(x => x.id).ToPagedList(Sayfa, 10));
        }
        #endregion

        #region Kullanıcı Ekle
        [HttpGet]
        public ActionResult Ekle()
        {
            SetYetkiListele();
            return View();
        }

        [HttpPost]
        [LoginFilter]
        public JsonResult Ekle(Kullanici kullanici, int? yetki_id, HttpPostedFileBase Resim)
        {
            if (ModelState.IsValid)
            {
                var KullaniciVarmi = _kullaniciRepository.KullaniciBul(kullanici.email);
                if (KullaniciVarmi != null)
                {
                    return Json(new ResultJson { Success = false, Message = kullanici.email + " Daha önce Kayıt Edilmiş" });
                }
                if (kullanici.fotograf == null)
                {
                    if (Resim.ContentLength > 2048000)
                    {
                        return Json(new ResultJson { Success = false, Message = "Dosya boyutu 2 MB'yi geçmemelidir." });

                    }
                    else if (Resim.ContentLength > 0 && Resim.ContentLength <= 2048000)
                    {
                        ResimYukle.kullaniciResim(Resim, kullanici);
                        kullanici.fotograf = kullanici.fotograf;
                    }
                }

                kullanici.aktif = false;
                kullanici.tarih = DateTime.Now.ToLocalTime().ToString();
                kullanici.yetki_id = Convert.ToInt32(yetki_id);
                _kullaniciRepository.Insert(kullanici);
                try
                {
                    _kullaniciRepository.Save();
                    return Json(new ResultJson { Success = true, Message = "Kullanıcı Ekleme İşleminiz Başarılı" });
                }
                catch (Exception ex)
                {
                    return Json(new ResultJson { Success = false, Message = "Kullanıcı Eklerken Hata Oluştu !" });
                }
            }
            return Json(new ResultJson { Success = false, Message = "Kullanıcı Eklerken Hata Oluştu !" });
        }

        #endregion

        #region Kullanıcı Detay
        [LoginFilter]
        public ActionResult Detay(int id)
        {
            Kullanici gelenKullanici = _kullaniciRepository.GetById(id);

            if (gelenKullanici == null)
            {
                //throw new Exception("Kullanıcı Bulunamadı !");
                return Json(new ResultJson { Success = false, Message = "Kullanıcı Bulunamadı!" });
            }
            else
            {
                SetYetkiListele();
                return View(gelenKullanici);
            }
        }
        #endregion

        #region Kullanıcı Düzenle
        [HttpGet]
        [LoginFilter]
        public ActionResult Duzenle(int id)
        {
            Kullanici gelenKullanici = _kullaniciRepository.GetById(id);

            if (gelenKullanici == null)
            {
                //throw new Exception("Kullanıcı Bulunamadı !");
                return Json(new ResultJson { Success = false, Message = "Kullanıcı Bulunamadı!" });
            }
            else
            {
                SetYetkiListele();
                return View(gelenKullanici);
            }
        }

        [HttpPost]
        [LoginFilter]
        public JsonResult Duzenle(Kullanici kullanici, int? yetki_id, HttpPostedFileBase Resim)
        {
            Kullanici gelenKullanici = _kullaniciRepository.GetById(kullanici.id);

            var EmailVarmi = _kullaniciRepository.KullaniciBul(kullanici.email);
            if (EmailVarmi != null && gelenKullanici.email != kullanici.email)
            {
                return Json(new ResultJson { Success = false, Message = "Bu Mail Adresi Sistemde Kayıtlı" });
            }

            gelenKullanici.kullanici_adi = kullanici.kullanici_adi;
            gelenKullanici.adsoyad = kullanici.adsoyad;
            gelenKullanici.email = kullanici.email;
            gelenKullanici.aktif = kullanici.aktif;
            gelenKullanici.yetki_id = Convert.ToInt32(yetki_id);
            gelenKullanici.sifre = kullanici.sifre;

            if (Resim != null && Resim.ContentLength > 0 && Resim.ContentLength <= 2048000)
            {
                if (gelenKullanici.fotograf != null)
                {
                    string url = gelenKullanici.fotograf;
                    string resimPath = Server.MapPath(url);
                    FileInfo files = new FileInfo(resimPath);
                    if (files.Exists)
                    {
                        files.Delete();
                    }
                }
                ResimYukle.kullaniciResim(Resim, kullanici);
                gelenKullanici.fotograf = kullanici.fotograf;
            }
            try
            {
                _kullaniciRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Başarılı Bir Şekilde Güncellendi" });
            }
            catch (Exception ex)
            {
                return Json(new ResultJson { Success = false, Message = "Güncelleme İşlemi Başarısız" });
            }

        }
        #endregion

        #region Kullanıcı Sil
        [LoginFilter]
        public JsonResult Sil(Kullanici kullanici)
        {
            Kullanici dbKullanici = _kullaniciRepository.GetById(kullanici.id);
            if (dbKullanici == null)
            {
                return Json(new ResultJson { Success = false, Message = "Kullanıcı Bulunamadı !" });
            }

            try
            {
                if (dbKullanici != null)
                {
                    string Resim = dbKullanici.fotograf;
                    string resimPath = Server.MapPath(Resim);
                    FileInfo files = new FileInfo(resimPath);
                    if (files.Exists)
                    {
                        files.Delete();
                    }
                }
                _kullaniciRepository.Delete(kullanici.id);
                _kullaniciRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Kullanıcı Silme işlemi Başarılı" });
            }
            catch (Exception)
            {
                return Json(new ResultJson { Success = false, Message = "Kullanıcı Silme İşlemi Sırasında Bir Hata Oluştu !" });
            }
        }
        #endregion

        #region Aktif / Pasif Yapar
        [LoginFilter]
        public ActionResult Aktif(int id)
        {
            Kullanici gelenKullanici = _kullaniciRepository.GetById(id);
            if (gelenKullanici.aktif == true)
            {
                gelenKullanici.aktif = false;
                _kullaniciRepository.Save();
                TempData["Bilgi"] = "İşleminiz Başarılı";
                return RedirectToAction("Index", "Kullanici");
            }
            else if (gelenKullanici.aktif == false)
            {
                gelenKullanici.aktif = true;
                _kullaniciRepository.Save();
                TempData["Bilgi"] = "İşleminiz Başarılı";
                return RedirectToAction("Index", "Kullanici");
            }
            return View();
        }
        #endregion

        #region Yetki Listesi
        public void SetYetkiListele(object rol = null)
        {
            var YetkiList = _yetkiRepository.GetAll().ToList();
            ViewBag.Yetki = YetkiList;
        }
        #endregion
    }
}