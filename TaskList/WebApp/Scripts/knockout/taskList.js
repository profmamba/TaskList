(function() {
  'use strict';

  var taskListController, taskListViewModel,
    __bind = function(fn, me){ return function(){ return fn.apply(me, arguments); }; };

  taskListViewModel = (function() {

    function taskListViewModel() {
      this.update = __bind(this.update, this);

    }

    taskListViewModel.prototype.mapping = {
      key: function(item) {
        return ko.utils.unwrapObservable(item.taskId);
      }
    };

    taskListViewModel.prototype.items = ko.observableArray();

    taskListViewModel.prototype.currentItem = ko.observableArray();

    taskListViewModel.prototype.isEmpty = ko.observable(true);

    taskListViewModel.prototype.formFeedback = new feedbackViewModel();

    taskListViewModel.prototype.update = function() {
      return this.isEmpty(this.items === null || !this.items().length);
    };

    return taskListViewModel;

  })();

  taskListController = function(vm, apiAddress) {
    var _this = this;
    _this.vm = vm;
    _this.apiAddress = apiAddress;
    _this.saveEntry = function() {
      var jqXHR;
      jqXHR = $.ajax({
        url: _this.apiAddress + '/Task',
        data: ko.toJS(_this.vm.currentItem),
        type: _this.vm.currentItem().taskId === 0 ? 'POST' : 'PUT'
      }).done(function(result) {
        var entry, index;
        $('#modalTask').modal('hide');
        if (_this.vm.currentItem().taskId === 0) {
          _this.vm.items.unshift({
            taskId: ko.observable(result.data.taskId),
            taskType: ko.observable(result.data.taskType),
            createDate: ko.observable(result.data.createDate),
            taskTypeName: ko.observable(result.data.taskTypeName),
            description: ko.observable(result.data.description)
          });
        } else {
          index = _this.vm.items.mappedIndexOf({
            taskId: result.data.taskId
          });
          entry = _this.vm.items()[index];
          entry.description(result.data.description);
          entry.taskType(result.data.taskType);
          entry.taskTypeName(result.data.taskTypeName);
        }
        return _this.vm.update();
      });
      _this.vm.formFeedback.displayApiResult(jqXHR);
      return false;
    };
    _this.addEntry = function() {
      _this.vm.currentItem({
        taskId: 0,
        taskType: 1,
        description: ''
      });
      $('#modalTask').modal('show');
      return false;
    };
    _this.editEntry = function(entry) {
      var context;
      context = ko.contextFor(entry);
      _this.vm.currentItem({
        taskId: context.$data.taskId(),
        taskType: context.$data.taskType(),
        description: context.$data.description()
      });
      $('#modalTask').modal('show');
      return false;
    };
    _this.deleteEntry = function(entry) {
      var context;
      context = ko.contextFor(entry);
      bootbox.confirm('Are you sure you want to delete task "' + context.$data.description() + '"?', function(result) {
        var jqXHR;
        if (result) {
          jqXHR = $.ajax({
            url: _this.apiAddress + '/Task/' + context.$data.taskId(),
            type: 'DELETE',
            dataType: 'json',
            success: function() {
              _this.vm.items.mappedRemove({
                taskId: context.$data.taskId()
              });
              return _this.vm.update();
            }
          });
          return _this.vm.formFeedback.displayApiResult(jqXHR);
        }
      });
      return false;
    };
    _this.initialise = function() {
      $.get(_this.apiAddress + '/Task', function(result) {
        _this.vm.items = ko.mapping.fromJS(result, _this.vm.mapping);
        ko.applyBindings(_this.vm, document.getElementById('vmTasks'));
        return _this.vm.update();
      });
      $('#profile-feed-1').delegate('.delete', 'click', function() {
        return _this.deleteEntry(this);
      }).delegate('.edit', 'click', function() {
        return _this.editEntry(this);
      });
      $('#modalTask button.submit').click(function() {
        return _this.saveEntry();
      });
      return $('#vmTasks a.create').click(function() {
        return _this.addEntry();
      });
    };
    _this.initialise();
    return _this;
  };

  $(document).ready(function() {
    var apiAddress, controller, vm;
    apiAddress = '/api';
    vm = new taskListViewModel();
    return controller = new taskListController(vm, apiAddress);
  });

}).call(this);
