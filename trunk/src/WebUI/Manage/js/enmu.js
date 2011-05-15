var Menu = {
    init: function(setting) {
        this.createmenu(setting);
    },
    createmenu: function(setting) {
        var rootMenu = $("#" + setting + " > ul");
        var headers = rootMenu.find("li");
        headers.hover(
			function() {
			    var linkWidth = $(this).width();
			    var aa = $(this).children("ul:eq(0)");
			    if ($(this).children("a:eq(0)").attr('id') != 'noNext') {
			        $(this).children("a:eq(0)").addClass("selected");
			    } else {
			        $(this).children("a:eq(0)").addClass("noNextS");
			    }
			    showTimer = setTimeout(function() {
			        aa.css({ left: linkWidth, top: 0 }).show(200);
			    }, 300);
			},
			function() {
			    clearTimeout(showTimer);
			    if ($(this).children("a:eq(0)").attr('id') != 'noNext') {
			        $(this).children("a:eq(0)").attr("class", '');
			    } else {
			        $(this).children("a:eq(0)").attr('class', 'noNext');
			    }
			    var aa = $(this).children("ul:eq(0)");
			    aa.hide(200);
			}
		);
        headers.each(function() {
            if ($(this).children().length == 1) {
                $(this).children("a:eq(0)").attr('class', 'noNext');
                $(this).children("a:eq(0)").attr('id', 'noNext');
            }
        });
    }
}
