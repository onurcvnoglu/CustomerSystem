using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerSystem.ViewModels
{
    public class CustomerEditVM
    {
        public int customer_id { get; set; }

        [Required]
        [DisplayName("Ad")]
        public string customer_name { get; set; }
        [Required]
        [DisplayName("Soyad")]
        public string customer_surname { get; set; }

        [Required(ErrorMessage = "Telefon Numarası Boş Bırakılamaz")]
        [Display(Name = "Telefon No")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Doğru Bir Numara Giriniz")]

        public string customer_phone { get; set; }
        [Required]
        [DisplayName("Adres")]
        public string customer_address { get; set; }
        [Required]
        [DisplayName("Tarife Adı")]
        public int schedule_id { get; set; }
        [DisplayName("Tarife Adı")]
        public IEnumerable<SelectListItem> ScheduleList { get; set; }
    }
}