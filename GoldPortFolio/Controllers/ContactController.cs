using GoldPortFolio.Data;
using GoldPortFolio.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldPortFolio.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ContactController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(ContactPage model)
        {
            if (ModelState.IsValid)
            {
                _db.ContactPageTbl.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);

        }
    }
}
