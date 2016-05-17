using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ReleaseNote.ViewModels
{
    public class ReleaseViewModel
    {
        [Required]
        public string Id { get; set; }
        public string Version { get; set; }
        [AllowHtml]
        [Required]
        public string ReleaseNotes { get; set; }
        public string ProjectId { get; set; }
        public string ChannelId { get; set; }
        [Required]
        public string SelectedPackages { get; set; }

    }
}