using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReleaseNote.Models;

namespace ReleaseNote.ViewModels
{
    public class OctopusProjectViewModel
    {
        public OctopusProject OctopusProject { get; set; }

        public IEnumerable<OctopusRelease> ProjectReleases { get; set; }
    }
}