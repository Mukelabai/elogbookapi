
using System;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Configuration;
using System.Web.Security;
using elogbook.Model.Faculty;
using System.Collections.Generic;
using elogbookapi.Models;

namespace elogbook.Model

{
    public class SecurityController
    {
        private string connectionString;
        //onstructor
        public SecurityController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();

        }
        public List<StaffContact> GetStaffIDsForUserAccounts(int institutionId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_StaffIDsLoadForUserAccounts", institutionId);
                
                List<StaffContact> iDs = new List<StaffContact>();
                while (reader.Read())
                {
                    StaffContact contact = new global::StaffContact();
                    contact.StaffNumber = Utility.GetStringFromReader(reader, "StaffNumber");
                    contact.EmailAddress = Utility.GetStringFromReader(reader, "EmailAddress");
                    iDs.Add(contact);
                }
                reader.Close();
                return iDs;
            }
        }
        public string GetUserRightsOnly(int institutionId, string roleName)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_UserRightsLoadRightsOnly", institutionId, roleName);
                
                string rights = null;
                if (reader.Read())
                {
                    rights = Utility.GetStringFromReader(reader, "Rights");
                }
                reader.Close();
                return rights;
            }
        }
        public string GetInstitutionModules(int institutionId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_ModulesLoadByInstitutionId", institutionId);
                
                string modules = null;
                if (reader.Read())
                {
                    modules = Utility.GetStringFromReader(reader, "Modules");
                }
                reader.Close();
                return modules;
            }

        }
        public string GetStaffNameFromUsername(string username)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                object record = SqlHelper.ExecuteScalar(sqlConnection, "spGT_StaffLoadNameByUsername", username);
                return record is DBNull ? null : Convert.ToString(record);
            }

        }
        public Institution GetInstitutionDetails(string userName)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_InstitutionLoadHeaderForUser", userName);
                Institution school = new Institution();
                
                if (reader.Read())
                {
                    school.InstitutionName = Utility.GetStringFromReader(reader, "InstitutionName");
                    school.ContactDetails = Utility.GetStringFromReader(reader, "ContactDetails");
                    school.Motto = Utility.GetStringFromReader(reader, "Motto");
                    school.LogoURL = Utility.GetStringFromReader(reader, "LogoURL");
                }
                reader.Close();
                return school;
            }
        }
        public string GetRoleRights(string roleName)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                return Convert.ToString(SqlHelper.ExecuteScalar(sqlConnection, "spGT_SSRoleRightsLoad", roleName));
            }
        }
        public bool UpdateRoleRights(string roleName, string rights)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_RoleRightsUpdate", roleName, rights) > 0;
            }
        }

        public bool UpdateInstitutionRoles(string roleName, int institutionId, string rights)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_InstitutionRolesInsert", roleName, institutionId, rights) > 0;
            }
        }
        public bool UpdateRoleInstitutionId(string roleName, int institutionId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_RoleInstitutionIdUpdate", roleName, institutionId) > 0;
            }
        }
        public string GetUserLevel(string userName)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                return Convert.ToString(SqlHelper.ExecuteScalar(sqlConnection, "spGT_SSUserLoadLevel", userName));
            }
        }
        public int GetUserInstitutionId(string userName)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                return Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnection, "spGT_UserLoadInstitutionId", userName));
            }
        }
        public int GetUserInstitutionId()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                return Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnection, "spGT_UserLoadInstitutionId", Membership.GetUser().UserName));
            }
        }
        

        public bool UpdateStaffUsername(string staffNumber, string userName, int institutionId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_StaffUpdateUsername", staffNumber, userName, institutionId) > 0;
            }
        }
        public bool UpdateStaffUsernameById(int staffId, string userName, int institutionId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_StaffUpdateUsernameByStaffId", staffId, userName, institutionId) > 0;
            }
        }
        public bool UpdateStudentUsername(string computerNumber, string userName, int institutionId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_StudentUpdateUsername", computerNumber, userName, institutionId) > 0;
            }
        }
       

       

        public bool AddUserInstitution(string userName, int institutionId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_UsersUpdateInstitutionId", userName, institutionId) > 0;
            }
        }
       

        public string Encrypt(string inp)
        {

            MD5CryptoServiceProvider hasher = new MD5CryptoServiceProvider();

            byte[] tBytes = Encoding.ASCII.GetBytes(inp);

            byte[] hBytes = hasher.ComputeHash(tBytes);

            StringBuilder sb = new StringBuilder();

            for (int c = 0; c < hBytes.Length; c++)

                sb.AppendFormat("{0:x2}", hBytes[c]);

            return (sb.ToString());

        }


        //
        public void DeleteUserRoles(int userId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlHelper.ExecuteNonQuery(sqlConnection, "spCM_DeleteUserRoles", userId);
            }
        }
        public bool UpdateUserRoles(int userId, string roleName)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                return SqlHelper.ExecuteNonQuery(sqlConnection, "spCM_UpdateUserRoles", userId, roleName) > 0;
            }
        }


        public bool DeleteUser(int UserId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_DeleteUser", UserId) > 0;//if afftected rows >0 then user deleted
            }
        }

        public bool UpdatePassword(string username, string oldPassword, string newPassword)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                return SqlHelper.ExecuteNonQuery(sqlConnection, "UpdatePassword", username, oldPassword, newPassword) > 0;//if afftected rows >0 then password updated
            }
        }

       

       
        public bool DeleteLogRequest(int UserId, string request, bool granted, DateTime requestdate)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                return SqlHelper.ExecuteNonQuery(sqlConnection, "spGT_SALogDelete", UserId, request, granted, requestdate) > 0;
            }
        }



        public bool EmailAddressExists(string email)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                return Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnection, "spGT_SAEmailExists", email)) > 0;
            }
        }
        public User GetAPIUser(String username)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                using (SqlDataReader reader = SqlHelper.ExecuteReader(sqlConnection, "spGT_APIUserLoad", username))
                {
                    User user = null;
                    if (reader.Read())
                    {
                        user = new User()
                        {
                            UserId = Utility.GetIntFromReader(reader, "UserId"),
                            WebUsername = Utility.GetStringFromReader(reader, "WebUsername"),
                            LastName = Utility.GetStringFromReader(reader, "LastName"),
                            FirstName = Utility.GetStringFromReader(reader, "FirstName"),
                            Sex = Utility.GetStringFromReader(reader, "Sex"),
                            UserNumber = Utility.GetStringFromReader(reader, "UserNumber"),
                            RoleName = Utility.GetStringFromReader(reader, "RoleName")
                        };
                    }

                    return user;
                }
            }
        }

    }
}
