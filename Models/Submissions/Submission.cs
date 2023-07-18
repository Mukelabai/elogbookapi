using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Submission
/// </summary>
public class Submission
{
    public Submission()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private string mentor, status;
    private int gradeId;

    public string Mentor
    {
        get
        {
            return mentor;
        }

        set
        {
            mentor = value;
        }
    }

    public string Status
    {
        get
        {
            return status;
        }

        set
        {
            status = value;
        }
    }

    public int GradeId
    {
        get
        {
            return gradeId;
        }

        set
        {
            gradeId = value;
        }
    }
}