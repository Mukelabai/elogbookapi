using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ElogbookQuestion
/// </summary>
public class ElogbookQuestion
{
    public ElogbookQuestion()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private double sectionOrder, parentOrder, childOrder;
    private int elogbookId;
    private int sectionId, questionId, childQuestionId;
    private string sectionName, questionText, questionOptions,  responseType, childQuestionText, childQuestionDisplayText, parentOption,  childResponseType, childQuestionOptions;

    public int ElogbookId
    {
        get
        {
            return elogbookId;
        }

        set
        {
            elogbookId = value;
        }
    }

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

    public int QuestionId
    {
        get
        {
            return questionId;
        }

        set
        {
            questionId = value;
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

    public string SectionName
    {
        get
        {
            return sectionName;
        }

        set
        {
            sectionName = value;
        }
    }

    public string QuestionText
    {
        get
        {
            return questionText;
        }

        set
        {
            questionText = value;
        }
    }

    public string QuestionOptions
    {
        get
        {
            return questionOptions;
        }

        set
        {
            questionOptions = value;
        }
    }

   

    public string ResponseType
    {
        get
        {
            return responseType;
        }

        set
        {
            responseType = value;
        }
    }

    public string ChildQuestionText
    {
        get
        {
            return childQuestionText;
        }

        set
        {
            childQuestionText = value;
        }
    }

    public string ChildQuestionDisplayText
    {
        get
        {
            return childQuestionDisplayText;
        }

        set
        {
            childQuestionDisplayText = value;
        }
    }

    public string ParentOption
    {
        get
        {
            return parentOption;
        }

        set
        {
            parentOption = value;
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

    public string ChildQuestionOptions
    {
        get
        {
            return childQuestionOptions;
        }

        set
        {
            childQuestionOptions = value;
        }
    }
}