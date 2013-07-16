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

    events: {
        "click .task-check": "handleTaskClick"
    },

    initialize: function () {
        this.items = this.options.items;
    },

    render: function () {
        //        var html = "<table class=\"taskListTable\">";
        var html = "<table class=\"taskListTable\">";

        _.each(this.items.models, function (item) {

            var chBoxStatus = "";
            if (item.get("Status") !== "Done")
                chBoxStatus = "";
            else
                chBoxStatus = "checked";

            console.log(item.get("Title"));
            html = html.concat("<tr><div class=\"" + chBoxStatus + "\">");
            html = html.concat("<input type=\"checkbox\" class=\"task-check\" id=\"" + item.get("id") + " \"" + chBoxStatus + ">");
            html = html.concat("<a href=\"/Task/Details/" + item.get("id").toString() + "\">" + item.get("Title") + "</a>");
            html = html.concat("</div></tr>");
        })

        html = html.concat("</table>");

        this.$el.html(html);


    },

    handleTaskClick: function (event) {

        

        var taskid = parseInt($(event.currentTarget).attr('id'));
        var model = this.items.get(taskid);

        if ($(event.currentTarget).parent().attr('class') === "") {

            var task = $(event.currentTarget).parent();
            task.toggleClass('checked');
            model.save({ remaining_hours: 0 });
            model.save({ status: "Done" });
        }
        else {

            var task = $(event.currentTarget).parent();
            task.toggleClass();
            model.save({ status: "In Progress" });
        }
            
    }
});