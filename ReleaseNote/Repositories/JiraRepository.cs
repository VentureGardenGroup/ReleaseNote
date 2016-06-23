using System;
using System.Collections.Generic;
using ReleaseNote.Models;
using ReleaseNote.Repositories.API;
using RestSharp;
using RestSharp.Authenticators;

namespace ReleaseNote.Repositories
{
    public class JiraRepository:IJiraRepository
    {
        public RestClient Client { get; private set; }

        public JiraRepository(string server, string username, string password)
        {
            Client = new RestClient(server)
            {
                Authenticator = new HttpBasicAuthenticator(username,password)
            };
        }
        public List<JiraProject> GetAllProjects()
        {
            var request = new RestRequest("rest/api/2/project", Method.GET);
            var response = Client.Execute<List<JiraProject>>(request);
            return response.Data;
        }

        public JiraProject GetJiraProject(string id)
        {
            throw new NotImplementedException();
        }

        public JiraIssue GetJiraIssues(string projectId)
        {
            var request = new RestRequest("rest/api/2/search?jql=project={slug}&fields=id,key,summary,description,issuetype,status", Method.GET);
            request.AddUrlSegment("slug", projectId);
            var response =  Client.Execute<JiraIssue>(request);
            return response.Data;
        }

        public Issue GetJiraIssue(string id)
        {
            throw new NotImplementedException();
        }
    }
}