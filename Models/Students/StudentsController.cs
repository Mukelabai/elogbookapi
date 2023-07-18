using elogbook.Model;
using elogbookapi.Models.Students;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StudentsController
/// </summary>
public class StudentsController
{
    private string connectionString;
    public StudentsController()
    {
        connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
    }
    public List<String> GetComputerNumbersUnAssigned(int institutionId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_ComputerNumbersLoadUnAssignedUserAccounts", institutionId))
            {
                
                List<String> iDs = new List<string>();
                while (reader.Read())
                {
                    string iD = Utility.GetStringFromReader(reader, "ComputerNumber");
                    iDs.Add(iD);
                }
                reader.Close();
                return iDs;
            }
        }
    }
    public List<String> GetComputerIDsCompletedMoreThan2YearsAgo(int institutionId)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_ComputerNumbersLoadCompletedTwoYearsAgo", institutionId))
            {
                List<String> iDs = new List<string>();
                while (reader.Read())
                {
                    string iD = Utility.GetStringFromReader(reader, "ComputerNumber");
                    iDs.Add(iD);
                }
                reader.Close();
                return iDs;
            }
        }
    }

    public Student GetStudent(String username)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIStudentLoad", username))
            {
                Student student = null;
                if (reader.Read())
                {
                    student = new Student
                    {
                        WebUsername = Utility.GetStringFromReader(reader, "WebUsername"),
                        LastName = Utility.GetStringFromReader(reader, "LastName"),
                        FirstName = Utility.GetStringFromReader(reader, "FirstName"),
                        Sex = Utility.GetStringFromReader(reader, "Sex"),
                        ComputerNumber = Utility.GetStringFromReader(reader, "ComputerNumber")
                    };
                }
                
                return student;
            }
        }
    }
}