using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Zach_Financial_Portal.Models;
using Zach_Financial_Portal.Helpers;
using Zach_Financial_Portal.Extensions;
using System.Threading.Tasks;

namespace Zach_Financial_Portal.Controllers
{
    
    public class HouseholdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RoleHelper roleHelper = new RoleHelper();
        private NotificationHelper notificationHelper = new NotificationHelper();

        // GET: Households
        public ActionResult Index()
        {
            return View(db.Households.ToList());
        }

        // GET: Households/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // GET: Households/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Greeting,Created")] Household household)
        {
            if (ModelState.IsValid)
            {
                household.Created = DateTime.Now;
                db.Households.Add(household);
                db.SaveChanges();

                //Update User Record to indicate they are in a household

                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);
                user.Household = household;
                db.SaveChanges();

                //Update role to Head Of Household if Memeber Creates New Household
                roleHelper.RemoveUserFromRole(userId, "Guest");
                roleHelper.AddUserToRole(userId, "HeadOfHousehold");
                await ControllerContext.HttpContext.RefreshAuthentication(user);

                return RedirectToAction("Index");
            }

            return View(household);
        }

        //Leaving a Household//////////////////////////////////////////////////////////////////
        public async Task<ActionResult> LeaveAsync()
        {
            var userId = User.Identity.GetUserId();

            //Determine User's Role
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
            var user = db.Users.Find(userId);

            switch (myRole)
            {
                case "HeadOfHousehold":

                    var inhabitants = db.Users.Where(u => u.HouseholdId == user.HouseholdId).Count();
                    if(inhabitants > 1)
                    {
                        TempData["Message"] = $"You are unable to leave the Household at this time because there are still <b>{inhabitants}<b> members that occupy the household. Please appoint a new Head Of Household.";
                        return RedirectToAction("ExitDenied");
                    }

                    user.HouseholdId = null;
                    db.SaveChanges();

                    roleHelper.RemoveUserFromRole(userId, "HeadOfHousehold");
                    roleHelper.AddUserToRole(userId, "Guest");
                    await ControllerContext.HttpContext.RefreshAuthentication(user);

                    return RedirectToAction("Index", "home");

                case "Member":
                default:
                    user.HouseholdId = null;
                    db.SaveChanges();

                    roleHelper.RemoveUserFromRole(userId, "Member");
                    await ControllerContext.HttpContext.RefreshAuthentication(user);

                    return RedirectToAction("Index", "Home");
            }
        }

        //Exit Denied Get and Post Section/////////////////////////////////////////////////////////////////////
        public ActionResult ExitDenied()
        {
            return View();
        }

        public ActionResult AppointSuccessor()
        {
            var userId = User.Identity.GetUserId();
            var myHouseholdId = db.Users.Find(userId).HouseholdId ?? 0;

            if (myHouseholdId == 0)
                return RedirectToAction("Index");

            var members = db.Users.Where(u => u.HouseholdId == myHouseholdId && u.Id != userId);
            ViewBag.NewHoH = new SelectList(members, "Id", "FullName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AppointSuccessorAsync(string newHoH)
        {
            if (string.IsNullOrEmpty(newHoH))
                return RedirectToAction("Index", "Home");

            var me = db.Users.Find(User.Identity.GetUserId());
            me.HouseholdId = null;
            db.SaveChanges();

            roleHelper.RemoveUserFromRole(me.Id, "HeadOfHousehold");
            roleHelper.AddUserToRole(me.Id, "Guest");
            await ControllerContext.HttpContext.RefreshAuthentication(me);

            roleHelper.RemoveUserFromRole(newHoH, "Member");
            roleHelper.AddUserToRole(newHoH, "HeadOfHousehold");

            //Add a new notification record
            notificationHelper.SendNewRoleNotification(newHoH, "HeadOfHousehold");

            return RedirectToAction("Index", "Home");
        }

        // GET: Households/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Greeting,Created")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Entry(household).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(household);
        }

        // GET: Households/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Household household = db.Households.Find(id);
            db.Households.Remove(household);
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
