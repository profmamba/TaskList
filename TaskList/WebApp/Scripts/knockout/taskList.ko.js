var taskListViewModel = function () {
	var self = this;

	self.mapping = {
		key: function (item) {
			return ko.utils.unwrapObservable(item.TaskId);
		}
	};

	self.items = ko.observableArray();
	self.currentItem = ko.observableArray();
	self.isEmpty = ko.observable(true);

	self.formFeedback = new feedbackViewModel();

	self.update = function () {
		self.isEmpty(self.items === null || self.items().length === 0);
	};
};

var taskListController = function (vm, apiAddress) {
	var self = this;

	self.apiAddress = apiAddress;
	self.vm = vm;


	self.saveEntry = function () {
		var jqXHR = $.post(self.apiAddress + '/Task', ko.toJS(vm.currentItem))
			.done(function (result) {
				$('#modalTask').modal('hide');
				if (vm.currentItem().TaskId === 0) {
					vm.items.unshift(result.data);
				}
				else {
					var index = vm.items.mappedIndexOf({ TaskId: result.data.TaskId });
					var entry = vm.items()[index];
					entry.Description(result.data.Description);
					entry.TaskType(result.data.TaskType);
					entry.TaskTypeName(result.data.TaskTypeName);
				}
				vm.update();
			});

		vm.formFeedback.displayApiResult(jqXHR);

		return false;
	};

	self.addEntry = function () {
		vm.currentItem({
			TaskId: 0,
			TaskType: 1,
			Description: ''
		});

		$('#modalTask').modal('show');
		return false;
	};

	self.deleteEntry = function (entry) {
		var context = ko.contextFor(entry);
		bootbox.confirm(
			'Are you sure you want to delete task "' + context.$data.Description() + '"?',
			function (result) {
				if (result) {
					var jqXHR = $.ajax({
						url: self.apiAddress + '/Task/' + context.$data.TaskId(),
						type: 'DELETE',
						dataType: 'json',
						success: function () {
							vm.items.mappedRemove({ TaskId: context.$data.TaskId() });
							vm.update();
						}
					});

					vm.formFeedback.displayApiResult(jqXHR);
				}

			});
		return false;
	};

	self.editEntry = function (entry) {
		var context = ko.contextFor(entry);

		vm.currentItem({
			TaskId: context.$data.TaskId(),
			TaskType: context.$data.TaskType(),
			Description: context.$data.Description(),
		});

		$('#modalTask').modal('show');
		return false;
	};

	self.initialize = function () {
		$.get(self.apiAddress + '/Task', function (result) {
			vm.items = ko.mapping.fromJS(result, vm.mapping);
			ko.applyBindings(vm, document.getElementById('vmTasks'));
			vm.update();
		});
	};

	self.initialize();

	$('#profile-feed-1')
		.delegate('.delete', 'click', function () { return self.deleteEntry(this); })
		.delegate('.edit', 'click', function () { return self.editEntry(this); });

	$('#modalTask button.submit').click(function () { return self.saveEntry(); });
	$('#vmTasks a.create').click(function () { return self.addEntry(); });

};