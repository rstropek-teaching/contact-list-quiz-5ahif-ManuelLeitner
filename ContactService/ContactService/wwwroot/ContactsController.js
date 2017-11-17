
var app = angular.module('contactsApp', []);
app.controller('contactController', function ($scope, $http) {

    $scope.filterValid = true;
    function errorMessage(error) {
        alert(error.status + ": " + error.data);
    }

    function resetNew() {
        $scope.newContact = { id: '', firstName: '', lastName: '', email: '' };
    }
    resetNew();

    function load() {
        $http.get("api/contacts").then(function (response) {
            $scope.contacts = response.data;
        }, errorMessage);
    }
    $scope.load = load;

    $scope.add = function () {
        $http.post("api/contacts", $scope.newContact).then(function () {
            resetNew();
            load();
        }, errorMessage);
    }

    $scope.delete = function (id) {
        $http.delete("api/contacts/" + id).then(function () {
            load();
        }, errorMessage);
    }

    function search (response) {
        $scope.contacts = response.data;
        $scope.filterValid = true;
    }

    $scope.search = function () {
        $http.get("api/contacts/findByName/" + $scope.nameFilter).then(search, function (error) {
            errorMessage(error);
            $scope.filterValid = false;
        });
    }

    $scope.searchQuiet = function () {
        $http.get("api/contacts/findByName/" + $scope.nameFilter).then(search, function () {
            $scope.filterValid = false;
        });
    }

    load();
});