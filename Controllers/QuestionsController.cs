using elogbookapi.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace elogbookapi.Controllers
{
    public class QuestionsController : ApiController
    {
        // GET: api/Questions
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Questions/5
        public string Get(int id)
        {
            return "value";
        }

        //GET: api/Questions/SubmissionIdT=?
        public IHttpActionResult GetSubmissionQuestions(string SubmissionIdT)
        {
            try
            {
                List<APIQuestion> questions = APIController.GetQuestions(SubmissionIdT);
                return Ok(questions);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        // POST: api/Questions
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Questions/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Questions/5
        public void Delete(int id)
        {
        }
    }
}
