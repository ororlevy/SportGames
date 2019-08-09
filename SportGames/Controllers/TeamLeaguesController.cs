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
    public class TeamLeaguesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TeamLeagues

        public ActionResult Index(String nameofleague, String nameofteam)
        {

            var teamleague = db.TeamLeagues.Where(p => (
            (p.League.NameOfLeague.ToLower().Contains(nameofleague.ToLower()) || nameofleague == null || nameofleague == "") &&
            (p.Team.Name.ToLower().Contains(nameofteam.ToLower()) || nameofteam == null || nameofteam == "")
            )).Include(t => t.League).Include(t => t.Team);
            if (Request.IsAjaxRequest())
                return PartialView(teamleague);


            return View(teamleague.ToList());
        }

        // GET: TeamLeagues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamLeague teamLeague = db.TeamLeagues.Find(id);
            if (teamLeague == null)
            {
                return HttpNotFound();
            }
            return View(teamLeague);
        }

        // GET: TeamLeagues/Create
        public ActionResult Create()
        {
            ViewBag.LeagueId = new SelectList(db.Leagues, "LeagueId", "NameOfLeague");
            ViewBag.TeamId = new SelectList(db.Team, "TeamId", "Name");
            return View();
        }

        // POST: TeamLeagues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamId,LeagueId")] TeamLeague teamLeague)
        {
            if (ModelState.IsValid)
            {
                db.TeamLeagues.Add(teamLeague);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LeagueId = new SelectList(db.Leagues, "LeagueId", "NameOfLeague", teamLeague.LeagueId);
            ViewBag.TeamId = new SelectList(db.Team, "TeamId", "Name", teamLeague.TeamId);
            return View(teamLeague);
        }

        // GET: TeamLeagues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamLeague teamLeague = db.TeamLeagues.Find(id);
            if (teamLeague == null)
            {
                return HttpNotFound();
            }
            ViewBag.LeagueId = new SelectList(db.Leagues, "LeagueId", "NameOfLeague", teamLeague.LeagueId);
            ViewBag.TeamId = new SelectList(db.Team, "TeamId", "Name", teamLeague.TeamId);
            return View(teamLeague);
        }

        // POST: TeamLeagues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamId,LeagueId")] TeamLeague teamLeague)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamLeague).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LeagueId = new SelectList(db.Leagues, "LeagueId", "NameOfLeague", teamLeague.LeagueId);
            ViewBag.TeamId = new SelectList(db.Team, "TeamId", "Name", teamLeague.TeamId);
            return View(teamLeague);
        }

        // GET: TeamLeagues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamLeague teamLeague = db.TeamLeagues.Find(id);
            if (teamLeague == null)
            {
                return HttpNotFound();
            }
            return View(teamLeague);
        }

        // POST: TeamLeagues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamLeague teamLeague = db.TeamLeagues.Find(id);
            db.TeamLeagues.Remove(teamLeague);
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
    }
}
