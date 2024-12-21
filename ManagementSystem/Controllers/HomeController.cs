using ManagementSystem.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private ManagementSystemEntities entities = new ManagementSystemEntities();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                entities.Dispose();
            }
            base.Dispose(disposing);
        }

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
            if (ModelState.IsValid)
            {
                entities.Employees.Add(model);
                entities.SaveChanges();

                // Store a success message in TempData
                TempData["SuccessMessage"] = "Employee created successfully!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = entities.Employees.FirstOrDefault(x => x.EmployeeID == id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Employee model)
        {
            if (ModelState.IsValid)
            {
                var data = entities.Employees.FirstOrDefault(x => x.EmployeeID == model.EmployeeID);
                if (data == null)
                {
                    return HttpNotFound();
                }

                data.FirstName = model.FirstName;
                data.LastName = model.LastName;
                data.Email = model.Email;
                data.PhoneNumber = model.PhoneNumber;
                data.CreatedDate = model.CreatedDate;
                entities.SaveChanges();
                TempData["SuccessMessage"] = "Employee updated successfully!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var data = entities.Employees.FirstOrDefault(x => x.EmployeeID == id);
            if (data == null)
            {
                return HttpNotFound();
            }
            entities.Employees.Remove(data);
            entities.SaveChanges();
            TempData["SuccessMessage"] = "Employee deleted successfully!";
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            var data = entities.Employees.FirstOrDefault(x => x.EmployeeID == id.Value);
            if (data == null)
            {
                return HttpNotFound();
            }

            return View(data);
        }
    }
}
