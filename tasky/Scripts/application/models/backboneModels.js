var Task = Backbone.Model.extend({
	urlRoot: '/api/taskapi',

	defaults: {
		title: '',
		description: '',
		estimate_Hours: 0,
		remaining_Hours: 0,
		status: '',
		teamMember: null,
		story: null
	}
});

var Sprint = Backbone.Model.extend({
    urlRoot: 'api/sprintapi',

    defaults: {
        title: '',
        startdate: null,
        enddate: null
    }
});