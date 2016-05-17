using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Octopus.Client.Model;

namespace ReleaseNote.Models
{
    public class OctopusRelease
    {
        public DateTime Assembled { get; set; }
        public string Id { get; set; }
        public string Version { get; set; }
        public string ReleaseNotes { get; set; }
        public string ProjectId { get; set; }
        public string ChannelId { get; set; }
        public List<OctopusSelectedPackage> SelectedPackages { get; set; }
        public string AssembledOn { get { return Assembled.ToString("D"); } }
    }
}