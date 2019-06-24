using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerSystem.ViewModels;
using CustomerSystem.Models;
using System.Data.Entity;
using PagedList;

namespace CustomerSystem.Controllers
{
    public class BillController : Controller
    {
        customerSystemDbEntities db = new customerSystemDbEntities();
        // GET: Bill
        public ActionResult billDetail(int page=1,int pageSize=10)
        {
            List<BillVm> billVmList = new List<BillVm>();

            var billList = (from bil in db.Bills
                            join cust in db.Customers on bil.customer_id equals cust.customer_id
                            select new { cust.customer_name, cust.customer_surname, bil.customer_id, bil.bill_total, bil.bill_date, bil.bill_id });
            
            foreach (var item in billList)
            {
                BillVm billVm = new BillVm();
                billVm.bill_id = item.bill_id;
                billVm.customer_id = item.customer_id;
                billVm.customer_name = item.customer_name;
                billVm.customer_surname = item.customer_surname;
                billVm.bill_total = item.bill_total;
                billVm.bill_date = item.bill_date;
                billVmList.Add(billVm);
            }
            PagedList<BillVm> billInfo = new PagedList<BillVm>(billVmList, page, pageSize);
            return View(billInfo);
        }
        private IEnumerable<SelectListItem> GetCustomerList()
        {
            var cust = db.Customers
                .Select(m => new SelectListItem
                {
                    Value = m.customer_id.ToString(),
                    Text = m.customer_id.ToString()
                })
                .ToList();

            return (cust);
        }
        public ActionResult billCreate()
        {
            BillCreateVM billCreateVM = new BillCreateVM();
            billCreateVM.customerList = GetCustomerList();
            return View(billCreateVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult billCreate(BillCreateVM billCreateVM)
        {
            var bills = new Bill
            {
                customer_id = billCreateVM.customer_id,
                bill_total = billCreateVM.total,
                bill_date = billCreateVM.bill_date
            };
            db.Bills.Add(bills);
            db.SaveChanges();
            return RedirectToAction("billDetail");
        }
        public ActionResult billEdit(int? id)
        {
            Bill bill = db.Bills.Find(id);
            BillEditVM billEditVM = new BillEditVM();
            billEditVM.bill_id = bill.bill_id;
            billEditVM.customer_name = bill.Customer.customer_name;
            billEditVM.customer_surname = bill.Customer.customer_surname;
            billEditVM.bill_total = bill.bill_total;
            billEditVM.bill_date = bill.bill_date;
            return View(billEditVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult billEdit(BillEditVM billEditVM, int? bill_id)
        {
            Bill bill = db.Bills.Find(bill_id);
            bill.bill_date = billEditVM.bill_date;
            bill.bill_total = billEditVM.bill_total;
            db.Entry(bill).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("billDetail");

        }
        public ActionResult billDelete(int? id)
        {
            Bill bill = db.Bills.Find(id);
            db.Bills.Remove(bill);
            db.SaveChanges();
            return RedirectToAction("billDetail");
        }
    }
}