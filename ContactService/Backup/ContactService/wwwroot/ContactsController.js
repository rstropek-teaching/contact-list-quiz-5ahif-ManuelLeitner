
var app = angular.module('contactsApp', []);
app.controller('contactController', function ($scope, $http) {

    $scope.url = "";

    $scope.filterValid = true;
    function errorMessage(error) {
        alert(error.status + ": " + error.data);
    }

    function resetNew() {
        $scope.newContact = { firstName: '', lastName: '', email: '' };
    }
    resetNew();

    function load() {
        $http.get($scope.url + "api/contacts").then(function (response) {
            $scope.contacts = response.data;
        }, errorMessage);
    }
    $scope.load = load;

    $scope.add = function () {
        $http.post($scope.url + "api/contacts", $scope.newContact).then(function () {
            resetNew();
            load();
        }, errorMessage);
    }

    $scope.delete = function (id) {
        $http.delete($scope.url + "api/contacts/" + id).then(function () {
            load();
        }, errorMessage);
    }

    function search(response) {
        $scope.contacts = response.data;
        $scope.filterValid = true;
    }

    $scope.search = function () {
        $http.get($scope.url + "api/contacts/findByName/" + $scope.nameFilter).then(search, function (error) {
            errorMessage(error);
            $scope.filterValid = false;
        });
    }

    $scope.searchQuiet = function () {
        $http.get($scope.url + "api/contacts/findByName/" + $scope.nameFilter).then(search, function () {
            $scope.filterValid = false;
        });
    }

    load();
});