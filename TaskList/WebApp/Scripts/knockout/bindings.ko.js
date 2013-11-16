jQuery.timeago.settings.allowFuture = true;

ko.bindingHandlers.timeago = {
	update: function (element, valueAccessor) {
		var value = ko.utils.unwrapObservable(valueAccessor());
		var $this = $(element);

		// Set the title attribute to the new value = timestamp
		$this.attr('title', value);

		// If timeago has already been applied to this node, don't reapply it -
		// since timeago isn't really flexible (it doesn't provide a public
		// remove() or refresh() method) we need to do everything by ourselves.
		if ($this.data('timeago')) {
			var datetime = $.timeago.datetime($this);
			var distance = (new Date().getTime() - datetime.getTime());
			var inWords = $.timeago.inWords(distance);

			// Update cache and displayed text..
			$this.data('timeago', { 'datetime': datetime });
			$this.text(inWords);
		} else {
			// timeago hasn't been applied to this node -> we do that now!
			$this.timeago();
			
		}
	}
};