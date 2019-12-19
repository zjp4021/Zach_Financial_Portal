﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zach_Financial_Portal.Models;

namespace Zach_Financial_Portal.Helpers
{
    public class UserHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private bool IsAuthenticated = HttpContext.Current.Request.IsAuthenticated;
        public string GetAvatarPath()
        {
            if (IsAuthenticated)
            {
                var userId = HttpContext.Current.User.Identity.GetUserId();
                var user = db.Users.Find(userId);

                return user.AvatarPath;
            }
            return "/Avatars/default_user.png";

        }


        public string GetFirstName()
        {
            if (IsAuthenticated)
            {
                var userId = HttpContext.Current.User.Identity.GetUserId();
                var user = db.Users.Find(userId);

                return user.FirstName;
            }
            return "Guest";
        }

        public string GetLastName()
        {
            if (IsAuthenticated)
            {
                var userId = HttpContext.Current.User.Identity.GetUserId();
                var user = db.Users.Find(userId);

                return user.LastName;
            }
            return "User";
        }

        public string GetFullName()
        {
            if (IsAuthenticated)
            {
                var userId = HttpContext.Current.User.Identity.GetUserId();
                var user = db.Users.Find(userId);

                return user.FullName;
            }
            return "User";
        }

        public string GetFullName(string Id)
        {
            if (IsAuthenticated)
            {

                var user = db.Users.Find(Id);

                return user.FullName;
            }
            return "User";
        }

        public string GetDisplayName()
        {
            if (IsAuthenticated)
            {
                var userId = HttpContext.Current.User.Identity.GetUserId();
                var user = db.Users.Find(userId);

                return user.DisplayName;
            }
            return "User";

        }

        public string GetEmail()
        {
            if (IsAuthenticated)
            {
                var userId = HttpContext.Current.User.Identity.GetUserId();
                var user = db.Users.Find(userId);

                return user.Email;
            }
            return "User";

        }
    }
}