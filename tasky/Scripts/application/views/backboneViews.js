var StoryListView = Backbone.View.extend({
    initialize: function () {
        this.stories = this.options.items;
        var view = this;
        $( ".storyContainers" ).sortable({
            connectWith: ".storyContainers",
            placeholder: ".storyContainerPH",
            receive: function (event, ui) {
                var newStatus = ui.item.parent('div').parent('div').attr('id');
                var storyId = ui.item.attr('id');
                var story = view.stories.get(storyId);
                console.log(story);
                story.save({ status: newStatus });
            }
        }).disableSelection();
    },

    render: function () {
        this.$el.find(".storyContainers").children().remove();
        var el = this.$el;

        _.each(this.stories.models, function (story) {
            var html = '<div class="sortableEntry" id="' + story.get("id").toString() + '" >';
            html = html.concat('<a href="/Story/Details/' + story.get("id").toString() + '">' + story.get("title") + '</a></div>');
            var idString = story.get("status").replace(" ", "-");
            console.log('idString is: #' + idString);
            console.log(el.find("#" + idString));
            console.log(el.find("#" + idString).find(".storyContainers"));
            el.find("#"+idString).find(".storyContainers").append(html);
        })
    }

});

var StoryTitleListView = Backbone.View.extend({
    initialize: function () {
        this.sprints = this.options.items;
        var view = this;
            $( ".storyContainers" ).sortable({
                connectWith: ".storyContainers",
                placeholder: ".storyContainerPH",
                receive: function (event, ui) {
                    var sprintID = ui.item.parent('div').attr('id');
                    var storyID = ui.item.attr('data-id');
                    var oldSprint = ui.item.attr('data-sprintID');

                    var sprint = view.sprints.get(oldSprint);
                    var story = sprint.get('stories').get(storyID);

                    console.log(sprintID);
                    console.log(storyID);
                    console.log(oldSprint);
                    console.log(sprint);
                    console.log(story);
                    story.save({ sprintId: sprintID });
                    view.sprints.get(sprintID).get('stories').add(story);
                    view.sprints.get(oldSprint).get('stories').remove(story);

                }
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
    },

    detailsDrag: function () {
        console.log("Inside detailsDrag method");
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