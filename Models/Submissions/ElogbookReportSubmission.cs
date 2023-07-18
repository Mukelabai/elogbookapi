using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ElogbookReport
/// </summary>
public class ElogbookReportSubmission
{
    public ElogbookReportSubmission()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private int assignmentId, rotationYear, academicYear, elogbookId, programId, studyYear, studentId, questionId;
    private long submissionId;
    private string rotation, rotationPeriod, elogbook, program, elogbookVersion, mentor,
hospital,
status,
grade,
gradedBy,

student,
sex,
computerNumber,

section,
category,
questionText,
parentQuestion,
responseText,
patient;
    private double displayOrder, sectionOrder;
    private int responseYear, responseMonthNo;
    private string responseMonth,responseType;
    private bool showOnDashboard;

    public int AssignmentId
    {
        get
        {
            return assignmentId;
        }

        set
        {
            assignmentId = value;
        }
    }

    public int RotationYear
    {
        get
        {
            return rotationYear;
        }

        set
        {
            rotationYear = value;
        }
    }

    public int AcademicYear
    {
        get
        {
            return academicYear;
        }

        set
        {
            academicYear = value;
        }
    }

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

    public int ProgramId
    {
        get
        {
            return programId;
        }

        set
        {
            programId = value;
        }
    }

    public int StudyYear
    {
        get
        {
            return studyYear;
        }

        set
        {
            studyYear = value;
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

    public string Rotation
    {
        get
        {
            return rotation;
        }

        set
        {
            rotation = value;
        }
    }

    public string RotationPeriod
    {
        get
        {
            return rotationPeriod;
        }

        set
        {
            rotationPeriod = value;
        }
    }

    public string Elogbook
    {
        get
        {
            return elogbook;
        }

        set
        {
            elogbook = value;
        }
    }

    public string Program
    {
        get
        {
            return program;
        }

        set
        {
            program = value;
        }
    }

    public string ElogbookVersion
    {
        get
        {
            return elogbookVersion;
        }

        set
        {
            elogbookVersion = value;
        }
    }

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

    public string Hospital
    {
        get
        {
            return hospital;
        }

        set
        {
            hospital = value;
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

    public string Grade
    {
        get
        {
            return grade;
        }

        set
        {
            grade = value;
        }
    }

    public string GradedBy
    {
        get
        {
            return gradedBy;
        }

        set
        {
            gradedBy = value;
        }
    }

    public string Student
    {
        get
        {
            return student;
        }

        set
        {
            student = value;
        }
    }

    public string Sex
    {
        get
        {
            return sex;
        }

        set
        {
            sex = value;
        }
    }

    public string ComputerNumber
    {
        get
        {
            return computerNumber;
        }

        set
        {
            computerNumber = value;
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

    public string Category
    {
        get
        {
            return category;
        }

        set
        {
            category = value;
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

    public bool ShowOnDashboard
    {
        get
        {
            return showOnDashboard;
        }

        set
        {
            showOnDashboard = value;
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
}