using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SchoolsController
/// </summary>
public class SchoolsController
{
    #region Public Methods
    private string connectionString;

    public SchoolsController()
    {
        connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
    }

    
    public bool DeleteSchool(int schoolId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_SchoolDelete", schoolId) > 0;
        }
    }
  
    public bool DeleteProgrammeDuration(int durationId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_ProgrammeDurationDelete", durationId) > 0;
        }
    }
   
    public bool DeleteProgramme(int programId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_ProgrammeDelete", programId) > 0;
        }
    }

    public bool AddCourseOutline(int courseId, string outline, string username, int institionId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_CourseOutlineInsert", courseId, outline, username, institionId) > 0;
        }
    }

    public bool DeleteCourse(int courseId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_CourseDelete", courseId) > 0;
        }
    }
    public bool AddProgrammeCourse(int programId, int courseId, bool isMandatory, int institutionId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_ProgrammeCoursesInsert",
           programId,
            courseId,
            isMandatory,
            institutionId) > 0;
        }
    }
    public bool DeleteProgrammeCourses(int programId, string selectedCourses)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_ProgrammeCoursesDelete", programId, selectedCourses) > 0;
        }
    }
    public bool DeleteProgrammeCourses(int programId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_ProgrammeCoursesDeleteAll", programId) > 0;
        }
    }


  
   
    public bool DeleteSubjectArea(int subjectAreaId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_SubjectAreaDelete", subjectAreaId) > 0;
        }
    }
    //Fees
 
    public bool UpdateUserRights(int staffId, string userRights)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_StaffUpdateUserRights",
            staffId, userRights) > 0;
        }
    }
    public bool AddSystemLog(string request, string username, bool granted)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_LogTableInsert",
            request, username, granted) > 0;
        }
    }
 
 

    public string GetUserRole(string staffNumber)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return Convert.ToString(SqlHelper.ExecuteScalar(sqlConnection, "spGT_StaffLoadUserRole", staffNumber));
        }
    }

    public bool DeletePeriodCoursesPerPeriod(int period, int departmentId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_PeriodCoursesDeletePeriod", period, departmentId) > 0;
        }
    }
    public bool AddPeriodCourses(int period, int courseId, int institutionId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_PeriodCoursesInsert", period, courseId, institutionId) > 0;
        }
    }
    public bool DeletePeriodCourses(int period, int courseId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_PeriodCoursesDelete", period, courseId) > 0;
        }
    }

    //Staff COurses
    public bool AddStaffCourse(int StaffId, int CourseId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_StaffCoursesInsert", StaffId, CourseId) > 0;
        }
    }
    public string GetRoleDescription(int portalId, string roleName)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return Convert.ToString(SqlHelper.ExecuteScalar(sqlConnection, "spGT_GetRoleDescription", portalId, roleName));
        }
    }

   
    public string GetUserRightsOnly(string username)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            return Convert.ToString(SqlHelper.ExecuteScalar(sqlConnection, "spGT_UserRightsLoadRightsOnly", username));
            //List<UserRight> rights =  CBO.FillCollection<UserRight>((IDataReader)SqlHelper.ExecuteReader(sqlConnection, "spGT_UserRightsLoadRightsOnly", portalId, username));
            //ArrayList userRights = null;
            //if (rights != null && rights.Count > 0)
            //{
            //    userRights = new ArrayList();
            //    foreach (UserRight right in rights)
            //    {
            //        userRights.Add(right.Rights);
            //    }

            //}
            //return userRights;
        }
    }
   
    #endregion

    
}