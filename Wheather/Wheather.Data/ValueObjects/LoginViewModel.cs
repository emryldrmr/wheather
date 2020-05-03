using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wheather.Data.ValueObjects
{
    public class LoginViewModel
    {
        [DisplayName("Kullanıcı Email"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string email { get; set; }

        [DisplayName("Şifre"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            DataType(DataType.Password),
            StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string sifre { get; set; }
    }
}