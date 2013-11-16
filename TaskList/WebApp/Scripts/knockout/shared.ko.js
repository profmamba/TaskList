var feedbackViewModel = function () {
	var self = this;

	self.Css = ko.observable('');
	self.Message = ko.observable('');
	self.Visible = ko.observable(false);

	self.displayApiResult = function (jqXHR) {
		self.Visible(true);
		jqXHR
			.done(function (data) {
				self.Css('alert alert-success');
				self.Message('<strong>Success!</strong>&nbsp;' + data.message);
			})
			.fail(function () {
				self.Css('alert alert-danger');
				self.Message('<strong>Oops!</strong>&nbsp;An error has occurred. Please try again later.');
			});
		setTimeout(function () { self.Visible(false); }, 5000);
	};
};