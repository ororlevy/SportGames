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
    
    
    public class LeaguesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public static int laLigaConunter=0;
        public static int bonConunter = 0;
        public static int premConunter = 0;
        public static int ilsConunter = 0;
        public static int interConunter = 0;
        // GET: Leagues




        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        // GET: Leagues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Leagues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
        public ActionResult LeagueData(String id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            League league = db.Leagues.SingleOrDefault(l => l.NameOfLeague == id);
            if (league == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.LeagueName = league;
            switch (id)
            {
                case "La Liga Santander":laLigaConunter++;
                    ViewBag.Count = laLigaConunter;
                    break;
                case "Premier League":
                    premConunter++;
                    ViewBag.Count = premConunter;
                    break;
                case "Bundesliga":
                    bonConunter++;
                    ViewBag.Count = bonConunter;
                    break;
                case "Israeli Premier League":
                    ilsConunter++;
                    ViewBag.Count = ilsConunter;
                    break;
                 case "uefa champions league":
                    interConunter++;
                    ViewBag.Count = interConunter;
                    break;
            }
            var q = (from tl in db.TeamLeagues
                     join t in db.Team on tl.TeamId  equals t.TeamId
                     where tl.LeagueId == league.LeagueId
                     select t).ToList();
            
            var top = (from tl in db.TeamLeagues
                          join t in db.Team on tl.TeamId equals t.TeamId
                          where tl.LeagueId == league.LeagueId
                       select t).ToList();

            ViewBag.Top = (from i in top
                          orderby i.Wins descending
                          select i).Take(3).ToArray();
            ViewBag.Id = id;
            return View(q);
        }
    }
}
