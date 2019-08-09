using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportGames.Models;

namespace SportGames.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LeaguesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Leagues
        public ActionResult Index(String name, String country)
        {

            var leagues = db.Leagues.Where(p => (
            (p.NameOfLeague.ToLower().Contains(name.ToLower()) || name == null || name == "") &&
            (p.Country.ToLower().Contains(country.ToLower()) || country == null || country == "")));
            if (Request.IsAjaxRequest())
                return PartialView(leagues);


            return View(leagues.ToList());
        }

        // GET: Leagues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            League league = db.Leagues.Find(id);
            if (league == null)
            {
                return HttpNotFound();
            }
            return View(league);
        }

        // GET: Leagues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Leagues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LeagueId,NameOfLeague,Country,ImgURL,Description")] League league)
        {
            if (ModelState.IsValid)
            {
                db.Leagues.Add(league);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(league);
        }

        // GET: Leagues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            League league = db.Leagues.Find(id);
            if (league == null)
            {
                return HttpNotFound();
            }
            return View(league);
        }

        // POST: Leagues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LeagueId,NameOfLeague,Country,ImgURL,Description")] League league)
        {
            if (ModelState.IsValid)
            {
                db.Entry(league).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(league);
        }

        // GET: Leagues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            League league = db.Leagues.Find(id);
            if (league == null)
            {
                return HttpNotFound();
            }
            return View(league);
        }

        // POST: Leagues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            League league = db.Leagues.Find(id);
            db.Leagues.Remove(league);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult LeagueData(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var q = (from tl in db.TeamLeagues
                     join t in db.Team on tl.TeamId  equals t.TeamId
                     where tl.LeagueId == id
                     select t).ToList();
            ViewBag.LeagueName = db.Leagues.Find(id);

            return View(q);
        }
    }
}
