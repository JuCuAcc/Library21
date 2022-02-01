/// <reference path="../lib/angular.js/angular.js" />
/// <reference path="../lib/angular-route/angular-route.js" />

angular.module("BookIssueApp", []).controller("BookIssueCtrl", ['$scope',
    function ($scope) {
        $scope.BookIssueArray = [];
        $scope.load;

        $scope.load = function () {
            $.ajax({
                type: 'GET',
                contentType: 'application/json',
                url: '/BookIssue/GetBookIssues',
                success: function (data) {
                    $scope.BookIssueArray = data;
                    $scope.$apply();
                }
            });
        };

        $scope.BookArray = [];
        $scope.getBookList = function () {
            $.ajax({
                type: 'GET',
                contentType: 'application/json',
                url: '/Book/GetBooks',
                success: function (data) {
                    $scope.BookArray = data;
                    $scope.$apply();
                }
            });
        };


        $scope.checkselection = function () {
            if ($scope.bookID != "" && $scope.bookID != undefined) {
                $scope.msg = 'Selected Value : ' + $scope.bookID;
            }
            else {
                $scope.msg = 'Please Select Dropdown Value';
            }
        }

        $scope.load();
        $scope.bookIssueInfo = { bookIssueID: '', issueDate: '', memberAddress: '', bookID: '' };
        $scope.clear = function () {
            $scope.bookIssueInfo.bookIssueID = '';
            $scope.bookIssueInfo.issueDate = '';
            $scope.bookIssueInfo.memberAddress = '';
            $scope.bookIssueInfo.bookID = '';
        };

        //Add
        $scope.bookIssueInsert = function (bookIssueInfo) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json',
                url: '/BookIssue/AddBookIssue',
                data: JSON.stringify(bookIssueInfo),
                success: function (data) {
                    $scope.BookIssueArray = data;
                    $scope.load();
                    $scope.clear();
                }
            });
        };

        //Update
        $scope.updateStart = function (bookIssueInfo) {
            $scope.bookIssueInfo.bookIssueID = bookIssueInfo.bookIssueID;
            $scope.bookIssueInfo.issueDate = bookIssueInfo.issueDate;
            $scope.bookIssueInfo.memberAddress = bookIssueInfo.memberAddress;
            $scope.bookIssueInfo.bookID = bookIssueInfo.bookID;
        };

        $scope.updateConfirm = function (bookIssueInfo) {
            $.ajax({
                type: 'PUT',
                contentType: 'application/json',
                url: '/BookIssue/PutBookIssue',
                data: JSON.stringify(bookIssueInfo),
                success: function (data) {
                    $scope.BookIssueArray = data;
                    $scope.load();
                    $scope.clear();
                }
            });
        };

        //Delete
        $scope.deleteInformation = function (bookIssueInfo) {
            var state = confirm("Do you want to delete data????");
            if (state === true) {
                $.ajax({
                    type: 'DELETE',
                    contentType: 'application/json',
                    url: '/BookIssue/DeleteBookIssue',
                    data: JSON.stringify(bookIssueInfo),
                    success: function (data) {
                        $scope.BookIssueArray = data;
                        $scope.load();
                        $scope.clear();
                    }
                });
            }
        };

        $scope.cancel = function () {
            $scope.clear();
        };
    }
])