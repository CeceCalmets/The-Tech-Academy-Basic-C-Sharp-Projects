using System.Linq;
using System.Web.Mvc;
using YourNamespace.Models;  // Update with the correct namespace for your models
using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Define the path where the HTML file will be saved
        string filePath = @"C:\path\to\your\file.html";
        
        // HTML content you want to write to the file
        string htmlContent = @"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>My Sample HTML Page</title>
</head>
<body>

@model IEnumerable<YourNamespace.Models.Insuree>  

@{
    ViewBag.Title = "Admin - All Quotes";}
<h2>All Quotes Issued</h2>

<table class="table">
    <thead>
        <tr>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Email Address</th>
            <th>Quote</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var insuree in Model)
        {
            <tr>
                <td>@insuree.FirstName</td>
                <td>@insuree.LastName</td>
                <td>@insuree.Email</td>
                <td>@insuree.Quote</td>  <!-- Display the calculated quote -->
            </tr>
        }
    </tbody>
</table>
@model YourNamespace.Models.Insuree 

@{
    ViewBag.Title = "Create Insuree";
}

<h2>Create Insuree</h2>

@using (Html.BeginForm("Create", "Insuree", FormMethod.Post))
{
    @Html.AntiForgeryToken()

    <div class="form-horizontal">
        <h4>Insuree Details</h4>
        <hr />

        @Html.ValidationSummary(true)

        <div class="form-group">
            @Html.LabelFor(model => model.FirstName, htmlAttributes: new { @class = "control-label" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.FirstName)
                @Html.ValidationMessageFor(model => model.FirstName)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.LastName, htmlAttributes: new { @class = "control-label" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.LastName)
                @Html.ValidationMessageFor(model => model.LastName)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.Email, htmlAttributes: new { @class = "control-label" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Email)
                @Html.ValidationMessageFor(model => model.Email)
            </div>
        </div>

        <!-- Other input fields here -->

        <!-- The Quote field should NOT be visible -->
        @Html.HiddenFor(model => model.Quote)

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <button type="submit" class="btn btn-default">Create</button>
            </div>
        </div>
    </div>
}
</body>
</html>";

        // Write the HTML content to the file
        try
        {
            File.WriteAllText(filePath, htmlContent);
            Console.WriteLine("HTML file has been created successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error writing HTML file: " + ex.Message);
        }
    }
}

//c# content for assignment

namespace YourNamespace.Controllers
{
    public class InsureeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();  // Assuming ApplicationDbContext is your DbContext

        // GET: Insuree/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insuree/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Insuree insuree)
        {
            // Start with a base price of $50
            decimal quote = 50;

            // Add based on the age of the user
            if (insuree.Age <= 18)
            {
                quote += 100;  // User is 18 or under
            }
            else if (insuree.Age >= 19 && insuree.Age <= 25)
            {
                quote += 50;   // User is between 19 and 25
            }
            else if (insuree.Age >= 26)
            {
                quote += 25;   // User is 26 or older
            }

            // Add based on the car's year
            if (insuree.CarYear < 2000)
            {
                quote += 25;  // Car year is before 2000
            }
            else if (insuree.CarYear > 2015)
            {
                quote += 25;  // Car year is after 2015
            }

            // Add based on the car's make
            if (insuree.CarMake.ToLower() == "porsche")
            {
                quote += 25;  // Car make is Porsche

                // Add extra for the Porsche 911 Carrera
                if (insuree.CarModel.ToLower() == "911 carrera")
                {
                    quote += 25;  // Car model is 911 Carrera
                }
            }

            // Add based on speeding tickets
            quote += 10 * insuree.SpeedingTickets;  // Add $10 for each speeding ticket

            // Add based on DUI history
            if (insuree.HasDUI)
            {
                quote *= 1.25m;  // Add 25% for DUI
            }

            // Add based on full coverage
            if (insuree.FullCoverage)
            {
                quote *= 1.50m;  // Add 50% for full coverage
            }

            // Set the calculated quote
            insuree.Quote = quote;

            // Save the insuree object to the database (using Entity Framework)
            if (ModelState.IsValid)
            {
                db.Insurees.Add(insuree);  // Assuming 'db' is your DbContext
                db.SaveChanges();
                return RedirectToAction("Index");  // Redirect to the Index action or another appropriate action
            }

            return View(insuree);  // Return the view with the model if validation fails
        }

        // GET: Insuree/Admin
        [Authorize(Roles = "Admin")]  // Restrict access to Admin users only
        public ActionResult Admin()
        {
            // Retrieve all insuree records, including their quotes, and pass them to the view
            var insurees = db.Insurees.ToList();  // Fetch all Insuree records
            return View(insurees);  // Pass the list to the Admin view
        }
    }
}
