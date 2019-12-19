using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Zach_Financial_Portal.Models;

namespace Zach_Financial_Portal.Helpers
{
    public class HouseholdHelper
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private RoleHelper roleHelper = new RoleHelper();



        public List<ApplicationUser> ListUsersInHouseholdInRole(int householdId, string roleName)
        {
            var userIdList = new List<ApplicationUser>();
            foreach (var user in UsersInHousehold(householdId))
            {
                if (roleHelper.IsUserInRole(user.Id, roleName))
                    userIdList.Add(user);
            }
            return userIdList;
        }


        public bool IsUserInHousehold(string userId, int householdId)
        {
            var household = db.Households.Find(householdId);
            var flag = household.Users.Any(u => u.Id == userId);
            return (flag);
        }
        public ICollection<Household> ListUserHouseholds(string userId)
        {
            var households = db.Households.ToList();
            var householdList = new List<Household>();

            foreach (var house in households)
            {

                if (IsUserInHousehold(userId, house.Id))
                {

                    householdList.Add(house);
                    db.SaveChanges();
                }


            }



            return (householdList);
        }

        //public ICollection<Household> ListUserHouseholds(string userId)
        //{
        //    ApplicationUser user = db.Users.Find(userId);

        //    var projects = user.Household.ToList();
        //    return (projects);
        //}

        public void AddUserToHousehold(string userId, int householdId)
        {
            if (!IsUserInHousehold(userId, householdId))
            {
                Household house = db.Households.Find(householdId);
                var newUser = db.Users.Find(userId);

                house.Users.Add(newUser);
                db.SaveChanges();
            }
        }

        public void RemoveUserFromHousehold(string userId, int householdId)
        {
            if (IsUserInHousehold(userId, householdId))
            {
                Household house = db.Households.Find(householdId);
                var delUser = db.Users.Find(userId);

                house.Users.Remove(delUser);
                db.Entry(house).State = EntityState.Modified; // just saves this obj instance.
                db.SaveChanges();
            }
        }

        public ICollection<ApplicationUser> UsersInHousehold(int householdId)
        {
            return db.Households.Find(householdId).Users;
        }
        //public ICollection<ApplicationUser> UsersNotInHousehold(int householdId)
        //{
        //    return db.Users.Where(u => u.Household.All(p => p.Id != householdId)).ToList();
        //}
    }
}
