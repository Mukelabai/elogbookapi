using elogbook.Model;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace elogbookapi.Models.API
{
    static class APIController
    {
        static string connectionString;
        static APIController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
        }

        //get student data
        public static UserData GetStudentData(string username)
        {
            UserData data = new UserData();
            //get assignments
            data.WebUsername = username;
            data.Assignments = GetStudentAssignments(username);
            data.Mentors = GetMentors();
            data.Hospitals = GetHospitals();
            data.Submissions = GetStudentSubmissions(username);
            //for the student, get all questions for all his assignemnts
            List<APIQuestion> questions = new List<APIQuestion>();
            List<APIElogbookCompetence> competences = new List<APIElogbookCompetence>();
            foreach(APIAssignment a in data.Assignments)
            {
                questions.AddRange(GetQuestionsByAssignmentId(a.AssignmentId));
                competences.AddRange(GetCompetenciesByAssignmentId(a.AssignmentId));
            }
            data.Questions = questions;
            data.Competences = competences;
            
            //data.Cases = GetCases(username);
            return data;

        }

        //get supervisor data
        public static UserData GetSupervisorData(string username)
        {
            UserData data = new UserData();
            data.WebUsername = username;
            //get assignments
            data.Assignments = GetSupervisorAssignments(username);
            data.Mentors = GetMentors();
            data.Hospitals = GetHospitals();
            data.Grades = GetGrades();

            return data;

        }

        //get submissiondata
        public static APISubmissionData GetSubmissionData(string submissionIdT)
        {
            APISubmissionData data = new APISubmissionData();
            //get assignments
            data.SubmissionIdT = Utility.ParseGuid(submissionIdT);
            data.Cases = GetSubmissionCases(submissionIdT);
            //data.Questions = GetQuestions(submissionIdT);
            data.Responses = GetQuestionResponses(submissionIdT);
            data.AssignmentResponses = GetQuestionResponsesForAssignmentLevel(submissionIdT);
            data.Comments = GetCommentsForSubmissionIdT(submissionIdT);
            data.ElogbookAchievements = GetAchievementsForSubmission(submissionIdT);
            

            return data;

        }
        public static APIAssignmentData GetAssignmentData(int assignmentId)
        {
            APIAssignmentData data = new APIAssignmentData();
            data.Submissions = GetStaffSubmissions(assignmentId);
            data.Questions = GetQuestionsByAssignmentId(assignmentId);
            data.Competences = GetCompetenciesByAssignmentId(assignmentId);
            return data;
        }
        public static APICaseData GetCaseDataForSubmissions(string submissionIds)
        {
            APICaseData data = new APICaseData();
            data.Cases = GetCasesForSubmissionIds(submissionIds);
            data.Responses = GetResponsesForSubmissions(submissionIds);
            data.AssignmentResponses = GetQuestionResponsesForAssignmentLevelBySubmisionIds(submissionIds);
            data.Comments = GetCommentsForSubmissionIds(submissionIds);
            data.Achievements = GetAchievementsForSubmissionIds(submissionIds);
            return data;
        }
        public static List<APIAssignment> GetStudentAssignments(string username)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIElogbookAssignmentLoadForStudent", username))
                {
                    List<APIAssignment> records = new List<APIAssignment>();
                    while (reader.Read())
                    {
                        records.Add(new APIAssignment
                        {
                            AssignmentId = Utility.GetIntFromReader(reader, "AssignmentId"),
                            AssignmentName = Utility.GetStringFromReader(reader, "Rotation"),
                            DueDate = Utility.GetDateFromReader(reader, "DueDate")
                        });

                    }

                    return records;
                }
            }
        }

        public static List<APIAssignment> GetSupervisorAssignments(string username)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIElogbookAssignmentLoadForSupervisor", username))
                {
                    List<APIAssignment> records = new List<APIAssignment>();
                    while (reader.Read())
                    {
                        records.Add(new APIAssignment
                        {
                            AssignmentId = Utility.GetIntFromReader(reader, "AssignmentId"),
                            AssignmentName = Utility.GetStringFromReader(reader, "AssignmentName"),
                            AcademicYear = Utility.GetIntFromReader(reader, "AcademicYear"),
                            DueDate = Utility.GetDateFromReader(reader, "DueDate"),
                            Rotation = Utility.GetStringFromReader(reader, "Rotation"),
                            RotationStart = Utility.GetDateFromReader(reader, "RotationStart"),
                            RotationEnd = Utility.GetDateFromReader(reader, "RotationEnd"),
                            ElogbookId = Utility.GetIntFromReader(reader, "ElogbookId"),
                            ElogbookName = Utility.GetStringFromReader(reader, "ElogbookName"),
                        });

                    }

                    return records;
                }
            }
        }
        public static List<APIMentor> GetMentors()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIMentorsLoadAll"))
                {
                    List<APIMentor> records = new List<APIMentor>();
                    while (reader.Read())
                    {
                        records.Add(new APIMentor
                        {
                            MentorId = Utility.GetIntFromReader(reader, "MentorId"),
                            MentorName = Utility.GetStringFromReader(reader, "MentorName")

                        });

                    }

                    return records;
                }
            }
        }
        public static List<APIHospital> GetHospitals()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIHospitalLoadDDL"))
                {
                    List<APIHospital> records = new List<APIHospital>();
                    while (reader.Read())
                    {
                        records.Add(new APIHospital
                        {
                            HospitalId = Utility.GetIntFromReader(reader, "HospitalId"),
                            HospitalName = Utility.GetStringFromReader(reader, "HospitalName")

                        });

                    }

                    return records;
                }
            }
        }

        //submisisons
        public static List<APISubmission> GetStudentSubmissions(string username)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIElogbookSubmissionsLoadForStudentView", username))
                {
                    List<APISubmission> records = new List<APISubmission>();
                    while (reader.Read())
                    {
                        records.Add(new APISubmission
                        {
                            SubmissionId = Utility.GetIntFromReader(reader, "SubmissionId"),
                            SubmissionIdT = Utility.GetGuidFromReader(reader, "SubmissionIdT"),
                            StudentId = Utility.GetIntFromReader(reader, "StudentId"),
                            MentorId = Utility.GetIntFromReader(reader, "MentorId"),
                            HospitalId = Utility.GetIntFromReader(reader, "HospitalId"),
                            AssignmentId = Utility.GetIntFromReader(reader, "AssignmentId"),
                            CreatedBy = Utility.GetStringFromReader(reader, "CreatedBy"),
                            CreatedOn = Utility.GetDateFromReader(reader, "CreatedOn"),
                            UpdatedBy = Utility.GetStringFromReader(reader, "UpdatedBy"),
                            UpdatedOn = Utility.GetDateFromReader(reader, "UpdatedOn"),
                            Rotation = Utility.GetStringFromReader(reader, "Rotation"),
                            Hospital = Utility.GetStringFromReader(reader, "Hospital"),
                            Mentor = Utility.GetStringFromReader(reader, "Mentor"),
                            SubmissionName = Utility.GetStringFromReader(reader, "SubmissionName"),
                            Cases = Utility.GetIntFromReader(reader, "Cases"),
                            Status = Utility.GetStringFromReader(reader, "Status"),
                            Grade = Utility.GetStringFromReader(reader, "Grade"),
                            IsPublished = Utility.GetBoolFromReader(reader, "IsPublished"),
                        });

                    }

                    return records;
                }
            }
        }
        //Cases
        public static List<APICase> GetCases(string username)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APICasesLoad", username))
                {
                    List<APICase> records = new List<APICase>();
                    while (reader.Read())
                    {
                        records.Add(new APICase
                        {

                            CaseUID = Utility.GetGuidFromReader(reader, "CaseUID"),
                            Patient = Utility.GetStringFromReader(reader, "Patient"),
                            SubmissionId = Utility.GetIntFromReader(reader, "SubmissionId"),
                            SubmissionIdT = Utility.GetGuidFromReader(reader, "SubmissionIdT"),
                            StudentId = Utility.GetIntFromReader(reader, "StudentId"),
                            CreatedBy = Utility.GetStringFromReader(reader, "CreatedBy"),
                            CreatedOn = Utility.GetDateFromReader(reader, "CreatedOn"),
                            UpdatedBy = Utility.GetStringFromReader(reader, "UpdatedBy"),
                            UpdatedOn = Utility.GetDateFromReader(reader, "UpdatedOn")

                        });

                    }

                    return records;
                }
            }
        }
        public static List<APICase> GetSubmissionCases(string submissionIdT)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APICasesLoadForSubmission", Utility.ParseGuid(submissionIdT)))
                {
                    List<APICase> records = new List<APICase>();
                    while (reader.Read())
                    {
                        records.Add(new APICase
                        {

                            CaseUID = Utility.GetGuidFromReader(reader, "CaseUID"),
                            Patient = Utility.GetStringFromReader(reader, "Patient"),
                            SubmissionId = Utility.GetIntFromReader(reader, "SubmissionId"),
                            SubmissionIdT = Utility.GetGuidFromReader(reader, "SubmissionIdT"),
                            StudentId = Utility.GetIntFromReader(reader, "StudentId"),
                            CreatedBy = Utility.GetStringFromReader(reader, "CreatedBy"),
                            CreatedOn = Utility.GetDateFromReader(reader, "CreatedOn"),
                            UpdatedBy = Utility.GetStringFromReader(reader, "UpdatedBy"),
                            UpdatedOn = Utility.GetDateFromReader(reader, "UpdatedOn")

                        });

                    }

                    return records;
                }
            }
        }
        //Questions
        public static List<APIQuestion> GetQuestions(string submissionIdT)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIElogbookQuestionsLoadForSubmission", Utility.ParseGuid(submissionIdT)))
                {
                    List<APIQuestion> records = new List<APIQuestion>();
                    while (reader.Read())
                    {
                        records.Add(new APIQuestion
                        {

                            AssignmentId = Utility.GetIntFromReader(reader, "AssignmentId"),
                            SectionId = Utility.GetIntFromReader(reader, "SectionId"),
                            SectionName = Utility.GetStringFromReader(reader, "SectionName"),
                            SectionOrder = Utility.GetDoubleFromReader(reader, "SectionOrder"),
                            DisplayOrder = Utility.GetDoubleFromReader(reader, "DisplayOrder"),
                            QuestionId = Utility.GetIntFromReader(reader, "QuestionId"),
                            QuestionText = Utility.GetStringFromReader(reader, "QuestionText"),
                            QuestionOptions = Utility.GetStringFromReader(reader, "QuestionOptions"),
                            HasSub = Utility.GetBoolFromReader(reader, "HasSub"),
                            IsSub = Utility.GetBoolFromReader(reader, "IsSub"),
                            ParentId = Utility.GetIntFromReader(reader, "ParentId"),
                            ParentOption = Utility.GetStringFromReader(reader, "ParentOption"),
                            ResponseType = Utility.GetStringFromReader(reader, "ResponseType"),
                            ElogbookId = Utility.GetIntFromReader(reader, "ElogbookId"),
                            InstitutionId = Utility.GetIntFromReader(reader, "InstitutionId"),
                            ShowOnDashboard = Utility.GetBoolFromReader(reader, "ShowOnDashboard"),
                            IsForSupervisor = Utility.GetBoolFromReader(reader, "IsForSupervisor"),
                            IsAssignmentLevel = Utility.GetBoolFromReader(reader, "IsAssignmentLevel")
                        });

                    }

                    return records;
                }
            }
        }
        public static List<APIQuestion> GetQuestionsByAssignmentId(int assignmentId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIElogbookQuestionsLoadForAssignment", assignmentId))
                {
                    List<APIQuestion> records = new List<APIQuestion>();
                    while (reader.Read())
                    {
                        records.Add(new APIQuestion
                        {

                            AssignmentId = Utility.GetIntFromReader(reader, "AssignmentId"),
                            SectionId = Utility.GetIntFromReader(reader, "SectionId"),
                            SectionName = Utility.GetStringFromReader(reader, "SectionName"),
                            SectionOrder = Utility.GetDoubleFromReader(reader, "SectionOrder"),
                            DisplayOrder = Utility.GetDoubleFromReader(reader, "DisplayOrder"),
                            QuestionId = Utility.GetIntFromReader(reader, "QuestionId"),
                            QuestionText = Utility.GetStringFromReader(reader, "QuestionText"),                           
                            QuestionOptions = Utility.GetStringFromReader(reader, "QuestionOptions"),
                            HasSub = Utility.GetBoolFromReader(reader, "HasSub"),
                            IsSub = Utility.GetBoolFromReader(reader, "IsSub"),
                            ParentId = Utility.GetIntFromReader(reader, "ParentId"),
                            ParentOption = Utility.GetStringFromReader(reader, "ParentOption"),
                            ResponseType = Utility.GetStringFromReader(reader, "ResponseType"),
                            ElogbookId = Utility.GetIntFromReader(reader, "ElogbookId"),
                            InstitutionId = Utility.GetIntFromReader(reader, "InstitutionId"),
                            ShowOnDashboard = Utility.GetBoolFromReader(reader, "ShowOnDashboard"),
                            IsForSupervisor = Utility.GetBoolFromReader(reader, "IsForSupervisor"),
                            IsAssignmentLevel = Utility.GetBoolFromReader(reader, "IsAssignmentLevel")
                        });

                    }

                    return records;
                }
            }
        } 
        public static List<APIElogbookCompetence> GetCompetenciesByAssignmentId(int assignmentId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIElogbookCompetencesLoadByAssignmentId", assignmentId))
                {
                    List<APIElogbookCompetence> records = new List<APIElogbookCompetence>();
                    while (reader.Read())
                    {
                        records.Add(new APIElogbookCompetence
                        {

                            CompetenceId = Utility.GetLongFromReader(reader, "CompetenceId"),
                            ElogbookId = Utility.GetIntFromReader(reader, "ElogbookId"),
                            QuestionId = Utility.GetIntFromReader(reader, "QuestionId"),
                            QuestionText = Utility.GetStringFromReader(reader, "QuestionText"),
                            QuestionOption = Utility.GetStringFromReader(reader, "QuestionOption"),
                            Expected = Utility.GetIntFromReader(reader, "Expected"),
                            AssignmentId = Utility.GetIntFromReader(reader, "AssignmentId")
                        });

                    }

                    return records;
                }
            }
        }
        public static List<APICase> GetCasesForSubmission(string submissionIdT)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIElogbookLoadCasesForSubmission", Utility.ParseGuid(submissionIdT)))
                {
                    List<APICase> records = new List<APICase>();
                    while (reader.Read())
                    {
                        records.Add(new APICase
                        {

                            CaseUID = Utility.GetGuidFromReader(reader, "CaseUID"),
                            CaseId = Utility.GetIntFromReader(reader, "CaseId")


                        });

                    }

                    return records;
                }
            }
        }
        public static List<APIResponses> GetResponses(long caseId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIElogbookCaseResponsesLoadForSubmission", caseId))
                {
                    List<APIResponses> records = new List<APIResponses>();
                    while (reader.Read())
                    {
                        records.Add(new APIResponses
                        {


                            QuestionId = Utility.GetIntFromReader(reader, "QuestionId"),
                            ResponseText = Utility.GetStringFromReader(reader, "ResponseText")
                            /*CreatedBy = Utility.GetStringFromReader(reader, "CreatedBy"),
                            CreatedOn = Utility.GetDateFromReader(reader, "CreatedOn"),
                            UpdatedBy = Utility.GetStringFromReader(reader, "UpdatedBy"),
                            UpdatedOn = Utility.GetDateFromReader(reader, "UpdatedOn"),*/

                        });

                    }

                    return records;
                }
            }
        }

        public static List<APICaseResponse> GetCaseResponses(string submissionIdT)
        {
            List<APICaseResponse> caseResponses = new List<APICaseResponse>();
            List<APICase> cases = GetCasesForSubmission(submissionIdT);
            foreach (APICase patient in cases)
            {
                APICaseResponse caseResponse = new APICaseResponse();
                caseResponse.CaseUID = patient.CaseUID;

                List<APIResponses> responses = GetResponses(patient.CaseId);
                caseResponse.QuestionResponses = responses;
                caseResponses.Add(caseResponse);
            }
            return caseResponses;
        }

        public static List<APIQuestionResponse> GetQuestionResponses(string submissionIdT)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIElogbookResponsesLoadForSubmission", Utility.ParseGuid(submissionIdT)))
                {
                    List<APIQuestionResponse> records = new List<APIQuestionResponse>();
                    while (reader.Read())
                    {
                        records.Add(new APIQuestionResponse
                        {

                            CaseUID = Utility.GetGuidFromReader(reader, "CaseUID"),
                            QuestionId = Utility.GetIntFromReader(reader, "QuestionId"),
                            ResponseText = Utility.GetStringFromReader(reader, "ResponseText"),
                            SubmissionIdT = Utility.GetGuidFromReader(reader, "SubmissionIdT")
                            //CreatedBy = Utility.GetStringFromReader(reader, "CreatedBy"),
                            //CreatedOn = Utility.GetDateFromReader(reader, "CreatedOn"),
                            //UpdatedBy = Utility.GetStringFromReader(reader, "UpdatedBy"),
                            //UpdatedOn = Utility.GetDateFromReader(reader, "UpdatedOn"),

                        });

                    }

                    return records;
                }
            }
        }
 public static List<APIAssignmentQuestionResponse> GetQuestionResponsesForAssignmentLevel(string submissionIdT)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIElogbookResponsesLoadForSubmissionAssignmentLevelQuestions", Utility.ParseGuid(submissionIdT)))
                {
                    List<APIAssignmentQuestionResponse> records = new List<APIAssignmentQuestionResponse>();
                    while (reader.Read())
                    {
                        records.Add(new APIAssignmentQuestionResponse
                        {


                            QuestionId = Utility.GetIntFromReader(reader, "QuestionId"),
                            ResponseText = Utility.GetStringFromReader(reader, "ResponseText"),
                            SubmissionIdT = Utility.GetGuidFromReader(reader, "SubmissionIdT"),
                            SubmissionId = Utility.GetLongFromReader(reader, "SubmissionId")
                            //CreatedBy = Utility.GetStringFromReader(reader, "CreatedBy"),
                            //CreatedOn = Utility.GetDateFromReader(reader, "CreatedOn"),
                            //UpdatedBy = Utility.GetStringFromReader(reader, "UpdatedBy"),
                            //UpdatedOn = Utility.GetDateFromReader(reader, "UpdatedOn"),

                        });

                    }

                    return records;
                }
            }
        }
        public static List<APIAssignmentQuestionResponse> GetQuestionResponsesForAssignmentLevelBySubmisionIds(string submissionIds)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIElogbookResponsesLoadForSubmissionAssignmentLevelQuestionsByIds", submissionIds))
                {
                    List<APIAssignmentQuestionResponse> records = new List<APIAssignmentQuestionResponse>();
                    while (reader.Read())
                    {
                        records.Add(new APIAssignmentQuestionResponse
                        {


                            QuestionId = Utility.GetIntFromReader(reader, "QuestionId"),
                            ResponseText = Utility.GetStringFromReader(reader, "ResponseText"),
                            SubmissionIdT = Utility.GetGuidFromReader(reader, "SubmissionIdT"),
                            SubmissionId = Utility.GetLongFromReader(reader, "SubmissionId")
                           

                        });

                    }

                    return records;
                }
            }
        }
        //SUPERVISOR
        public static List<APIGrade> GetGrades()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_ElogbookAssignmentGradesLoadAll"))
                {
                    List<APIGrade> records = new List<APIGrade>();
                    while (reader.Read())
                    {
                        records.Add(new APIGrade
                        {

                            GradeId = Utility.GetIntFromReader(reader, "GradeId"),
                            GradeName = Utility.GetStringFromReader(reader, "GradeName"),

                        });

                    }

                    return records;
                }
            }
        }
        //student submissions for an assignment
        public static List<APIStaffSubmission> GetStaffSubmissions(int assignmentId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIElogbookSubmissionsLoadForLecturer", assignmentId))
                {
                    List<APIStaffSubmission> records = new List<APIStaffSubmission>();
                    while (reader.Read())
                    {
                        records.Add(new APIStaffSubmission
                        {
                            AssignmentId = Utility.GetIntFromReader(reader, "AssignmentId"),
                            Student = Utility.GetStringFromReader(reader, "Student"),
                            SubmissionId = Utility.GetIntFromReader(reader, "SubmissionId"),
                            SubmissionIdT = Utility.GetGuidFromReader(reader, "SubmissionIdT"),
                            StudentId = Utility.GetIntFromReader(reader, "StudentId"),
                            Mentor = Utility.GetStringFromReader(reader, "Mentor"),
                            Hospital = Utility.GetStringFromReader(reader, "Hospital"),
                            Cases = Utility.GetIntFromReader(reader, "Cases"),
                            UpdatedOn = Utility.GetDateFromReader(reader, "UpdatedOn"),
                            InstitutionId = Utility.GetIntFromReader(reader, "InstitutionId"),
                            Status = Utility.GetStringFromReader(reader, "Status"),
                            GradeId = Utility.GetIntFromReader(reader, "GradeId"),
                            Grade = Utility.GetStringFromReader(reader, "Grade"),
                            GradedBy = Utility.GetStringFromReader(reader, "GradedBy"),
                            GradedByName = Utility.GetStringFromReader(reader, "GradedByName"),
                            GradedOn = Utility.GetDateFromReader(reader, "GradedOn"),
                        });

                    }

                    return records;
                }
            }
        }
        //cases and responses
        public static List<APIResponses> GetResponsesForSubmissions(string submissionIds)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIElogbookCaseResponsesLoadForSubmissionIds", submissionIds))
                {
                    List<APIResponses> records = new List<APIResponses>();
                    while (reader.Read())
                    {
                        records.Add(new APIResponses
                        {


                            QuestionId = Utility.GetIntFromReader(reader, "QuestionId"),
                            ResponseText = Utility.GetStringFromReader(reader, "ResponseText"),
                            CaseId = Utility.GetLongFromReader(reader, "CaseId"),
                            SubmissionId = Utility.GetLongFromReader(reader, "SubmissionId")
                            /*CreatedBy = Utility.GetStringFromReader(reader, "CreatedBy"),
                            CreatedOn = Utility.GetDateFromReader(reader, "CreatedOn"),
                            UpdatedBy = Utility.GetStringFromReader(reader, "UpdatedBy"),
                            UpdatedOn = Utility.GetDateFromReader(reader, "UpdatedOn"),*/

                        });

                    }

                    return records;
                }
            }
        }

        public static List<APICase> GetCasesForSubmissionIds(string submissionIds)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIElogbookLoadCasesForSubmissionIds", submissionIds))
                {
                    List<APICase> records = new List<APICase>();
                    while (reader.Read())
                    {
                        records.Add(new APICase
                        {

                            CaseUID = Utility.GetGuidFromReader(reader, "CaseUID"),
                            CaseId = Utility.GetIntFromReader(reader, "CaseId"),
                            Patient = Utility.GetStringFromReader(reader, "Patient"),
                            StudentId = Utility.GetIntFromReader(reader, "StudentId"),
                            CreatedOn = Utility.GetDateFromReader(reader, "CreatedOn"),
                            UpdatedOn = Utility.GetDateFromReader(reader, "UpdatedOn"),
                            SubmissionId = Utility.GetLongFromReader(reader, "SubmissionId")


                        });

                    }

                    return records;
                }
            }
        }
        public static List<APISubmissionComment> GetCommentsForSubmissionIds(string submissionIds)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIElogbookSubmissionCommentsLoadAll", submissionIds))
                {
                    List<APISubmissionComment> records = new List<APISubmissionComment>();
                    while (reader.Read())
                    {
                        records.Add(new APISubmissionComment
                        {

                            CommentId = Utility.GetLongFromReader(reader, "CommentId"),
                            CommentText = Utility.GetStringFromReader(reader, "CommentText"),
                            CreatedBy = Utility.GetStringFromReader(reader, "CreatedBy"),
                            CreatedOn = Utility.GetDateFromReader(reader, "CreatedOn"),
                            SubmissionId = Utility.GetLongFromReader(reader, "SubmissionId"),
                            InstitutionId = Utility.GetIntFromReader(reader, "InstitutionId"),
                            CreatedByFullName = Utility.GetStringFromReader(reader, "CreatedByFullName"),
                            UserType = Utility.GetStringFromReader(reader, "UserType")


                        });

                    }

                    return records;
                }
            }
        }

        public static List<APISubmissionComment> GetCommentsForSubmissionIdT(string submissionIdT)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIElogbookSubmissionCommentsLoadBySubmissionIdT", Utility.ParseGuid(submissionIdT)))
                {
                    List<APISubmissionComment> records = new List<APISubmissionComment>();
                    while (reader.Read())
                    {
                        records.Add(new APISubmissionComment
                        {
                            SubmissionIdT = Utility.GetGuidFromReader(reader, "SubmissionIdT"),
                            CommentId = Utility.GetLongFromReader(reader, "CommentId"),
                            CommentText = Utility.GetStringFromReader(reader, "CommentText"),
                            CreatedBy = Utility.GetStringFromReader(reader, "CreatedBy"),
                            CreatedOn = Utility.GetDateFromReader(reader, "CreatedOn"),
                            SubmissionId = Utility.GetLongFromReader(reader, "SubmissionId"),
                            InstitutionId = Utility.GetIntFromReader(reader, "InstitutionId"),
                            CreatedByFullName = Utility.GetStringFromReader(reader, "CreatedByFullName"),
                            UserType = Utility.GetStringFromReader(reader, "UserType")


                        });

                    }

                    return records;
                }
            }
        }

        public static List<ElogbookAchievement> GetAchievementsForSubmission(string submissionIdT)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIElogbookAchievementLoadForSubmissionIdT", Utility.ParseGuid(submissionIdT)))
                {
                    List<ElogbookAchievement> records = new List<ElogbookAchievement>();
                    
                    while (reader.Read())
                    {
                        records.Add(new ElogbookAchievement
                        {
                            SubmissionIdT = Utility.GetGuidFromReader(reader, "SubmissionIdT"),
                            QuestionId = Utility.GetIntFromReader(reader, "QuestionId"),
                            QuestionText = Utility.GetStringFromReader(reader, "QuestionText"),//responses
                            StudentId = Utility.GetIntFromReader(reader, "StudentId"),
                            SubmissionId = Utility.GetIntFromReader(reader, "SubmissionId"),
                            ElogbookId = Utility.GetIntFromReader(reader, "ElogbookId"),
                            AssignmentId = Utility.GetIntFromReader(reader, "AssignmentId"),//,
                            Expected = Utility.GetIntFromReader(reader, "Expected"),
                            Achieved = Utility.GetIntFromReader(reader, "Achieved"),
                            AchievedPercentage = Utility.GetDoubleFromReader(reader, "AchievedPercentage"),
                            QuestionOption = Utility.GetStringFromReader(reader, "QuestionOption")//option mapped to competence
                        });
                    }
                    return records;
                }
            }
        }

        public static List<ElogbookAchievement> GetAchievementsForSubmissionIds(string submissionIds)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIElogbookAchievementLoadForSubmissionIds", submissionIds))
                {
                    List<ElogbookAchievement> records = new List<ElogbookAchievement>();

                    while (reader.Read())
                    {
                        records.Add(new ElogbookAchievement
                        {
                            SubmissionIdT = Utility.GetGuidFromReader(reader, "SubmissionIdT"),
                            QuestionId = Utility.GetIntFromReader(reader, "QuestionId"),
                            QuestionText = Utility.GetStringFromReader(reader, "QuestionText"),//responses
                            StudentId = Utility.GetIntFromReader(reader, "StudentId"),
                            SubmissionId = Utility.GetIntFromReader(reader, "SubmissionId"),
                            ElogbookId = Utility.GetIntFromReader(reader, "ElogbookId"),
                            AssignmentId = Utility.GetIntFromReader(reader, "AssignmentId"),//,
                            Expected = Utility.GetIntFromReader(reader, "Expected"),
                            Achieved = Utility.GetIntFromReader(reader, "Achieved"),
                            AchievedPercentage = Utility.GetDoubleFromReader(reader, "AchievedPercentage"),
                            QuestionOption = Utility.GetStringFromReader(reader, "QuestionOption")//option mapped to competence
                        });
                    }
                    return records;
                }
            }
        }
    }
}