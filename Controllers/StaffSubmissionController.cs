using elogbookapi.Models.API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace elogbookapi.Controllers
{
    public class StaffSubmissionController : ApiController
    {
        // GET: api/StaffSubmission
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/StaffSubmission/5
        public IHttpActionResult Get(int assignmentId)
        {
            try
            {
                APIAssignmentData data = APIController.GetAssignmentData(assignmentId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        public IHttpActionResult GetSubmissionCases(string submissionIds)
        {
            try
            {
                APICaseData data = APIController.GetCaseDataForSubmissions(submissionIds);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        // POST: api/StaffSubmission
        public string RequestMessage { get; set; }
        private async void SetMessage(HttpRequestMessage message)
        {
            RequestMessage = await message.Content.ReadAsStringAsync();
        }
        // POST: api/StaffSubmission
        public IHttpActionResult Post(HttpRequestMessage message)
        {
            List<APIError> errorMessages = new List<APIError>();
            try
            {
                //errorMessages.Add(new APIError { MessageType = "I", ErrorMessage = "Got request from URIL"+message.RequestUri.AbsoluteUri, DetailedMessage = "Method used is "+message.Method.Method });
                SetMessage(message);
                //errorMessages.Add(new APIError { MessageType = "I", ErrorMessage = "Starting to serialize object", DetailedMessage = RequestMessage });
                var data = JsonConvert.DeserializeObject<APISubmissionCommentData>(RequestMessage);
                SubmissionController sc = new SubmissionController();
                //save submission grades
                int countSuccessSubmissions = 0;
                int countSuccessComments = 0;
                int countSuccessResponses = 0;
                foreach (APIStaffSubmission s in data.Submissions)
                {
                    //add grade
                    try
                    {
                        sc.APIAddSubmissionGrade(s);
                        countSuccessSubmissions++;

                        //delete comments for user and submission
                        sc.APIDeleteSubmissionCommentsForUser(s.SubmissionId, data.WebUsername);


                        //Delete responses for each submission

                        //now save reponses for submissions
                        var sResponses = data.Responses.Where(r => r.SubmissionId == s.SubmissionId).ToList();
                        foreach (APIResponses r in sResponses)
                        {
                            //  errorMessages.Add(new APIError { MessageType = "I", ErrorMessage = string.Format("Case {0}, ParentQuestionId={1}, ChildQuestionId={2}: About to add response: {3} " , c.Patient,r.ParentQuestionId,r.ChildQuestionId, r.ResponseText), DetailedMessage = "" });
                            try
                            {
                                sc.APIAddResponse(r.CaseId, s.SubmissionId, r.QuestionId, r.ResponseText, data.WebUsername, DateTime.Now, DateTime.Now);
                                countSuccessResponses++;
                                //     errorMessages.Add(new APIError { MessageType = "I", ErrorMessage = string.Format("Case {0}, ParentQuestionId={1}, ChildQuestionId={2}: Added response: {3} ", c.Patient, r.ParentQuestionId, r.ChildQuestionId, r.ResponseText), DetailedMessage = "" });

                            }
                            catch (Exception ex)
                            {
                                    errorMessages.Add(new APIError { MessageType = "E", ErrorMessage = ex.Message, DetailedMessage = ex.StackTrace });
                            }
                        }
                        //save responses to genral questions

                        


                    }
                    catch(Exception ex)
                    {
                       
                        errorMessages.Add(new APIError { MessageType = "E", ErrorMessage = string.Format("Failed to add submission grade for {0}, error: {1} ", s.Student, ex.Message), DetailedMessage = ex.StackTrace });
                    }
                }

                //first remove all previous comments made by user

                //now save comments
                foreach (APISubmissionComment c in data.Comments)
                {
                    //add grade
                    try
                    {
                        sc.APIAddSubmissionComment(c);
                        countSuccessComments++;
                    }
                    catch (Exception ex)
                    {
                        
                        errorMessages.Add(new APIError { MessageType = "E", ErrorMessage = string.Format("Failed to add comment  {0}, error: {1} ", c.CommentText, ex.Message), DetailedMessage = ex.StackTrace });
                    }
                }
                
                errorMessages.Add(new APIError { MessageType = "I", ErrorMessage = string.Format("Uploaded {0} submissions, {1} responses, and {2} comments",countSuccessSubmissions,countSuccessResponses, countSuccessComments), DetailedMessage = "" });
               
         
                return Ok(errorMessages);

            }
            catch (Exception ex)
            {
                errorMessages.Add(new APIError { MessageType = "E", ErrorMessage = ex.Message, DetailedMessage = ex.StackTrace });
                return Ok(errorMessages);
            }

        }

        // PUT: api/StaffSubmission/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/StaffSubmission/5
        public void Delete(int id)
        {
        }
    }
}
