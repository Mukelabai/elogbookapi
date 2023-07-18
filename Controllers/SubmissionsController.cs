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
    public class SubmissionsController : ApiController
    {
        // GET: api/Submissions
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Submissions/5
        public string Get(int id)
        {
            return "value";
        }
        //GET: api/Submissions/SubmissionIdT=?
        public IHttpActionResult GetSubmissionQuestions(string SubmissionIdT)
        {
            try
            {
                APISubmissionData data = APIController.GetSubmissionData(SubmissionIdT);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        public string RequestMessage { get; set; }
        private async void SetMessage(HttpRequestMessage message)
        {
            RequestMessage = await message.Content.ReadAsStringAsync();
        }
        // POST: api/Submissions
        public IHttpActionResult Post(HttpRequestMessage message)
        {
            List<APIError> errorMessages = new List<APIError>();
            try
            {
                //errorMessages.Add(new APIError { MessageType = "I", ErrorMessage = "Got request from URIL"+message.RequestUri.AbsoluteUri, DetailedMessage = "Method used is "+message.Method.Method });
                SetMessage(message);
                //errorMessages.Add(new APIError { MessageType = "I", ErrorMessage = "Starting to serialize object", DetailedMessage = RequestMessage });
                var data = JsonConvert.DeserializeObject<APISubmissionData>(RequestMessage);
                //errorMessages.Add(new APIError { MessageType = "I", ErrorMessage = "Done serializing", DetailedMessage = "" });
                //add or update the Submission
                SubmissionController sc = new SubmissionController();
                //this also deletes all existing cases and responses
                long submissionId = sc.APIAddSubmission(data.Submission);
                //delete submission comments
                sc.APIDeleteSubmissionCommentsForUser(submissionId, data.Submission.CreatedBy);
               // errorMessages.Add(new APIError {MessageType="I", ErrorMessage = "Added submission Id "+submissionId, DetailedMessage = "" });
                foreach (APICase c in data.Cases)
                {
                    try
                    {
                       // errorMessages.Add(new APIError { MessageType = "I", ErrorMessage = "About to add case " + c.Patient, DetailedMessage = "" });
                        c.SubmissionId = submissionId;
                        c.CaseId = sc.APIAddCase(c);
                       // errorMessages.Add(new APIError { MessageType = "I", ErrorMessage = "Added case " + c.Patient, DetailedMessage = "" });
                        //get responses for case
                       // errorMessages.Add(new APIError { MessageType = "I", ErrorMessage = "About to add responses for case " + c.Patient, DetailedMessage = "" });
                        List<APIQuestionResponse> caseResponses = data.Responses.Where(r => r.CaseUID == c.CaseUID).ToList();
                        foreach (APIQuestionResponse r in caseResponses)
                        {
                          //  errorMessages.Add(new APIError { MessageType = "I", ErrorMessage = string.Format("Case {0}, ParentQuestionId={1}, ChildQuestionId={2}: About to add response: {3} " , c.Patient,r.ParentQuestionId,r.ChildQuestionId, r.ResponseText), DetailedMessage = "" });
                            try
                            {
                                sc.APIAddResponse(c.CaseId, submissionId,r.QuestionId,r.ResponseText, c.UpdatedBy, c.CreatedOn, c.UpdatedOn);
                           //     errorMessages.Add(new APIError { MessageType = "I", ErrorMessage = string.Format("Case {0}, ParentQuestionId={1}, ChildQuestionId={2}: Added response: {3} ", c.Patient, r.ParentQuestionId, r.ChildQuestionId, r.ResponseText), DetailedMessage = "" });

                            }
                            catch (Exception ex)
                            {
                            //    errorMessages.Add(new APIError { MessageType = "E", ErrorMessage = ex.Message, DetailedMessage = ex.StackTrace });
                            }
                        }
                        errorMessages.Add(new APIError { MessageType = "I", ErrorMessage = "Added case " + c.Patient, DetailedMessage = "" });
                    }
                    catch(Exception ex)
                    {
                        errorMessages.Add(new APIError { MessageType = "E", ErrorMessage = string.Format("Failed to add case {0}, error: {1} ",c.Patient,ex.Message), DetailedMessage = ex.StackTrace });
                    }

                }
                //now save comments
                foreach(APISubmissionComment c in data.Comments)
                {
                    try
                    {
                        c.SubmissionId = submissionId;
                        sc.APIAddSubmissionComment(c);
                    }catch(Exception ex)
                    {

                    }
                }
                //save assignment general questions
                foreach (APIAssignmentQuestionResponse a in data.AssignmentResponses)
                {
                    try
                    {
                        a.SubmissionId = submissionId;
                        sc.APIAddResponse(-1, submissionId, a.QuestionId, a.ResponseText, data.Submission.UpdatedBy, data.Submission.CreatedOn, data.Submission.UpdatedOn);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                //save achievements
                foreach(ElogbookAchievement ea in data.ElogbookAchievements)
                {
                    ea.SubmissionId = submissionId;
                    sc.APIAddSubmissionAchievement(ea);
                }

                return Ok(errorMessages);
                
            }
            catch(Exception ex)
            {
                errorMessages.Add(new APIError { MessageType="E", ErrorMessage = ex.Message, DetailedMessage = ex.StackTrace });
                return Ok(errorMessages);
            }
            
        }

        // PUT: api/Submissions/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Submissions/5
        public void Delete(int id)
        {
        }
    }
}
