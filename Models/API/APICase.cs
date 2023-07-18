using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elogbookapi.Models.API
{
    public class APICase
    {
        public long CaseId { get; set; }
        public Guid CaseUID { get; set; }
        public string Patient { get; set; }
        public long SubmissionId { get; set; }
        public Guid SubmissionIdT { get; set; }
        public int StudentId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}