using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReleaseNote.Models
{
    public class OctopusProject
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ReleaseVersion { get; set; }
        public string ReleaseId { get; set; }

        public string Slug { get; set; }
        public string Id { get; set; }

        public DateTime ReleaseCreatedOn { get; set; }

        public string ReleaseCreatedOnStr { get { return ReleaseCreatedOn.ToString("D"); } }
    }
}