using elogbook.Model;
using elogbookapi.Models.API;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SubmissionController
/// </summary>
public class SubmissionController
{
    private string connectionString;
    public SubmissionController()
    {
        connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
    }

    public bool CreateSubmission(int mentorId, int hospitalId, int assignmentId, string username, int institutionId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_ElogbookSubmissionsInsert", mentorId, hospitalId, assignmentId, username, institutionId) > 0;
        }
    }

    public List<ElogbookQuestion> GetQuestionsForSubmission(int submissionId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_ElogbookQuestionsLoadForSubmission", submissionId))
            {
                List<ElogbookQuestion> records = new List<ElogbookQuestion>();

                while (reader.Read())
                {
                    ElogbookQuestion record = new ElogbookQuestion();
                    record.SectionId = Utility.GetIntFromReader(reader, "SectionId");

                    record.SectionName = Utility.GetStringFromReader(reader, "SectionName");
                    record.SectionOrder = Utility.GetDoubleFromReader(reader, "SectionOrder");
                    record.QuestionId = Utility.GetIntFromReader(reader, "QuestionId");
                    record.QuestionText = Utility.GetStringFromReader(reader, "QuestionText");
                    record.ParentOrder = Utility.GetDoubleFromReader(reader, "ParentOrder");
                    record.QuestionOptions = Utility.GetStringFromReader(reader, "QuestionOptions");
                    record.ResponseType = Utility.GetStringFromReader(reader, "ResponseType");
                    record.ChildQuestionId = Utility.GetIntFromReader(reader, "ChildQuestionId");
                    record.ChildQuestionText = Utility.GetStringFromReader(reader, "ChildQuestionText");
                    record.ChildOrder = Utility.GetDoubleFromReader(reader, "ChildOrder");
                    record.ChildQuestionDisplayText = Utility.GetStringFromReader(reader, "ChildQuestionDisplayText");
                    record.ParentOption = Utility.GetStringFromReader(reader, "ParentOption");
                    record.ChildResponseType = Utility.GetStringFromReader(reader, "ChildResponseType");
                    record.ChildQuestionOptions = Utility.GetStringFromReader(reader, "ChildQuestionOptions");
                    records.Add(record);
                }
                return records;
            }
        }
    }

    public long AddCase(ElogbookCase elogbookCase)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return Convert.ToInt64(SqlHelper.ExecuteScalar(sqlConnection, "spGT_ElogbookCasesInsert",
                elogbookCase.Patient, elogbookCase.SubmissionId, elogbookCase.InstitutionId, elogbookCase.UpdatedBy));
        }
    }
    public bool AddResponse(long caseId, int parentQuestionId, int childQuestionId, string responseText, string updatedBy, int institutionId, long submissionId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_ElogbookResponsesInsert",
                caseId, parentQuestionId, childQuestionId, responseText, updatedBy, institutionId, submissionId) > 0; ;
        }
    }

    public List<ElogbookResponse> GetResponsesForCase(long caseId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_ElogbookResponsesLoadForCase", caseId))
            {
                List<ElogbookResponse> records = new List<ElogbookResponse>();

                while (reader.Read())
                {
                    ElogbookResponse record = new ElogbookResponse();
                    record.CaseId = Utility.GetLongFromReader(reader, "CaseId");
                    record.ParentQuestionId = Utility.GetIntFromReader(reader, "ParentQuestionId");
                    record.ChildQuestionId = Utility.GetIntFromReader(reader, "ChildQuestionId");
                    record.ResponseText = Utility.GetStringFromReader(reader, "ResponseText");
                    record.SubmissionId = Utility.GetLongFromReader(reader, "SubmissionId");

                    records.Add(record);
                }
                return records;
            }
        }
    }

    public bool AddSubmissionComment(string comment, string username, long submissionId, int institutionId, string userType)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_ElogbookSubmissionCommentsInsert",
                comment, username, submissionId, institutionId, userType) > 0;
        }
    }
    public bool UpdateSubmissionStatus(long submissionId, string status)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_ElogbookSubmissionStatusUpdate",
                submissionId, status) > 0;
        }
    }
    public bool UpdateSubmissionGrade(long submissionId, int gradeId, string gradedBy)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_ElogbookSubmissionGradeInsert",
                submissionId, gradeId, gradedBy) > 0;
        }
    }
    public string GetSubmissionStatus(long submissionId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            object status = SqlHelper.ExecuteScalar(sqlConnection, "spGT_ElogbookSubmissionLoadStatus",
                submissionId);
            if (status is DBNull)
            {
                return null;
            }
            else
            {
                return Convert.ToString(status);
            }

        }
    }
    public Submission GetSubmissionStatusGrades(long submissionId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_ElogbookSubmissionsLoadStatusGrades",
                submissionId))
            {

                Submission record = null;
                if (reader.Read())
                {
                    record = new Submission();
                    record.Mentor = Utility.GetStringFromReader(reader, "Mentor");
                    record.Status = Utility.GetStringFromReader(reader, "Status");
                    record.GradeId = Utility.GetIntFromReader(reader, "GradeId");
                }
                return record;
            }


        }
    }
    public string GetSubmissionMentor(long submissionId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            object mentor = SqlHelper.ExecuteScalar(sqlConnection, "spGT_ElogbookSubmissionsLoadMentor",
                submissionId);
            if (mentor is DBNull)
            {
                return null;
            }
            else
            {
                return Convert.ToString(mentor);
            }

        }
    }

    public List<ElogbookReportSubmission> GetElogbookReportSubmission(int academicYear)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_ElogbookReportLoadAssignmentSubmissions", academicYear))
            {
                List<ElogbookReportSubmission> records = new List<ElogbookReportSubmission>();

                while (reader.Read())
                {
                    ElogbookReportSubmission record = new ElogbookReportSubmission();
                    record.AssignmentId = Utility.GetIntFromReader(reader, "AssignmentId");
                    record.Rotation = Utility.GetStringFromReader(reader, "Rotation");
                    record.RotationPeriod = Utility.GetStringFromReader(reader, "RotationPeriod");
                    record.RotationYear = Utility.GetIntFromReader(reader, "RotationYear");
                    record.AcademicYear = Utility.GetIntFromReader(reader, "AcademicYear");
                    record.ElogbookId = Utility.GetIntFromReader(reader, "ElogbookId");
                    record.Elogbook = Utility.GetStringFromReader(reader, "Elogbook");
                    record.Program = Utility.GetStringFromReader(reader, "Program");
                    record.ProgramId = Utility.GetIntFromReader(reader, "ProgramId");
                    record.StudyYear = Utility.GetIntFromReader(reader, "StudyYear");
                    record.ElogbookVersion = Utility.GetStringFromReader(reader, "ElogbookVersion");
                    record.SubmissionId = Utility.GetIntFromReader(reader, "SubmissionId");
                    record.Mentor = Utility.GetStringFromReader(reader, "Mentor");
                    record.Hospital = Utility.GetStringFromReader(reader, "Hospital");
                    record.Status = Utility.GetStringFromReader(reader, "Status");
                    record.Grade = Utility.GetStringFromReader(reader, "Grade");
                    record.GradedBy = Utility.GetStringFromReader(reader, "GradedBy");
                    record.StudentId = Utility.GetIntFromReader(reader, "StudentId");
                    record.Student = Utility.GetStringFromReader(reader, "Student");
                    record.Sex = Utility.GetStringFromReader(reader, "Sex");
                    record.ComputerNumber = Utility.GetStringFromReader(reader, "ComputerNumber");
                    records.Add(record);

                }

                return records;

            }
        }
    }

    public List<ElogbookReportResponse> GetElogbookReportResponses(long submissionId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_ElogbookReportLoadResponses", submissionId))
            {
                List<ElogbookReportResponse> records = new List<ElogbookReportResponse>();

                while (reader.Read())
                {
                    ElogbookReportResponse record = new ElogbookReportResponse();
                    record.CaseId = Utility.GetLongFromReader(reader, "CaseId");
                    record.Patient = Utility.GetStringFromReader(reader, "Patient");
                    record.ParentQuestionId = Utility.GetIntFromReader(reader, "ParentQuestionId");
                    record.ParentOrder = Utility.GetDoubleFromReader(reader, "ParentOrder");
                    record.ParentQuestion = Utility.GetStringFromReader(reader, "ParentQuestion");
                    record.ParentResponseType = Utility.GetStringFromReader(reader, "ParentResponseType");
                    record.ParentOnDashboard = Utility.GetBoolFromReader(reader, "ParentOnDashboard");
                    record.SectionId = Utility.GetIntFromReader(reader, "SectionId");
                    record.SectionOrder = Utility.GetDoubleFromReader(reader, "SectionOrder");
                    record.ParentSection = Utility.GetStringFromReader(reader, "ParentSection");
                    record.ParentCategory = Utility.GetStringFromReader(reader, "ParentCategory");
                    record.ChildQuestionId = Utility.GetIntFromReader(reader, "ChildQuestionId");
                    record.ChildOrder = Utility.GetDoubleFromReader(reader, "ChildOrder");
                    record.ChildQuestion = Utility.GetStringFromReader(reader, "ChildQuestion");
                    record.ChildResponseType = Utility.GetStringFromReader(reader, "ChildResponseType");
                    record.ChildOnDashboard = Utility.GetBoolFromReader(reader, "ChildOnDashboard");
                    record.ChildCategory = Utility.GetStringFromReader(reader, "ChildCategory");
                    record.ResponseText = Utility.GetStringFromReader(reader, "ResponseText");
                    record.ResponseYear = Utility.GetIntFromReader(reader, "ResponseYear");
                    record.ResponseMonthNo = Utility.GetIntFromReader(reader, "ResponseMonthNo");
                    record.ResponseMonth = Utility.GetStringFromReader(reader, "ResponseMonth");

                    records.Add(record);

                }

                return records;

            }
        }
    }

    public bool DeleteElogbookReport(int academicYear)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_ElogbookReportDeleteForYear", academicYear) > 0;
        }
    }

    public bool AddElogbookReport(ElogbookReportSubmission submission)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_ELogbookReportInsert",
                submission.AssignmentId,
