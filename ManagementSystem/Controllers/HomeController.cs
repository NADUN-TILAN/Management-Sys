using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        ManagementSystemEntities entities = new ManagementSystemEntities();

        public ActionResult Index()
        {
            var listData = entities.Employees.ToList();
            return View(listData);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee model)
        {
            entities.Employees.Add(model);
            entities.SaveChanges();
            ViewBag.Message = "Data Insert Successfully";
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = entities.Employees.Where(x => x.EmployeeID == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Employee model)
        {
            var data = entities.Employees.Where(x => x.EmployeeID == model.EmployeeID).FirstOrDefault();
            if (data != null)
            {
                data.FirstName = model.FirstName;
                data.LastName = model.LastName;
                data.Email = model.Email;
                data.PhoneNumber = model.PhoneNumber;
                data.CreatedDate = model.CreatedDate;
            }
            return RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {
            var data = entities.Employees.Where(x => x.EmployeeID == id).FirstOrDefault();
            entities.Employees.Remove(data);
            entities.SaveChanges();
            ViewBag.Message = "Record Delete Successfully";
            return RedirectToAction("index");
        }


    }
}