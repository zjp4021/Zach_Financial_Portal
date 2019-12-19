using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zach_Financial_Portal.Models;

namespace Zach_Financial_Portal.Helpers
{
    public class InvitationHelper
    {
        public static async void MarkAsInvalid(int id)
        {
            var db = new ApplicationDbContext();
            var invitation = db.Invitations.Find(id);
            invitation.IsValid = false;
            await db.SaveChangesAsync();
        }
        
    }
}