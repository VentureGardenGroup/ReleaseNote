/**
 * Created by Peter.Makafan on 4/10/2016.
 */
/* global ko */

var projectId = $("#projectId").val();
var jiraUrl = "http://projects.splasherstech.com:8080/browse/";

ko.utils.stringStartsWith = function (string, startsWith) {
    string = string || "";
    if (startsWith.length > string.length)
        return false;
    return string.substring(0, startsWith.length) === startsWith;
};

var Issue = function (issue) {
    var self = this;
    self.key = ko.observable(issue.Key);
    self.summary = ko.observable(issue.Summary);
    self.description = ko.observable(issue.Description);
    self.status = ko.observable(issue.StatusName);
    self.issueType = ko.observable(issue.IssueType);
    return self;
};


var AppViewModel = function () {
    var self = this;

    self.issues = ko.observableArray([]);
    self.pageSize = ko.observable(5);
    self.currentPageIndex = ko.observable(0);
    self.search = ko.observable("");
    self.dirty = ko.observable(false);
    self.currentLength = ko.observable(0);
    self.checkedIssuesKeys = ko.observableArray();
    self.checkedIssues = ko.observableArray();

    self.currentPage = ko.computed(function () {
        var filter = self.search().toLowerCase();
        var pagesize;
        var startIndex;
        var endIndex;
        if (!filter) {
            self.currentLength(self.issues().length);
            pagesize = parseInt(self.pageSize(), 10);
            startIndex = pagesize * self.currentPageIndex();
            endIndex = startIndex + pagesize;
            return self.issues.slice(startIndex, endIndex);
        } else {
            var issues = ko.utils.arrayFilter(self.issues(), function (issue) {
                return ko.utils.stringStartsWith(issue.key().toLowerCase(), filter);
            });
            var length = issues.length;
            self.currentLength(length);
            self.dirty() ? self.currentPageIndex(0) : 0;
            var currentIndex = parseInt(self.currentPageIndex(), 10);
            pagesize = parseInt(self.pageSize(), 10);
            startIndex = pagesize * currentIndex;
            endIndex = startIndex + pagesize;
            self.dirty(false);
            return issues.slice(startIndex, endIndex);;
        }

    }, self);

    self.dirtyCalculations = ko.computed(function () {
        self.search();
        self.dirty(true);
    });

    self.nextPage = function () {
        if (((self.currentPageIndex() + 1) * self.pageSize()) < self.currentLength()) {
            self.currentPageIndex(self.currentPageIndex() + 1);
        }
        else {
            self.currentPageIndex(0);
        }
    };
    self.previousPage = function () {
        if (self.currentPageIndex() > 0) {
            self.currentPageIndex(self.currentPageIndex() - 1);
        }
        else {
            self.currentPageIndex((Math.ceil(self.currentLength() / self.pageSize())) - 1);
        }
    };

    self.selectTicket = function (data, elem) {
        var hiddenElement = $("#releasenote");
        var hiddenText = hiddenElement.val();
        var $checkBox = $(elem.currentTarget);
        var checked = $checkBox.is(":checked");
        var issueType = data.issueType();
        if ("bug" == issueType.toLowerCase()) {
            issueType += " Fix";
        }
        var issueUrl = "<a href=" + jiraUrl + data.key() + " target='_blank'>" + data.key() + "</a>";
        var value = "<p>" + issueUrl + " - " + "<b>" + issueType + "</b>" + " - " + data.summary() + "</p>";
        var list;
        var index;
        if (checked) {
            list = hiddenText.split("^");
            index = list.indexOf(value);
            if (index < 0) {
                hiddenText += value + "^";
                hiddenElement.val(hiddenText)
                self.checkedIssues.push(data);
            }
        }
        else {
            list = hiddenText.split("^");
            index = list.indexOf(value);
            if (index >= 0) {
                list.splice(index, 1);
                hiddenText = list.join("^");
                hiddenElement.val(hiddenText);
                self.checkedIssues.remove(data);

            }

        }
        return true;
    };

    $.getJSON("/issue/index/" + projectId, function (data) {
        self.issues(ko.utils.arrayMap(data.tickets, function (i) {
            return new Issue(i);
        }));
    });

};



$(document).ready(function () {
    ko.applyBindings(new AppViewModel());
});
