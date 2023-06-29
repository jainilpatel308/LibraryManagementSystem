using Lib2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lib2.Controllers
{
    public class IssueBookController : Controller
    {
        // GET: IssueBook
        private lib1Entities1 db = new lib1Entities1();
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetBook()
        {
            var books = db.books.Select(p => new
            {
                ID=p.id,
                Bname=p.bname
            }).ToList();
            return Json(books, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Save(issuebook issue)
        {
            if (ModelState.IsValid)
            {

                db.issuebooks.Add(issue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issue);
        }

        [HttpPost]
        public ActionResult GetMid(int m_id)
        {
            var memberid = (from s in db.members where s.id == m_id select s.name).ToList();
            return Json(memberid, JsonRequestBehavior.AllowGet);
        }
        
    }
}