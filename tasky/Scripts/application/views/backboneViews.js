var StoryListView = Backbone.View.extend({
    initialize: function () {
        this.stories = this.options.items;
    },

    render: function () {
        var html = '<table class="storyListTable">';

        _.each(this.stories.models, function (story) {
            html = html.concat('<tr><td><a href="/Story/Details/' + story.get("id").toString() + '">' + story.get("title") + '</div></a></td></tr>');
        })

        html = html.concat('</table>');

        this.$el.html(html);
    }

});

var StoryTitleListView = Backbone.View.extend({
    initialize: function () {
        this.sprints = this.options.items;
        console.log(this.sprints);
    },

    render: function () {
        var html = "";

        _.each(this.sprints.models, function (item) {
            console.log('item story is: ' + item.get("stories"));

            _.each(item.get("stories").models, function(story) {
                html = html.concat('<a href="/Story/Details/' + story.get("id") + '">');
                html = html.concat(story.get("title"));
                html = html.concat('</a>');
                console.log(html);
            })

            console.log('inside BB-view. item sprintid is: ' + item.get('sprintid') + 'html is: ' + html);
            $('table#'+item.get('sprintid')).html(html);
        })
    }
})

var TaskListView = Backbone.View.extend({
    initialize: function () {
        this.items = this.options.items;
    },

    render: function () {
        //        var html = "<table class=\"taskListTable\">";
        var html = '<table class="taskListTable">';

        _.each(this.items.models, function (item) {
            console.log(item.get("Title"));
            html = html.concat('<div class="taskItem">');
            html = html.concat('<tr><div class="unchecked">');
            html = html.concat('<input type="checkbox" class="task-check" />');
            html = html.concat('<a href="/Task/Details/' + item.get("id").toString() + '">' + item.get("Title") + '</a>');
            html = html.concat('</div></tr>');
            html = html.concat('</div>');
            //            html = html.concat("<tr><td><a href=\"/Task/Details/" + item.get("id").toString() + "\">" + item.get("Title") + "</a></td></tr>");
        })

        html = html.concat('</table>');

        this.$el.html(html);

    }
});