using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zach_Financial_Portal.Models
{
    public class EditProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }
    }
}