using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StaffContact
/// </summary>
public class StaffContact
{
    private int userID, schoolId;
    private string phoneNumber, emailAddress, staffNumber, webUsername;

    public int UserID
    {
        get
        {
            return userID;
        }

        set
        {
            userID = value;
        }
    }

    public int SchoolId
    {
        get
        {
            return schoolId;
        }

        set
        {
            schoolId = value;
        }
    }

    public string PhoneNumber
    {
        get
        {
            return phoneNumber;
        }

        set
        {
            phoneNumber = value;
        }
    }

    public string EmailAddress
    {
        get
        {
            return emailAddress;
        }

        set
        {
            emailAddress = value;
        }
    }

    public string StaffNumber
    {
        get
        {
            return staffNumber;
        }

        set
        {
            staffNumber = value;
        }
    }

    public string WebUsername
    {
        get
        {
            return webUsername;
        }

        set
        {
            webUsername = value;
        }
    }

    public StaffContact()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}