using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ElogbookResponse
/// </summary>
public class ElogbookResponse
{
    public ElogbookResponse()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private long caseId, submissionId;
    private int parentQuestionId, childQuestionId;
    private string responseText;

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

    public int ParentQuestionId
    {
        get
        {
            return parentQuestionId;
        }

        set
        {
            parentQuestionId = value;
        }
    }

    public int ChildQuestionId
    {
        get
        {
            return childQuestionId;
        }

        set
        {
            childQuestionId = value;
        }
    }

    public string ResponseText
    {
        get
        {
            return responseText;
        }

        set
        {
            responseText = value;
        }
    }
}