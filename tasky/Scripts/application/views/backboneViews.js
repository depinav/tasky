var DragStoryByStatusView = Backbone.View.extend({
    initialize: function () {
        this.stories = this.options.items;
    },

    render: function () {
        this.$el.find(".storyContainers").children().remove();
        var el = this.$el;

        _.each(this.stories.models, function (story) {
            var html = '<div class="sortableEntry">';
            html = html.concat('<a href="/Story/Details/' + story.get("id").toString() + '">' + story.get("title") + '</a></div>');
            var idString = story.get("status").replace(" ", "-");
            console.log('idString is: #' + idString);
            console.log(el.find("#" + idString));
            console.log(el.find("#" + idString).find(".storyContainers"));
            el.find("#"+idString).find(".storyContainers").append(html);
        })
    }

});

var DragStoryBySprintView = Backbone.View.extend({
    initialize: function () {
        this.sprints = this.options.items;
        var view = this;
        $( ".storyContainers" ).sortable({
            connectWith: ".storyContainers",
            placeholder: ".storyContainerPH",

            //event handler to change a story's sprint
            receive: function (event, ui) {
                var newSprintID = $(this).attr('id');
                var storyID = ui.item.attr('data-id');
                var oldSprintID = ui.sender.attr('id');

                var oldSprint = view.sprints.get(oldSprintID);
                var story = oldSprint.get('stories').get(storyID);

                story.save({ sprintId: newSprintID });
                view.sprints.get(newSprintID).get('stories').add(story);
                view.sprints.get(oldSprintID).get('stories').remove(story);
                console.log("rec" + newSprintID);
            },

            //event handler to update story's order
            update: function (event, ui) {
                //IMPORTANT NOTE: When the user drags a thing from one list to another list, this event fires twice - once for the "old" list, and once for the "new" list
                //for updating the prioritization this is what we want, but if you're adding something else to this method it might not be what you want
                // http://stackoverflow.com/questions/3492828/jquery-sortable-connectwith-calls-the-update-method-twice
                
                var sprintId = $(this).attr("id");

                //get the list of story IDs, in othe order that the user put them in, for this sprint
                var storyIdList = _.map($(this).children(), function (entry) {
                    return $(entry).attr("data-id");
                });

                console.log("update" + sprintId);
            },
        }).disableSelection();
    },

    render: function () {
        _.each(this.sprints.models, function (item) {
            var html = '';
            _.each(item.get("stories").models, function (story) {
                html = html.concat('<div class="sortableEntry" data-id=' + story.get("id") + ' data-sprintID = ' + item.get("sprintid") + '><a href="/Story/Details/' + story.get("id") + '" >');
                html = html.concat(story.get("title"));
                html = html.concat('</a></div>');                
            })

            $('div#' + item.get('sprintid')).html(html);
        })
    }
})

var TaskListView = Backbone.View.extend({

    events: {
        "click .task-check": "handleTaskClick"
    },

    initialize: function () {
        this.items = this.options.items;
    },

    render: function () {
        //        var html = "<table class=\"taskListTable\">";
        var html = '<table class="taskListTable">';

        _.each(this.items.models, function (item) {

            var chBoxStatus = "";
            if (item.get("Status") !== "Done")
                chBoxStatus = "";
            else
                chBoxStatus = "checked";

            console.log(item.get("Title"));
            
            html = html.concat("<tr><div class=\"" + chBoxStatus + "\">");
            html = html.concat("<input type=\"checkbox\" class=\"task-check\" id=\"" + item.get("id") + " \"" + chBoxStatus + ">");
            html = html.concat('<a href="/Task/Details/' + item.get("id").toString() + '">' + item.get("Title") + '</a>');
            html = html.concat('</div></tr>');

        })

        html = html.concat('</table>');

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