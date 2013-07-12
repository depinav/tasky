var Task = Backbone.Model.extend({
	urlRoot: '/api/TaskAPI',

	defaults: {
		title: "",
		description: "",
		estimate_hours: 0,
		remaining_hours: 0,
		status: "",
		team_member: null,
		story: null
	}
});

var task = new Task();
