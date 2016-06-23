using System.Collections.Generic;
using ReleaseNote.Models;

namespace ReleaseNote.Repositories.API
{
   public interface IOctopusRepositoryReleaseNote
    {
       IEnumerable<OctopusProject> GetDashBoard();

       IEnumerable<OctopusRelease> GetReleases(string projectSlugOrId);

       OctopusRelease GetRelease(string id);

       OctopusProject GetProject(string projectId);

       OctopusRelease UpdateReleaseNote(OctopusRelease release);

       IEnumerable<OctopusProject> GetProjects();
    }
}