submission.Rotation,
submission.RotationPeriod,
submission.RotationYear,
submission.AcademicYear,
submission.ElogbookId,
submission.Elogbook,
submission.Program,
submission.ProgramId,
submission.StudyYear,
submission.ElogbookVersion,
submission.SubmissionId,
submission.Mentor,
submission.Hospital,
submission.Status,
submission.Grade,
submission.GradedBy,
submission.StudentId,
submission.Student,
submission.Sex,
submission.ComputerNumber,
submission.QuestionId,
submission.Section,
submission.Category,
submission.QuestionText,
submission.ParentQuestion,
submission.ResponseText,
submission.Patient,
submission.DisplayOrder,
submission.ResponseYear,
submission.ResponseMonthNo,
submission.ResponseMonth,
submission.SectionOrder,
submission.ShowOnDashboard,
submission.ResponseType) > 0;
        }
    }

    public List<ElogbookDashboardQuestion> GetDashboardQuestions(string role, string username)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_ElogbookReportLoadDashboardQuestions", role, username))
            {
                List<ElogbookDashboardQuestion> records = new List<ElogbookDashboardQuestion>();

                while (reader.Read())
                {
                    ElogbookDashboardQuestion record = new ElogbookDashboardQuestion();
                    record.SectionOrder = Utility.GetDoubleFromReader(reader, "SectionOrder");
                    record.Section = Utility.GetStringFromReader(reader, "Section");
                    record.DisplayOrder = Utility.GetDoubleFromReader(reader, "DisplayOrder");
                    record.Question = Utility.GetStringFromReader(reader, "Question");
                    record.ResponseText = Utility.GetStringFromReader(reader, "ResponseText");
                    record.ResponseType = Utility.GetStringFromReader(reader, "ResponseType");
                    record.Number = Utility.GetIntFromReader(reader, "Number");


                    records.Add(record);

                }

                return records;

            }
        }
    }

    //API
    public long APIAddSubmission(APISubmission s)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return Convert.ToInt64(
                SqlHelper.ExecuteScalar(sqlConnection, "spGT_APIElogbookSubmissionsInsert",
                s.SubmissionIdT, s.MentorId, s.HospitalId, s.AssignmentId, s.UpdatedBy, s.IsPublished)
                );
        }
    }
    public long APIAddCase(APICase c)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return Convert.ToInt64(
                SqlHelper.ExecuteScalar(sqlConnection, "spGT_APIElogbookCasesInsert",
                c.Patient, c.SubmissionId, c.UpdatedBy, c.CreatedOn, c.UpdatedOn)
                );
        }
    }
    public bool APIAddResponse(long caseId, long submissionId, int questionId, string responseText, string updatedBy, DateTime createdOn, DateTime updatedOn)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return
                SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_APIElogbookResponsesInsert",
                caseId, questionId, responseText, updatedBy, submissionId, createdOn, updatedOn) > 0;

        }
    }
    public bool APIAddSubmissionGrade(APIStaffSubmission s)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            if (s.GradedOn.Year < 2000)
            {
                return false;
            }
            else
            {
                return
                    SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_APIElogbookSubmissionGradeInsert",
                    s.SubmissionId, s.GradeId, s.GradedBy, s.GradedOn, s.Status) > 0;
            }

        }
    }
    public bool APIDeleteSubmissionCommentsForUser(long submissionId, string username)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return
                SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_APIElogbookCommentsDeleteByUserAndSubmission",
                submissionId, username) > 0;

        }
    }

    public bool APIAddSubmissionComment(APISubmissionComment c)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_APIElogbookSubmissionCommentsInsert",
                c.CommentText, c.CreatedBy, c.CreatedOn, c.SubmissionId, c.InstitutionId, c.UserType) > 0;
        }
    }

    public bool APIAddSubmissionAchievement(ElogbookAchievement ea)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_APIELogbookAchievementInsert",
                ea.AssignmentId,ea.SubmissionId,ea.QuestionId,ea.QuestionOption,ea.StudentId,ea.Achieved) > 0;
        }
    }
}