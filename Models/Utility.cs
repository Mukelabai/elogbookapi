using DotNetNuke.Common.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Utility
/// </summary>
/// 
namespace elogbook.Model

{
    public class Utility
    {

        public Utility()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static string GetAnnouncmentURLQuery(String senderId, string phone, string message)
        {
            return string.Format("https://bulksms.zamtel.co.zm/api/v2.1/action/send/api_key/1f43c62af5c4ee0994f19a218a227fc4/contacts/{0}/senderId/{1}/message/{2}", phone, senderId, CleanMessage(message));
            //return string.Format("https://apps.zamtel.co.zm/bulksms/api/action/send/api_key/1f43c62af5c4ee0994f19a218a227fc4/senderId/{0}/contacts/{1}/message/{2}", senderId, phone, message);

        }
        public static string GetPaymentQuery(string institutionId)
        {
            string url = ConfigurationManager.AppSettings["paymentAPIURL"].ToString();
            return string.Format("{0}{1}", url, institutionId);

        }
        public static string CleanMessage(string message)
        {
            //message = message.Replace("/", "-").Replace("\\", "-").Replace("\n", " ");
            //message = message.Replace("+", "%0A");
            return HttpUtility.UrlEncode(message);
            //return message;
        }

        public static int ExecuteNonQuery(string spName, params object[] values)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(spName, sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(Utility.GetParameters(values));
                sqlConnection.Open();
                int result = cmd.ExecuteNonQuery();
                sqlConnection.Close(); sqlConnection.Dispose();
                return result;
            }
        }

