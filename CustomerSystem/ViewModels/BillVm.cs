using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerSystem.ViewModels
{
    public class BillVm
    {
        public int bill_id { get; set; }
        [DisplayName("Müşteri Fatura No")]
        public int customer_id { get; set; }
        [Required]
        [DisplayName("Müşteri Adı")]
        public string customer_name { get; set; }
        [Required]
        [DisplayName("Müşteri Soyadı")]
        public string customer_surname { get; set; }
        [Required]
        [DisplayName("Toplam Tutar")]
        public int bill_total { get; set; }
        [Required]
        [DisplayName("Tarih")]
        public System.DateTime bill_date { get; set; }
    }
}