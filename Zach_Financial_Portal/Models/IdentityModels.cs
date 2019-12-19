using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Zach_Financial_Portal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        //[StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "First Name must have minimum of 1, and no grater then 20")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        //[StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "Lastname Name must have minimum of 1, and no grater then 20")]
        public string LastName { get; set; }

        [Display(Name = "Display Name")]
        //[StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "Display Name must have minimum of 1, and no grater then 20")]
        public string DisplayName { get; set; }

        public string AvatarPath { get; set; }
        public int? HouseholdId { get; set; }

        public virtual Household Household { get; set; }


        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        //Children

        public virtual ICollection<Transaction> Transaction { get; set; }
        public virtual ICollection<Budget> Budget { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<BankAccounts> BankAccounts { get; set; }

        public ApplicationUser()
        {
            Transaction = new HashSet<Transaction>();
            Budget = new HashSet<Budget>();
            Notification = new HashSet<Notification>();
            BankAccounts = new HashSet<BankAccounts>();
        }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("AzureConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Zach_Financial_Portal.Models.BankAccounts> BankAccounts { get; set; }

        public System.Data.Entity.DbSet<Zach_Financial_Portal.Models.Household> Households { get; set; }


        public System.Data.Entity.DbSet<Zach_Financial_Portal.Models.Budget> Budgets { get; set; }

        public System.Data.Entity.DbSet<Zach_Financial_Portal.Models.BudgetItem> BudgetItems { get; set; }

        public System.Data.Entity.DbSet<Zach_Financial_Portal.Models.Invitation> Invitations { get; set; }

        public System.Data.Entity.DbSet<Zach_Financial_Portal.Models.Notification> Notifications { get; set; }

        public System.Data.Entity.DbSet<Zach_Financial_Portal.Models.Transaction> Transactions { get; set; }
    }
}