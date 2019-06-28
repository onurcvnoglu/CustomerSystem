using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomerSystem.Models;
using CustomerSystem.ViewModels;
using PagedList;

namespace CustomerSystem.Controllers
{
    public class CustomerDetailsController : Controller
    {
        private customerSystemDbEntities db = new customerSystemDbEntities();
        
        // GET: CustomerDetails
        public ActionResult customerDetail(int page=1,int pageSize=10)  //sayfalama işlemi page = başlangıç , pagesize 1 sayfadaki satır sayısı
        {
            List<CustomerVM> customerVMList = new List<CustomerVM>();  //bilgileri tutmak için bir liste oluşturduk
            var customerlist = (from Cust in db.Customers
                                join sch in db.Schedules on Cust.schedule_id equals sch.schedule_id orderby Cust.customer_id
                                select new { Cust.customer_id, Cust.customer_name, Cust.customer_surname, Cust.customer_phone, Cust.customer_address, sch.schedule_name, sch.schedule_speed });   //bizim için gerekli verileri aldık

            foreach (var item in customerlist)
            {
                CustomerVM customerVM = new CustomerVM();

                customerVM.customer_id = item.customer_id;
                customerVM.customer_name = item.customer_name;
                customerVM.customer_surname = item.customer_surname;
                customerVM.customer_phone = item.customer_phone;
                customerVM.customer_address = item.customer_address;
                customerVM.schedule_name = item.schedule_name;
                customerVM.schedule_speed = item.schedule_speed;

                customerVMList.Add(customerVM);
            }
            PagedList<CustomerVM> customerInfo = new PagedList<CustomerVM>(customerVMList, page, pageSize); //customerInfo değişkenine sayfalayıp attık. 
            return View(customerInfo);
        }
        [HttpPost]
        public ActionResult customerDetail(string txtArama, int page = 1, int pageSize = 10)    //Arama Butonu için
        {
            int txtId = Convert.ToInt32(txtArama);
            List<CustomerVM> customerVMList = new List<CustomerVM>();
            var customerSearch = db.Customers.Find(txtId);
            if (customerSearch==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerVM customerVM = new CustomerVM();

            customerVM.customer_id = customerSearch.customer_id;
            customerVM.customer_name = customerSearch.customer_name;
            customerVM.customer_surname = customerSearch.customer_surname;
            customerVM.customer_phone = customerSearch.customer_phone;
            customerVM.customer_address = customerSearch.customer_address;
            customerVM.schedule_name = customerSearch.Schedule.schedule_name;
            customerVM.schedule_speed = customerSearch.Schedule.schedule_speed;

            customerVMList.Add(customerVM);
            PagedList<CustomerVM> customerInfo = new PagedList<CustomerVM>(customerVMList, page, pageSize); //customerInfo değişkenine sayfalayıp attık. 
            return View(customerInfo);
        }
        private IEnumerable<SelectListItem> GetScheduleList()   // listeye aldık 
        {
            var sch = db.Schedules
                .Select(m => new SelectListItem
                {
                    Value = m.schedule_id.ToString(),
                    Text = m.schedule_name
                })
                .ToList();

            return (sch);
        }
        private IEnumerable<SelectListItem> GetCountryCode()   // listeye aldık 
        {
            var cList = db.CountriesCodes
                .Select(m => new SelectListItem
                {
                    Value = m.country_code.ToString(),
                    Text = m.country_name
                })
                .ToList();

            return (cList);
        }
        public ActionResult customerCreate()
        {
            CustomerCreateVM customerCreateVM = new CustomerCreateVM();
            customerCreateVM.scheduleList= GetScheduleList();
            customerCreateVM.countryList = GetCountryCode();

            //ViewBag.schedule_name = new SelectList(db.Schedules, "schedule_id", "schedule_name");
            return View(customerCreateVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult customerCreate(CustomerCreateVM customerCreateVM )
        {
            var model = new Customer
            {
                customer_name = customerCreateVM.customer_name,
                customer_surname = customerCreateVM.customer_surname,
                customer_address = customerCreateVM.customer_address,
                customer_phone = customerCreateVM.customer_phone+Request.Form["codeNum"]+Request.Form["thirdNum"]+Request.Form["fourthNum"],
                schedule_id = customerCreateVM.schedule_id
            };
            db.Customers.Add(model);
            db.SaveChanges();
            return RedirectToAction("customerDetail");
        }
        
        public ActionResult customerEdit(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            CustomerEditVM customerEditVM = new CustomerEditVM();
            customerEditVM.customer_id = customer.customer_id;
            customerEditVM.customer_name = customer.customer_name;
            customerEditVM.customer_surname = customer.customer_surname;
            customerEditVM.customer_phone = customer.customer_phone;
            customerEditVM.customer_address = customer.customer_address;
            customerEditVM.ScheduleList = GetScheduleList().ToList();
            return View(customerEditVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult customerEdit(CustomerEditVM customerEditVM, int? customer_id)
        {
            Customer customer = db.Customers.Find(customer_id);

            customer.customer_name = customerEditVM.customer_name;
            customer.customer_surname = customerEditVM.customer_surname;
            customer.customer_address = customerEditVM.customer_address;
            customer.customer_phone = customerEditVM.customer_phone;
            customer.schedule_id = customerEditVM.schedule_id;
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("customerDetail");
        }
        public ActionResult customerDelete(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Customer customer = db.Customers.Find(id);
                db.Customers.Remove(customer);
                db.SaveChanges();

                return RedirectToAction("customerDetail");
            }
            
        }
    }
}