using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zach_Financial_Portal.Enum;

namespace Zach_Financial_Portal.Models
{
    public class BankAccounts
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public string OwnerId { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        //public string AccountType { get; set; }
        public float StartingBalance { get; set; }
        public float CurrentBalance { get; set; }
        public float LowLevelBalance { get; set; }

        public virtual Household Household { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public virtual AccountType AccountType { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public BankAccounts()
        {
            Transactions = new HashSet<Transaction>();
        }

    }
}