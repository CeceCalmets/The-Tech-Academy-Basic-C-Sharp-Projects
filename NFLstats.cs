using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace NFLStatsMVC
{
    // A simplified version where everything is in one file (not ideal for production)
    
    // Model: Player
    public class Player
    {
        public int PlayerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Team { get; set; }

        public virtual Stats Stats { get; set; }

        // Fantasy Points Calculation
        public decimal CalculateFantasyPoints()
        {
            if (Stats == null) return 0;
            decimal fantasyPoints = 0;
            fantasyPoints += Stats.PassingYards / 25;
            fantasyPoints += Stats.RushingYards / 10;
            fantasyPoints += Stats.ReceivingYards / 10;
            fantasyPoints += Stats.Touchdowns * 6;
            fantasyPoints -= Stats.Interceptions * 2;
            return fantasyPoints;
        }
    }

    // Model: Stats
    public class Stats
    {
        public int StatsId { get; set; }
        public int PlayerId { get; set; }

        public int PassingYards { get; set; }
        public int RushingYards { get; set; }
        public int ReceivingYards { get; set; }
        public int Touchdowns { get; set; }
        public int Interceptions { get; set; }

        public virtual Player Player { get; set; }
    }

    // Context (Database Connection)
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Stats> Stats { get; set; }
        public ApplicationDbContext() : base("name=DefaultConnection") { }
    }

    // Controller: PlayersController
    public class PlayersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Players
        public ActionResult Index()
        {
            var players = db.Players.Include("Stats").ToList();
            return View(players);
        }

        // GET: Players/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Include("Stats").FirstOrDefault(p => p.PlayerId == id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Position,Team")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(player);
        }

        // GET: Players/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Include("Stats").FirstOrDefault(p => p.PlayerId == id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlayerId,FirstName,LastName,Position,Team")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(player);
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Include("Stats").FirstOrDefault(p => p.PlayerId == id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Include("Stats").FirstOrDefault(p => p.PlayerId == id);
            db.Players.Remove(player);
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

    // Global.asax (Application Entry Point)
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    // Route Config (Routing Information)
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Players", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
