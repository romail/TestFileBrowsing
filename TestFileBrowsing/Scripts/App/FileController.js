var app = angular.module('fileBrowsingApp', []);


var baseAddress = '/api/FileBrowser/';
var url = "";


app.factory('fileBrowsinFactory', function ($http) {
    return {
        getDrivesList: function () {
            url = baseAddress + "GetGetAllDrives";
            return $http.get(url);
        },
        getAllSubFolders: function (path) {
            url = baseAddress + "GetSubFolders?path=" + path;
            return $http.get(url);
        },
        getAllFolderFiles: function (path) {
            url = baseAddress + "GetFolderFiles?path=" + path;
            return $http.get(url);
        },
        GetFolderFilesCount: function (path) {
            url = baseAddress + "GetFolderFilesCount?path=" + path;
            return $http.get(url);
        }

        
    };
});


app.controller('mainCtrl', function mainCtrl($scope, fileBrowsinFactory) {

    $scope.drives = [];
    $scope.drive = null;

    $scope.folders = [];
    $scope.cuurPath - null;

    $scope.files = [];
    $scope.count = [];

    $scope.getAll = function () {
        $scope.drives = null;
        fileBrowsinFactory.getDrivesList().success(function (data) {
            $scope.drives = data.data;
        }).error(function (data) {
            $scope.error = "An Error has occured while Loading department! " + data.ExceptionMessage;
        });
    };

    $scope.ShowFolders = function (e) {
        var path = $(e.target).data('path');

        $scope.cuurPath = path;
        
        fileBrowsinFactory.getAllSubFolders(path).success(function (data) {
            $scope.folders = null;
            $scope.folders = data.data;
        }).error(function (data) {
            $scope.error = "An Error has occured while Loading department! " + data.ExceptionMessage;
        });

        $scope.ShowFiles(path);
        $scope.FilesCount(path);
    }


    $scope.ShowFiles = function (path) {
        fileBrowsinFactory.getAllFolderFiles(path).success(function (data) {
            $scope.files = null;
            $scope.files = data.data;
        }).error(function (data) {
            $scope.error = "An Error has occured while Loading department! " + data.ExceptionMessage;
        });
    }

    $scope.FilesCount = function (path) {
        fileBrowsinFactory.GetFolderFilesCount(path).success(function (data) {
            $scope.count = data.data;
        }).error(function (data) {
            $scope.error = "An Error has occured while Loading department! " + data.ExceptionMessage;
        });
    }


    


    $scope.getAll();
    
});
