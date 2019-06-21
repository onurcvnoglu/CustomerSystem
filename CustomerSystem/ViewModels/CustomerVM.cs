using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerSystem.ViewModels
{
    public class CustomerVM
    {
        [Required]
        [DisplayName("XDSL No")]
        public int customer_id { get; set; }
        [Required]
        [DisplayName("Ad")]
        public string customer_name { get; set; }
        [Required]
        [DisplayName("Soyad")]
        public string customer_surname { get; set; }
        [Required]
        [DisplayName("Telefon No")]
        public string customer_phone { get; set; }
        [Required]
        [DisplayName("Adres")]
        public string customer_address { get; set; }
        [Required]
        [DisplayName("Tarife Adı")]
        public string schedule_name { get; set; }
        [Required]
        [DisplayName("Tarife Hızı")]
        public string schedule_speed { get; set; }
    }
}