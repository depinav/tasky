var TaskList = Backbone.Collection.extend({
	model: Task
});

var StoryList = Backbone.Collection.extend({
    model: Story
});

var ReleaseList = Backbone.Collection.extend({
    model: Release
});