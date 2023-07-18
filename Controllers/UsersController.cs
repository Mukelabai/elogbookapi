using elogbook.Model;
using elogbookapi.Models;
using elogbookapi.Models.API;
using elogbookapi.Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace elogbookapi.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/<controller>
        [System.Web.Http.HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [System.Web.Http.HttpGet]
        public string Get(int id)
        {
            return "value";
        }
        // GET api/<controller>/5
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetUser(string username,string password)
        {
            if (Membership.ValidateUser(username,password))
            {
                //return user object
                SecurityController sc = new SecurityController();
                User user = sc.GetAPIUser(username);
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
            
        }
        // GET api/<controller>/?username=?&isStudent=?
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetUserData(string username, bool isStudent)
        {
            try
            {
                if (isStudent)
                {
                    return Ok(APIController.GetStudentData(username));
                }
                else
                {
                    //get staff data
                    return Ok(APIController.GetSupervisorData(username));
                }
            }catch(Exception ex)
            {
                return Ok(ex.Message);
            }

        }
        // GET api/<controller>/5
        [System.Web.Http.HttpGet]
        public IHttpActionResult ChangePassword(string username, string oldpassword,string password)
        {
            try
            {
                MembershipUser user = Membership.GetUser(username);
                if (user == null)
                {
                    return NotFound();
                }
                else if
                (!Membership.ValidateUser(username, oldpassword))
                {
                    return Ok("Invalid user");
                }
                else if (!user.ChangePassword(oldpassword, password))
                {
                    return Ok("Invalid password");
                }
                else
                {
                    return Ok();
                }
            }catch(Exception ex)
            {
                return Ok(ex.Message);
            }

        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}