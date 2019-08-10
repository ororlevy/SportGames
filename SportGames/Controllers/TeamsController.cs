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
    
    public class TeamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Admin")]
        // GET: Teams
        public ActionResult Index(String nameofcoach, String nameofteam, String country,String wins,String losses)
        {
            int num1 = 0;
            int num2 = 0;
            if (wins != null && !wins.Equals(""))
            {
                num1 = Convert.ToInt32(wins);
            }
            if (losses != null && !losses.Equals(""))
            {
                num2 = Convert.ToInt32(losses);
            }
            var team = db.Team.Where(p => (
            (p.Coach.Name.ToLower().Contains(nameofcoach.ToLower()) || nameofcoach == null || nameofcoach == "") &&
            (p.Name.ToLower().Contains(nameofteam.ToLower()) || nameofteam == null || nameofteam == "") &&
            (p.Country.ToLower().Contains(country.ToLower()) || country == null || country == "") &&
            (p.Wins == num1 || wins == null || wins == "") &&
            (p.Losses == num2 || losses == null || losses == "")
            )).Include(p => p.Coach);
            if (Request.IsAjaxRequest())
                return PartialView(team);


            return View(team.ToList());
        }
        [Authorize(Roles = "Admin")]
        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Team.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }
        [Authorize(Roles = "Admin")]
        // GET: Teams/Create
        public ActionResult Create()
        {
            ViewBag.CoachId = new SelectList(db.Coaches, "CoachId", "Name");
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamId,Name,Country,Wins,Losses,CoachId,ImgURL")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Team.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CoachId = new SelectList(db.Coaches, "CoachId", "Name", team.CoachId);
            return View(team);
        }
        [Authorize(Roles = "Admin")]
        // GET: Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Team.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            ViewBag.CoachId = new SelectList(db.Coaches, "CoachId", "Name", team.CoachId);
            return View(team);
        }
        [Authorize(Roles = "Admin")]
        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamId,Name,Country,Wins,Losses,CoachId,ImgURL")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CoachId = new SelectList(db.Coaches, "CoachId", "Name", team.CoachId);
            return View(team);
        }
        [Authorize(Roles = "Admin")]
        // GET: Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Team.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }
        [Authorize(Roles = "Admin")]
        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Team.Find(id);
            db.Team.Remove(team);
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
        
        public ActionResult TeamData(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var q = (from t in db.Team
                     join p in db.Players on t.TeamId equals p.TeamId
                     where t.TeamId == id 
                     select p).ToList();
            ViewBag.team = db.Team.Find(id);
            ViewBag.coach = db.Coaches.Find(db.Team.Find(id).CoachId);
            return View(q);
        }
        public ActionResult GroupBy()
        {
            var group = (from t in db.Team
                         group t by t.Country into g
                         select g).ToList();
            ViewBag.Group = group;
            return View(db.Team.ToList());
        }
    }
}
