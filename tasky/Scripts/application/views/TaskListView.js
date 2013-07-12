var TaskListView = Backbone.View.extend({
    initialize: function () {
        this.items = this.options.items;
    },

    render: function () {
        var html = "<table class=\"taskListTable\">";
        
        _.each(this.items.models, function(item) {
            html = html.concat("<tr><td>" + item.get("title") + "</td></tr>");
        })

        html = html.concat("</table>");

        this.$el.html(html);

    }
});