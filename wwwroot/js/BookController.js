/// <reference path="../lib/angular.js/angular.js" />
/// <reference path="../lib/angular-route/angular-route.js" />

angular.module("BookApp", []).controller("BookCtrl", ['$scope',
    function ($scope) {
        $scope.BookArray = [];
        $scope.load;
        $scope.load = function () {
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

        $scope.load();
        $scope.bookInfo = { bookID: '', bookName: '', bookPublisher: '', category: '' };
        $scope.clear = function () {
            $scope.bookInfo.bookID = '';
            $scope.bookInfo.bookName = '';
            $scope.bookInfo.bookPublisher = '';
            $scope.bookInfo.category = '';
        };

        //Add
        $scope.bookInsert = function (bookInfo) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json',
                url: '/Book/AddBook',
                data: JSON.stringify(bookInfo),
                success: function (data) {
                    $scope.BookArray = data;
                    $scope.load();
                    $scope.clear();
                }
            });
        };

        //Update
        $scope.updateStart = function (bookInfo) {
            $scope.bookInfo.bookID = bookInfo.bookID;
            $scope.bookInfo.bookName = bookInfo.bookName;
            $scope.bookInfo.bookPublisher = bookInfo.bookPublisher;
            $scope.bookInfo.category = bookInfo.category;
        };

        $scope.updateConfirm = function (bookInfo) {
            $.ajax({
                type: 'PUT',
                contentType: 'application/json',
                url: '/Book/UpdateBook',
                data: JSON.stringify(bookInfo),
                success: function (data) {
                    $scope.BookArray = data;
                    $scope.load();
                    $scope.clear();
                }
            });
        };

        //Delete
        $scope.deleteInformation = function (bookInfo) {
            var state = confirm("Do you want to delete data????");
            if (state === true) {
                $.ajax({
                    type: 'DELETE',
                    contentType: 'application/json',
                    url: '/Book/DeleteBook',
                    data: JSON.stringify(bookInfo),
                    success: function (data) {
                        $scope.BookArray = data;
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
