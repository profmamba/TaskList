'use strict'

#TaskList ViewModel to keep state
class taskListViewModel 

	mapping: {key: (item) -> ko.utils.unwrapObservable(item.taskId)}

	items: ko.observableArray()
	currentItem: ko.observableArray()
	isEmpty: ko.observable(true)

	formFeedback: new feedbackViewModel()

	update: () =>  @isEmpty(@items == null || !@items().length)
	

#TaskList Controller
taskListController = (vm, apiAddress) ->
	
	_this.vm = vm
	_this.apiAddress = apiAddress

	#Saves the currentItem to the server and updates the local view model
	_this.saveEntry = () =>
		jqXHR = 
			$.ajax({
				url: @apiAddress + '/Task',
				data: ko.toJS(@vm.currentItem),
				type: if @vm.currentItem().taskId == 0 then 'POST' else 'PUT'
			})
			.done (result) =>
				$('#modalTask').modal('hide')
				if @vm.currentItem().taskId is 0  
					@vm.items.unshift({
						taskId: ko.observable(result.data.taskId),
						taskType: ko.observable(result.data.taskType),
						createDate: ko.observable(result.data.createDate),
						taskTypeName: ko.observable(result.data.taskTypeName),
						description: ko.observable(result.data.description),
					})
				else
					index = @vm.items.mappedIndexOf( { taskId: result.data.taskId })
					entry = @vm.items()[index]
					entry.description(result.data.description)
					entry.taskType(result.data.taskType)
					entry.taskTypeName(result.data.taskTypeName)
				@vm.update()
		@vm.formFeedback.displayApiResult(jqXHR)

		false
	
	#Shows the item modal with a new item
	_this.addEntry = () =>
		@vm.currentItem({
			taskId: 0,
			taskType: 1,
			description: ''
		})

		$('#modalTask').modal('show')
		false

	#Shows the item modal with an existing item
	_this.editEntry = (entry) =>
		context = ko.contextFor(entry)

		@vm.currentItem({
			taskId: context.$data.taskId(),
			taskType: context.$data.taskType(),
			description: context.$data.description(),
		});


		$('#modalTask').modal('show')
		false

	#Deletes an entry (showing a confirmation popup first using the bootbox plugin)
	_this.deleteEntry = (entry) =>
		context = ko.contextFor(entry)
	
		bootbox.confirm(
			'Are you sure you want to delete task "' + context.$data.description() + '"?',
			(result) =>
				if result 
					jqXHR = $.ajax({
						url: @apiAddress + '/Task/' + context.$data.taskId(),
						type: 'DELETE',
						dataType: 'json',
						success: () =>
							@vm.items.mappedRemove({ taskId: context.$data.taskId() })
							@vm.update()
					})

					@vm.formFeedback.displayApiResult(jqXHR)
		)
		false

	#get data from the server and apply ko bindings and events
	_this.initialise = () =>
		$.get(
			@apiAddress + '/Task',
			(result) =>
				@vm.items = ko.mapping.fromJS(result, @vm.mapping)
				ko.applyBindings(@vm, document.getElementById('vmTasks'))
				@vm.update()
		)

		$('#profile-feed-1')
			.delegate('.delete', 'click', () -> return _this.deleteEntry(this))
			.delegate('.edit', 'click', () -> return _this.editEntry(this))
		
		$('#modalTask button.submit').click(() => return @saveEntry())
		$('#vmTasks a.create').click(() => return @addEntry())
	
	_this.initialise()

	_this
	
$(document).ready(() ->
	apiAddress = '/api';
	vm = new taskListViewModel();
	controller = new taskListController(vm, apiAddress);
)

	
