using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zach_Financial_Portal.Enum;

namespace Zach_Financial_Portal.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int BankAccountsId { get; set; }
        public int BudgetItemId { get; set; }
        public string OwnerId { get; set; }
        public TransactionType TransactionTypeId { get; set; }
        public DateTime Created { get; set; }
        public string Amount { get; set; }
        public string Memo { get; set; }

        public virtual ApplicationUser Owner { get; set; }
        public virtual BankAccounts BankAccounts { get; set; }
        public virtual BudgetItem BudgetItem { get; set; }

    }
}