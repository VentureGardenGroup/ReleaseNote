using System.Collections.Generic;

public class Issuetype
{
    public string self { get; set; }
    public string id { get; set; }
    public string description { get; set; }
    public string iconUrl { get; set; }
    public string name { get; set; }
    public bool subtask { get; set; }
}

public class StatusCategory
{
    public string self { get; set; }
    public int id { get; set; }
    public string key { get; set; }
    public string colorName { get; set; }
    public string name { get; set; }
}

public class Status
{
    public string self { get; set; }
    public string description { get; set; }
    public string iconUrl { get; set; }
    public string name { get; set; }
    public string id { get; set; }
    public StatusCategory statusCategory { get; set; }
}

public class Fields
{
    public string summary { get; set; }
    public Issuetype issuetype { get; set; }
    public string description { get; set; }
    public Status status { get; set; }
}

public class Issue
{
    public string expand { get; set; }
    public string id { get; set; }
    public string self { get; set; }
    public string key { get; set; }
    public Fields fields { get; set; }
}

public class JiraIssue
{
    public string expand { get; set; }
    public int startAt { get; set; }
    public int maxResults { get; set; }
    public int total { get; set; }
    public List<Issue> issues { get; set; }
}