        public static object ExecuteScalar(string spName, params object[] values)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(spName, sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(Utility.GetParameters(values));
                sqlConnection.Open();
                object result = cmd.ExecuteScalar();
                sqlConnection.Close(); sqlConnection.Dispose();
                return result;
            }
        }

        public static SqlDataReader ExecuteReader(string spName, string parNames, params object[] values)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(spName, sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < values.Length; i++)
                {
                    cmd.Parameters[0].Value = values[i];
                }
                cmd.Parameters.AddRange(Utility.GetParameters(parNames, values));
                sqlConnection.Open();
                SqlDataReader result = cmd.ExecuteReader();
                sqlConnection.Close();
                sqlConnection.Dispose();
                return result;
            }
        }
        public static SqlParameter[] GetParameters(params object[] values)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            foreach (object value in values)
            {
                SqlParameter par = new SqlParameter();
                par.SqlValue = value;
                pars.Add(par);
            }
            return pars.ToArray();

        }

        public static string CleanNumber(string phoneNumber)
        {
            phoneNumber = phoneNumber.Replace(" ", "").Replace("O", "0").Replace("+", "");
            if (!phoneNumber.StartsWith("260") && phoneNumber.StartsWith("0"))
            {
                phoneNumber = "26" + phoneNumber;
            }
            else if (!phoneNumber.StartsWith("260"))
            {
                phoneNumber = "260" + phoneNumber;
            }

            return phoneNumber;
        }

        public static void writeToLog(StreamWriter w, String message)
        {
            if (w != null)
            {
                w.WriteLine(message);
                w.Flush();
            }
        }
        
        public static string GetCurrentTimeString()
        {
            return string.Format("{0}", DateTime.Now.ToShortDateString().Replace("/", "_").Replace(":", "_"));
        }
        public static void ClearCheckList(CheckBoxList chkList)
        {
            foreach (ListItem item in chkList.Items)
            {
                item.Selected = false;
            }
        }
        private string GetRandomUsername()
        {
            int firstPart = new Random().Next(1000, 5000);
            int secondPart = new Random().Next(6000, 9999);
            return string.Format("{0}-{1}", firstPart, secondPart);
        }
        public static int GetIntFromDropDownList(DropDownList ddlList)
        {
            return string.IsNullOrWhiteSpace(ddlList.SelectedValue) ? -1 : Convert.ToInt32(ddlList.SelectedValue);
        }
        public static string GetStringtFromDropDownList(DropDownList ddlList)
        {
            return string.IsNullOrWhiteSpace(ddlList.SelectedValue) ? null : ddlList.SelectedValue;
        }
       
        public static string GetUserNameFromStaffNumber(string staffNumber, int institutionId)
        {
            return string.Format("{0}-{1}", institutionId, staffNumber);
        }
        




        public static bool IsNullOrWhiteSpace(string value)
        {
            return value == null || value.Trim() == "";
        }
        public static bool IsNullOrWhiteSpace(DropDownList ddlList)
        {
            return ddlList.SelectedValue == null || ddlList.SelectedValue.Trim() == "";
        }

        public static void CheckAllItems(CheckBoxList chkList, bool check)
        {
            foreach (ListItem item in chkList.Items)
            {
                item.Selected = check;
            }
        }
        public static int CheckedItemsCount(CheckBoxList chkList)
        {
            int count = 0;
            foreach (ListItem item in chkList.Items)
            {
                if (item.Selected) count++;
            }

            return count;
        }

        public static List<string> Shuffle(List<string> list)
        {

            Random randomNumbers = new Random();
            // for each item, pick another random item and swap them
            for (int first = 0; first < list.Count; first++)
            {
                // select a random number

                // random.
                int second = randomNumbers.Next(list.Count);

                // swap current item with randomly selected item
                string temp = list[first];
                list[first] = list[second];
                list[second] = temp;
            } // end for

            return list;
        } // end method 


        //public static List<Location> Shuffle(List<Location> list)
        //{

        //    Random randomNumbers = new Random();
        //    // for each item, pick another random item and swap them
        //    for (int first = 0; first < list.Count; first++)
        //    {
        //        // select a random number

        //        // random.
        //        int second = randomNumbers.Next(list.Count);

        //        // swap current item with randomly selected item
        //        Location temp = list[first];
        //        list[first] = list[second];
        //        list[second] = temp;
        //    } // end for

        //    return list;
        //} // end method 

        public static string Reverse(string text)
        {
            if (text == null) return null;

            // this was posted by petebob as well 
            char[] array = text.ToCharArray();
            Array.Reverse(array);
            return new String(array);
        }
        public static int GetIntFromReader(SqlDataReader reader, string columnName)
        {
            return reader[columnName] is DBNull ? -1 : Convert.ToInt32(reader[columnName]);
        }
        public static long GetLongFromReader(SqlDataReader reader, string columnName)
        {
            return reader[columnName] is DBNull ? -1 : Convert.ToInt64(reader[columnName]);
        }
        public static double GetDoubleFromReader(SqlDataReader reader, string columnName)
        {
            return reader[columnName] is DBNull ? -1 : Convert.ToDouble(reader[columnName]);
        }
        public static int? GetNullIntFromReader(SqlDataReader reader, string columnName)
        {
            return reader[columnName] is DBNull ? null : (int?)Convert.ToInt32(reader[columnName]);
        }

        public static Guid GetGuidFromReader(SqlDataReader reader, string columnName)
        {
            string guid = Convert.ToString(reader[columnName]);
            return reader[columnName] is DBNull ? Guid.Empty : Guid.Parse(guid);
        }
        public static string GetStringFromReader(SqlDataReader reader, string columnName)
        {
            return reader[columnName] is DBNull ? null : Convert.ToString(reader[columnName]);
        }
        public static bool GetBoolFromReader(SqlDataReader reader, string columnName)
        {
            return reader[columnName] is DBNull ? false : Convert.ToBoolean(reader[columnName]);
        }
        public static DateTime GetDateFromReader(SqlDataReader reader, string columnName)
        {
            return reader[columnName] is DBNull ? Null.NullDate : Convert.ToDateTime(reader[columnName]);
        }
        public static Guid ParseGuid(string myGuid)
        {
            return string.IsNullOrWhiteSpace(myGuid) ? Null.NullGuid : Guid.Parse(myGuid);
        }

        public int GetPos(Dictionary<string, string> tasks, string key)
        {

            var iter = tasks.Keys.GetEnumerator();
            int position = -1;
            for (int i = 0; i < tasks.Keys.Count; i++)
            {

                if (iter.MoveNext())
                {
                    if (key == iter.Current)
                    {
                        position = i + 1;
                        break;
                    }
                }

            }
            return position;
        }


        public bool userHasRight(string userRight)
        {
            bool hasRight = false;
            SecurityController sc = new SecurityController();
            string[] roles = Roles.GetRolesForUser();
            string rights = null;
            foreach (string role in roles)
            {
                rights = sc.GetRoleRights(role);
                if (rights.Contains(userRight))
                {
                    hasRight = true;
                    break;
                }
            }

            return hasRight;
        }
        
       
        public static string GetSelectedItemText(CheckBoxList chkList)
        {
            ArrayList list = new ArrayList();

            foreach (ListItem item in chkList.Items)
            {
                if (item.Selected)
                {
                    list.Add(item.Text);
                }

            }
            return string.Join(",", list.ToArray());
        }

        public static bool UserIsAllowedTo(string right, string userRights)
        {

            bool contains = !string.IsNullOrWhiteSpace(userRights) && userRights.Contains(right);
            bool isAdmin = Roles.IsUserInRole("System Admin") || UserIsGTSAdmin();
            bool userIsNotNull = Membership.GetUser() != null;
            bool finalResult = userIsNotNull && (isAdmin || contains);

            return finalResult;
        }
        public static bool InstitutionHasModule(string modules, string module)
        {
            return !string.IsNullOrWhiteSpace(modules) && modules.Contains(module);



        }
        public static bool UserIsGTSAdmin()
        {
            return Membership.GetUser() != null && Membership.GetUser().UserName != null && Roles.IsUserInRole("GTSAdmin");
        }
        public static bool UserIsLoggedIn()
        {
            return Membership.GetUser() != null;
        }
        public static void SetDropDownListValue(DropDownList ddlList, string value)
        {
            ddlList.SelectedValue = string.IsNullOrWhiteSpace(value) ? null : value;
        }
        

       
       

        public Dictionary<string, string> GetImportTasks()
        {
            //
            //tasks for importing
            Dictionary<string, string> tasks = new Dictionary<string, string>();

            tasks.Add("Students", "Students");


            return tasks;
        }

        public static void CreateCSV(string fileName, SqlDataReader reader)
        {

            StreamWriter sw = new StreamWriter(fileName);
            object[] output = new object[reader.FieldCount];

            //for (int i = 0; i < reader.FieldCount; i++)
            //    output[i] = reader.GetName(i);

            //sw.WriteLine(string.Join(";", output));

            while (reader.Read())
            {
                reader.GetValues(output);
                sw.WriteLine(string.Join(";", output));
            }

            sw.Close();
            reader.Close();
        }


        public static String GetStringFromCollection(NameValueCollection data, string field)
        {
            return (string)data[field];
        }
        public static Double GetDoubleFromCollection(NameValueCollection data, string field)
        {
            return Convert.ToDouble(data[field]);
        }


     

    }
}