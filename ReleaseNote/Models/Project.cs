using System.ComponentModel.DataAnnotations;

namespace ReleaseNote.Models
{
    public class Project
    {
        public int   Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, Display(Name = "Jira Project Name")]
        public string Jira { get; set; }
        [Required, Display(Name = "Octopus Project Name")]
        public string Octopus { get; set; }

    }
}
