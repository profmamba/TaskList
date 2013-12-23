'use strict'

#An extension model to display feedback consistently
#Intended to be used as a property of another ViewModel 
class @feedbackViewModel 
	#Fields
	
	css: ko.observable('')
	message: ko.observable('')
	visible: ko.observable(false)

	#Functions
	
	displayApiResult: (jqXHR) =>
		
		@visible true
		
		jqXHR
			.done (data) =>
				@css('alert alert-success')
				@message('<strong>Success!</strong>&nbsp;' + data.message)
			.fail () =>
				@css('alert alert-danger')
				@message('<strong>Oops!</strong>&nbsp;An error has occurred. Please try again later.')
		
		setTimeout((() => @visible false), 5000)
