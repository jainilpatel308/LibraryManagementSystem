using Lib2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lib2.Controllers
{
    public class ReturnBookController : Controller
    {
        // GET: ReturnBook\
        private lib1Entities1 db = new lib1Entities1();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetMid(int mid)
        {
            var memberid = (from s in db.issuebooks where s.m_id == mid
                            select new
                            {
                                IssueDate = s.issuedate,
                                ReturnDate = s.returndate,
                                MemberId = s.m_id,
                                Bookname = s.book_id,
                                ElaspedDays = SqlFunctions.DateDiff("day", s.returndate, DateTime.Now)
                            }).ToArray();
                           
                           
            return Json(memberid,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Save(returnbook issue)
        {
            if (ModelState.IsValid)
            {

                db.returnbooks.Add(issue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issue);
        }
    }
}