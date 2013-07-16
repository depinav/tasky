var StoryListView = Backbone.View.extend({
    initialize: function () {
        this.stories = this.options.items;
    },

    render: function () {
        var html = '<table class="storyListTable">';

        _.each(this.stories.models, function (story) {
            html = html.concat('<tr><td><a href="/Story/Details/' + story.get("id").toString() + '"><div class="draggableDiv">' + story.get("title") + '</div></a></td></tr>');
        })

        html = html.concat('</table>');

        this.$el.html(html);
    }

});

var TaskListView = Backbone.View.extend({
    initialize: function () {
        this.items = this.options.items;
    },

    render: function () {
        var html = '<table class="taskListTable">';

        _.each(this.items.models, function (item) {
            console.log(item.get("Title"));
            html = html.concat('<tr><td><a href="/Task/Details/' + item.get("id").toString() + '"><div class="draggableDiv">' + item.get("Title") + '</a></td></tr>');
        })

        html = html.concat("</table>");

        this.$el.html(html);

    }
});