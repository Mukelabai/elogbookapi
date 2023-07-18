using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ElogbookCase
/// </summary>
public class ElogbookCase
{
    public ElogbookCase()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private long caseId, submissionId;
    private string caseUID, patient, createdBy, updatedBy;
    private int institutionId, studentId;
    private DateTime createdOn, updatedOn;

    public long CaseId
    {
        get
        {
            return caseId;
        }

        set
        {
            caseId = value;
        }
    }

    public long SubmissionId
    {
        get
        {
            return submissionId;
        }

        set
        {
            submissionId = value;
        }
    }

    public string CaseUID
    {
        get
        {
            return caseUID;
        }

        set
        {
            caseUID = value;
        }
    }

    public string Patient
    {
        get
        {
            return patient;
        }

        set
        {
            patient = value;
        }
    }

    public string CreatedBy
    {
        get
        {
            return createdBy;
        }

        set
        {
            createdBy = value;
        }
    }

    public string UpdatedBy
    {
        get
        {
            return updatedBy;
        }

        set
        {
            updatedBy = value;
        }
    }

    public int InstitutionId
    {
        get
        {
            return institutionId;
        }

        set
        {
            institutionId = value;
        }
    }

    public int StudentId
    {
        get
        {
            return studentId;
        }

        set
        {
            studentId = value;
        }
    }

    public DateTime CreatedOn
    {
        get
        {
            return createdOn;
        }

        set
        {
            createdOn = value;
        }
    }

    public DateTime UpdatedOn
    {
        get
        {
            return updatedOn;
        }

        set
        {
            updatedOn = value;
        }
    }
}