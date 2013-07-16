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

var Story = Backbone.Model.extend({
    urlRoot: 'api/storyapi',

    defaults: {
        title: '',
        description: '',
        points: 0,
        status: '',
        sprintId: 0
    }
});