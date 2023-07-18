using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elogbookapi.Models.API
{
    public class APICaseResponse
    {
        public Guid CaseUID { get; set; }
        public List<APIResponses> QuestionResponses { get; set; }
    }
}