using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ElogbookReportResponse
/// </summary>
public class ElogbookReportResponse
{
    public ElogbookReportResponse()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private int sectionId, parentQuestionId, childQuestionId, responseYear, responseMonthNo, studentId, institutionId;
    private long caseId, submissionId;
    private string patient, parentQuestion, parentSection, parentCategory, childQuestion, childCategory, responseText, responseMonth, parentResponseType, childResponseType;
      private double parentOrder, childOrder, sectionOrder;
    private bool parentOnDashboard, childOnDashboard;
    public int SectionId
    {
        get
        {
            return sectionId;
        }

        set
        {
            sectionId = value;
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

    public int ResponseYear
    {
        get
        {
            return responseYear;
        }

        set
        {
            responseYear = value;
        }
    }

    public int ResponseMonthNo
    {
        get
        {
            return responseMonthNo;
        }

        set
        {
            responseMonthNo = value;
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

    public string ParentQuestion
    {
        get
        {
            return parentQuestion;
        }

        set
        {
            parentQuestion = value;
        }
    }

    public string ParentSection
    {
        get
        {
            return parentSection;
        }

        set
        {
            parentSection = value;
        }
    }

    public string ParentCategory
    {
        get
        {
            return parentCategory;
        }

        set
        {
            parentCategory = value;
        }
    }

    public string ChildQuestion
    {
        get
        {
            return childQuestion;
        }

        set
        {
            childQuestion = value;
        }
    }

    public string ChildCategory
    {
        get
        {
            return childCategory;
        }

        set
        {
            childCategory = value;
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

    public string ResponseMonth
    {
        get
        {
            return responseMonth;
        }

        set
        {
            responseMonth = value;
        }
    }

    public double ParentOrder
    {
        get
        {
            return parentOrder;
        }

        set
        {
            parentOrder = value;
        }
    }

    public double ChildOrder
    {
        get
        {
            return childOrder;
        }

        set
        {
            childOrder = value;
        }
    }

    public string ParentResponseType
    {
        get
        {
            return parentResponseType;
        }

        set
        {
            parentResponseType = value;
        }
    }

    public string ChildResponseType
    {
        get
        {
            return childResponseType;
        }

        set
        {
            childResponseType = value;
        }
    }



    public double SectionOrder
    {
        get
        {
            return sectionOrder;
        }

        set
        {
            sectionOrder = value;
        }
    }

    public bool ParentOnDashboard
    {
        get
        {
            return parentOnDashboard;
        }

        set
        {
            parentOnDashboard = value;
        }
    }

    public bool ChildOnDashboard
    {
        get
        {
            return childOnDashboard;
        }

        set
        {
            childOnDashboard = value;
        }
    }
}