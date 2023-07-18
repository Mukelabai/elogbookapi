using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Institution
/// </summary>
/// 
namespace elogbook.Model.Faculty
{
    public class Institution
    {
        private string institutionName, learningPeriodsType, motto, telephone, email, fax, postBox, contactDetails, logoURL;

        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }

        public string PostBox
        {
            get { return postBox; }
            set { postBox = value; }
        }

        public string Motto
        {
            get { return motto; }
            set { motto = value; }
        }

        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string InstitutionName
        {
            get { return institutionName; }
            set { institutionName = value; }
        }

        public string LearningPeriodsType
        {
            get { return learningPeriodsType; }
            set { learningPeriodsType = value; }
        }
        private int learningPeriods, institutionId;

        public int InstitutionId
        {
            get { return institutionId; }
            set { institutionId = value; }
        }

        public int LearningPeriods
        {
            get { return learningPeriods; }
            set { learningPeriods = value; }
        }

        private bool examTimeTableIsManual;

        public bool ExamTimeTableIsManual
        {
            get { return examTimeTableIsManual; }
            set { examTimeTableIsManual = value; }
        }

        public string ContactDetails
        {
            get
            {
                return contactDetails;
            }

            set
            {
                contactDetails = value;
            }
        }

        public string LogoURL
        {
            get
            {
                return logoURL;
            }

            set
            {
                logoURL = value;
            }
        }
    }
}