var Task = Backbone.Model.extend({
    urlRoot: '/api/taskapi',

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