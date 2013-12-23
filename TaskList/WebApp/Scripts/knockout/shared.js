(function() {
  'use strict';

  var __bind = function(fn, me){ return function(){ return fn.apply(me, arguments); }; };

  this.feedbackViewModel = (function() {

    function feedbackViewModel() {
      this.displayApiResult = __bind(this.displayApiResult, this);

    }

    feedbackViewModel.prototype.css = ko.observable('');

    feedbackViewModel.prototype.message = ko.observable('');

    feedbackViewModel.prototype.visible = ko.observable(false);

    feedbackViewModel.prototype.displayApiResult = function(jqXHR) {
      var _this = this;
      this.visible(true);
      jqXHR.done(function(data) {
        _this.css('alert alert-success');
        return _this.message('<strong>Success!</strong>&nbsp;' + data.message);
      }).fail(function() {
        _this.css('alert alert-danger');
        return _this.message('<strong>Oops!</strong>&nbsp;An error has occurred. Please try again later.');
      });
      return setTimeout((function() {
        return _this.visible(false);
      }), 5000);
    };

    return feedbackViewModel;

  })();

}).call(this);
