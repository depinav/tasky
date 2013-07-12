var TaskListView = Backbone.View.extend({
    initialize: function (listItems) {
        this.items = listItems;
    },

    render: function () {
        var html = "<table class=\"taskListTable\">";
        
        for (item in this.items) {
            html = html.concat("<td><tr>" + item.title + "</tr></td>");
        }

        html = html.concat("</table>");

        this.$el.html(html);

    }
});