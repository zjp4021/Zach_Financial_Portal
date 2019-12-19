using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zach_Financial_Portal.Models;

namespace Zach_Financial_Portal.Helpers
{
    public class NotificationHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public void SendNewRoleNotification(string newHoH, string role)
        {
            var newUser = db.Users.Find(newHoH);
            var notification = new Notification
            {   
                HouseholdId = (int)newUser.HouseholdId,
                Created = DateTime.Now,
                IsRead = false,
                RecipientId = newHoH,
                Body = $"You have been appointed as the successor of {newUser.Household.Name} with the new role of {role}"
            };
            db.Notifications.Add(notification);
            db.SaveChanges();

        }
        public static List<Notification> GetUnreadNotifications()
        {
            var currentUserId = HttpContext.Current.User.Identity.GetUserId();
            return db.Notifications.Include("Recipient").Where(t => t.RecipientId == currentUserId && !t.IsRead).ToList();
        }
    }
}