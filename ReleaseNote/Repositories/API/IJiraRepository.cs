using System.Collections.Generic;
using ReleaseNote.Models;

namespace ReleaseNote.Repositories.API
{
    public interface IJiraRepository
    {
        List<JiraProject> GetAllProjects();

        JiraProject GetJiraProject(string id);

        JiraIssue GetJiraIssues(string projectId);

        Issue GetJiraIssue(string id);
    }
}
