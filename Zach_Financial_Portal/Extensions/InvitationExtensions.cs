using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Zach_Financial_Portal.Models;

namespace Zach_Financial_Portal.Extensions
{
    public static class InvitationExtensions
    {
        public static async Task EmailInvitation(this Invitation invitation)
        {
            var Url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var callbackUrl = Url.Action("AcceptInvitation", "Account", new { recipientEmail = invitation.RecipientEmail, code = invitation.Code }, protocol: HttpContext.Current.Request.Url.Scheme);
            var from = $"Money Manager{WebConfigurationManager.AppSettings["emailfrom"]}";
            
            var emailMessage = new MailMessage(from, invitation.RecipientEmail)
            {
                Subject = $"You have been invited to join the Money Manager Application!",
                Body = $"Please accept this invitation and register as a new household member <a href=\"{callbackUrl}\">here</a>",
                IsBodyHtml = true
            };
            
            var svc = new PersonalEmail();
            await svc.SendAsync(emailMessage);
        }
        public static async Task MarkAsInvalid(this Invitation invitation)
        {
            var db = new ApplicationDbContext();
            db.Invitations.Attach(invitation);
            invitation.IsValid = false;
            await db.SaveChangesAsync();
        }
        public static async Task RefreshAuthentication(this HttpContextBase context, ApplicationUser user)
        {
            context.GetOwinContext().Authentication.SignOut();
            await context.GetOwinContext().Get<ApplicationSignInManager>().SignInAsync(user, isPersistent: false, rememberBrowser: false);
        }

    }
}