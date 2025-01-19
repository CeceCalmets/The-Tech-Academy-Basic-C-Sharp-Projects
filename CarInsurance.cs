using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace CarInsuranceMVC
{
    // Model: Policy (represents user input for car insurance)
    public class Policy
    {
        public int PolicyId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [Range(18, 120, ErrorMessage = "Age must be between 18 and 120.")]
        public int Age { get; set; }

        [Required]
        public string CarMake { get; set; }

        [Required]
        [Range(1900, 2025, ErrorMessage = "Please enter a valid car year.")]
        public int CarYear { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool HasDUI { get; set; }
        public bool FullCoverage { get; set; }

        // Calculating the insurance quote based on business logic
        public decimal CalculateQuote()
        {
            decimal quote = 50;

            if (Age <= 18) quote += 100;
            else if (Age >= 19 && Age <= 25) quote += 50;
            else if (Age >= 26) quote += 25;

            if (CarYear < 2000) quote += 25;
            else if (CarYear > 2015) quote += 25;

            if (CarMake.ToLower() == "porsche") quote += 25;
            if (CarMake.ToLower() == "porsche" && CarYear == 911) quote += 25;

            if (HasDUI) quote *= 1.25m;
            if (FullCoverage) quote *= 1.50m;

            return quote;
        }
    }

    // Database Context (simplified for this example)
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Policy> Policies { get; set; }
        public ApplicationDbContext() : base("name=DefaultConnection") { }
    }

    // Controller: InsuranceController
    public class InsuranceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Insurance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insurance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Age,CarMake,CarYear,Email,HasDUI,FullCoverage")] Policy policy)
        {
            if (ModelState.IsValid)
            {
                policy.CalculateQuote();  // Calculate the quote based on the user input
                db.Policies.Add(policy);
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(policy);
        }

        // GET: Insurance/Admin
        public ActionResult Admin()
        {
            var policies = db.Policies.ToList();
            return View(policies);
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
                defaults: new { controller = "Insurance", action = "Create", id = UrlParameter.Optional }
            );
        }
    }

    // Views

    // Create View: Create.cshtml
    public class CreateView : WebViewPage
    {
        public override void Execute()
        {
            @* HTML form for creating a new insurance policy *@
            <h2>Create Insurance Policy</h2>
            @using (Html.BeginForm())
            {
                @Html.AntiForgeryToken()
                <div class="form-group">
                    @Html.LabelFor(model => model.FirstName)
                    @Html.EditorFor(model => model.FirstName)
                    @Html.ValidationMessageFor(model => model.FirstName)
                </div>

                <div class="form-group">
                    @Html.LabelFor(model => model.LastName)
                    @Html.EditorFor(model => model.LastName)
                    @Html.ValidationMessageFor(model => model.LastName)
                </div>

                <div class="form-group">
                    @Html.LabelFor(model => model.Age)
                    @Html.EditorFor(model => model.Age)
                    @Html.ValidationMessageFor(model => model.Age)
                </div>

                <div class="form-group">
                    @Html.LabelFor(model => model.CarMake)
                    @Html.EditorFor(model => model.CarMake)
                    @Html.ValidationMessageFor(model => model.CarMake)
                </div>

                <div class="form-group">
                    @Html.LabelFor(model => model.CarYear)
                    @Html.EditorFor(model => model.CarYear)
                    @Html.ValidationMessageFor(model => model.CarYear)
                </div>

                <div class="form-group">
                    @Html.LabelFor(model => model.Email)
                    @Html.EditorFor(model => model.Email)
                    @Html.ValidationMessageFor(model => model.Email)
                </div>

                <div class="form-group">
                    @Html.LabelFor(model => model.HasDUI)
                    @Html.EditorFor(model => model.HasDUI)
                </div>

                <div class="form-group">
                    @Html.LabelFor(model => model.FullCoverage)
                    @Html.EditorFor(model => model.FullCoverage)
                </div>

                <button type="submit" class="btn btn-default">Submit</button>
            }
        }
    }

    // Admin View: Admin.cshtml
    public class AdminView : WebViewPage<List<Policy>>
    {
        public override void Execute()
        {
            <h2>Issued Insurance Quotes</h2>
            <table class="table">
                <thead>
                    <tr>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Car Make</th>
                        <th>Car Year</th>
                        <th>Email</th>
                        <th>Quote</th>
                    </tr>
                </thead>
                <tbody>
                    @foreach (var policy in Model)
                    {
                        <tr>
                            <td>@policy.FirstName</td>
                            <td>@policy.LastName</td>
                            <td>@policy.CarMake</td>
                            <td>@policy.CarYear</td>
                            <td>@policy.Email</td>
                            <td>@policy.CalculateQuote()</td>
                        </tr>
                    }
                </tbody>
            </table>
        }
    }
}
