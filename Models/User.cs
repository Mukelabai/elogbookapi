using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elogbookapi.Models
{
    public class User
    {
        private string webUsername, lastName, firstName, userNumber, sex,roleName;

        public string WebUsername { get => webUsername; set => webUsername = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string UserNumber { get => userNumber; set => userNumber = value; }
        public string Sex { get => sex; set => sex = value; }
        public string RoleName { get => roleName; set => roleName = value; }
        public int UserId { get; set; }
    }
}