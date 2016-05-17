using System.Collections.Generic;
using System.Linq;
using Octopus.Client;
using Octopus.Client.Model;
using ReleaseNote.Models;
using ReleaseNote.Repositories.API;


namespace ReleaseNote.Repositories
{
    public class OctopusRepositoryReleaseNote: IOctopusRepositoryReleaseNote
    {
        private readonly OctopusRepository _octopusRepository;

        public OctopusRepositoryReleaseNote(string server , string apiKey)
        {
            var endpoint = new OctopusServerEndpoint(server, apiKey);
            _octopusRepository = new OctopusRepository(endpoint);
        }


        public IEnumerable<OctopusProject> GetDashBoard()
        {
            var dashboard = _octopusRepository.Dashboards.GetDashboard();
            var projects = dashboard.Projects;
            var items = dashboard.Items;
            var projectsList = new List<OctopusProject>();
            foreach (var dashboardProjectResource in projects)
            {
                var project = new OctopusProject
                {
                    Name = dashboardProjectResource.Name,
                    Slug = dashboardProjectResource.Slug,
                    Id = dashboardProjectResource.Id
                   
                };
                var item =  items.FirstOrDefault(x => x.ProjectId == dashboardProjectResource.Id);
                if (item != null)
                {
                    project.ReleaseVersion = item.ReleaseVersion;
                    project.ReleaseCreatedOn = item.Created.Date;
                    project.ReleaseId = item.ReleaseId;
                }
                projectsList.Add(project);
            }
            return projectsList;
        }


        public IEnumerable<OctopusRelease> GetReleases(string projectSlugOrId)
        {
           
            //var releases = _octopusRepository.Releases.FindMany(x => x.ProjectId == projectSlugOrId);
           var resource = _octopusRepository.Projects.Get(projectSlugOrId);
           var releases = _octopusRepository.Projects.GetReleases(resource);
            return releases.Items.Select(r => new OctopusRelease()
            {
                Id= r.Id,
                Version = r.Version,
                Assembled = r.Assembled.Date,
                ReleaseNotes = r.ReleaseNotes,
                ProjectId = r.ProjectId,
                ChannelId = r.ChannelId,
                SelectedPackages = r.SelectedPackages.Select(x=> new OctopusSelectedPackage()
                {
                    StepName = x.StepName,
                    Version = x.Version
                }).ToList()
            });
        }

        public OctopusRelease GetRelease(string id)
        {
           var octopusRelease = _octopusRepository.Releases.FindOne(x => x.Id == id);
            OctopusRelease release = null;
            if (octopusRelease != null)
            {
                release = new OctopusRelease()
                {
                    Id = octopusRelease.Id,
                    Version = octopusRelease.Version,
                    Assembled = octopusRelease.Assembled.Date,
                    ReleaseNotes = octopusRelease.ReleaseNotes,
                    ProjectId = octopusRelease.ProjectId,
                    ChannelId = octopusRelease.ChannelId,
                    SelectedPackages = octopusRelease.SelectedPackages.Select(x => new OctopusSelectedPackage()
                    {
                        StepName = x.StepName,
                        Version = x.Version
                    }).ToList()
                };
            }
            return release;
        }


        public OctopusProject GetProject(string projectId)
        {
            var octopusProject = _octopusRepository.Projects.FindOne(p => p.Id == projectId);
            OctopusProject project = null;
            if (octopusProject != null)
            {
                project = new OctopusProject
                {
                    Name = octopusProject.Name,
                    Slug = octopusProject.Slug,
                    Id = octopusProject.Id,
                    Description = octopusProject.Description,
                };
            }
            
            return project;
        }


        public OctopusRelease UpdateReleaseNote(OctopusRelease release)
        {
           
            var releaseResource = _octopusRepository.Releases.Get(release.Id);
            releaseResource.ReleaseNotes = release.ReleaseNotes;

            var octopusRelease = _octopusRepository.Releases.Modify(releaseResource);
            return   new OctopusRelease()
            {
                Id = octopusRelease.Id,
                Version = octopusRelease.Version,
                Assembled = octopusRelease.Assembled.Date,
                ReleaseNotes = octopusRelease.ReleaseNotes,
                ProjectId = octopusRelease.ProjectId,
                ChannelId = octopusRelease.ChannelId,
                SelectedPackages = octopusRelease.SelectedPackages.Select(x => new OctopusSelectedPackage()
                {
                    StepName = x.StepName,
                    Version = x.Version
                }).ToList()
            };
        }
    }
}