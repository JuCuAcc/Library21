/// <reference path="../lib/angular.js/angular.min.js" />
/// <reference path="../lib/angular-route/angular-route.min.js" />



// Defining Angular

var app = angular.module('MainModule', ['ngRoute']);


// Get API Address

var baseUrl = 'https://localhost:44362/api/';
//var baseUrl = 'https://localhost:44362/api/Books/';
//var baseUrl2 = 'https://localhost:44362/api/BookIssue/';


// To Configure route, We need 2 parameter routeProvider, locationProvider

app.config(function ($routeProvider, $locationProvider) {
    $routeProvider
        .when('/', { templateUrl: '/Pages/Default.html' })
        .when('/Book', { templateUrl: '/Pages/Book.html', controller: 'BookCtrl' })
        .when('/BookIssue', { templateUrl: '/Pages/BookIssue.html', controller: 'BookIssueCtrl' })
        .otherwise({ redirectTo: '/' });

    $locationProvider.hashPrefix('');
})

app.factory('BookFact', function ($http) {
    var factory = {};
    factory.GetBooks = function () {
        return $http.get(baseUrl + "Books/");
    }
    factory.SaveBooks = function (obj) {
        return $http.post(baseUrl + "Books/", obj);
    }
    factory.UpdateBooks = function (obj) {
        return $http.put(baseUrl +"Books/" + obj.BookID, obj);
    }
    factory.DeleteBooks = function (id) {
        return $http.delete(baseUrl +"Books/" + id);
    }

    return factory;
});

// Book Issue Factory

app.factory('BookIssueFact', function ($http) {
    var factory = {};
    factory.GetIssueBooks = function () {
        return $http.get(baseUrl +"BookIssues/");
    }
    factory.SaveIssueBooks = function (obj) {
        return $http.post(baseUrl +"BookIssues/", obj);
    }
    factory.UpdateIssueBooks = function (obj) {
        return $http.put(baseUrl + "BookIssues/" + obj.BookIssueID, obj);
    }
    factory.DeleteIssueBooks = function (id) {
        return $http.delete(baseUrl +"BookIssues/" + id);
    }
    return factory;
});

app.controller('BookCtrl', function ($scope, BookFact) {

    Init();
    function Init() {
        BookFact.GetBooks().then(function (res) {
            $scope.BookList = res.data;
        })
    }


    $scope.SaveBook = function () {
        BookFact.SaveBooks($scope.objBook).then(function () {
            Init();
            Clear();
        })
    }


    $scope.EditBook = function (b) {
        $scope.objBook = b;
    }

    $scope.UpdateBook = function () {
        BookFact.UpdateBooks($scope.objBook).then(function () {
            Init();
            Clear();
        })
    }

    $scope.DeleteBook = function (id) {
        BookFact.DeleteBooks(id).then(function () {
            Init();
        })


    }

    function Clear() {
        $scope.objBook = null;
    }
})

// Book Issue Controller

app.controller('BookIssueCtrl', function ($scope, BookIssueFact) {

    Init();
    function Init() {
        BookIssueFact.GetIssueBooks().then(function (res) {
            $scope.BookIssueList = res.data;
        })
    }


    $scope.BookArray1 = [];
    $scope.GetBooks = function () {
        $scope.BookArray1 = $http.get(baseUrl + "Books/");
        return BookArray1;
    }


    //$scope.BookArray = [];
    //$scope.getBookList = function () {
    //    $.ajax({
    //        type: 'GET',
    //        contentType: 'application/json',
    //        url: '/Book/GetBooks',
    //        success: function (data) {
    //            $scope.BookArray = data;
    //            $scope.$apply();
    //        }
    //    });
    //};




    $scope.SaveIssueBook = function () {
        BookFact.SaveIssueBooks($scope.objBookIssue).then(function () {
            Init();
            Clear();
        })
    }


    $scope.EditIssueBook = function (b) {
        $scope.objBookIssue = b;
    }

    $scope.UpdateIssueBook = function () {
        BookIssueFact.UpdateIssueBooks($scope.objBookIssue).then(function () {
            Init();
            Clear();
        })
    }

    $scope.DeleteIssueBook = function (id) {
        BookIssueFact.DeleteIssueBooks(id).then(function () {
            Init();
        })


    }

    function Clear() {
        $scope.objBookIssue = null;
    }


    
})