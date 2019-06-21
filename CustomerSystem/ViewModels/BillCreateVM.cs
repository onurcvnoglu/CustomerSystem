using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerSystem.ViewModels
{
    public class BillCreateVM
    {
        [Required]
        [DisplayName("Müşteri No")]
        public int customer_id { get; set; }
        [Required]
        [DisplayName("Müşteri No")]
        public IEnumerable<SelectListItem> customerList { get; set; }
        [Required]
        [DisplayName("Toplam Tutar")]
        public int total { get; set; }
        [Required]
        [DisplayName("Tarih")]
        public DateTime bill_date { get; set; }
    }
}