var QuotesApp = angular.module("QuotesApp", ["ngRoute", "ngResource"]).
    config(function ($routeProvider) {
        $routeProvider.
            when('/', { controller: ListCtrl, templateUrl: 'List.html' }).
            when('/new', { controller: CreateCtrl, templateUrl: 'details.html' }).
            when('/edit/:itemId', { controller: EditCtrl, templateUrl: 'details.html' }).
            otherwise({ redirectTo: '/' });
    });



QuotesApp.factory('Quotes', function ($resource) {
    return $resource('/api/quotes/GetQuotes/:id', { id: '@id' }, { update: { method: 'PUT' } });
});

QuotesApp.factory('QuotesPost', function ($resource) {
    return $resource('/api/quotes/PostQuotes', { id: '@id' }, { update: { method: 'PUT' } });
});

QuotesApp.factory('QuotesPut', function ($resource) {
    return $resource('/api/quotes/PutQuotes/:id', { id: '@id' }, { update: { method: 'PUT' } });
});

QuotesApp.factory('QuotesAgg', function ($resource) {
    //return $resource('/api/QuoteCount', { id: '@id' }, { update: { method: 'PUT' } });
    return $resource('/api/quotes/QuotesCount');
});

var CreateCtrl = function ($scope, $location, QuotesPost) {
    $scope.save = function () {
        QuotesPost.save($scope.item, function () {
            $location.path('/');
        });
    }
    $scope.crud = " Add ";
};


var EditCtrl = function ($scope, $routeParams, $location, Quotes, QuotesPut) {
    $scope.item = Quotes.get({ id: $routeParams.itemId });

    $scope.save = function () {
        QuotesPut.update({ id: $scope.item.Id }, $scope.item, function () {
            $location.path('/');
        });
    };
    $scope.crud = " Update ";
};


var ListCtrl = function ($scope, $location, Quotes, QuotesAgg) {
    $scope.search = function () {
        Quotes.query({
            q: $scope.query,
            sort: $scope.sort_order,
            desc: $scope.is_desc
        }, function (data) {
            $scope.Quotes = data;
        });


        QuotesAgg.query(function (data) {
            $scope.QuotesAgg = data;
        });        
    };

    $scope.sort_by = function (col) {
        if ($scope.sort_order === col) {
            $scope.is_desc = !$scope.is_desc;
        } else {
            $scope.sort_order = col;
            $scope.is_desc = false;
        }
        $scope.reset();
    }
    $scope.reset = function () {
        $scope.search();
    }

    $scope.delete = function () {
        var id = this.contact.Id;
        Quotes.delete({ id: id }, function () {
            $('#contact_'+id).fadeOut();
        });
    }    
    $scope.sort_order = "Author";
    $scope.is_desc = false;
    $scope.reset();    
};

QuotesApp.directive('sorted', function () {
    return {
        scope: true,
        transclude: true,
        template: '<a style="cursor:pointer" ng-click="do_sort()" ng-transclude></a>' +
            '<span ng-show="do_show(true)"><i class="icon-arrow-down"></i></span>' +
            '<span ng-show="do_show(false)"><i class="icon-arrow-up"></i></span>',

        controller: function ($scope, $element, $attrs) {
            $scope.sort = $attrs.sorted;

            $scope.do_sort = function () { $scope.sort_by($scope.sort); };

            $scope.do_show = function (asc) {
                return (asc != $scope.is_desc) && ($scope.sort_order == $scope.sort);
            };
        }
    };
});