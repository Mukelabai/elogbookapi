using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ElogbookDashboardQuestion
/// </summary>
public class ElogbookDashboardQuestion
{
    public ElogbookDashboardQuestion()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private string question, responseText, responseType, section;
    private int number;
    private double sectionOrder, displayOrder;

    public string Question
    {
        get
        {
            return question;
        }

        set
        {
            question = value;
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

    public int Number
    {
        get
        {
            return number;
        }

        set
        {
            number = value;
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

    public double DisplayOrder
    {
        get
        {
            return displayOrder;
        }

        set
        {
            displayOrder = value;
        }
    }

    public string Section
    {
        get
        {
            return section;
        }

        set
        {
            section = value;
        }
    }
}